using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class FurryBoss : Boss
{
    public EnemySpawner EnemySpawner;
    public GameObject seedEnemyPrefab;
    public GameObject seedPrefab;
    // Start is called before the first frame update

    void Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer > attackInterval)
        {
            Attack();
            attackTimer = 0;
        }
    }

    public void Attack()
    {
        var r = Random.Range(0, 1f);
        if (r > 0.50f)
        {
            for (int i = 0; i < 2; i++)
            {
                attackHandler.DamageRandomRow(damage, attackDelay);
            }
        }
        else
        {
            SpawnEgg();
        }
    }

    void SpawnEgg()
    {
        var tile = board.GetRandomTile();
        var seed = Instantiate(seedPrefab, transform.position + new Vector3(0, 0, -2), Quaternion.identity);
        Vector2 targetPos = tile.transform.position;
        seed.GetComponent<Rigidbody2D>().AddForce((targetPos - new Vector2(transform.position.x, transform.position.y)) * 100);
        seed.GetComponent<SeedProjectile>().targetTile = tile;
    }

    void SpawnEggTest()
    {
        var tile = board.GetRandomTile();
        var seed = Instantiate(seedPrefab, transform.position + new Vector3(0, 0, -1), Quaternion.identity);
        var rigidBody = seed.GetComponent<Rigidbody2D>();
        Vector2 targetPos = tile.transform.position;
        float initialAngle;
        if (targetPos.x < transform.position.x)
        {
            initialAngle = 135;
        }
        else
        {
            initialAngle = 45;
        }


        /*
                var vectorAngle = new Vector2(Mathf.Sin(initialAngle), Mathf.Cos(initialAngle));

                float gravity = Physics.gravity.magnitude;
                float angle = initialAngle * Mathf.Deg2Rad;
                print("angle:" + angle);

                Vector2 planarTarget = new Vector2(targetPos.x, 0);
                Vector2 planarPosition = new Vector2(transform.position.x, 0);

                float distance = Vector2.Distance(planarTarget, planarPosition);
                float yOffset = transform.position.y - targetPos.y;
                float initialVelocity = 1 / Mathf.Cos(angle) * Mathf.Sqrt(0.5f * gravity * Mathf.Pow(distance, 2) / (distance * Mathf.Tan(angle) + yOffset));

                Vector2 velocity = new Vector2(0, initialVelocity * Mathf.Sin(angle));
                float angleBetweenObjects = Vector2.Angle(planarTarget, planarPosition);
                Vector2 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector2.up) * velocity;
                print(finalVelocity);
                rigidBody.velocity = finalVelocity;


               var distance = targetPos.x - transform.position.x;
               var yOffset = targetPos.y - transform.position.y;


               var v0 = 1 / Mathf.Cos(angle) * Mathf.Sqrt(0.5f * gravity * Mathf.Pow(distance, 2) / (distance * Mathf.Tan(angle) + yOffset));

               rigidBody.velocity = v0 * vectorAngle;
        */

        float gravity = Physics.gravity.magnitude;
        // Selected angle in radians

        // Planar distance between objects
        Vector2 p1 = new Vector2(transform.position.x, transform.position.y);
        Vector2 p2 = new Vector2(targetPos.x, transform.position.y);
        float Xdistance = Vector2.Distance(p1, p2);

        // Distance along the y axis between objects
        float yOffset = transform.position.y - targetPos.y; //Its not work


        float angle = initialAngle * Mathf.Deg2Rad;

        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(Xdistance, 2)) / (Xdistance * Mathf.Tan(angle) + yOffset));

        Vector2 velocity = new Vector2(initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        // Rotate our velocity to match the direction between the two objects
        float angleBetweenObjects = Vector2.Angle(Vector2.right, p2 - p1);
        Vector2 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector2.right) * velocity;

        // Fire!
        //rigid.velocity = finalVelocity;
        rigidBody.velocity = finalVelocity;
    }

}
