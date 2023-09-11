using System.Collections;
using System.Collections.Generic;
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
    private float m_LaserLifetime;

    private float laserExtendSpeed = 80.0f; //80.0f is optimal, set lower to debug laser

    private bool m_IsThisEnemyLaser;

    private GameObject m_Collider;
    private SpawnSide m_LaserDirection;
    // Start is called before the first frame update
    void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
        shutDownLaser = false;
        shootingLaser = true;
        m_IsThisEnemyLaser = true;
    }

    public void Init(Vector2 firePos, int2 currentTilePos, SpawnSide direction, float lifetime, bool enemyLaser)
    {
        //m_LaserLifetime = 100.0f; //OBS use for debug purposes
        m_LaserLifetime = lifetime;
        shutDownLaser = false;
        shootingLaser = true;
        m_IsThisEnemyLaser = enemyLaser;
        m_LaserDirection = direction;

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
        /*
         * Damn it: That GetClosestObstacleOnPath needs to take in a direction. And the direction is actually
         * supposed to be oposite to where the bird is facing..
         * So if you ever refactor and remove SpawnDir enum, then keep this in mind. It will ruin things right here.
         */
        Tile closestObstacleTile = GridTileBoard.instance.GetClosestObstacleOnPath((Direction) direction, tempTile);
        Vector3 laserEndPos;
        if (closestObstacleTile != null)
        {
            Vector2 closest = closestObstacleTile.transform.position;
            laserEndPos = new Vector3(closest.x, closest.y, 0.0f);
            //TODO: This will make the laser stop at the middle of the collided tile, we might want to offset a little bit so it's a bit before the middle.
        }
        else
        {
            laserEndPos = m_FirePosition + Vector3.Scale(new Vector3(20.0f, 20.0f, 20.0f), m_FirePosDirection); //Vector3.Scale is vector multiplication
        }

        /*
         *  Daniel: Massive TODO!
         *  Store the closest gameObject. Because sometimes a laser should be able to destroy it,
         *  and then move along on the road!
         */

        m_LaserEndPos = laserEndPos;
        m_LineRenderer.SetPosition(1, m_FirePosition);

        CreateColliderForLaser();
    }

    //To get collision working on the laser, we need to create and attach a mesh object to it and a BoxCollider2D to that meshObject!
    private void CreateColliderForLaser()
    {
        m_Collider = new GameObject();
        m_Collider.transform.parent = transform;
        m_Collider.transform.localPosition = new Vector3(0, 0, 0);

        Mesh mesh = new Mesh();
        m_Collider.AddComponent<MeshFilter>().mesh = mesh;
        BoxCollider2D boxCollider = m_Collider.AddComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
        
        LaserCollision laserColScript = m_Collider.AddComponent<LaserCollision>();
        laserColScript.SetIsEnemyLaser(m_IsThisEnemyLaser);

        m_Collider.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        //This offsets the collision box perfectly
        /*
         * Bugfix: We multiply localscale by the 0.5f here to make it's collision width/thickness smaller.
         * Otherwise it might cover more that one row (e.g hit rocks on multiple rows/cols)
         */
        if (m_LaserDirection == SpawnSide.Left || m_LaserDirection == SpawnSide.Right)
        {
            m_Collider.transform.localScale -= new Vector3(1.0f, 0.0f, 0.0f);
            m_Collider.transform.localScale = Vector3.Scale(m_Collider.transform.localScale, new Vector3(1.0f, 0.5f, 1.0f));
        }
        else
        {
            m_Collider.transform.localScale -= new Vector3(0.0f, 1.0f, 0.0f);
            m_Collider.transform.localScale = Vector3.Scale(m_Collider.transform.localScale, new Vector3(0.5f, 1.0f, 1.0f));
        }
    }

    public void ScaleCollider(Vector3 scaleDirection) //ScaleDirection should be a vector that only has value in the direction of scaling
    {
        Vector3 absVector = new Vector3(Mathf.Abs(scaleDirection.x), Mathf.Abs(scaleDirection.y), Mathf.Abs(scaleDirection.z));
        m_Collider.transform.localScale += absVector;
        m_Collider.transform.localPosition += scaleDirection / 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(shootingLaser)
        {
            Vector3 currLaserEndPos = m_LineRenderer.GetPosition(1);
            //Extend the laser until it reaches the m_LaserEndPos
            if (Vector2.Distance(currLaserEndPos, m_LaserEndPos) > 0.4f) //WARNING: This is pretty dangerous because we're using incrementation to offset the laserBeam!
            {
                Vector3 scaleDirection = Vector3.Scale(new Vector3(1.0f, 1.0f, 1.0f), m_FirePosDirection) * laserExtendSpeed * Time.deltaTime;
                currLaserEndPos = currLaserEndPos + scaleDirection;
                m_LineRenderer.SetPosition(1, currLaserEndPos);

                ScaleCollider(scaleDirection);
            }
        }
        else
        {
            //Laser is done
            float LineWidth = m_LineRenderer.widthMultiplier;
            m_LineRenderer.widthMultiplier = LineWidth - 0.01f;

            //Destroy the collider
            if(m_Collider)
            {
                Destroy(m_Collider);
            }
        }

        if(m_LineRenderer.widthMultiplier <= 0.0f)
        {
            Destroy(gameObject);
        }

        m_LaserLifetime -= 1.0f * Time.deltaTime;
        if(m_LaserLifetime <= 0.0f)
        {
            shootingLaser = false;
        }
    }

    void ShootLaser()
    {
        shootingLaser = true; //Might not be needed? We just automatically shoot laser on init!
    }

    public void DisableLaser()
    {
        shutDownLaser = true; //Might not be needed
        shootingLaser = false;
    }
}
