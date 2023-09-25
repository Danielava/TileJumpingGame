using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedProjectile : MonoBehaviour
{
    public Tile targetTile;
    public GameObject seedPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        var targetPos = new Vector2(targetTile.transform.position.x, targetTile.transform.position.y);
        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), targetPos) < 0.5f)
        {
            var seed = Instantiate(seedPrefab, targetPos, Quaternion.identity);
            seed.GetComponent<Enemy>().CurrentTile = targetTile;
            Destroy(gameObject);
        }
    }
}
