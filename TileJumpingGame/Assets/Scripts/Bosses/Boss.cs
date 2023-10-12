using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public BossHealthUI bossHealthUI;

    public float currentHealth;
    public float maxHealth = 100f;
    public int damage;
    public AttackHandler attackHandler;

    public float attackInterval;
    public float attackDelay;
    protected float attackTimer;

    public float SpeedMultiplier = 1f;

    public GridTileBoard board;

    public int Phase;
    public float[] PhaseCutoffs = new float[] { 0 };

    //make list of premade attacks for each boss
    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }


    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        bossHealthUI.SetHealth(currentHealth, maxHealth);

        if (PhaseCutoffs.Length > Phase && currentHealth / maxHealth < PhaseCutoffs[Phase])
        {
            Phase++;
            AdvancePhase();
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void AdvancePhase()
    {

    }

    public void Die()
    {
        print("Boss is dead :) ");
    }
}
