using UnityEngine;

public class MapZone : MonoBehaviour
{
    [Header("ok daddy")]
    public string mapName = "pls name";
    public Vector3 size = new Vector3(10f, 5f, 10f);

    [Header("gazmo")]
    public Color fillColor  = new Color(0f, 0.8f, 1f, 0.12f);
    public Color wireColor  = new Color(0f, 0.8f, 1f, 1f);
    public Color labelColor = Color.white;

    private bool wasInside;

    private void Update()
    {
        if (Camera.main == null) return;

        bool inside = IsInside(Camera.main.transform.position);
        if (inside == wasInside) return;

        wasInside = inside;

        if (inside)
            MapZoneDisplay.Instance?.EnterZone(this);
        else
            MapZoneDisplay.Instance?.ExitZone(this);
    }

    private void OnDisable()
    {
        if (wasInside)
        {
            wasInside = false;
            MapZoneDisplay.Instance?.ExitZone(this);
        }
    }

    public bool IsInside(Vector3 worldPos)
    {
        Vector3 local = transform.InverseTransformPoint(worldPos);
        return Mathf.Abs(local.x) < size.x * 0.5f
            && Mathf.Abs(local.y) < size.y * 0.5f
            && Mathf.Abs(local.z) < size.z * 0.5f;
    }

    private void OnDrawGizmos()
    {
        Matrix4x4 prev = Gizmos.matrix;
        Gizmos.matrix = transform.localToWorldMatrix;

        Gizmos.color = fillColor;
        Gizmos.DrawCube(Vector3.zero, size);

        Gizmos.color = wireColor;
        Gizmos.DrawWireCube(Vector3.zero, size);

        Gizmos.matrix = prev;

#if UNITY_EDITOR
        UnityEditor.Handles.color = labelColor;
        UnityEditor.Handles.Label(
            transform.position + Vector3.up * (size.y * 0.5f + 0.3f),
            $"map: {mapName.ToLower()}"
        );
#endif
    }
}