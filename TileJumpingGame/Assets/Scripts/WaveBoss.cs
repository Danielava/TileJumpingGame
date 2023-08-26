using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBoss : Boss
{
    //make list of premade attacks for each boss
    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer > AttackInterval)
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
            for (int i = 0; i < Board.GetSizeY(); i++)
            {
                AttackHandler.DamageWaveRow(Damage, i, AttackDelay, 0.25f, i % 2 == 0);
                AttackHandler.DamageWaveRow(Damage, i, AttackDelay, 0.25f, i % 2 == 0, 1.25f);
            }
        }
        else if (r > 0.5f)
        {
            for (int i = 0; i < Board.GetSizeY(); i++)
            {
                AttackHandler.DamageWaveColumn(Damage, i, AttackDelay, 0.25f, i % 2 == 0);
                AttackHandler.DamageWaveColumn(Damage, i, AttackDelay, 0.25f, i % 2 == 0, 1.25f);
            }
        }
        else if (r > 0.25f)
        {
            AttackHandler.DamageWaveSpiral(Damage, AttackDelay, 0.05f, 0.5f);
        }
        else
        {
            AttackHandler.DamageWaveDiagonal(Damage, AttackDelay, 0.25f);
        }
    }
}
