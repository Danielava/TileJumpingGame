using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Board;

public enum Direction
{
    Up,
    Left,
    Down,
    Right,
    NONE
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
    public PlayerState m_PlayerState = PlayerState.Idle;
    private Spell m_PreparedSpell;

    public float MoveSpeed;

    public Direction bufferedMove;
    private float bufferTime = 0.2f;
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
            else if (m_PlayerState == PlayerState.Moving)
            {
                bufferedMove = Direction.Up;
                StartCoroutine(ClearBuffer(bufferTime));
            }
            else
            {
                StartCoroutine(Move(Direction.Up, 1));
            }
        }
        //Down
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (m_PlayerState == PlayerState.PreparingSpell)
            {
                PlayerCastPreparedSpell(Direction.Down);
            }
            else if(m_PlayerState == PlayerState.Moving)
            {
                bufferedMove = Direction.Down;
                StartCoroutine(ClearBuffer(bufferTime));
            } 
            else
            {
                StartCoroutine(Move(Direction.Down, 1));
            }
        }
        //Right
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (m_PlayerState == PlayerState.PreparingSpell)
            {
                PlayerCastPreparedSpell(Direction.Right);
            }
            else if (m_PlayerState == PlayerState.Moving)
            {
                bufferedMove = Direction.Right;
                StartCoroutine(ClearBuffer(bufferTime));
            }
            else
            {
                StartCoroutine(Move(Direction.Right, 1));
            }
        }
        //Left
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (m_PlayerState == PlayerState.PreparingSpell)
            {
                PlayerCastPreparedSpell(Direction.Left);
            }
            else if (m_PlayerState == PlayerState.Moving)
            {
                bufferedMove = Direction.Left;
                StartCoroutine(ClearBuffer(bufferTime));
            }
            else
            {
                StartCoroutine(Move(Direction.Left, 1));
            }
        }
    }

    private IEnumerator Move(Direction direction, int steps, float speed = 1)
    {
        if (m_PlayerState != PlayerState.Idle || steps == 0) yield break;

        Tile tile = null;
        while (tile == null && steps != 0)
        {
            switch (direction)
            {
                case Direction.Up:
                    if (board.CanMoveTo(Player.CurrentTile.xPos, Player.CurrentTile.yPos + steps))
                    {
                        tile = board.GetTile(Player.CurrentTile.xPos, Player.CurrentTile.yPos + steps);
                        break;
                    }
                    break;
                case Direction.Down:
                    if (board.CanMoveTo(Player.CurrentTile.xPos, Player.CurrentTile.yPos - steps))
                    {
                        tile = board.GetTile(Player.CurrentTile.xPos, Player.CurrentTile.yPos - steps);
                        break;
                    }
                    break;
                case Direction.Right:
                    if (board.CanMoveTo(Player.CurrentTile.xPos + steps, Player.CurrentTile.yPos))
                    {
                        tile = board.GetTile(Player.CurrentTile.xPos + steps, Player.CurrentTile.yPos);
                        break;
                    }
                    break;
                case Direction.Left:
                    if (board.CanMoveTo(Player.CurrentTile.xPos - steps, Player.CurrentTile.yPos))
                    {
                        tile = board.GetTile(Player.CurrentTile.xPos - steps, Player.CurrentTile.yPos);
                        break;
                    }
                    break;
            }
            steps--;
        }

        if (tile != null)
        {
            var moveLockDuration = MoveSpeed * (steps + 1) * speed;
            gameObject.AddComponent<Move>().Init(tile.gameObject.transform.position, moveLockDuration, null);
            m_PlayerState = PlayerState.Moving;
            StartCoroutine(EnterTile(moveLockDuration, direction, tile));
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
        m_PlayerState = PlayerState.Idle;
        StartCoroutine(Move(direction, 3, 0.1f));
    }

    private IEnumerator EnterTile(float waitTime, Direction direction, Tile tile)
    {

        yield return new WaitForSeconds(waitTime);

        Player.EnterTile(tile);
        m_PlayerState = PlayerState.Idle;
        if (tile.tileType == TileType.Ice)
        {
            StartCoroutine(Move(direction, 1));
        } else if (bufferedMove != Direction.NONE)
        {
            StartCoroutine(Move(bufferedMove, 1));
            bufferedMove = Direction.NONE;
        }
    }

    private IEnumerator ClearBuffer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        if(bufferedMove != Direction.NONE)
        {
            bufferedMove = Direction.NONE;
        }
    }
}
