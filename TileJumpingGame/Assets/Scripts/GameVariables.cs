using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVariables : MonoBehaviour
{
    public static int TOTAL_NR_OF_SPELLS = 1; //Change this when adding spells
    public static int NR_LEVELS = 1;
    public static int TOTAL_NR_OF_COLLECTIBLES = 3; //E.g of collectibles: Fire, stone, coins (that is 3)

    public static Spell[] SPELLS = new Spell[TOTAL_NR_OF_SPELLS]; //Change this when adding spells

    static bool setup = false;

    private void Awake()
    {
        if (!setup)
        {
            //---Populate the global SPELLS list---
            SPELLS[(int)SPELL_NAMES.FireBall] = new FireBall();
            //TODO: Add your spells here as you create more!
        }
        setup = true;
    }

    public enum SPELL_NAMES
    {
        FireBall,
        count // = TOTAL_NR_OF_SPELLS
    }
}
