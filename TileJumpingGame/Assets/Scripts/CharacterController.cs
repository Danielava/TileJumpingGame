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

public enum PlayerState
{
    Idle,
    Moving,
    PreparingSpell,
    CastingSpell
}

public class CharacterController : MonoBehaviour
{
    public int[] start_tile = new int[2]; //The tile to start at. They are 0-indexed.
    private TileBoard board;
    public Player Player;
    
    public Inventory m_Inventory;

    public GameObject m_DirectionalArrows; //Activated when player picks a directional spell
    public PlayerState m_PlayerState;
    private Spell m_PreparedSpell;


    // Start is called before the first frame update
    void Start()
    {
        board = GameObject.Find("Board").GetComponent<TileBoard>();
        var firstTile = board.GetTile(0, 0);
        Player.EnterTile(firstTile);
        //m_DirectionalArrows.SetActive(false);
        m_PlayerState = PlayerState.Idle;
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
            if (m_PlayerState == PlayerState.PreparingSpell)
            {
                PlayerCastPreparedSpell(Direction.Up);
            }
            else
            {
                Move(Direction.Up, 1);
            }
        }
        //Down
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (m_PlayerState == PlayerState.PreparingSpell)
            {
                PlayerCastPreparedSpell(Direction.Down);
            }
            else
            {
                Move(Direction.Down, 1);
            }
        }
        //Right
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (m_PlayerState == PlayerState.PreparingSpell)
            {
                PlayerCastPreparedSpell(Direction.Right);
            }
            else
            {
                Move(Direction.Right, 1);
            }
        }
        //Left
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (m_PlayerState == PlayerState.PreparingSpell)
            {
                PlayerCastPreparedSpell(Direction.Left);
            }
            else
            {
                Move(Direction.Left, 1);
            }
        }
    }

    private void Move(Direction direction, int steps)
    {
        if (steps == 0)
        {
            return;
        }

        Tile tile = null;
        switch (direction)
        {
            case Direction.Up:
                if (board.CanMoveTo(Player.CurrentTile.xPos, Player.CurrentTile.yPos + steps))
                {
                    tile = board.GetTile(Player.CurrentTile.xPos, Player.CurrentTile.yPos + steps);
                } else
                {
                    Move(direction, steps - 1);
                }
                break;
            case Direction.Down:
                if (board.CanMoveTo(Player.CurrentTile.xPos, Player.CurrentTile.yPos - steps))
                {
                    tile = board.GetTile(Player.CurrentTile.xPos, Player.CurrentTile.yPos - steps);
                }
                else
                {
                    Move(direction, steps - 1);
                }
                break;
            case Direction.Right:
                if (board.CanMoveTo(Player.CurrentTile.xPos + steps, Player.CurrentTile.yPos))
                {
                    tile = board.GetTile(Player.CurrentTile.xPos + steps, Player.CurrentTile.yPos);
                }
                else
                {
                    Move(direction, steps - 1);
                }
                break;
            case Direction.Left:
                if (board.CanMoveTo(Player.CurrentTile.xPos - steps, Player.CurrentTile.yPos))
                {
                    tile = board.GetTile(Player.CurrentTile.xPos - steps, Player.CurrentTile.yPos);
                }
                else
                {
                    Move(direction, steps - 1);
                }
                break;
        }
        if (tile != null)
        {
            Player.EnterTile(tile);
            if (tile.tileType == TileType.Ice)
            {
                Move(direction, 1);
            }
        }
    }

    public void PrepareSpell(Spell spell)
    {
        m_PlayerState = PlayerState.PreparingSpell;
        //m_DirectionalArrows.SetActive(true);
        m_PreparedSpell = spell;
    }

    private void PlayerCastPreparedSpell(Direction direction)
    {
        Player.CastSpell(m_PreparedSpell, direction);
        m_PlayerState = PlayerState.Idle;
        //m_DirectionalArrows.SetActive(true);
    }

    public void Teleport(Direction direction)
    {
        Move(direction, 3);
    }
}
