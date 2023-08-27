using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBoss1 : Boss
{
    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer > attackInterval)
        {
            var r = Random.Range(0, 1f);
            if (r < 0.33f)
            {
                attackHandler.DamageRandomRow(damage, attackDelay);
            }
            else if (r < 0.66f)
            {
                attackHandler.DamageRandomColumn(damage, attackDelay);
            }
            else
            {
                attackHandler.DamageRandomXPattern(damage, attackDelay);
            }
            attackTimer = 0;
        }
    }
}
