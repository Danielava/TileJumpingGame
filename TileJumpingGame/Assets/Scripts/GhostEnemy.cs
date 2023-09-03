using UnityEngine;


public class GhostEnemy : Enemy
{
    public GameObject Lamp;
    // Start is called before the first frame update
    
    public override void TakeDamage(int damage)
    {
        SpawnLamp();
        Destroy(gameObject);
    }

    private void SpawnLamp()
    {
        var tile = CurrentTile;

        var powerObj = Instantiate(Lamp, new Vector3(tile.xPos, tile.yPos, 0), Quaternion.identity, tile.transform);
        tile.AddPowerUp(powerObj.GetComponent<Powerup>());
    }
}
