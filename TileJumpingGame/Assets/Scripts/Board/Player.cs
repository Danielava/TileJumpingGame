using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;
    private Inventory inventory;

    public Tile CurrentTile { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterTile(Tile tile)
    {
        transform.position = tile.transform.position;
        CurrentTile = tile;

        //pickups and shit
        tile.PickUpPowerUps(inventory);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("oowie noo i got hit :(");
    }
}
