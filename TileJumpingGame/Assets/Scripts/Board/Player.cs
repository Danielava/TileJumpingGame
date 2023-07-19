using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerHealthUI playerHealthUI;
    public int maxHealth;
    private int currentHealth;
    public Inventory inventory;

    public Tile CurrentTile { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = maxHealth;
        playerHealthUI.SetMaxHealth(maxHealth);
        playerHealthUI.SetCurrentHealth(currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void EnterTile(Tile tile)
    {
        float zPos = transform.position.z;
        transform.position = tile.transform.position + new Vector3(0,0,zPos);
        CurrentTile = tile;

        //pickups and shit
        tile.PickUpPowerUps(inventory);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        playerHealthUI.SetCurrentHealth(currentHealth);
        //Debug.Log("oowie noo i got hit :(");
    }

    public void CastSpell(Spell spell, Direction? direction = null)
    {
        if (direction.HasValue)
        {
            spell.CastDirectionalSpell(direction.Value);
        } else
        {
            spell.CastSpell();
        }

        foreach (var elementCost in spell.GetSpellCastCost())
        {
            inventory.IncrementElement(elementCost.Key, -elementCost.Value);
        }
        inventory.UpdateSpellPanelNumbers();
    }
}
