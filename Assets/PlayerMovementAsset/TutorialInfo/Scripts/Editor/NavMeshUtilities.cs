using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public class NavMeshUtilities : MonoBehaviour
{
    [MenuItem("Light Brigade/Debug/Force Cleanup NavMesh")]
    public static void ForceCleanupNavMesh()
    {
        if (Application.isPlaying)
            return;

        NavMesh.RemoveAllNavMeshData();
        Debug.Log("All NavMesh data has been removed.");
    }
}
