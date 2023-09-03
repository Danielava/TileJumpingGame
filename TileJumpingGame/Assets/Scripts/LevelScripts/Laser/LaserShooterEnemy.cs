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
    private Rigidbody2D m_Rb;

    public void Init(SpawnSide side, Vector3 spawnPos)
    {
        m_Animator = gameObject.GetComponent<Animator>();
        m_SpawnedSide = side;
        m_SpawnPosition = spawnPos;
        m_Rb = gameObject.GetComponent<Rigidbody2D>();

        //Assign a random force
        Vector2 force;
        float forceIntensity = Random.Range(-10.0f, 10.0f);
        if ((m_SpawnedSide == SpawnSide.Left) || (m_SpawnedSide == SpawnSide.Right))
        {
            force = new Vector2(0.0f, forceIntensity);
        }
        else
        {
            force = new Vector2(forceIntensity, 0.0f);
        }

        m_Rb.AddForce(force, ForceMode2D.Impulse);
    }

    public Vector2 GetSpawnPosition()
    {
        return m_SpawnPosition;
    }

    public SpawnSide GetSpawnSide()
    {
        return m_SpawnedSide;
    }
}
