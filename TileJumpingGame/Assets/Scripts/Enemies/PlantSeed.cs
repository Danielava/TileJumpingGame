using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlantSeed : Enemy
{

    public GameObject enemyPrefab;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy(2.0f));
    }


    public IEnumerator SpawnEnemy(float delay)
    {
        yield return new WaitForSeconds(delay);
        var newEnemy = Instantiate(enemyPrefab, transform);
        newEnemy.transform.parent = transform.parent;
        newEnemy.GetComponent<Enemy>().CurrentTile = CurrentTile;
        newEnemy.GetComponent<Enemy>().CurrentTile.entityOnTile = newEnemy.GetComponent<Enemy>();
        Destroy(gameObject);
    }
}
