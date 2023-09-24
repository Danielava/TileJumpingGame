using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * PROCESS TO ADD A NEW SPELL!!!
 * 
 * I) Create and add the new spell prefab to the GameVariable SPELLS list!
 * II) Increment the static variable TOTAL_NR_OF_SPELLS
 * III) Add your spell to the enum SPELL_NAMES
 * IV) It's very important that you in your spell script assign this enum to the m_ID! E.g: "m_ID = (int)GameVariables.SPELL_NAMES.Counter;"
 * V) IF IT'S STILL NOT WORKING!!! Then please repeat these steps, there is a chance unity reverted something for you (e.g removed the prefab from this object when you edited the spell)
 * VI) That's it!
 */
public class GameVariables : MonoBehaviour
{

    public static GameVariables instance; //singleton

    public static int TOTAL_NR_OF_SPELLS = 7; //Change this when adding spells
    public static int NR_LEVELS = 1;
    public static int TOTAL_NR_OF_COLLECTIBLES = 3; //E.g of collectibles: Fire, stone, coins (that is 3)

    public Spell[] SPELLS = new Spell[TOTAL_NR_OF_SPELLS]; //Change this when adding spells

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        for(int i = 0; i < TOTAL_NR_OF_SPELLS; i++)
        {
            SPELLS[i].InitSpell();
        }
    }

    /*----- OLD SYSTEM, Creating spells in code, we now create them as prefabs and access the list via singleton ------
    static bool setup = false;

    private void Awake()
    {
        if (!setup)
        {
            //---Populate the global SPELLS list---
            SPELLS[(int)SPELL_NAMES.FireBall] = new FireBall("FireBall");
            SPELLS[(int)SPELL_NAMES.Bomb] = new Bomb("Bomb");
        }
        setup = true;
    }
    */


    public enum SPELL_NAMES
    {
        FireBall,
        Bomb,
        Sprint,
        TripleShot,
        Counter,
        Laser,
        Portal,
        count // = TOTAL_NR_OF_SPELLS
    }
}
