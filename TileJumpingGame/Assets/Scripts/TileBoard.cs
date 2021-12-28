using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBoard : MonoBehaviour
{
    //Adding material (art) to our board of tiles
    [Header("Art stuff")]
    [SerializeField] private Material m_TileMaterial;

    public float m_TileSize = 1.0f;
    private const int TILE_COUNT_X = 8;
    private const int TILE_COUNT_Y = 4;

    private GameObject[,] m_Tiles; //[,] means it's 2D //[,,] would mean 3D
    private Vector3[,] m_TilePositions;

    private void Awake()
    {
        GenerateAllTiles(m_TileSize, TILE_COUNT_X, TILE_COUNT_Y);
    }

    private void GenerateAllTiles(float tilesize, int tileCountX, int tileCountY)
    {
        //Compute each tiles positions and put them in the m_TilePositions list.
        m_TilePositions = new Vector3[tileCountX, tileCountY];

        m_Tiles = new GameObject[tileCountX, tileCountY];
        for (int x = 0; x < tileCountX; x++)
        {
            for (int y = 0; y < tileCountY; y++)
            {
                m_Tiles[x, y] = GenerateSingleTile(tilesize, x, y);
                m_TilePositions[x, y] = new Vector3(x * m_TileSize, 0.0f, y * m_TileSize) + new Vector3(m_TileSize / 2.0f, 0, m_TileSize / 2.0f);
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
        vertices[0] = new Vector3(x * tilesize, 0, y * tilesize);
        vertices[1] = new Vector3(x * tilesize, 0, (y+1) * tilesize);
        vertices[2] = new Vector3((x+1) * tilesize, 0, y * tilesize);
        vertices[3] = new Vector3((x+1) * tilesize, 0, (y+1) * tilesize);

        int[] triangles = new int[] { 0, 1, 2, 1, 3, 2 }; //The two triangles of the quad are defines by these vertices

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        tileObject.layer = LayerMask.NameToLayer("Tile"); //Adds each tile to the layer category named Tile (you have to create this layer manually in editor).
        tileObject.AddComponent<BoxCollider>();

        return tileObject;
    }

    //Positioning

    public GameObject[,] GetTileList()
    {
        return m_Tiles;
    }

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

    //Input the x,y index of the tile and it should return the tiles position.
    public Vector3 GetTilePosition(int x, int y)
    {
        //The second addition is to center player in the tile.
        return m_TilePositions[x, y];
    }
}
