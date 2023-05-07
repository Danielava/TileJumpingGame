using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Spell
{
    // Start is called before the first frame update
    void Start()
    {
        m_ID = (int)GameVariables.SPELL_NAMES.FireBall;
    }

    /*
     *  To perform the fireball spell, you need 3 fire.
     */
    public override bool CheckIfSpellAvailable(Inventory inventory)
    {
        if (inventory.GetNrOfFireElements() >= 3)
        {
            return true;
        }
        return false;
    }
}
