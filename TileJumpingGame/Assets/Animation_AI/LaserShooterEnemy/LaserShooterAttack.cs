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

    public float attackPreparationTimer = 1.0f;
    private float attackPreparationTimerOG;
    public float laserLifeTime; //values betwen 0.5 - 1.0 are fast and cool, some enemies might want higher than that.

    private bool m_LaserShot;

    private Laser m_Laser;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_LaserShooter = animator.GetComponent<LaserShooterEnemy>();
        m_OGPos = m_LaserShooter.transform.position;
        m_Rb = animator.GetComponent<Rigidbody2D>();
        m_SpawnSide = m_LaserShooter.GetSpawnSide();
        m_LaserShot = false;
        m_Laser = null;
        attackPreparationTimerOG = attackPreparationTimer;
        //laserLifeTime = 0.5f;

        //m_LaserShooter.ShootLaser(laserLifeTime);
        //TODO: Play the preparation VFX here
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackPreparationTimer -= 1.0f * Time.deltaTime;
        if(!m_Laser && attackPreparationTimer <= 0 && !m_LaserShot)
        {
            //TODO: Turn off the preparation VFX here
            m_Laser = m_LaserShooter.ShootLaserReturn(laserLifeTime);
            m_LaserShot = true;
        }
        
        if(m_LaserShot && m_Laser == null)
        {
            //Laser animation has finished, so we can move to "Move" state again
            animator.SetBool("IsMoving", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackPreparationTimer = attackPreparationTimerOG;
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Attack");
    }
}
