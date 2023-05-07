using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int xPos;
    public int yPos;
    public List<Powerup> powerUps;
    public List<Hazard> hazards;
    public bool unEnterable;
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
        transform.localPosition = new Vector3(x, 0, y);
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

    public void EnterTile()
    {
        //add checks for hazards, items, etc
        PickUpPowerUps();
        TriggerHazards();
    }

    private void PickUpPowerUps()
    {
        foreach (Powerup powerup in powerUps)
        {
            //add to inventory
            PowerupSpawner.DecrementPowerUpCountOnScreen();
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
