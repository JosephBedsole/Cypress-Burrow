using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(MeshFilter))]
public class MeshFilterEditor : Editor {

    public override void OnInspectorGUI ()
    {
        int triCount = 0;

        if (targets == null || targets.Length < 2)
        {
            MeshFilter filter = target as MeshFilter;
            triCount = filter.sharedMesh.triangles.Length / 3;
        }
        else
        {
            for (int i = 0; i < targets.Length; i++)
            {
                MeshFilter filter = targets[i] as MeshFilter;
                triCount += filter.sharedMesh.triangles.Length / 3;
            }
        }

        EditorGUILayout.LabelField("Tri Count: " + triCount);
        base.OnInspectorGUI();
    }


}
