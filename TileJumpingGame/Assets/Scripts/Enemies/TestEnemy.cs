using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : Enemy
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartMove(MoveTimer, true, () =>
        {
            MoveRandom();
            var attacktypes = AttackType.GetValues(typeof(AttackType));
            var attackType = (AttackType)attacktypes.GetValue(UnityEngine.Random.Range(0, attacktypes.Length));
            Attack(attackType);
        }));

    }
}
