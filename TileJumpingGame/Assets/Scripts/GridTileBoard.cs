using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridTileBoard : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject tilemapGO;
    public GameObject firstTile;
    public List<Tile> tiles;

    public static GridTileBoard instance; //singleton

    private int rayStep = 1;

    public float tileSize = 1; //TODO: Doesn't do anything?
    public int TILE_COUNT_X = 0;
    public int TILE_COUNT_Y = 0;

    public GameObject DefaultTilePrefab;
    public GameObject EmptyTilePrefab;
    public GameObject IceTilePrefab;
    public GameObject BoulderTilePrefab;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        firstTile = GameObject.FindGameObjectsWithTag("Tile").ToList().OrderBy(tile => tile.transform.position.x + tile.transform.position.y).First();


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

    public Vector2 GetTilePosition(int x, int y)
    {
        return tiles[x * y].transform.position;
    }

    public Vector2 GetVirtualTilePosition(int x, int y)
    {
        Vector2 resPos;
        resPos.x = tileSize * x;
        resPos.y = tileSize * y;

        resPos += new Vector2(tileSize / 2.0f, tileSize / 2.0f); //Pos need to be offseted a bit with the new Grid system

        return resPos;
    }

    public List<Tile> GetRowOrColOfTiles(Direction dir, Tile tilePos, bool sortList)
    {
        List<Tile> res;
        switch (dir)
        {
            case Direction.Up:
                res = tiles.Where(t => (t.xPos == tilePos.xPos) && (t.yPos > tilePos.yPos)).ToList();
                res.Sort((t1, t2) => t1.yPos.CompareTo(t2.yPos));
                break;
            case Direction.Down:
                res = tiles.Where(t => (t.xPos == tilePos.xPos) && (t.yPos < tilePos.yPos)).ToList();
                res.Sort((t1, t2) => t2.yPos.CompareTo(t1.yPos));
                break;
            case Direction.Left:
                res = tiles.Where(t => (t.yPos == tilePos.yPos) && (t.xPos < tilePos.xPos)).ToList();
                res.Sort((t1, t2) => t2.xPos.CompareTo(t1.xPos));
                break;
            default: //Direction.Right:
                res = tiles.Where(t => (t.yPos == tilePos.yPos) && (t.xPos > tilePos.xPos)).ToList();
                res.Sort((t1, t2) => t1.xPos.CompareTo(t2.xPos));
                break;
        }
        return res;
    }

    public Tile GetClosestObstacleOnPath(Direction dir, Tile tilePos)
    {
        List<Tile> tilesOnPath = GetRowOrColOfTiles(dir, tilePos, true); //Should now be sorted from closest to farthest
        List<Tile> obstaclesOnPath = tilesOnPath.Where(t=> (t.tileType == Assets.Scripts.Board.TileType.Boulder)).ToList(); //TODO: Also if there is a wall there

        if (obstaclesOnPath.Count > 0)
        {
            Tile closestObstacleTile = obstaclesOnPath[0];
            return closestObstacleTile;
        }

        return null;
    }

    public void FreezeMiddleTiles()
    {

    }
}