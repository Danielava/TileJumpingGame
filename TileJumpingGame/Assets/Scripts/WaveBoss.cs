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
        var rowNr = Random.Range(1, Board.GetSizeY() - 1);

        if(Random.Range(0, 1f) > 0.5f)
        {
            //AttackHandler.DamageWaveRow(Damage, rowNr, AttackDelay, 0.25f, true);
            //AttackHandler.DamageWaveRow(Damage, rowNr + 1, AttackDelay, 0.25f, true);
            //AttackHandler.DamageWaveRow(Damage, rowNr - 1, AttackDelay, 0.25f, true);
            //AttackHandler.DamageWaveRow(Damage, rowNr, AttackDelay, 0.25f, true, 1.25f);
            //AttackHandler.DamageWaveRow(Damage, rowNr + 1, AttackDelay, 0.25f, true, 1.25f);
            //AttackHandler.DamageWaveRow(Damage, rowNr - 1, AttackDelay, 0.25f, true, 1.25f);
            //AttackHandler.DamageWaveRow(Damage, rowNr, AttackDelay, 0.25f, true, 2.5f);
            //AttackHandler.DamageWaveRow(Damage, rowNr + 1, AttackDelay, 0.25f, true, 2.5f);
            //AttackHandler.DamageWaveRow(Damage, rowNr - 1, AttackDelay, 0.25f, true, 2.5f);
            for(int i = 0; i < Board.GetSizeY(); i++)
            {
                AttackHandler.DamageWaveRow(Damage, i, AttackDelay, 0.25f, i % 2 == 0);
                AttackHandler.DamageWaveRow(Damage, i, AttackDelay, 0.25f, i % 2 == 0, 1.25f);
            }
        } 
        else
        {
            //AttackHandler.DamageWaveColumn(Damage, rowNr, AttackDelay, 0.25f, true);
            //AttackHandler.DamageWaveColumn(Damage, rowNr + 1, AttackDelay, 0.25f, true);
            //AttackHandler.DamageWaveColumn(Damage, rowNr - 1, AttackDelay, 0.25f, true);
            //AttackHandler.DamageWaveColumn(Damage, rowNr, AttackDelay, 0.25f, true, 1.25f);
            //AttackHandler.DamageWaveColumn(Damage, rowNr + 1, AttackDelay, 0.25f, true, 1.25f);
            //AttackHandler.DamageWaveColumn(Damage, rowNr - 1, AttackDelay, 0.25f, true, 1.25f);
            //AttackHandler.DamageWaveColumn(Damage, rowNr, AttackDelay, 0.25f, true, 2.5f);
            //AttackHandler.DamageWaveColumn(Damage, rowNr + 1, AttackDelay, 0.25f, true, 2.5f);
            //AttackHandler.DamageWaveColumn(Damage, rowNr - 1, AttackDelay, 0.25f, true, 2.5f);
            for (int i = 0; i < Board.GetSizeY(); i++)
            {
                AttackHandler.DamageWaveColumn(Damage, i, AttackDelay, 0.25f, i % 2 == 0);
                AttackHandler.DamageWaveColumn(Damage, i, AttackDelay, 0.25f, i % 2 == 0, 1.25f);
            }
        }
    }
}
