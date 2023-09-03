using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SpawnLaserShooter : StateMachineBehaviour
{
    private LaserShooterEnemy m_LaserShooter;
    private Vector2 m_OGPos;
    private Rigidbody2D m_Rb;
    private SpawnSide m_SpawnSide;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_LaserShooter = animator.GetComponent<LaserShooterEnemy>();
        m_OGPos = m_LaserShooter.transform.position;
        m_Rb = animator.GetComponent<Rigidbody2D>();
        m_SpawnSide = m_LaserShooter.GetSpawnSide();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float speed = 5.0f;
        if((m_SpawnSide == SpawnSide.Left) || (m_SpawnSide == SpawnSide.Right))
        {
            speed *= 2.0f;
        }
        Vector2 newpos = Vector2.MoveTowards(m_Rb.position, m_LaserShooter.GetSpawnPosition(), speed * Time.fixedDeltaTime);
        m_Rb.MovePosition(newpos);

        if(Vector2.Distance(m_Rb.position, m_LaserShooter.GetSpawnPosition()) < 1e-5f)
        {
            //Switch to Idle stance
            animator.SetTrigger("Idle");
            m_Rb.velocity = new Vector2(0.0f, 0.0f);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Idle");
    }
}
