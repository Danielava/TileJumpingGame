using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class LaserLevelLogic : MonoBehaviour
{
    public LaserShooterEnemy m_LaserShooterEnemy;

    // Start is called before the first frame update
    void Start()
    {
        //Spawn two each at each side
        SpawnLaserShooter(2, SpawnSide.Left);
        SpawnLaserShooter(2, SpawnSide.Right);
        SpawnLaserShooter(2, SpawnSide.Up);
        SpawnLaserShooter(2, SpawnSide.Down);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnLaserShooter(int amount, SpawnSide side)
    {
        //Spawn a few LaserShooterEnemies at given positions
        GridTileBoard board = GridTileBoard.instance;
        float tileSize = board.tileSize;

        int boardSize = ((side == SpawnSide.Up) || (side == SpawnSide.Left)) ? board.TILE_COUNT_X : board.TILE_COUNT_Y;

        List<int> numbers = new List<int>();
        for(int i = 0; i < boardSize; i++)
        {
            numbers.Add(i);
        }
        for(int i = 0; i < amount; i++)
        {
            if(numbers.Count <= 0)
            {
                break;
            }
            int rndIndex = UnityEngine.Random.Range(0, numbers.Count);
            int indexToSpawnAt = numbers[rndIndex];
            numbers.RemoveAt(rndIndex);

            Camera camera = Camera.main;
            float halfHeight = camera.orthographicSize;
            float halfWidth = camera.aspect * halfHeight;

            //Spawn your enemy
            float boardHeight = board.TILE_COUNT_X * tileSize;
            float boardWidth = board.TILE_COUNT_Y * tileSize;

            Vector2 spawnPosition;
            Vector2 initPosition; //Init pos is a pos outisde the screen which our enemies will spawn, and then they will fly towards their given spawnPosition.
            int2 tilePos;
            float offset = 0.05f;
            if (side == SpawnSide.Left)
            {
                tilePos.x = -1; tilePos.y = indexToSpawnAt;
                spawnPosition = board.GetVirtualTilePosition(tilePos.x, tilePos.y);

                initPosition = Camera.main.ViewportToWorldPoint(new Vector2(0.0f - offset, 0.0f));// 0.0f in x pos represents left of viewport, we offset it a bit to end up slightly outisde of it!
                initPosition.y = spawnPosition.y;
            }
            else if (side == SpawnSide.Right)
            {
                tilePos.x = board.TILE_COUNT_X; tilePos.y = indexToSpawnAt;
                spawnPosition = board.GetVirtualTilePosition(tilePos.x, tilePos.y);

                initPosition = Camera.main.ViewportToWorldPoint(new Vector2(1.0f + offset, 0.0f));
                initPosition.y = spawnPosition.y;
            }
            else if (side == SpawnSide.Down)
            {
                tilePos.x = indexToSpawnAt; tilePos.y = -1;
                spawnPosition = board.GetVirtualTilePosition(tilePos.x, tilePos.y);

                initPosition = Camera.main.ViewportToWorldPoint(new Vector2(0.0f, 0.0f));
                initPosition.y -= halfWidth/2.0f;
                initPosition.x = spawnPosition.x;
            }
            else //SpawnSide.Up
            {
                tilePos.x = indexToSpawnAt; tilePos.y = board.TILE_COUNT_Y;
                spawnPosition = board.GetVirtualTilePosition(tilePos.x, tilePos.y);

                initPosition.y = halfWidth + tileSize;
                initPosition.x = spawnPosition.x;
            }

            //Debug.Log("InitPos: " + initPosition + " SpawnPos: " + spawnPosition);
            Instantiate(m_LaserShooterEnemy, initPosition, Quaternion.identity).Init(side, spawnPosition, tilePos);
        }
    }
}
