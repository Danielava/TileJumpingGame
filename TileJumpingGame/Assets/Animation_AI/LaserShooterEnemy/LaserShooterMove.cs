using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LaserShooterMove : StateMachineBehaviour
{
    private LaserShooterEnemy m_LaserShooter;
    private Vector2 m_OGPos;
    private Rigidbody2D m_Rb;
    private SpawnSide m_SpawnSide;

    private Vector2 m_NewPos;
    private int2 m_TilePos;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_LaserShooter = animator.GetComponent<LaserShooterEnemy>();
        m_OGPos = m_LaserShooter.transform.position;
        m_Rb = animator.GetComponent<Rigidbody2D>();
        m_SpawnSide = m_LaserShooter.GetSpawnSide();

        GridTileBoard board = GridTileBoard.instance;
        //TODO: Pick a pos and speed
        int randomIndex = 0;
        if (m_SpawnSide == SpawnSide.Left)
        {
            randomIndex = UnityEngine.Random.Range(0, board.TILE_COUNT_X);
            m_TilePos = new int2(-1, randomIndex);
            m_NewPos = board.GetVirtualTilePosition(m_TilePos.x, m_TilePos.y);
        }
        else if(m_SpawnSide == SpawnSide.Right)
        {
            randomIndex = UnityEngine.Random.Range(0, board.TILE_COUNT_X);
            m_TilePos = new int2(board.TILE_COUNT_X, randomIndex);
            m_NewPos = board.GetVirtualTilePosition(m_TilePos.x, m_TilePos.y);
        }
        else if (m_SpawnSide == SpawnSide.Up)
        {
            randomIndex = UnityEngine.Random.Range(0, board.TILE_COUNT_Y);
            m_TilePos = new int2(randomIndex, board.TILE_COUNT_Y);
            m_NewPos = board.GetVirtualTilePosition(m_TilePos.x, m_TilePos.y);
        }
        else //Down
        {
            randomIndex = UnityEngine.Random.Range(0, board.TILE_COUNT_Y);
            m_TilePos = new int2(randomIndex, -1);
            m_NewPos = board.GetVirtualTilePosition(m_TilePos.x, m_TilePos.y);
        }

        m_LaserShooter.SetTilePosition(m_TilePos);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Walk towards that position and prepare to shoot! (or 20% chance of moving again to a new pos)
        float speed = 3.0f;
        Vector2 newpos = Vector2.MoveTowards(m_Rb.position, m_NewPos, speed * Time.fixedDeltaTime);
        m_Rb.MovePosition(newpos);

        if (Vector2.Distance(m_Rb.position, m_NewPos) < 1e-5f)
        {
            //Switch to Idle stance
            //TODO: 50% chance to stay Idle or Attack
            animator.SetTrigger("Attack");
            //animator.SetTrigger("Idle");
            m_Rb.velocity = new Vector2(0.0f, 0.0f);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsMoving", false);
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Attack");
    }
}
