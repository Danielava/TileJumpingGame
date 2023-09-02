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

    public TileBoard board;
    //make list of premade attacks for each boss
    void Start()
    {
        currentHealth = maxHealth;
    }


    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        bossHealthUI.SetHealth(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        print("Boss is dead :) ");
    }
}
