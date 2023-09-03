using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnSide
{
    Up,
    Down,
    Left,
    Right
};

public class LaserShooterEnemy : EnemyRoot
{
    private Animator m_Animator;
    private SpawnSide m_SpawnedSide;
    private Vector3 m_SpawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        //m_Animator = gameObject.GetComponent<Animator>();
        //m_Animator.GetComponent<SpawnLaserShooter>().InitEnemy(this);
    }

    public void Init(SpawnSide side, Vector3 spawnPos)
    {
        m_Animator = gameObject.GetComponent<Animator>();
        //m_Animator.GetComponent<SpawnLaserShooter>().InitEnemy(this);
        m_SpawnedSide = side;
        m_SpawnPosition = spawnPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
