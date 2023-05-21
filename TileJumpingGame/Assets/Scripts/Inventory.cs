using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Will be located in the Player object
public class Inventory : MonoBehaviour
{
    private const int MAX_NUMBER_OF_ELEMENT = 10; //You can't have more than this amount of each element!

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
    private int[] m_AvailablePlayerSpellsPrevious = new int[GameVariables.TOTAL_NR_OF_SPELLS];
    private SpellPanel m_SpellPanel; //The spell UI basically where you select spells to cast.

    static bool setup = false; //TODO: Might not be needed! Remove

    private void Start()
    {
        if (!setup)
        {

        }
        setup = true;

        m_SpellPanel = GameObject.FindGameObjectWithTag("SpellPanel").GetComponent<SpellPanel>();
        m_SpellPanel.SetInventory(this);

        for (int i = 0; i < GameVariables.TOTAL_NR_OF_SPELLS; i++)
        {
            m_AvailablePlayerSpells[i] = 0;
            m_AvailablePlayerSpellsPrevious[i] = 0;
        }

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
        //m_SpellPanel.RenderSpellPanel(); //DON'T USE FOR NOW. WE RENDER WHEN CALLING m_SpellPanel.AddSpellToPanel();
    }

    /*
       Helper function for ComputeAndShowAvailableSpells().
       Will fill the list m_AvailablePlayerSpells.
    */
    private void PopulateAvailableSpellsList()
    {
        /*
         *  OBS! This whole system of m_AvailablePlayerSpellsPrevious is totally useless if
         *  we only wanna display one of each spell regardless... This system is only useful
         *  if we want to allow multiple of the same spell in the panel.. we have to think about this..
         * 
         */
        for (int i = 0; i < GameVariables.TOTAL_NR_OF_SPELLS; i++)
        {
            m_AvailablePlayerSpells[i] = GameVariables.SPELLS[i].CheckIfSpellAvailable(this);
        }

        for (int i = 0; i < GameVariables.TOTAL_NR_OF_SPELLS; i++)
        {
            /*
             * TODO: A clever solution here would be to have a m_AvailablePlayerSpellsPrevious
             * which holds the previous amount of spells! Compare your newly computed m_AvailablePlayerSpells
             * to the m_AvailablePlayerSpellsPrevious, and only if the number between index i in these lists differ
             * do we want to AddSpellToPanel().. i.e we turn that index i to Dirty!
             * 
             * This m_AvailablePlayerSpellsPrevious list will then update after this process, i.e below here! 
             */
             if(m_AvailablePlayerSpellsPrevious[i] < m_AvailablePlayerSpells[i])
             {
                m_SpellPanel.AddSpellToPanel(GameVariables.SPELLS[i]);
             }
        }

        for (int i = 0; i < GameVariables.TOTAL_NR_OF_SPELLS; i++)
        {
            m_AvailablePlayerSpellsPrevious[i] = m_AvailablePlayerSpells[i];
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

    public void IncrementEarth(int value)
    {
        m_NrOfEarthElement += value;
        if(m_NrOfEarthElement >= MAX_NUMBER_OF_ELEMENT)
        {
            m_NrOfEarthElement = MAX_NUMBER_OF_ELEMENT;
        }
        m_NrOfEarthElement = Mathf.Max(m_NrOfEarthElement, 0);
    }

    public void IncrementFire(int value)
    {
        m_NrOfFireElement = m_NrOfFireElement + value;
        if (m_NrOfFireElement >= MAX_NUMBER_OF_ELEMENT)
        {
            m_NrOfFireElement = MAX_NUMBER_OF_ELEMENT;
        }
        m_NrOfFireElement = Mathf.Max(m_NrOfFireElement, 0);
    }

    public void IncrementCoin(int value)
    {
        m_NrOfCoins += value;
        m_NrOfCoins = Mathf.Max(m_NrOfCoins, 0);
    }
}
