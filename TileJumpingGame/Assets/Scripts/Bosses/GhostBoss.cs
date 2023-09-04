using System.Linq;
using UnityEngine;

public class GhostBoss : Boss
{
    public FogHandler Fog;
    public EnemySpawner EnemySpawner;
    // Start is called before the first frame update

    public GameObject LampAdd;
    void Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer > attackInterval)
        {
            SpawnWaves();
            attackTimer = 0;
        }
    }

    public void SpawnWaves()
    {
        var r = Random.Range(0, 1f);
        if (r > 0.75f)
        {
            for (int i = 0; i < board.GetSizeY(); i++)
            {
                attackHandler.DamageWaveRow(damage, i, attackDelay, 0.25f, i % 2 == 0);
                attackHandler.DamageWaveRow(damage, i, attackDelay, 0.25f, i % 2 == 0, 1.25f);
            }
        }
        else if (r > 0.5f)
        {
            for (int i = 0; i < board.GetSizeY(); i++)
            {
                attackHandler.DamageWaveColumn(damage, i, attackDelay, 0.25f, i % 2 == 0);
                attackHandler.DamageWaveColumn(damage, i, attackDelay, 0.25f, i % 2 == 0, 1.25f);
            }
        }
        else if (r > 0.25f)
        {
            if (Phase < 2)
            {
                SpawnLampAdd();

            } else
            {
                attackHandler.DamageWaveSpiral(damage, attackDelay, 0.05f, 0.5f);
            }
        }
        else
        {
            attackHandler.DamageWaveDiagonal(damage, attackDelay, 0.25f);
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    protected override void AdvancePhase()
    {
        if(Phase == 1)
        {
            ActivateFog(3);
        }
        if(Phase == 2)
        {
            ActivateFog(2);
            //GameObject.FindGameObjectsWithTag("Lamp").ToList().ForEach(e => GameObject.Destroy(e));
            attackInterval *= 0.75f;
        }
    }

    private void ActivateFog(int visionRange)
    {
        Fog.SetFog(visionRange);
    }

    private void SpawnLampAdd()
    {
        EnemySpawner.SpawnEnemy(LampAdd.GetComponent<Enemy>());
    }
}
