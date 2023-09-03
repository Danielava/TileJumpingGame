using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class GridTileBoard : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject tilemapGO;
    public GameObject firstTile;
    public List<Tile> tiles;

    private int rayStep = 1;

    public float tileSize = 1;
    public int TILE_COUNT_X = 0;
    public int TILE_COUNT_Y = 0;

    void Awake()
    {
        var startXpos = firstTile.transform.position.x;

        var rayOrigin = firstTile.transform.position + new Vector3(0, 0, -1);

        var currentX = 0;
        var currentY = 0;

        // Bitshift layer 8 "Tile" to ignore other layers
        var layerMask = 1 << 8;

        while (true)
        {
            var hitObject = Physics2D.Raycast(rayOrigin, new Vector3(0, 0, 10), 10, layerMask);

            if (hitObject)
            {
                tiles.Add(hitObject.transform.gameObject.GetComponent<Tile>());
                rayOrigin += new Vector3(rayStep, 0, 0);
                hitObject.transform.gameObject.GetComponent<Tile>().xPos = currentX;
                hitObject.transform.gameObject.GetComponent<Tile>().yPos = currentY;
                currentX += 1;
                continue;
            }

            var newLineHitObject = Physics2D.Raycast(new Vector3(startXpos, rayOrigin.y + rayStep, rayOrigin.z), new Vector3(0, 0, 10), 10, layerMask);

            if (newLineHitObject)
            {
                rayOrigin = new Vector3(startXpos, rayOrigin.y + rayStep, rayOrigin.z);
                currentY += 1;
                currentX = 0;
                continue;
            }

            TILE_COUNT_X = currentX;
            TILE_COUNT_Y = currentY + 1;

            break;
        }
    }

    public float GetTileSize()
    {
        return tileSize;
    }

    public int GetSizeX()
    {
        return TILE_COUNT_X;
    }

    public int GetSizeY()
    {
        return TILE_COUNT_Y;
    }

    public Tile GetTile(int x, int y)
    {


        return tiles[x + TILE_COUNT_X * y];
    }

    public bool CanMoveTo(int x, int y)
    {
        /*
        print("xy: " + x + ", " + y);
        print("tilepos: " + tiles[x * TILE_COUNT_Y + y].xPos + ", " + tiles[x * TILE_COUNT_Y + y].yPos);*/
        var r = x >= TILE_COUNT_X || x < 0 || y >= TILE_COUNT_Y || y < 0 || !tiles[x + TILE_COUNT_X * y].canWalkOn;
        return !r;
    }

    public Tile GetRandomTile()
    {
        var x = Random.Range(0, TILE_COUNT_X);
        var y = Random.Range(0, TILE_COUNT_Y);

        return tiles[x + TILE_COUNT_X * y];
    }

    public Tile GetValidSpawnPoint()
    {
        var rnd = new System.Random();
        return tiles.Where(t => t.canWalkOn).OrderBy(x => rnd.Next()).First();
    }
}