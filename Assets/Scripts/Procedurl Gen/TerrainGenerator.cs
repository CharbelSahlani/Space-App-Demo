
/*
 Grid Generator
 Author : Nour Bou Nasr
 Desc : procedural generate terrain depending on player position(infinite landscape)
 Objective : create a procedural generator landscape with planes (infinetely)
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject plane;
    private int radius = 5; // how many planes to spawn around player
    private int plane_offset = 10; //amount of squares a plane takes 
    private Vector3 start_pos = Vector3.zero;
    private Hashtable tile_table = new Hashtable(); //if we have a plane or not   

    // Update is called once per frame
    void Update()
    {
        if(start_pos == Vector3.zero)
        {
            for(int x = -radius; x < radius; x++)
            {
                for(int z = -radius; z < radius; z++)
                {
                    Vector3 pos = new Vector3(x * plane_offset + player_location_x, 0, z * plane_offset + player_location_z);
                    //check if position already exists in hastable, if yes skip
                    if (!tile_table.Contains(pos))
                    {
                        gameObject _plane = Instantiate(plane, pos, Quaternion.identity);
                        tile_table.Add(pos, _plane);
                    }
                }
            }
        }
        if (has_player_moved)
        {
            for (int x = -radius; x < radius; x++)
            {
                for (int z = -radius; z < radius; z++)
                {
                    Vector3 pos = new Vector3(x * plane_offset + player_location_x, 0, z * plane_offset + player_location_z);
                    //check if position already exists in hastable, if yes skip
                    if (!tile_table.Contains(pos))
                    {
                        gameObject _plane = Instantiate(plane, pos, Quaternion.identity);
                        tile_table.Add(pos, _plane);
                    }
                }
            }
        }
    }

    //calculate distance traveled on X position
    private int player_move_x
    {
        get
        {
            return (int)(player.transform.position.x - start_pos.x);
        }

    }

    private int player_move_z
    {
        get
        {
            return (int)(player.transform.position.z - start_pos.z);
        }
    }

    private int player_location_x
    {
        get
        {
            return (int)Mathf.Floor(player.transform.position.x/plane_offset) * plane_offset;
        }
    }

    private int player_location_z
    {
        get
        {
            return (int)Mathf.Floor(player.transform.position.z / plane_offset) * plane_offset;
        }
    }

    bool has_player_moved()
    {
        if(Mathf.Abs(player_move_x) >= plane_offset || Mathf.Abs(player_move_z) >= plane_offset)
        {
            return true;
        }
        return false;
    }
}
