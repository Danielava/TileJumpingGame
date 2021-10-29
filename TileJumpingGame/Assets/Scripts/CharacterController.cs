using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public int[] start_tile = new int[2];
    private TileBoard board;
    // Start is called before the first frame update
    void Start()
    {
        board = GameObject.Find("Board").GetComponent<TileBoard>();
        GameObject[,] tiles = board.GetTileList();
        //Set players start position to the specified tile position
        if (start_tile[0] >= tiles.GetLength(0) || start_tile[0] < 0 ||
            start_tile[1] >= tiles.GetLength(1) || start_tile[1] < 0) //The x coord.
        {
            start_tile[0] = 0;
            start_tile[1] = 0;
        }
        Debug.Log(start_tile[0]);
        Debug.Log(start_tile[1]);
        //Assign the position.
        transform.position = board.GetTilePosition(start_tile[0], start_tile[1]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
