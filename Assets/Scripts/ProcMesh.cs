using System.Collections;
using System.Collections.Generic;
using UnityEngine;
          // Warning ::::::::::::: You must go into the mesh(script) settings and click on "Set-up Mesh" to show changes that you've made;
          // Automatically attaches components on objects with this script; You can also add scripts to objects using the same method;
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class ProcMesh : MonoBehaviour {

    [Tooltip("Number of tiles of to draw")] // Can use [space] to make spaces and [multiline]... to add a larger field
    [Range(1, 16250)]
    public int tileCount = 10;




    [ContextMenu("Setup Mesh")]
    void SetupMesh()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        List<Vector3> verts = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        List<Color> colors = new List<Color>();
        List<int> tris = new List<int>();

        for (int i = 0; i < tileCount; ++i)
        {
            // Quad with the origin at the center; You can give different numbers to make different shapes, sizes, placement compared to origin, and much more;
            Vector3 pos = Vector3.right * i;
            verts.AddRange(new Vector3[]
            {
                pos + new Vector3(-0.5f, -0.5f, 0),
                 pos + new Vector3(-0.5f, 0.5f, 0),
                  pos + new Vector3(0.5f, 0.5f, 0),
                   pos + new Vector3(0.5f, -0.5f, 0),
            });

            uvs.AddRange(new Vector2[]
            {
                Vector2.zero,
                Vector2.up,
                Vector2.one,
                Vector2.right,
            });

            colors.AddRange(new Color[]
            {
                Color.white,
                Color.white,
                Color.white,
                Color.white,
            });

            // Assign tris last or you will break things!
            int index = i * 4;
            tris.AddRange(new int[]
            {
                index, index + 1, index + 2,
                index, index + 2, index + 3
            });

        }

        Mesh mesh = new Mesh();
        mesh.name = "ProcMesh";
        mesh.vertices = verts.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.colors = colors.ToArray();
        mesh.triangles = tris.ToArray();




        //// Quad with the origin at the center; You can give different numbers to make different shapes, sizes, placement compared to origin, and much more;
        //mesh.vertices = new Vector3[]
        //{
        //    new Vector3(-1,-1,0),
        //     new Vector3(-1,1,0),
        //      new Vector3(1,1,0),
        //       new Vector3(1,-1,0),
        //};

        //mesh.uv = new Vector2[]
        //{
        //    Vector2.zero,
        //    Vector2.up,
        //    Vector2.one,
        //    Vector2.right,
        //};

        //mesh.colors = new Color[]
        //{
        //    Color.red,
        //    Color.blue,
        //    Color.yellow,
        //    Color.green,
        //};

        //// Assign tris last; Don't want to break unity...lol;
        //mesh.triangles = new int[] { 0, 1, 2, 0, 2, 3 };

        meshFilter.mesh = mesh;

    }

}
