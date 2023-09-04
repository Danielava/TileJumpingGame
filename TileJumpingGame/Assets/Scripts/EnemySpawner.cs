using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    public GridTileBoard TileBoard;

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
            SpawnEnemy(Enemy.GetComponent<Enemy>());
        }
    }

    public void SpawnEnemy(Enemy enemy)
    {
        if (transform.childCount < MaxEnemyAmount)
        {
            var tile = TileBoard.GetValidSpawnPoint();

            var enemyObj = Instantiate(enemy, new Vector3(tile.transform.position.x, tile.transform.position.y, -0.04f), Quaternion.identity, transform);

            enemyObj.GetComponent<Enemy>().CurrentTile = tile;
        }
    }
}
