using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeshCreator 
{
    GameObject gameObject;
    List<Vector3> linePositions;
    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    MeshCollider meshCollider;

    List<Vector3> vertices;
    List<int> triangles;
    List<Vector2> uvs;

    public void createObject(LineDrawer lineDrawer)
    {
        //Mesh objesi yoksa yenisini yarat�r.
        if(gameObject == null)
        {
            gameObject = new GameObject("meshObject");
            meshFilter = gameObject.AddComponent<MeshFilter>();
            meshRenderer = gameObject.AddComponent<MeshRenderer>();
            meshCollider = gameObject.AddComponent<MeshCollider>();

            Material material = new Material(Shader.Find("Standard"));
            material.color = Color.red;

            meshRenderer.material = material;

            vertices = new List<Vector3>();
            triangles = new List<int>();
            uvs = new List<Vector2>();

            //Line drawerdan al�nan posizyonlar mesh yaratma fonksiyonuna g�nderilir.
            linePositions = lineDrawer.getLinePositions();
            meshFilter.mesh = createMesh(linePositions);

            //Yarat�lan mesh i�in collider yarat�l�r.
            meshCollider.sharedMesh = meshFilter.mesh;

        }
    }

    public void destroyMesh()
    {
        //Mesh varsa yok eder.
        if(gameObject != null)
        {
            Object.Destroy(gameObject);
        }
    }

    private Mesh createMesh(List<Vector3> positions)
    {
        Mesh mesh = new Mesh();
        int vertexIndex = 0;

        //Gelen b�t�n pozisyonlar i�in o noktada bir "voxel" mesh olu�turulur. Bu voxeller birlikte b�t�n �ekli olu�turur.
        foreach (Vector3 position in positions)
        {
            for (int k = 0; k < 6; k++)
            {
                for (int i = 0; i < 6; i++)
                {
                    int triangleIndex = MeshData.voxelTris[k, i];
                    vertices.Add(MeshData.voxelVerts[triangleIndex] + position);
                    triangles.Add(vertexIndex);

                    uvs.Add(Vector2.zero);

                    vertexIndex++;
                }
            }
        }


        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();

        mesh.RecalculateNormals();

        return mesh;
    }

}
