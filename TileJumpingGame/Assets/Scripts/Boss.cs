using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int Health;
    public int Damage;
    public AttackHandler AttackHandler;
    // Start is called before the first frame update

    public float AttackInterval;
    public float AttackDelay;
    private float attackTimer;

    //make list of premade attacks for each boss
    public int Attacks = 3;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer > AttackInterval)
        {
            var r = Random.Range(0, 1f);
            if (r < 0.33f)
            {
                AttackHandler.DamageRandomRow(Damage, AttackDelay);
            }
            else if (r < 0.66f)
            {
                AttackHandler.DamageRandomColumn(Damage, AttackDelay);
            }
            else
            {
                AttackHandler.DamageRandomXPattern(Damage, AttackDelay);
            }
            attackTimer = 0;
        }
    }
}