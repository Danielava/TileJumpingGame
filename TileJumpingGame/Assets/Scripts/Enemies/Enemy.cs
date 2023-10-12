using System;
using System.Collections;
using UnityEngine;

public enum AttackType
{
    Plus,
    Circle
}

public class Enemy : BoardEntity
{
    public int MaxHealth;
    public int CurrentHealth;

    public float MoveInterval;
    public float AttackDelay;

    public float SpeedMultiplier = 1;

    public AttackHandler AttackHandler;

    public AttackType AttackType;

    public Player Player;
    // Start is called before the first frame update

    override public void Awake()
    {
        base.Awake();
        Player = GameObject.Find("Player").GetComponent<Player>();
        AttackHandler = GameObject.Find("GameManager").GetComponent<AttackHandler>();
    }


    protected void MoveRandom()
    {
        var dir = UnityEngine.Random.Range(0, 4);
        switch (dir)
        {
            case 0:
                StartCoroutine(TryMove(Direction.Up));
                break;
            case 1:
                StartCoroutine(TryMove(Direction.Down));
                break;
            case 2:

                StartCoroutine(TryMove(Direction.Right));
                break;
            case 3:
                StartCoroutine(TryMove(Direction.Left));
                break;
        }
    }

    // Improve this simple SHIT algorithm
    public void MoveTowardsPlayer()
    {
        var xDist = Player.CurrentTile.xPos - CurrentTile.xPos;
        var yDist = Player.CurrentTile.yPos - CurrentTile.yPos;


        if (Mathf.Abs(xDist) + Mathf.Abs(yDist) <= 1)
        {
            return;
        }

        // Replace shitty pathfinding with a* or something
        if (Mathf.Abs(xDist) > Mathf.Abs(yDist))
        {
            if (xDist > 0)
            {
                StartCoroutine(TryMove(Direction.Right));
            }
            else
            {
                StartCoroutine(TryMove(Direction.Left));
            }
        }
        else
        {
            if (yDist > 0)
            {
                StartCoroutine(TryMove(Direction.Up));
            }
            else
            {
                StartCoroutine(TryMove(Direction.Down));
            }
        }
    }

    protected void Attack(AttackType attackType)
    {
        switch (attackType)
        {
            case AttackType.Plus:
                AttackHandler.DamagePlus(CurrentTile.xPos, CurrentTile.yPos, 1, AttackDelay * SpeedMultiplier, 2);
                break;
            case AttackType.Circle:
                AttackHandler.DamageCircle(CurrentTile.xPos, CurrentTile.yPos, 1, AttackDelay * SpeedMultiplier, 1);
                break;
        }
    }

    protected void CheckAndEnterTile(int xPos, int yPos)
    {
        if (Board.CanMoveTo(xPos, yPos))
        {
            EnterTile(Board.GetTile(xPos, yPos));
        }
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

    public virtual void TakeDamage(float damage)
    {
        Destroy(gameObject);
    }
}
