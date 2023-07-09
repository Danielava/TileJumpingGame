using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBoss : MonoBehaviour
{
    public BossHealthUI bossHealthUI;

    public float CurrentHealth;
    public float MaxHealth = 100f;
    public int Damage;
    public AttackHandler AttackHandler;

    public float AttackInterval;
    public float AttackDelay;
    private float attackTimer;

    public TileBoard Board;
    //make list of premade attacks for each boss
    public int Attacks = 3;
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

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        bossHealthUI.SetHealth(CurrentHealth, MaxHealth);
    }

    public void SpawnWaves()
    {
        var rowNr = Random.Range(1, Board.GetSizeY() - 1);

        if(Random.Range(0, 1f) > 0.5f)
        {
            AttackHandler.DamageWaveRow(Damage, rowNr, AttackDelay, 0.25f, true);
            AttackHandler.DamageWaveRow(Damage, rowNr + 1, AttackDelay, 0.25f, true);
            AttackHandler.DamageWaveRow(Damage, rowNr - 1, AttackDelay, 0.25f, true);
            AttackHandler.DamageWaveRow(Damage, rowNr, AttackDelay, 0.25f, true, 1.25f);
            AttackHandler.DamageWaveRow(Damage, rowNr + 1, AttackDelay, 0.25f, true, 1.25f);
            AttackHandler.DamageWaveRow(Damage, rowNr - 1, AttackDelay, 0.25f, true, 1.25f);
            AttackHandler.DamageWaveRow(Damage, rowNr, AttackDelay, 0.25f, true, 2.5f);
            AttackHandler.DamageWaveRow(Damage, rowNr + 1, AttackDelay, 0.25f, true, 2.5f);
            AttackHandler.DamageWaveRow(Damage, rowNr - 1, AttackDelay, 0.25f, true, 2.5f);
        } 
        else
        {
            AttackHandler.DamageWaveColumn(Damage, rowNr, AttackDelay, 0.25f, true);
            AttackHandler.DamageWaveColumn(Damage, rowNr + 1, AttackDelay, 0.25f, true);
            AttackHandler.DamageWaveColumn(Damage, rowNr - 1, AttackDelay, 0.25f, true);
            AttackHandler.DamageWaveColumn(Damage, rowNr, AttackDelay, 0.25f, true, 1.25f);
            AttackHandler.DamageWaveColumn(Damage, rowNr + 1, AttackDelay, 0.25f, true, 1.25f);
            AttackHandler.DamageWaveColumn(Damage, rowNr - 1, AttackDelay, 0.25f, true, 1.25f);
            AttackHandler.DamageWaveColumn(Damage, rowNr, AttackDelay, 0.25f, true, 2.5f);
            AttackHandler.DamageWaveColumn(Damage, rowNr + 1, AttackDelay, 0.25f, true, 2.5f);
            AttackHandler.DamageWaveColumn(Damage, rowNr - 1, AttackDelay, 0.25f, true, 2.5f);
        }
    }
}
