using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Board;

public class CharacterController : MonoBehaviour
{
    public int[] start_tile = new int[2]; //The tile to start at. They are 0-indexed.
    private GridTileBoard board;
    public Player Player;
    public Inventory Inventory;
    public GameObject DirectionalArrows; //Activated when player picks a directional spell
    public float MoveSpeed;
    public Direction bufferedMove;
    // Start is called before the first frame update
    void Start()
    {
        board = GameObject.Find("Board").GetComponent<GridTileBoard>();
        var firstTile = board.GetTile(1, 1);
        Player.EnterTile(firstTile);
        //m_DirectionalArrows.SetActive(false);
        Player.PlayerState = PlayerState.Idle;
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
            CastPreparedSpellOrMove(Direction.Up);
        }
        //Down
        if (Input.GetKeyDown(KeyCode.S))
        {
            CastPreparedSpellOrMove(Direction.Down);
        }
        //Right
        if (Input.GetKeyDown(KeyCode.D))
        {
            CastPreparedSpellOrMove(Direction.Right);
        }
        //Left
        if (Input.GetKeyDown(KeyCode.A))
        {
            CastPreparedSpellOrMove(Direction.Left);
        }
    }

    public void CastPreparedSpellOrMove(Direction direction)
    {
        if (Player.PlayerState == PlayerState.PreparingSpell)
        {
            Player.PlayerCastPreparedSpell(direction);
        }
        else if (Player.PlayerState == PlayerState.Moving)
        {
            Player.bufferedMove = direction;
            StartCoroutine(Player.ClearBuffer());
        }
        else
        {
            StartCoroutine(Player.TryMove(direction, 1));
        }
    }
}
