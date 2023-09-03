using UnityEngine;

public class Lamp : Powerup
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void AddToInventory(Inventory inventory)
    {
        GameObject.Find("Fog").GetComponent<FogHandler>().IncreaseLight();
    }
}
