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

    public float MoveTimer;

    public GridTileBoard Board;
    public Tile CurrentTile;
    public AttackHandler AttackHandler;

    public AttackType AttackType;

    public Player Player;
    // Start is called before the first frame update

    void Awake()
    {
        Player = GameObject.Find("Player").GetComponent<Player>();
        AttackHandler = GameObject.Find("GameManager").GetComponent<AttackHandler>();
        Board = GameObject.Find("Board").GetComponent<GridTileBoard>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void MoveRandom()
    {
        var dir = UnityEngine.Random.Range(0, 4);
        switch (dir)
        {
            case 0:
                CheckAndEnterTile(CurrentTile.xPos, CurrentTile.yPos + 1);
                break;
            case 1:
                CheckAndEnterTile(CurrentTile.xPos, CurrentTile.yPos - 1);
                break;
            case 2:

                CheckAndEnterTile(CurrentTile.xPos + 1, CurrentTile.yPos);
                break;
            case 3:
                CheckAndEnterTile(CurrentTile.xPos - 1, CurrentTile.yPos);
                break;
        }
    }

    // Improve this simple SHIT algorithm
    protected void MoveTowardsPlayer()
    {
        var xDist = Player.CurrentTile.xPos - CurrentTile.xPos;
        var yDist = Player.CurrentTile.yPos - CurrentTile.yPos;

        if (Mathf.Abs(xDist) + Mathf.Abs(yDist) <= 1)
        {
            return;
        }


        if (Mathf.Abs(xDist) > Mathf.Abs(yDist))
        {
            if (xDist > 0)
            {
                CheckAndEnterTile(CurrentTile.xPos + 1, CurrentTile.yPos);
            }
            else
            {
                CheckAndEnterTile(CurrentTile.xPos - 1, CurrentTile.yPos);
            }
        }
        else
        {
            if (yDist > 0)
            {
                CheckAndEnterTile(CurrentTile.xPos, CurrentTile.yPos + 1);
            }
            else
            {
                CheckAndEnterTile(CurrentTile.xPos, CurrentTile.yPos - 1);
            }
        }
    }

    protected void Attack(AttackType attackType)
    {
        switch (attackType)
        {
            case AttackType.Plus:
                AttackHandler.DamagePlus(CurrentTile.xPos, CurrentTile.yPos, 1, MoveTimer / 2, 2);
                break;
            case AttackType.Circle:
                AttackHandler.DamageCircle(CurrentTile.xPos, CurrentTile.yPos, 1, MoveTimer / 2, 1);
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

    private void EnterTile(Tile tile)
    {
        float zPos = transform.position.z;
        transform.position = tile.transform.position + new Vector3(0, 0, zPos);
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

    public virtual void TakeDamage(int damage)
    {
        Destroy(gameObject);
    }
}
