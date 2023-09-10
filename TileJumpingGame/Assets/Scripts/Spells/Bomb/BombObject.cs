using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BombObject : MonoBehaviour
{
    private Tile m_CurrentTile; //The tile this bomb is placed on.
    private float m_LifeTime = 3.0f;

    public BombExplosion m_Explosion;

    private bool m_Exploded;
    private int m_NrOfExplosionSprites; //We use this to track how many explosion sprites have been queued for trigger, and once all of them have been, we can safely destroy this gameObject.
    private int m_NrOfExplosionSpritesCounter;

    private bool[] m_PathBlocked = new bool[4];

    public void Init(Tile playerTile)
    {
        m_Exploded = false;
        m_CurrentTile = playerTile;

        m_PathBlocked[0] = false;
        m_PathBlocked[1] = false;
        m_PathBlocked[2] = false;
        m_PathBlocked[3] = false;

        StartCoroutine(RunBombTimer());
    }

    IEnumerator RunBombTimer()
    {
        yield return new WaitForSeconds(m_LifeTime);
        if (!m_Exploded)
        {
            Explode();
        }
    }

    public void Explode()
    {
        m_Exploded = true;
        //TODO: Trigger the explosion animation
        float explosion_lifetime = 1.0f;
        int explosion_power = 10;
        Destroy(gameObject.GetComponent<BoxCollider2D>());
        Destroy(gameObject.GetComponent<SpriteRenderer>());
        TriggerExplosion(explosion_lifetime, explosion_power);
    }

    private void TriggerExplosion(float lifetime, int power)
    {
        GridTileBoard gameBoard = GridTileBoard.instance;
        //Distance between tiles = gameBoard.m_TileSize;
        List<Tile> tilesHorizontalUp; //The tiles where the explosion happens
        List<Tile> tilesHorizontalDown;
        List<Tile> tilesVerticalLeft;
        List<Tile> tilesVerticalRight;
        //Filling up our explosion lists

        bool sortList = true;
        tilesHorizontalUp = gameBoard.GetRowOrColOfTiles(Direction.Up, m_CurrentTile, sortList);
        tilesHorizontalDown = gameBoard.GetRowOrColOfTiles(Direction.Down, m_CurrentTile, sortList);
        tilesVerticalLeft = gameBoard.GetRowOrColOfTiles(Direction.Left, m_CurrentTile, sortList);
        tilesVerticalRight = gameBoard.GetRowOrColOfTiles(Direction.Right, m_CurrentTile, sortList);

        //Cut the bomb off at closest obstacle on path
        //Tile cutoffUp = GridTileBoard.instance.GetClosestObstacleOnPath(Direction.Up, tempTile);
        //Tile cutoffDown = GridTileBoard.instance.GetClosestObstacleOnPath(Direction.Down, tempTile);
        //Tile cutoffLeft = GridTileBoard.instance.GetClosestObstacleOnPath(Direction.Left, tempTile);
        //Tile cutoffDown = GridTileBoard.instance.GetClosestObstacleOnPath(Direction.Right, tempTile);


        int maxRowSize = Mathf.Max(tilesHorizontalUp.Count, tilesHorizontalDown.Count);
        int maxColSize = Mathf.Max(tilesVerticalLeft.Count, tilesVerticalRight.Count);
        int maxListSize = Mathf.Max(maxColSize, maxRowSize);

        //We add the +1 because of the explosion sprite we create where the bomb is placed
        m_NrOfExplosionSprites = 1 + Mathf.Min(power, tilesHorizontalUp.Count) + Mathf.Min(power, tilesHorizontalDown.Count) + Mathf.Min(power, tilesVerticalLeft.Count) + Mathf.Min(power, tilesVerticalRight.Count);
        m_NrOfExplosionSpritesCounter = 0;

        StartCoroutine(InitExplosionSprite(m_CurrentTile, lifetime, 0, Direction.NONE)); //The explosion at the tile where the bomb is positioned

        for (int i = 0; i < maxListSize; i++)
        {
            if(i > power)
            {
                break;
            }
            if(i < tilesHorizontalUp.Count)
            {
                if (!m_PathBlocked[(int)Direction.Up])
                {
                    StartCoroutine(InitExplosionSprite(tilesHorizontalUp[i], lifetime, i, Direction.Up));
                }
            }
            if (i < tilesHorizontalDown.Count)
            {
                if (!m_PathBlocked[(int)Direction.Down])
                {
                    StartCoroutine(InitExplosionSprite(tilesHorizontalDown[i], lifetime, i, Direction.Down));
                }
            }
            if (i < tilesVerticalLeft.Count)
            {
                if (!m_PathBlocked[(int)Direction.Left])
                {
                    StartCoroutine(InitExplosionSprite(tilesVerticalLeft[i], lifetime, i, Direction.Left));
                }
            }
            if (i < tilesVerticalRight.Count)
            {
                if (!m_PathBlocked[(int)Direction.Right])
                {
                    StartCoroutine(InitExplosionSprite(tilesVerticalRight[i], lifetime, i, Direction.Right));
                }
            }
        }
    }

    IEnumerator InitExplosionSprite(Tile t, float lifetime, int i, Direction direction)
    {
        if(t.tileType == Assets.Scripts.Board.TileType.Boulder)
        {
            m_PathBlocked[(int)direction] = true;
        }

        yield return new WaitForSeconds(0.05f * i);
        Instantiate(m_Explosion, t.transform.position + new Vector3(0, 0, -0.05f), Quaternion.identity * Quaternion.Euler(0, 0, -90)).Init(lifetime, this);
        
        m_NrOfExplosionSpritesCounter++;
        if(m_NrOfExplosionSpritesCounter >= m_NrOfExplosionSprites)
        {
            Destroy(gameObject); //All Explosion sprites have been initialized so you can safely destroy this object
        }
    }
}
