using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    Plus,
    Circle
}

public class Enemy : MonoBehaviour
{
    public int MaxHealth;
    public int CurrentHealth;

    public bool RandomMovement;
    public float MoveTimer;

    public TileBoard Board;
    public Tile CurrentTile;
    public AttackHandler AttackHandler;

    public AttackType AttackType;
    // Start is called before the first frame update
    void Start()
    {
        AttackHandler = GameObject.Find("NecessaryGameObjects/AttackHandler").GetComponent<AttackHandler>();
        Board = GameObject.Find("Board").GetComponent<TileBoard>();
        StartCoroutine(StartMove(MoveTimer, true, () => { MoveRandom(); }));

        var attacktypes = AttackType.GetValues(typeof(AttackType));
        AttackType = (AttackType) attacktypes.GetValue(UnityEngine.Random.Range(0, attacktypes.Length));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MoveRandom()
    {
        var dir = UnityEngine.Random.Range(0, 4);
        switch (dir)
        {
            case 0:
                if (Board.CanMoveTo(CurrentTile.xPos, CurrentTile.yPos + 1))
                {
                    EnterTile(Board.GetTile(CurrentTile.xPos, CurrentTile.yPos + 1));
                }
                break;
            case 1:
                if (Board.CanMoveTo(CurrentTile.xPos, CurrentTile.yPos - 1))
                {
                    EnterTile(Board.GetTile(CurrentTile.xPos, CurrentTile.yPos - 1));
                }
                break;
            case 2:
                if (Board.CanMoveTo(CurrentTile.xPos + 1, CurrentTile.yPos))
                {
                    EnterTile(Board.GetTile(CurrentTile.xPos + 1, CurrentTile.yPos));
                }
                break;
            case 3:
                if (Board.CanMoveTo(CurrentTile.xPos - 1, CurrentTile.yPos))
                {
                    EnterTile(Board.GetTile(CurrentTile.xPos - 1, CurrentTile.yPos));
                }
                break;
        }
        Attack();
    }

    private void MoveTowardsPlayer()
    {
        //do a-star or some shit
    }

    private void Attack()
    {
        switch (AttackType) {
            case AttackType.Plus:
                AttackHandler.DamagePlus(CurrentTile.xPos, CurrentTile.yPos, 1, MoveTimer / 2, 2);
                break;
            case AttackType.Circle:
                AttackHandler.DamageCircle(CurrentTile.xPos, CurrentTile.yPos, 1, MoveTimer / 2, 1);
                break;

        }
    }

    private void EnterTile(Tile tile)
    {
        float zPos = transform.position.z;
        transform.position = tile.transform.position + new Vector3(0,0,zPos);
        CurrentTile = tile;
    }
    public static IEnumerator StartMove(float duration, bool repeat, Action callback)
    {
        do
        {
            yield return new WaitForSeconds(duration);

            if (callback != null)
                callback();

        } while (repeat);
    }

    public void TakeDamage(int damage)
    {
        Destroy(gameObject);
    }
}
