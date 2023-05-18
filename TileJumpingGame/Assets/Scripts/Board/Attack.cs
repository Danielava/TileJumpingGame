using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    public float Time { get; set; }
    public int Damage { get; set; }
    private float Duration { get; set; }
    public List<Tile> Tiles { get; set; }

    public Attack(float time, int damage, List<Tile> tiles)
    {
        Time = time;
        Damage = damage;
        Tiles = tiles;

        tiles.ForEach(t => t.AddIncomingDamage(time));
    }

    public bool ReduceTimer(float t)
    {
        Duration += t;

        return (Duration >= Time);
    }
}
