using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fire : Powerup
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    public override void AddToInventory(Inventory inventory)
    {
        inventory.IncrementFire(1);
    }
}
