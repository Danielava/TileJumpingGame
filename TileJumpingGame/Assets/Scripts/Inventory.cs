using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Element
{
    Fire,
    Earth,
}

//Will be located in the Player object
public class Inventory : MonoBehaviour
{


    private const int MAX_NUMBER_OF_ELEMENT = 10; //You can't have more than this amount of each element!

    //-------------The amount of each elements you have collected---------------
    private Dictionary<Element, int> elementCountDict = new Dictionary<Element, int>();
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

        // Setup element dictionary
        var elements = Enum.GetValues(typeof(Element)).Cast<Element>().ToList();
        foreach (Element element in elements)
        {
            elementCountDict.Add(element, 0);
        }


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
            //print(GameVariables.instance.SPELLS.Length);
            m_AvailablePlayerSpells[i] = GameVariables.instance.SPELLS[i].CheckIfSpellAvailable(this);

            if (m_AvailablePlayerSpellsPrevious[i] < m_AvailablePlayerSpells[i])
            {
                if (m_AvailablePlayerSpellsPrevious[i] == 0)
                {
                    m_SpellPanel.AddSpellToPanel(i); //Spell appeared for the first time, add it to our panel
                }
            }
            m_AvailablePlayerSpellsPrevious[i] = m_AvailablePlayerSpells[i];
        }

        UpdateSpellPanelNumbers();
    }

    public void UpdateSpellPanelNumbers()
    {
        m_SpellPanel.UpdateSpellPanelNumbers();
    }

    private void ResetAvailableSpellsList()
    {
        for (int i = 0; i < GameVariables.TOTAL_NR_OF_SPELLS; i++)
        {
            m_AvailablePlayerSpells[i] = 0;
        }
    }

    //------------Getter methods------------
    public int GetElementCount(Element element)
    {
        return elementCountDict[element];
    }

    public void IncrementElement(Element element, int value)
    {
        elementCountDict[element] += value;
        if (elementCountDict[element] >= MAX_NUMBER_OF_ELEMENT)
        {
            elementCountDict[element] = MAX_NUMBER_OF_ELEMENT;
        }
        elementCountDict[element] = Mathf.Max(elementCountDict[element], 0);
    }

    public void IncrementCoin(int value)
    {
        m_NrOfCoins += value;
        m_NrOfCoins = Mathf.Max(m_NrOfCoins, 0);
    }

    public void SetAvailableSpellsAvailablePreviously(int spellIndex, int nrOfTheSpellAvailable)
    {
        m_AvailablePlayerSpellsPrevious[spellIndex] = nrOfTheSpellAvailable;
    }
}
