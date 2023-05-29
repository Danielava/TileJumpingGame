using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    public TileBoard Board;
    public List<Attack> Attacks;

    public Player Player;
    // Start is called before the first frame update
    void Start()
    {
        Attacks = new List<Attack>();
    }

    // Update is called once per frame
    void Update()
    {
        var t = Time.deltaTime;
        foreach(var attack in Attacks.ToList())
        {
            var attackDone = attack.ReduceTimer(t);

            if (attackDone)
            {
                if (attack.Tiles.Contains(Player.CurrentTile))
                {
                    Player.TakeDamage(attack.Damage);
                }

                Attacks.Remove(attack);
            }
        }
    }
    
    public void DamageRandomColumn(int damage, float delay)
    {
        var columnNr = Random.Range(0, Board.GetSizeX());

        var tiles = Board.tiles.Where(t => t.xPos == columnNr && t.canWalkOn).ToList();

        Attacks.Add(new Attack(delay, damage, tiles));
    }

    public void DamageRandomRow(int damage, float delay)
    {
        var rowNr = Random.Range(0, Board.GetSizeY());

        var tiles = Board.tiles.Where(t => t.yPos == rowNr && t.canWalkOn).ToList();

        Attacks.Add(new Attack(delay, damage, tiles));
    }

    public void DamagePlus(int xPos, int yPos, int damage, float delay, int range)
    {
        var tiles = Board.tiles
            .Where(t => 
                (t.yPos == yPos && Mathf.Abs(t.xPos - xPos) <= range ||
                t.xPos == xPos && Mathf.Abs(t.yPos - yPos) <= range)
                && t.canWalkOn)
            .ToList();

        Attacks.Add(new Attack(delay, damage, tiles));
    }

    public void DamageCircle(int xPos, int yPos, int damage, float delay, int range)
    {
        var tiles = Board.tiles
            .Where(t => Mathf.Abs(t.xPos - xPos) <= range && Mathf.Abs(t.yPos - yPos) <= range && t.canWalkOn)
            .ToList();

        Attacks.Add(new Attack(delay, damage, tiles));
    }

    //public void DamageXPattern(int x, int y, int damage, float delay)
    //{
    //    int diff = x - y;

    //    for (int i = 0; i < Board.GetSizeY(); i++)
    //    {
    //        if (diff >= 0 && diff < Board.GetSizeX())
    //            tiles[diff, i].AddIncomingDamage(delay);
    //        diff++;
    //    }

    //    int sum = x + y;
    //    for (int i = 0; i < Board.GetSizeY(); i++)
    //    {
    //        if (sum < Board.GetSizeX() && sum >= 0)
    //            tiles[sum, i].AddIncomingDamage(delay);
    //        sum--;
    //    }
    //}

    public void DamageRandomXPattern(int damage, float delay)
    {
        var x = Random.Range(0, Board.GetSizeX());
        var y = Random.Range(0, Board.GetSizeY());

        int diff = x - y;

        var tiles = new List<Tile>();

        for (int i = 0; i < Board.GetSizeY(); i++)
        {
            if (diff >= 0 && diff < Board.GetSizeX() && Board.GetTile(diff, i).canWalkOn)
                tiles.Add(Board.GetTile(diff, i));
            diff++;
        }

        int sum = x + y;
        for (int i = 0; i < Board.GetSizeY(); i++)
        {
            if (sum < Board.GetSizeX() && sum >= 0 && Board.GetTile(sum, i).canWalkOn)
                tiles.Add(Board.GetTile(sum, i));
            sum--;
        }

        Attacks.Add(new Attack(delay, damage, tiles));
    }

    public void DamageTiles(List<(int, int)> tilePositions, int damage, int delay)
    {

    }
}
