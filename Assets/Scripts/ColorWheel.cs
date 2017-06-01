using System.Collections;
using System.Collections.Generic;
using UnityEngine;
          // Warning ::::::::::::: You must go into the mesh(script) settings and click on "Set-up Mesh" to show changes that you've made;
          // Automatically attaches components on objects with this script; You can also add scripts to objects using the same method;
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class ColorWheel : MonoBehaviour {

    [Tooltip("Number of tiles of to draw")] // Can use [space] to make spaces and [multiline]... to add a larger field
    [Range(1, 16250)]
    public int tileCount = 10;

    public int segments=10;
   


    [ContextMenu("Setup Mesh")]
    void SetupMesh()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        List<Vector3> verts = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        List<Color> colors = new List<Color>();
        List<int> tris = new List<int>();

        float dA = (Mathf.PI * 2) / (float)segments;
        int index = 0;
        for (int i = 0; i < segments; ++i)
        {
            float frac = (float)i / (float)segments;
            float ang = frac * (Mathf.PI * 2);
            Vector3 p1 = new Vector3(-Mathf.Sin(ang), Mathf.Cos(ang), 0);
            Vector3 p2 = new Vector3(-Mathf.Sin(ang + dA), Mathf.Cos(ang + dA), 0);

        Vector3 pos = Vector3.right * i;
            verts.AddRange(new Vector3[]
            {

                Vector3.zero,
                p1,
                p2

            });

            uvs.AddRange(new Vector2[]
            {
               new Vector2 (0.5f, 0.5f),
               (Vector2)p1 * 0.5f + new Vector2(0.5f, 0.5f),
               (Vector2)p2 * 0.5f + new Vector2(0.5f, 0.5f)
            });

            colors.AddRange(new Color[]
            {
                Color.HSVToRGB(1,0,1),
                Color.HSVToRGB(frac,1,1),
                Color.HSVToRGB(frac + 1f / (float)segments,1,1)
            });

            // Assign tris last or you will break things!
           
            tris.AddRange(new int[]
            {
                index, index + 1, index + 2
            });
            index += 3;
        }

        Mesh mesh = new Mesh();
        mesh.name = "ProcMesh";
        mesh.vertices = verts.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.colors = colors.ToArray();
        mesh.triangles = tris.ToArray();

        meshFilter.mesh = mesh;

    }

}
