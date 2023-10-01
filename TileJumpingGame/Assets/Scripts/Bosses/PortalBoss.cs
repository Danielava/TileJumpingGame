using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PortalBoss : Boss
{
    public PortalHandler PortalHandler;

    public EnemyProjectile BossProjectile;
    // Start is called before the first frame update
    void Start()
    {
        PortalHandler = GameObject.Find("PortalHandler").GetComponent<PortalHandler>();
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer > attackInterval)
        {
            DeletePortals();
            SpawnPortals();
            SpawnBullets();
            attackTimer = 0;
        }
    }

    private static List<(int, int)> portalPositions = new List<(int, int)>() { (0, 2), (0, 3), (0, 4), (0, 5), (7, 2), (7, 3), (7, 4), (7, 5) };

    private void SpawnPortals()
    {
        //assume squareboard
        var grps = board.tiles.GroupBy(t => t.yPos).OrderBy(x => x.Key);


        for (int i = 0; i < grps.Count(); i++)
        {

        }

        var rnd = new System.Random();
        var positions = portalPositions.OrderBy(p => rnd.Next()).Take(4).ToArray();

        for (int i = 0; i < 4; i++)
        {
            var firstTile = board.GetTile(i + 2, 0);
            var secondTile = board.GetTile(positions[i].Item1, positions[i].Item2);
            PortalHandler.CreatePortalPair(firstTile, secondTile);
        }

    }

    private void DeletePortals()
    {
        PortalHandler.DeleteNonPlayerPortals();
    }

    private void SpawnBullets()
    {
        var rnd = new System.Random();

        float timer = 0;
        foreach (var x in new float[]{ -1.5f, -0.5f, 0.5f, 1.5f }.OrderBy(x => rnd.Next()))
        {
            StartCoroutine(SpawnBullet(new Vector3(transform.position.x + x, transform.position.y - 1, -0.5f), timer));
            timer += 0.15f;
        }
    }

    private IEnumerator SpawnBullet(Vector3 position, float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(BossProjectile, position, Quaternion.identity).Init(5f, Vector2.down);
    }
}
