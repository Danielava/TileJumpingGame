using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Board;

public enum Direction
{
    Up,
    Left,
    Down,
    Right
}

public class CharacterController : MonoBehaviour
{
    public int[] start_tile = new int[2]; //The tile to start at. They are 0-indexed.
    private TileBoard board;
    public Player Player;
    
    public Inventory m_Inventory;

    // Start is called before the first frame update
    void Start()
    {
        board = GameObject.Find("Board").GetComponent<TileBoard>();
        var firstTile = board.GetTile(0, 0);
        Player.EnterTile(firstTile);
    }

    private void Awake()
    {
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
            Move(Direction.Up);
        }
        //Down
        if (Input.GetKeyDown(KeyCode.S))
        {
            Move(Direction.Down);
        }
        //Right
        if (Input.GetKeyDown(KeyCode.D))
        {
            Move(Direction.Right);
        }
        //Left
        if (Input.GetKeyDown(KeyCode.A))
        {
            Move(Direction.Left);
        }
    }

    private void Move(Direction direction)
    {
        Tile tile = null;
        switch (direction)
        {
            case Direction.Up:
                if (board.CanMoveTo(Player.CurrentTile.xPos, Player.CurrentTile.yPos + 1))
                {
                    tile = board.GetTile(Player.CurrentTile.xPos, Player.CurrentTile.yPos + 1);
                }
                break;
            case Direction.Down:
                if (board.CanMoveTo(Player.CurrentTile.xPos, Player.CurrentTile.yPos - 1))
                {
                    tile = board.GetTile(Player.CurrentTile.xPos, Player.CurrentTile.yPos - 1);
                }
                break;
            case Direction.Right:
                if (board.CanMoveTo(Player.CurrentTile.xPos + 1, Player.CurrentTile.yPos))
                {
                    tile = board.GetTile(Player.CurrentTile.xPos + 1, Player.CurrentTile.yPos);
                }
                break;
            case Direction.Left:
                if (board.CanMoveTo(Player.CurrentTile.xPos - 1, Player.CurrentTile.yPos))
                {
                    tile = board.GetTile(Player.CurrentTile.xPos - 1, Player.CurrentTile.yPos);
                }
                break;
        }
        if (tile != null)
        {
            Player.EnterTile(tile);
            if (tile.tileType == TileType.Ice)
            {
                Move(direction);
            }
        }
    }
}
