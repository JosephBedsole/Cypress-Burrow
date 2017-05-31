using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joseph Bedsole (jbedsole)


          // Warning ::::::::::::: You must go into the mesh(script) settings and click on "Set-up Mesh" to show changes that you've made;
          // Automatically attaches components on objects with this script; You can also add scripts to objects using the same method;
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class ProcMeshCube : MonoBehaviour {

    [Tooltip("Number of tiles of to draw")] // Can use [space] to make spaces and [multiline]... to add a larger field
    [Range(0.1f, 100)] public float size = 1;

    [ContextMenu("Setup Mesh")]
    void SetupMesh()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        Mesh mesh = new Mesh();
        mesh.name = "ProcMeshCube";

        // Quad with the origin at the center; You can give different numbers to make different shapes, sizes, placement compared to origin, and much more;
        mesh.vertices = new Vector3[]
        {
            //front
            new Vector3(-size, -size, -size),
            new Vector3(-size, size, -size),
            new Vector3(size, size, -size),
            new Vector3(size, -size, -size),
            //back
            new Vector3(size, -size, size),
            new Vector3(size, size, size),
            new Vector3(-size, size, size),
            new Vector3(-size, -size, size),
            //right
            new Vector3(size, -size, -size),
            new Vector3(size, size, -size),
            new Vector3(size, size, size),
            new Vector3(size, -size, size),
            //left
            new Vector3(-size, -size, size),
            new Vector3(-size, size, size),
            new Vector3(-size, size, -size),
            new Vector3(-size, -size, -size),
            //top
            new Vector3(-size, size, -size),
            new Vector3(-size, size, size),
            new Vector3(size, size, size),
            new Vector3(size, size, -size),
            //bottom
            new Vector3(-size, -size, size),
            new Vector3(-size, -size, -size),
            new Vector3(size, -size, -size),
            new Vector3(size, -size, size)
        };

        mesh.normals = new Vector3[]
        {
            Vector3.back,
             Vector3.back,
             Vector3.back,
             Vector3.back,

             Vector3.forward,
             Vector3.forward,
             Vector3.forward,
             Vector3.forward,

              Vector3.right,
              Vector3.right,
              Vector3.right,
              Vector3.right,

             Vector3.left,
             Vector3.left,
             Vector3.left,
             Vector3.left,

             Vector3.up,
             Vector3.up,
             Vector3.up,
             Vector3.up,

             Vector3.down,
             Vector3.down,
             Vector3.down,
             Vector3.down
        };

        mesh.uv = new Vector2[]
        {
            Vector2.zero,
            Vector2.up,
            Vector2.one,
            Vector2.right,

            Vector2.zero,
            Vector2.up,
            Vector2.one,
            Vector2.right,

            Vector2.zero,
            Vector2.up,
            Vector2.one,
            Vector2.right,

            Vector2.zero,
            Vector2.up,
            Vector2.one,
            Vector2.right,

            Vector2.zero,
            Vector2.up,
            Vector2.one,
            Vector2.right,

            Vector2.zero,
            Vector2.up,
            Vector2.one,
            Vector2.right
        };

        mesh.colors = new Color[]
        {
            Color.white,
            Color.white,
            Color.white,
            Color.white,

            Color.white,
            Color.white,
            Color.white,
            Color.white,

            Color.white,
            Color.white,
            Color.white,
            Color.white,

            Color.white,
            Color.white,
            Color.white,
            Color.white,

            Color.white,
            Color.white,
            Color.white,
            Color.white,

            Color.white,
            Color.white,
            Color.white,
            Color.white
        };

        // Assign tris last; Don't want to break unity...lol;
        mesh.triangles = new int[] 
        {
            0, 1, 2, 0, 2, 3,
            4, 5, 6, 4, 6, 7,
            8, 9, 10, 8, 10, 11,
            12, 13, 14, 12, 14, 15,
            16, 17, 18, 16, 18, 19,
            20, 21, 22, 20, 22, 23
        };

        meshFilter.mesh = mesh;

    }

}
