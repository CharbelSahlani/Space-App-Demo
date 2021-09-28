/*
 Grid Generator
 Author : Nour Bou Nasr
 Desc : this is a basic grid generator
 Objective : create a procedural generator landscape
 Hints : I will be using Perlin Noise to offset y axis and create Noise 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] int worldsize_x = 10; //world size in the x pos 
    [SerializeField] int worldsize_z = 10; //world size in the z pos
    [SerializeField] float grid_offset = 1.1f; // offset or distance between blocks
    [SerializeField] GameObject block_tile; //the block we want to instanciate
    [SerializeField] int noise_height = 3; //the noise strenght
    [SerializeField] GameObject object_to_spawn; //object to spawn on the grid 
    private List<Vector3> block_positions = new List<Vector3>();
    [SerializeField] int num_of_obj_to_spawn = 20; //number of objects to spawn

    private void Start()
    {
        for(int x = 1; x <= worldsize_x; x++)
        {
            for(int z = 1; z <= worldsize_z; z++)
            {
                //getting pos and assigning it to x and z * offset then add it to the parent gameobject
                Vector3 pos = new Vector3(x * grid_offset, generate_noise(x, z, 8f) * noise_height, z * grid_offset);
                GameObject block = Instantiate(block_tile, pos, Quaternion.identity as gameObject);
                block_positions.Add(block.transform.position); //add the block position to the list
                block.transform.SetParent(this.transform);
            }
        }
        spawn_objects();
    }

    //method to generate perlin noise 
    private float generate_noise(int x , int z, float detail_scale)
    {
        float x_noise = (x * this.transform.position.x) / detail_scale;
        float z_noise = (z * this.transform.position.y) / detail_scale;
        return Mathf.PerlinNoise(x_noise, z_noise);
    }

    //spawn objects 
    private void spawn_objects()
    {
        for(int i = 1; i <= num_of_obj_to_spawn; i++)
        {
            GameObject new_obj = Instantiate(object_to_spawn, object_spawn_location(), Quaternion.identity);
        }
    }
    //random object location to be spawn in the spawn object methos
    private Vector3 object_spawn_location()
    {
        int rand_index = Random.Range(0, block_positions.Count());
        Vector3 new_pos = new Vector3(block_positions[rand_index].x, block_positions[rand_index].y + .5f, block_positions[rand_index].z);
        block_positions.RemoveAt(rand_index); //to prevent overlaping objects
        return new_pos;
    }


}
