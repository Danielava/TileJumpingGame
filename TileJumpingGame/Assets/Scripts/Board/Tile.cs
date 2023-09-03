using Assets.Scripts.Board;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int xPos;
    public int yPos;
    public List<Powerup> powerUps;
    public List<Hazard> hazards;
    public bool canWalkOn { 
        get { return tileType != TileType.Hole; } 
        private set { canWalkOn = value; } 
    }

    public GameObject incomingDamage;

    public TileType tileType;
    // Start is called before the first frame update
    void Start()
    {
        powerUps = new List<Powerup>();
        hazards = new List<Hazard>();
    }

    // Update is called once per frame
    void Update()
    {
        RemoveOldPowerUps();
    }

    public void Init(int x, int y)
    {
        xPos = x;
        yPos = y;
        transform.localPosition = new Vector3(x, y, 0);
    }

    public void AddPowerUp(Powerup powerup)
    {
        powerUps.Add(powerup);
        //Instantiate(powerup, transform, false);

    }

    private void RemoveOldPowerUps()
    {
        foreach (Powerup powerup in powerUps.ToList())
        {
            if (powerup.CheckDestruction())
            {
                PowerupSpawner.DecrementPowerUpCountOnScreen();
                Destroy(powerup.gameObject);
                powerUps.Remove(powerup);
            }
        }
    }

    public void EnterTile(Inventory inventory)
    {
        //add checks for hazards, items, etc
        PickUpPowerUps(inventory);
        TriggerHazards();
    }

    public void AddIncomingDamage(float delay)
    {
        var obj = Instantiate(incomingDamage);
        obj.GetComponent<IncomingDamage>().Init(delay);
        obj.transform.position = transform.position + new Vector3(0, 0, -0.01f);
    }

    public void PickUpPowerUps(Inventory inventory)
    {
        foreach (Powerup powerup in powerUps)
        {
            //add to inventory
            PowerupSpawner.DecrementPowerUpCountOnScreen();
            powerup.AddToInventory(inventory);
            Destroy(powerup.gameObject);
        }
        powerUps.Clear();
    }

    private void TriggerHazards()
    {
        foreach (Hazard hazard in hazards)
        {
            //do something with player

            //clear hazard?
        }
    }
}
