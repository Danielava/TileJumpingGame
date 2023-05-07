using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public int[] start_tile = new int[2]; //The tile to start at. They are 0-indexed.
    private TileBoard board;
    private int[] current_tile = new int[2]; //(x, y) pair

    public Inventory m_Inventory;
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
        //Assign the position.
        transform.position = board.GetTilePosition(start_tile[0], start_tile[1]);
        current_tile[0] = start_tile[0];
        current_tile[1] = start_tile[1];
    }

    /*
     * TODO: LOOK AT THIS TUTORIAL FOR SMOOTH MOVEMENT!
     * https://youtu.be/3kW54hU98os?t=411
     * 
     */

    // Update is called once per frame
    void Update()
    {
        //Movement, check for collision in all of them before updating character position.

        //Up
        if (Input.GetKeyDown(KeyCode.W))
        {
            //We move one UP in y-axis so check current_tile[0] collision
            if(current_tile[1]+1 < board.GetSizeY())
            {
                Move(current_tile[0], current_tile[1]+1);
                //print("Player went up!");
            }
            else
            {
                //print("Collision, can't move up!");
            }
        }
        //Down
        if (Input.GetKeyDown(KeyCode.S))
        {
            //We move one DOWN in y-axis so check current_tile[0] against 0 for collision
            if (current_tile[1] > 0)
            {
                Move(current_tile[0], current_tile[1]-1);
                //print("Player went down!");
            }
            else
            {
                //print("Collision, can't move down!");
            }
        }
        //Right
        if (Input.GetKeyDown(KeyCode.D))
        {
            //We move one RIGHT in x-axis so check current_tile[1] collision
            if (current_tile[0]+1 < board.GetSizeX())
            {
                Move(current_tile[0]+1, current_tile[1]);
                //print("Player went right!");
            }
            else
            {
                //print("Collision, can't move right!");
            }
        }
        //Left
        if (Input.GetKeyDown(KeyCode.A))
        {
            //We move one RIGHT in x-axis so check current_tile[1] collision
            if (current_tile[0] > 0)
            {
                Move(current_tile[0]-1, current_tile[1]);
                //print("Player went left!");
            }
            else
            {
                //print("Collision, can't move left!");
            }
        }
    }

    /*
    * Moves our player to another tile and updates the current_tile (= players current tile position)
    */
    void Move(int x, int y)
    {
        transform.position = board.GetTilePosition(x, y);
        current_tile[0] = x;
        current_tile[1] = y;
    }

    /*
     *  Input a position, returns true if that position exists.
     */
    bool CheckBoardBounds(int x, int y)
    {
        return false;
    }

    /*
     * Detect collision between the player and enemies or power-ups.
     */
    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Powerup")
        {
            Destroy(collision.gameObject);
            Debug.Log("Powerup hit");
        }

        if (collision.gameObject.tag == "Enemy")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            Debug.Log("Enemy hit");
        }

        if (collision.gameObject.tag == "Hazard")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            Debug.Log("Hazard hit");
        }
    }
}
