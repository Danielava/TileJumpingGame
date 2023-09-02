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

    public void Init(Tile playerTile)
    {
        m_Exploded = false;
        m_CurrentTile = playerTile;
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
        TileBoard gameBoard = TileBoard.instance;
        //Distance between tiles = gameBoard.m_TileSize;
        List<Tile> tilesHorizontalUp; //The tiles where the explosion happens
        List<Tile> tilesHorizontalDown;
        List<Tile> tilesVerticalLeft;
        List<Tile> tilesVerticalRight;
        //Filling up our explosion lists
        tilesVerticalRight = gameBoard.tiles.Where(t => (t.yPos == m_CurrentTile.yPos) && (t.xPos > m_CurrentTile.xPos)/* && t.canWalkOn*/).ToList(); //This will give us an explosion on the x row
        tilesVerticalLeft = gameBoard.tiles.Where(t => (t.yPos == m_CurrentTile.yPos) && (t.xPos < m_CurrentTile.xPos)).ToList();
        tilesHorizontalDown = gameBoard.tiles.Where(t => (t.xPos == m_CurrentTile.xPos) && (t.yPos < m_CurrentTile.yPos)).ToList();
        tilesHorizontalUp = gameBoard.tiles.Where(t => (t.xPos == m_CurrentTile.xPos) && (t.yPos > m_CurrentTile.yPos)).ToList();

        //Sort the lists
        tilesVerticalRight.Sort((t1, t2) => t1.xPos.CompareTo(t2.xPos));
        tilesVerticalLeft.Sort((t1, t2) => t2.xPos.CompareTo(t1.xPos));
        tilesHorizontalDown.Sort((t1, t2) => t2.yPos.CompareTo(t1.yPos));
        tilesHorizontalUp.Sort((t1, t2) => t1.yPos.CompareTo(t2.yPos));


        int maxRowSize = Mathf.Max(tilesHorizontalUp.Count, tilesHorizontalDown.Count);
        int maxColSize = Mathf.Max(tilesVerticalLeft.Count, tilesVerticalRight.Count);
        int maxListSize = Mathf.Max(maxColSize, maxRowSize);

        //We add the +1 because of the explosion sprite we create where the bomb is placed
        m_NrOfExplosionSprites = 1 + Mathf.Min(power, tilesHorizontalUp.Count) + Mathf.Min(power, tilesHorizontalDown.Count) + Mathf.Min(power, tilesVerticalLeft.Count) + Mathf.Min(power, tilesVerticalRight.Count);
        m_NrOfExplosionSpritesCounter = 0;

        StartCoroutine(InitExplosionSprite(m_CurrentTile, lifetime, 0));

        for (int i = 0; i < maxListSize; i++)
        {
            if(i > power)
            {
                break;
            }
            if(i < tilesHorizontalUp.Count)
            {
                StartCoroutine(InitExplosionSprite(tilesHorizontalUp[i], lifetime, i));
            }
            if (i < tilesHorizontalDown.Count)
            {
                StartCoroutine(InitExplosionSprite(tilesHorizontalDown[i], lifetime, i));
            }
            if (i < tilesVerticalLeft.Count)
            {
                StartCoroutine(InitExplosionSprite(tilesVerticalLeft[i], lifetime, i));
            }
            if (i < tilesVerticalRight.Count)
            {
                StartCoroutine(InitExplosionSprite(tilesVerticalRight[i], lifetime, i));
            }
        }
    }

    IEnumerator InitExplosionSprite(Tile t, float lifetime, int i)
    {
        yield return new WaitForSeconds(0.05f * i);
        Instantiate(m_Explosion, t.transform.position + new Vector3(0, 0, -0.05f), Quaternion.identity * Quaternion.Euler(0, 0, -90)).Init(lifetime, this);
        
        m_NrOfExplosionSpritesCounter++;
        if(m_NrOfExplosionSpritesCounter >= m_NrOfExplosionSprites)
        {
            Destroy(gameObject); //All Explosion sprites have been initialized so you can safely destroy this object
        }
    }
}
