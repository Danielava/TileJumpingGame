using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderTile : Tile
{
    public GameObject m_BoulderSprite;
    public int m_Hp;

    private GameObject m_BoulderSpriteInstance;

    void Start()
    {
        PlaceBoulderOnSprite();
    }
    public void PlaceBoulderOnSprite()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, -0.01f);
        m_BoulderSpriteInstance = Instantiate(m_BoulderSprite, pos, Quaternion.identity);
        m_BoulderSpriteInstance.GetComponent<Boulder>().Init(this);
    }

    public void TakeDamage(int damage)
    {
        m_Hp -= damage;
        if (m_Hp <= 0)
        {
            
            //We set TileType to default right away here to allow player to immidiately walk through the boulder once it's been destroyed by something:
            tileType = Assets.Scripts.Board.TileType.Normal;

            //TODO: Play a boulder destroy animation here and then destroy it fully after the animation ends or something.

            StartCoroutine(DestroyBoulderTile());
        }
    }

    IEnumerator DestroyBoulderTile()
    {
        yield return new WaitForSeconds(0.5f);
        //TODO: Transform this tile into a regular tile and then destroy it along with the boulder.
        int currTileIndex = xPos + GridTileBoard.instance.TILE_COUNT_X * yPos; //This formula is taken from GridTileBoard.cs -> GetTile()
        Tile newTile = Instantiate(GridTileBoard.instance.DefaultTilePrefab, transform.position, Quaternion.identity).GetComponent<Tile>();
        newTile.Init(xPos, yPos, transform.position);
        GridTileBoard.instance.tiles[currTileIndex] = newTile;

        Destroy(m_BoulderSpriteInstance);
        Destroy(gameObject);
    }
}
