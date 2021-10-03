/*
 Grid Generator
 Author : Nour Bou Nasr
 Desc : procedural generate meshes in case we need them 
 Objective : create a procedural generator landscape with quads
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGen : MonoBehaviour
{
    [SerializeField] int world_x; //size of the x axis
    [SerializeField] int world_z;//sz=ize of z

    private Mesh mesh; //the mesh we want to instanciate

    //defining the required variables to create a mesh
    private int[] triangles;
    private Vector3[] vertices;
    [SerializeField] float noise_height = 3f;
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        generate_mesh();
        update_mesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void generate_mesh()
    {
        triangles = new int[world_x * world_z * 6]; //6 bcz we are defining a full quad 
        vertices = new Vector3[(world_x + 1) * (world_z +1) ]; //+1 bcz vertices are 1 more than the quads
        for(int i=0, z = 0; z <= world_z; z++)
        {
            for(int x = 0; x<=world_x; x++)
            {
                vertices[i] = new Vector3(x, generate_noise(x,z,8f) * noise_height, z);
                i++;
            }
        }

        int tris = 0;
        int verts = 0;

        for (int i = 0, z = 0; z < world_z; z++)
        {
            for (int x = 0; x < world_x; x++)
            {
                //triamgle 1
                triangles[tris + 0] = verts + 0;
                triangles[tris + 1] = verts + world_z + 1;
                triangles[tris + 2] = verts + 1;

                //triangle 2
                triangles[tris + 3] = verts + 1;
                triangles[tris + 4] = verts + world_z + 1;
                triangles[tris + 5] = verts + world_z + 2;

                tris += 6;
                verts++;
            }
            verts++;
        }
    }

    void update_mesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
    //method to generate perlin noise 
    private float generate_noise(int x, int z, float detail_scale)
    {
        float x_noise = (x + this.transform.position.x) / detail_scale;
        float z_noise = (z + this.transform.position.y) / detail_scale;
        return Mathf.PerlinNoise(x_noise, z_noise);
    }

}
