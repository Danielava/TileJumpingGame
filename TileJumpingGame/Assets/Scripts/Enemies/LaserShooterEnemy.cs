using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public enum SpawnSide //TODO Move to GameVariables perhaps
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
    private int2 m_TilePos;

    public Laser m_Laser;

    public void Init(SpawnSide side, Vector3 spawnPos, int2 tilePos)
    {
        m_Animator = gameObject.GetComponent<Animator>();
        m_SpawnedSide = side;
        m_SpawnPosition = spawnPos;
        m_Rb = gameObject.GetComponent<Rigidbody2D>();
        m_TilePos = tilePos;

        //Assign a random force
        Vector2 force;
        float forceIntensity = UnityEngine.Random.Range(-10.0f, 10.0f);
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

    public void SetTilePosition(int2 tilePos)
    {
        m_TilePos = tilePos;
    }

    public int2 GetTilePosition()
    {
        return m_TilePos;
    }

    public void ShootLaser(float laserLifetime)
    {
        //float laserLifetime = 0.5f;
        bool enemyLaser = true;
        Instantiate(m_Laser, transform.position, Quaternion.identity).Init(transform.position, m_TilePos, m_SpawnedSide, laserLifetime, enemyLaser);
    }

    //Returns the laser gameObject that was shot, this is to track its status for AI, so they know when it's been destroyed
    public Laser ShootLaserReturn(float laserLifetime)
    {
        //float laserLifetime = 0.5f;
        Laser laser = Instantiate(m_Laser, transform.position, Quaternion.identity);
        bool enemyLaser = true;
        laser.Init(transform.position, m_TilePos, m_SpawnedSide, laserLifetime, enemyLaser);
        return laser;
    }
}
