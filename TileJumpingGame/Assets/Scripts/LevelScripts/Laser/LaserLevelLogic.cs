using System.Collections;
using System.Collections.Generic;
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
        TileBoard board = TileBoard.instance;
        float tileSize = board.m_TileSize;

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
            int rndIndex = Random.Range(0, numbers.Count);
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
            float offset = 0.05f;
            if (side == SpawnSide.Left)
            {
                spawnPosition.x = -tileSize;
                spawnPosition.y = indexToSpawnAt * tileSize;

                initPosition = Camera.main.ViewportToWorldPoint(new Vector2(0.0f - offset, 0.0f));// 0.0f in x pos represents left of viewport, we offset it a bit to end up slightly outisde of it!
                initPosition.y = spawnPosition.y; //TODO: Offset this a bit to make the flying into the screen cooler
            }
            else if (side == SpawnSide.Right)
            {
                spawnPosition.x = boardWidth;
                spawnPosition.y = indexToSpawnAt * tileSize;

                initPosition = Camera.main.ViewportToWorldPoint(new Vector2(1.0f + offset, 0.0f));
                initPosition.y = spawnPosition.y; //TODO: Offset this a bit to make the flying into the screen cooler
            }
            else if (side == SpawnSide.Down)
            {
                spawnPosition.y = -tileSize;
                spawnPosition.x = indexToSpawnAt * tileSize;

                initPosition = Camera.main.ViewportToWorldPoint(new Vector2(0.0f, 0.0f));
                initPosition.y -= halfWidth/2.0f;
                initPosition.x = spawnPosition.x;
            }
            else //SpawnSide.Up
            {
                spawnPosition.y = boardHeight;
                spawnPosition.x = indexToSpawnAt * tileSize;

                initPosition.y = halfWidth + tileSize;
                initPosition.x = spawnPosition.x;
            }

            Instantiate(m_LaserShooterEnemy, initPosition, Quaternion.identity).Init(side, spawnPosition);
        }
    }
}
