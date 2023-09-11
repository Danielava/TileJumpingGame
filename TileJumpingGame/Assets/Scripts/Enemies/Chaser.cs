using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : Enemy
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartMove(MoveTimer, true, () =>
        {
            MoveTowardsPlayer();
            if (Mathf.Abs(Player.CurrentTile.xPos - CurrentTile.xPos) + Mathf.Abs(Player.CurrentTile.yPos - CurrentTile.yPos) <= 1)
            {
                Attack(AttackType.Circle);
            }
        }));
        var attacktypes = AttackType.GetValues(typeof(AttackType));
        AttackType = (AttackType)attacktypes.GetValue(UnityEngine.Random.Range(0, attacktypes.Length));
    }
}
