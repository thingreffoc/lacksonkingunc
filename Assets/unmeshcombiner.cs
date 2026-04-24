using UnityEngine;
using UnityEditor;

public class unmeshcombiner : MonoBehaviour
{
    [MenuItem("Tools/Unmesh Combiner")]
    public static void CombineMeshes()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            MeshCollider meshCollider = obj.GetComponent<MeshCollider>();
            MeshFilter meshFilter = obj.GetComponent<MeshFilter>();

            // If the object has both a MeshCollider and a MeshFilter, copy the MeshCollider's mesh to the MeshFilter
            if (meshCollider != null && meshFilter != null)
            {
                meshFilter.sharedMesh = meshCollider.sharedMesh;
                Debug.Log($"Mesh from MeshCollider applied to MeshFilter on {obj.name}");
            }
            // If the object only has a MeshFilter, disable its Static flag
            else if (meshFilter != null && meshCollider == null)
            {
                GameObjectUtility.SetStaticEditorFlags(obj, 0); // Clears all static flags
                Debug.Log($"Static flag turned off for {obj.name} because it only has a MeshFilter.");
            }
        }
    }
}
