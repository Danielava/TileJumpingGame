using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Will be located in the Player object
public class Inventory : MonoBehaviour
{
    //-------------The amount of each elements you have collected---------------
    private int m_NrOfFireElement = 0;
    private int m_NrOfEarthElement = 0;
    private int m_NrOfCoins = 0;
    //--------------------------------------------------------------------------

    /*
     * This list will be filled with 0 at first, meaning we have no available spells.
     * Call ComputeAndShowAvailableSpells() to fill this list with available spells and visualize them in the UI.   
     */
    private int[] m_AvailablePlayerSpells = new int[GameVariables.TOTAL_NR_OF_SPELLS];

    static bool setup = false; //TODO: Might not be needed! Remove

    private void Start()
    {
        if (!setup)
        {

        }
        setup = true;

        ComputeAndShowAvailableSpells();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /*
       Will fill the list m_AvailablePlayerSpells and
       visualize them in the UI for the player to pick.     
    */
    public void ComputeAndShowAvailableSpells()
    {
        ResetAvailableSpellsList();
        PopulateAvailableSpellsList();
        //TODO: Implement some way of visualizing them as well.
    }

    /*
       Helper function for ComputeAndShowAvailableSpells().
       Will fill the list m_AvailablePlayerSpells.
    */
    private void PopulateAvailableSpellsList()
    {
        for(int i = 0; i < GameVariables.TOTAL_NR_OF_SPELLS; i++)
        {
            if(GameVariables.SPELLS[i].CheckIfSpellAvailable(this))
            {
                m_AvailablePlayerSpells[i]++;
            }
        }
    }

    private void ResetAvailableSpellsList()
    {
        for (int i = 0; i < GameVariables.TOTAL_NR_OF_SPELLS; i++)
        {
            m_AvailablePlayerSpells[i] = 0;
        }
    }

    //------------Getter methods------------

    public int GetNrOfFireElements()
    {
        return m_NrOfFireElement;
    }

    public int GetNrOfEarthElements()
    {
        return m_NrOfEarthElement;
    }

    public int GetNrOfCoins()
    {
        return m_NrOfCoins;
    }

    //--------------------------------------
}
