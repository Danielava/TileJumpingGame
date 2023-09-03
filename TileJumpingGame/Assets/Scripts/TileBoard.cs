using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileBoard : MonoBehaviour
{
    //Adding material (art) to our board of tiles
    [Header("Art stuff")]
    [SerializeField] private Material m_TileMaterial;

    public float m_TileSize = 1.0f;
    public int TILE_COUNT_X = 8;
    public int TILE_COUNT_Y = 5;

    public GameObject TilePrefab;
    public GameObject EmptyTilePrefab;
    public GameObject IceTilePrefab;
    public Tile[] tiles { get; private set; }

    //TODO: An idea to generate a separate list of invisible tiles that are used for flying enemies to spawn on
    //public Tile[] laserShooterTiles { get; private set; }

    public static TileBoard instance; //singleton

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        GenerateAllTiles();
    }

    private void GenerateAllTiles()
    {
        tiles = new Tile[TILE_COUNT_X * TILE_COUNT_Y];


        for (int x = 0; x < TILE_COUNT_X; x++)
        {
            for (int y = 0; y < TILE_COUNT_Y; y++)
            {
                //m_Tiles[x, y] = GenerateSingleTile(tilesize, x, y);
                //m_TilePositions[x, y] = new Vector3(x * m_TileSize, 0.0f, y * m_TileSize) + new Vector3(m_TileSize / 2.0f, 0, m_TileSize / 2.0f);

                var r = Random.Range(0, 1f);
                if (r > 0.9f)
                {
                    var tile = Instantiate(IceTilePrefab, transform).GetComponent<Tile>();

                    tile.Init(x + (int)m_TileSize / 2, y + (int)m_TileSize / 2);

                    tiles[x * TILE_COUNT_Y + y] = tile;
                }
                else if (r > 0.8f)
                {

                    var tile = Instantiate(EmptyTilePrefab, transform).GetComponent<Tile>();

                    tile.Init(x + (int)m_TileSize / 2, y + (int)m_TileSize / 2);

                    tiles[x * TILE_COUNT_Y + y] = tile;
                }
                else
                {
                    var tile = Instantiate(TilePrefab, transform).GetComponent<Tile>();

                    tile.Init(x + (int)m_TileSize / 2, y + (int)m_TileSize / 2);

                    tiles[x * TILE_COUNT_Y + y] = tile;
                }
            }
        }
    }

    private GameObject GenerateSingleTile(float tilesize, int x, int y)
    {
        GameObject tileObject = new GameObject(string.Format("X:{0}, Y:{1}", x, y));
        tileObject.transform.parent = transform; //Puts the tiles under their parent board.

        //We generate meshes for each tile
        Mesh mesh = new Mesh();
        tileObject.AddComponent<MeshFilter>().mesh = mesh;
        tileObject.AddComponent<MeshRenderer>().material = m_TileMaterial;

        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(x * tilesize, y * tilesize, 0);
        vertices[1] = new Vector3(x * tilesize, (y + 1) * tilesize, 0);
        vertices[2] = new Vector3((x + 1) * tilesize, y * tilesize, 0);
        vertices[3] = new Vector3((x + 1) * tilesize, (y + 1) * tilesize, 0);

        int[] triangles = new int[] { 0, 1, 2, 1, 3, 2 }; //The two triangles of the quad are defines by these vertices

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        tileObject.layer = LayerMask.NameToLayer("Tile"); //Adds each tile to the layer category named Tile (you have to create this layer manually in editor).
        tileObject.AddComponent<BoxCollider>();

        return tileObject;
    }

    //Positioning

    public float GetTileSize()
    {
        return m_TileSize;
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
        return tiles[x * TILE_COUNT_Y + y];
    }

    public bool CanMoveTo(int x, int y)
    {
        var r = x >= TILE_COUNT_X || x < 0 || y >= TILE_COUNT_Y || y < 0 || !tiles[x * TILE_COUNT_Y + y].canWalkOn;
        return !r;
    }

    public Tile GetRandomTile()
    {
        var x = Random.Range(0, TILE_COUNT_X);
        var y = Random.Range(0, TILE_COUNT_Y);

        return tiles[x * TILE_COUNT_Y + y];
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
}