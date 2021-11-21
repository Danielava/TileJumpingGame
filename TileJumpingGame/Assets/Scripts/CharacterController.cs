using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public int[] start_tile = new int[2]; //The tile to start at.
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
        //Assign the position.
        transform.position = board.GetTilePosition(start_tile[0], start_tile[1]);
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        //Up
        if (Input.GetKeyDown(KeyCode.W))
        {
            print("Player went up");
        }
        //Down
        if (Input.GetKeyDown(KeyCode.S))
        {
            print("space key was pressed");
        }
        //Right
        if (Input.GetKeyDown(KeyCode.D))
        {
            print("space key was pressed");
        }
        //Left
        if (Input.GetKeyDown(KeyCode.A))
        {
            print("space key was pressed");
        }
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
