using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBoss : Boss
{    // Update is called once per frame
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
            attackHandler.DamageWaveSpiral(damage, attackDelay, 0.05f, 0.5f);
        }
        else
        {
            attackHandler.DamageWaveDiagonal(damage, attackDelay, 0.25f);
        }
    }
}
