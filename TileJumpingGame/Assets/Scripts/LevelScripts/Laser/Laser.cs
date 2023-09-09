using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer m_LineRenderer;

    private Vector3 m_FirePosition;
    private Vector3 m_LaserEndPos;

    private Vector3 m_FirePosDirection;

    private bool shutDownLaser;
    private bool shootingLaser;
    private float laserLifetime;

    private float laserExtendSpeed = 80.0f;
    // Start is called before the first frame update
    void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
        shutDownLaser = false;
        shootingLaser = true;
    }

    public void Init(Vector2 firePos, int2 currentTilePos, SpawnSide direction, float lifetime)
    {
        laserLifetime = lifetime;
        shutDownLaser = false;
        shootingLaser = true;

        m_FirePosDirection = new Vector3(firePos.x, firePos.y, 0.0f);
        switch (direction)
        {
            case SpawnSide.Up:
                m_FirePosDirection.x = 0;
                m_FirePosDirection.y = -1;
                break;
            case SpawnSide.Down:
                m_FirePosDirection.x = 0;
                m_FirePosDirection.y = 1;
                break;
            case SpawnSide.Left:
                m_FirePosDirection.x = 1;
                m_FirePosDirection.y = 0;
                break;
            default: //SpawnSide.Right:
                m_FirePosDirection.x = -1;
                m_FirePosDirection.y = 0;
                break;
        }
        //m_FirePosDirection.Normalize();

        float zOffset = -0.3f;

        m_FirePosition = new Vector3(firePos.x, firePos.y, zOffset);
        m_LineRenderer = GetComponent<LineRenderer>();
        m_LineRenderer.SetPosition(0, m_FirePosition); //The first pos vector

        //Do a rayCast to determine second pos
        //Actually no, we don't need ray cast, we can just search manually

        //Find all objects 
        Tile tempTile = new Tile();
        tempTile.xPos = currentTilePos.x;
        tempTile.yPos = currentTilePos.y;
        Tile closestObstacleTile = GridTileBoard.instance.GetClosestObstacleOnPath((Direction) direction, tempTile);
        Vector3 laserEndPos;
        if (closestObstacleTile != null)
        {
            Vector2 closest = closestObstacleTile.transform.position;
            laserEndPos = new Vector3(0.0f, closest.x, closest.y); //TODO: This is wrong!
        }
        else
        {
            laserEndPos = m_FirePosition + Vector3.Scale(new Vector3(20.0f, 20.0f, 20.0f), m_FirePosDirection); //Vector3.Scale is vector multiplication
        }
        m_LaserEndPos = laserEndPos;
        m_LineRenderer.SetPosition(1, m_FirePosition);
    }

    // Update is called once per frame
    void Update()
    {

        if(shootingLaser)
        {
            Vector3 currLaserEndPos = m_LineRenderer.GetPosition(1);
            //Extend the laser until it reaches the m_LaserEndPos
            if (Vector2.Distance(currLaserEndPos, m_LaserEndPos) > 1e-2)
            {
                currLaserEndPos = currLaserEndPos + Vector3.Scale(new Vector3(1.0f, 1.0f, 1.0f), m_FirePosDirection) * laserExtendSpeed * Time.deltaTime;
                m_LineRenderer.SetPosition(1, currLaserEndPos);
            }
        }
        else
        {
            float LineWidth = m_LineRenderer.widthMultiplier;
            m_LineRenderer.widthMultiplier = LineWidth - 0.01f;
        }

        if(m_LineRenderer.widthMultiplier <= 0.0f)
        {
            Destroy(gameObject);
        }

        laserLifetime -= 1.0f * Time.deltaTime;
        if(laserLifetime <= 0.0f)
        {
            shootingLaser = false;
        }
    }

    void ShootLaser()
    {
        shootingLaser = true; //Might not be needed? We just automatically shoot laser on init!
    }

    void DisableLaser()
    {
        shutDownLaser = true; //Might not be needed
        shootingLaser = false;
    }
}
