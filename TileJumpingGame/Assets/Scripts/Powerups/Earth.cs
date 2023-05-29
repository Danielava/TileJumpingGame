using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : Powerup
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    public override void AddToInventory(Inventory inventory)
    {
        inventory.IncrementElement(Element.Earth, 1);
        base.AddToInventory(inventory);
    }
}
