using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    public GridTileBoard Board;
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
        foreach (var attack in Attacks.ToList())
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

    public void DamageWaveRow(int damage, int rowNr, float delay, float speed, bool reverse, float flatDelay = 0)
    {
        //var rowNr = Random.Range(0, Board.GetSizeY());

        var tiles = Board.tiles.Where(t => t.yPos == rowNr).ToList();
        if (reverse)
        {
            tiles.Reverse();
        }

        int i = 1;
        foreach (var tile in tiles)
        {
            i++;
            if (tile.canWalkOn)
                Attacks.Add(new Attack(flatDelay + delay + i * speed, damage, tile, flatDelay + i * speed));
        }
    }

    public void DamageWaveColumn(int damage, int columnNr, float delay, float speed, bool reverse, float flatDelay = 0)
    {
        //var rowNr = Random.Range(0, Board.GetSizeY());

        var tiles = Board.tiles.Where(t => t.xPos == columnNr).ToList();
        if (reverse)
        {
            tiles.Reverse();
        }

        int i = 0;
        foreach (var tile in tiles)
        {
            i++;
            if (tile.canWalkOn)
                Attacks.Add(new Attack(flatDelay + delay + i * speed, damage, tile, flatDelay + i * speed));
        }
    }

    public void DamageWaveDiagonal(int damage, float delay, float speed, float flatDelay = 0)
    {
        var tiles = Board.tiles;

        foreach (var tile in tiles)
        {
            if (tile.canWalkOn)
                Attacks.Add(new Attack(flatDelay + delay + (tile.xPos + tile.yPos) * speed, damage, tile, flatDelay + (tile.xPos + tile.yPos) * speed));
        }
    }

    public void DamageWaveSpiral(int damage, float delay, float speed, float flatDelay = 0)
    {
        int top = 0, bottom = Board.TILE_COUNT_X - 1, left = 0, right = Board.TILE_COUNT_Y - 1;

        int i = 0;
        while (top <= bottom && left <= right)
        {
            for (int col = left; col <= right; col++)
            {
                Attacks.Add(new Attack(flatDelay + delay + i * speed, damage, Board.tiles[top * Board.TILE_COUNT_Y + col], flatDelay + i * speed));
                i++;
            }
            top++;

            for (int row = top; row <= bottom; row++)
            {
                Attacks.Add(new Attack(flatDelay + delay + i * speed, damage, Board.tiles[row * Board.TILE_COUNT_Y + right], flatDelay + i * speed));
                i++;
            }
            right--;

            if (top <= bottom)
            {
                for (int col = right; col >= left; col--)
                {
                    Attacks.Add(new Attack(flatDelay + delay + i * speed, damage, Board.tiles[bottom * Board.TILE_COUNT_Y + col], flatDelay + i * speed));
                    i++;
                }
                bottom--;
            }

            if (left <= right)
            {
                for (int row = bottom; row >= top; row--)
                {
                    Attacks.Add(new Attack(flatDelay + delay + i * speed, damage, Board.tiles[row * Board.TILE_COUNT_Y + left], flatDelay + i * speed));
                    i++;
                }
                left++;
            }
        }

    }


    public void DamageWaveSpiral2(int damage, float delay, float speed, float flatDelay = 0)
    {

        var tiles = Board.tiles.ToList();

        var tileOrder = tiles.Select(t =>
        (
            t,
            SpiralOrder(t.xPos, t.yPos)
        ));

        foreach (var tile in tileOrder)
        {
            Attacks.Add(new Attack(flatDelay + delay + tile.Item2 * speed, damage, tile.Item1, flatDelay + tile.Item2 * speed));
        }
    }

    //doesnt work
    public int SpiralOrder(int x, int y)
    {
        var layer = Mathf.Min(x, y, Board.TILE_COUNT_X - 1 - x, Board.TILE_COUNT_Y - 1 - y);
        if (x <= y)
        {
            return layer * (Board.TILE_COUNT_Y - 2 * layer) + y - layer + 1;
        }
        else
        {
            return layer * (Board.TILE_COUNT_Y - 2 * layer) + Board.TILE_COUNT_X - 2 * layer + x - layer + y - layer + 1;
        }
    }


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
