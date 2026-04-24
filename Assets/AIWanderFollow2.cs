using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using Photon.Realtime;

public class AIWanderFollow2 : MonoBehaviourPun, IInRoomCallbacks
{
    public string targetTag;
    public float followDistance = 5f;
    public Transform[] points;
    public float patrolSpeed = 3.5f;
    public float followSpeed = 5f;
    public float minimumSpeedRequired = 2.0f;
    public Animator animator;
    public float animationMaxSpeed = 0.25f;

    private NavMeshAgent agent;
    private bool isFollowing = false;
    private bool isGoingLastKnown = false;
    private Transform currentTarget;
    private Vector3 lastKnownTargetPos;
    private Vector3 previousPosition;

    void Start()
    {
        PhotonNetwork.AddCallbackTarget(this);
        agent = GetComponent<NavMeshAgent>();
        previousPosition = transform.position;

        if (PhotonNetwork.IsMasterClient)
        {
            agent.enabled = true;
            agent.speed = patrolSpeed;
            GotoNextPoint();
        }
        else
        {
            if (agent != null) agent.enabled = false;
        }

        if (animator == null) animator = GetComponentInChildren<Animator>();
    }

    void OnDestroy()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void OnLeftRoom()
    {
        ResetBehavior();
    }

    void ResetBehavior()
    {
        isFollowing = false;
        isGoingLastKnown = false;
        currentTarget = null;
        lastKnownTargetPos = Vector3.zero;
        agent.speed = patrolSpeed;
        if (agent.enabled) GotoNextPoint();
        if (animator != null)
        {
            animator.speed = 0f;
            animator.SetFloat("MoveSpeed", 0f);
        }
    }

    void Update()
    {
        float actualSpeed = (transform.position - previousPosition).magnitude / Mathf.Max(Time.deltaTime, 0.0001f);
        previousPosition = transform.position;
        UpdateLocalAnimator(actualSpeed);

        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }

        if (!agent.enabled)
        {
            agent.enabled = true;
            GotoNextPoint();
        }

        FindClosestTargetWithTag(targetTag);
        bool targetReachable = false;

        if (currentTarget != null && currentTarget.CompareTag(targetTag))
        {
            float distanceToTarget = Vector3.Distance(currentTarget.position, transform.position);
            if (distanceToTarget <= followDistance)
            {
                NavMeshPath path = new NavMeshPath();
                if (agent.CalculatePath(currentTarget.position, path) && path.status == NavMeshPathStatus.PathComplete)
                {
                    targetReachable = true;
                    lastKnownTargetPos = currentTarget.position;
                }
            }
        }

        if (targetReachable)
        {
            agent.SetDestination(currentTarget.position);
            agent.speed = followSpeed;
            isFollowing = true;
            isGoingLastKnown = false;
        }
        else
        {
            if (isFollowing)
            {
                agent.SetDestination(lastKnownTargetPos);
                agent.speed = followSpeed;
                isFollowing = false;
                isGoingLastKnown = true;
            }
            else if (isGoingLastKnown)
            {
                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                {
                    isGoingLastKnown = false;
                    GoToForwardSafePoint();
                }
                else
                {
                }
            }
            else
            {
                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                    GotoNextPoint();
            }
        }
    }

    void UpdateLocalAnimator(float speed)
    {
        if (animator == null) return;
        float scaledSpeed = Mathf.Max(0f, speed * animationMaxSpeed);
        animator.speed = scaledSpeed;
        for (int i = 0; i < animator.parameters.Length; i++)
        {
            if (animator.parameters[i].name == "MoveSpeed")
            {
                animator.SetFloat("MoveSpeed", scaledSpeed);
                break;
            }
        }
    }

    void FindClosestTargetWithTag(string tag)
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);
        float closestDistance = Mathf.Infinity;
        Transform closestTarget = null;
        foreach (GameObject target in targets)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = target.transform;
            }
        }
        currentTarget = closestTarget;
    }

    void GotoNextPoint()
    {
        if (points == null || points.Length == 0) return;
        int nextPoint = Random.Range(0, points.Length);
        agent.destination = points[nextPoint].position;
        agent.speed = patrolSpeed;
    }

    void GoToForwardSafePoint()
    {
        Vector3 forwardDir = transform.forward;
        Vector3 startPos = transform.position;
        Vector3 bestPoint = startPos;
        float bestDot = -1f;
        float searchRadius = 6f;

        NavMeshHit[] hits = new NavMeshHit[10];
        int hitCount = 0;

        for (int i = 0; i < 10; i++)
        {
            Vector3 randomDir = Random.insideUnitSphere * searchRadius;
            randomDir.y = 0;
            Vector3 testPos = startPos + randomDir;
            if (NavMesh.SamplePosition(testPos, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
            {
                hits[hitCount++] = hit;
            }
        }

        for (int i = 0; i < hitCount; i++)
        {
            Vector3 dirToPoint = (hits[i].position - startPos).normalized;
            float dot = Vector3.Dot(forwardDir, dirToPoint);
            if (dot > bestDot)
            {
                bestDot = dot;
                bestPoint = hits[i].position;
            }
        }

        if (bestDot < 0.2f && hitCount > 0)
        {
            float closestDist = Mathf.Infinity;
            for (int i = 0; i < hitCount; i++)
            {
                float dist = Vector3.Distance(startPos, hits[i].position);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    bestPoint = hits[i].position;
                }
            }
        }

        agent.speed = patrolSpeed;
        agent.SetDestination(bestPoint);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followDistance);
    }

    public void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer) { }
    public void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer) { }
    public void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged) { }
    public void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps) { }
    public void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient) { }
}