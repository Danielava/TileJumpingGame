using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * Class is supposed to spawn powerups randomly on the board.
 * 
 * Version1: Just spawn random powerups on the board at start.
 * V1.1: 
 * 
 */

[System.Serializable]
public class PowerUpWrapper
{
    public Powerup powerup;
    public int spawnWeight;
}

public class PowerupSpawner : MonoBehaviour
{
    //A list of powerups that can potentially spawn.
    public List<PowerUpWrapper> m_PowerUps;

    private GridTileBoard board;
    private GameObject[,] tiles;

    private float m_SpawnFreq; //Put nr between 0-1, if 0 -> no spawns.
    private int MAX_POWEUPS_ON_SCREEN; //This could change e.g if we get frenzy or something.

    //If this is 3, then there is a chance that 3 items could spawn at the same time.
    public int m_MaxPowerUpsToSpawn;
    //Time between each spawn, it should also have some type of offset.
    public float m_TimeBetweenSpawns;
    private float m_TimeBetweenSpawnsOffset;
    private float timer;

    private static int m_CurrentPowerUpsOnScreen;

    int totalWeight;

    // Start is called before the first frame update
    void Start()
    {
        board = GameObject.Find("Board").GetComponent<GridTileBoard>();
        //tiles = board.GetTileList(); //Do we need this?
        totalWeight = m_PowerUps.Sum(it => it.spawnWeight);

        MAX_POWEUPS_ON_SCREEN = 10;
        m_CurrentPowerUpsOnScreen = 0;

        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= m_TimeBetweenSpawns && m_CurrentPowerUpsOnScreen <= m_MaxPowerUpsToSpawn)
        {
            //A random tile to spawn on.
            //Vector2Int tile_index = new Vector2Int();
            //tile_index.x = Random.Range(0, tiles.GetLength(0));
            //tile_index.y = Random.Range(0, tiles.GetLength(1));
            var tile = board.GetValidSpawnPoint();

            //Random object.
            int randomInt = Random.Range(0, totalWeight);
            foreach (PowerUpWrapper powerUpWrapper in m_PowerUps)
            {
                if (randomInt < powerUpWrapper.spawnWeight)
                {
                    var powerObj = Instantiate(powerUpWrapper.powerup, new Vector3(tile.transform.position.x, tile.transform.position.y, 0), Quaternion.identity, tile.transform);
                    tile.AddPowerUp(powerObj.GetComponent<Powerup>());
                    break;
                }
                randomInt -= powerUpWrapper.spawnWeight;
            }
            //Can't create object from the reference list as it gets null when destroyed.
            m_CurrentPowerUpsOnScreen++;
            timer = 0;
        }
    }

    public static void DecrementPowerUpCountOnScreen()
    {
        m_CurrentPowerUpsOnScreen--;
    }
}
