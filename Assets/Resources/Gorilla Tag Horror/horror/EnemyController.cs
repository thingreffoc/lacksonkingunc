using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyController : MonoBehaviourPun, IPunObservable
{
    public float moveSpeed = 3f;
    public float chaseRange = 10f;

    public bool resetOnDisable = false;

    public Transform[] resetOnDisableSpawnPoints;

    private Transform targetPlayer;
    private Vector3 initialPosition;

    private struct State
    {
        public Vector3 position;
        public Quaternion rotation;
        public float timestamp;
    }

    private State latestState;
    private State previousState;

    private float interpolationBackTime = 0.1f;
    private bool initialized;

    private bool masterReady => PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient;

    void Start()
    {
        initialPosition = transform.position;
        var now = Time.time;
        latestState = new State { position = transform.position, rotation = transform.rotation, timestamp = now };
        previousState = latestState;
        initialized = true;

        if (!photonView.IsMine)
        {
            photonView.RPC("RequestSyncPosition", photonView.Owner);
        }
    }

    void Update()
    {
        if (!masterReady)
        {
            return;
        }

        if (photonView.IsMine)
        {
            UpdateAI();
        }
        else
        {
            InterpolatedSync();
        }
    }

    private void UpdateAI()
    {
        float distanceToClosestPlayer = Mathf.Infinity;
        GameObject[] players = GameObject.FindGameObjectsWithTag("chaseme");

        foreach (GameObject p in players)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, p.transform.position);
            if (distanceToPlayer < distanceToClosestPlayer)
            {
                distanceToClosestPlayer = distanceToPlayer;
                targetPlayer = p.transform;
            }
        }

        if (targetPlayer != null)
        {
            if (distanceToClosestPlayer < chaseRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPlayer.position, moveSpeed * Time.deltaTime);
                Vector3 direction = targetPlayer.position - transform.position;
                if (direction != Vector3.zero)
                    transform.rotation = Quaternion.LookRotation(direction);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, initialPosition, moveSpeed * Time.deltaTime);
                Vector3 direction = initialPosition - transform.position;
                if (direction != Vector3.zero)
                    transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }

    private void InterpolatedSync()
    {
        float interpolationTime = Time.time - interpolationBackTime;

        if (latestState.timestamp > interpolationTime)
        {
            float t = Mathf.InverseLerp(previousState.timestamp, latestState.timestamp, interpolationTime);
            transform.position = Vector3.Lerp(previousState.position, latestState.position, t);
            transform.rotation = Quaternion.Slerp(previousState.rotation, latestState.rotation, t);
        }
        else
        {
            transform.position = latestState.position;
            transform.rotation = latestState.rotation;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            previousState = latestState;

            State state;
            state.position = (Vector3)stream.ReceiveNext();
            state.rotation = (Quaternion)stream.ReceiveNext();
            state.timestamp = Time.time;
            latestState = state;

            initialized = true;
        }
    }

    [PunRPC]
    public void SyncPosition(Vector3 pos, Quaternion rot)
    {
        transform.position = pos;
        transform.rotation = rot;
        var now = Time.time;
        latestState = new State { position = pos, rotation = rot, timestamp = now };
        previousState = latestState;
        initialized = true;
    }

    [PunRPC]
    public void RequestSyncPosition()
    {
        if (photonView.IsMine)
        {
            photonView.RPC("SyncPosition", RpcTarget.Others, transform.position, transform.rotation);
        }
    }

    void OnDisable()
    {
        if (!resetOnDisable)
            return;

        Vector3 targetPosition;

        if (resetOnDisableSpawnPoints != null && resetOnDisableSpawnPoints.Length > 0)
        {
            int randomIndex = Random.Range(0, resetOnDisableSpawnPoints.Length);
            Transform chosenSpawn = resetOnDisableSpawnPoints[randomIndex];

            if (chosenSpawn != null)
            {
                targetPosition = chosenSpawn.position;
            }
            else
            {
                targetPosition = initialPosition;
            }
        }
        else
        {
            targetPosition = initialPosition;
        }

        transform.position = targetPosition;

        initialPosition = targetPosition;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}