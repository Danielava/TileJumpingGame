using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerHealthUI playerHealthUI;
    public int maxHealth;
    private int currentHealth;
    public Inventory inventory;

    private GameObject m_CounterCharged; //E.g charged with counter spell

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
        if(InCounterState()) //
        {
            Debug.Log("Countered!!");
            Destroy(m_CounterCharged);
            m_CounterCharged = null;
            return;
        }
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


    public void SetCounterState(GameObject charge)
    {
        m_CounterCharged = charge;
    }

    public bool InCounterState()
    {
        return (m_CounterCharged != null);
    }
}
