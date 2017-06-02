using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class HeightMapping : MonoBehaviour
{

    public Texture2D texture;

    public float height;

    [ContextMenu("Setup Mesh")]
    void SetupMesh()
    {
        if (texture == null)
        {
            Debug.LogError("No texture found here. Good bye!");
            return;
        }

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        Vector3[] verts = new Vector3[texture.width * texture.height];
        Vector2[] uvs = new Vector2[texture.width * texture.height];
        Color[] colors = new Color[texture.width * texture.height];
        List<int> tris = new List<int>();

        for (int x = 0; x < texture.width; ++x)
        {
            for (int z = 0; z < texture.height; ++z)
            {
                verts[x + z * texture.width] = new Vector3(x, texture.GetPixel(x,z).r * height, z);
                uvs[x + z * texture.width] = new Vector2( ((float)x / (float)texture.width), ((float)z / (float)texture.height) );
                colors[x + z * texture.width] = Color.white;

                if (x < texture.width - 1 && z < texture.height -1)
                {
                    int nextRow = x + (z + 1) * texture.width;
                    tris.AddRange(new int[]
          {
                (x + z * texture.width), nextRow, nextRow + 1,
               (x + z * texture.width), nextRow + 1, (x + z * texture.width) + 1
          });
                }
            }
        }
        Mesh mesh = new Mesh();
        mesh.name = "HeightofMapo";
        mesh.vertices = verts;
        mesh.uv = uvs;
        mesh.colors = colors;
        mesh.triangles = tris.ToArray();
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;
    }
}