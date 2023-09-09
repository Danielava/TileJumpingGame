using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LaserShooterAttack : StateMachineBehaviour
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

        m_LaserShooter.ShootLaser();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
