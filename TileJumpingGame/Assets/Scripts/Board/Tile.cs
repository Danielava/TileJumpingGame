using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int xPos;
    public int yPos;
    public List<Powerup> PowerUps;

    public bool Enterable;
    // Start is called before the first frame update
    void Start()
    {
        PowerUps = new List<Powerup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(int x, int y)
    {
        xPos = x;
        yPos = y;
        transform.localPosition = new Vector3(x, 0, y);
    }

    public void addPowerUp(Powerup powerup)
    {
        PowerUps.Add(powerup);
        //Instantiate(powerup, transform, false);

    }

    public void EnterTile()
    {
        //add checks for hazards, items, etc
        foreach(Powerup powerup in PowerUps)
        {
            //add to inventory

            Destroy(powerup.gameObject);
        }
        PowerUps.Clear();
    }
}
