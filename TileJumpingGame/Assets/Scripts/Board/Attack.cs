using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    public float Time { get; set; }
    public int Damage { get; set; }
    private float SpawnDelay { get; set; }
    private float Duration { get; set; }
    public List<Tile> Tiles { get; set; }
    private bool spawned;

    public Attack(float time, int damage, List<Tile> tiles, float spawnDelay = 0)
    {
        SpawnDelay = spawnDelay;
        Time = time;
        Damage = damage;
        Tiles = tiles;
    }
    public Attack(float time, int damage, Tile tile, float spawnDelay = 0)
    {
        SpawnDelay = spawnDelay;
        Time = time;
        Damage = damage;
        Tiles = new List<Tile>() { tile };
    }

    public bool ReduceTimer(float t)
    {
        Duration += t;
        if(Duration >= SpawnDelay && !spawned)
        {
            Tiles.ForEach(t => t.AddIncomingDamage(Time - SpawnDelay));
            spawned = true;
        }

        return (Duration >= Time);
    }
}
