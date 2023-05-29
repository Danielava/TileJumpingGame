using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    public TileBoard TileBoard;

    public int MaxEnemyAmount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        if(transform.childCount < MaxEnemyAmount)
        {
            var tile = TileBoard.GetValidSpawnPoint();

            var enemyObj = Instantiate(Enemy, new Vector3(tile.xPos, 0, tile.yPos), Quaternion.identity, transform);

            enemyObj.GetComponent<Enemy>().CurrentTile = tile;
        }
    }
}
