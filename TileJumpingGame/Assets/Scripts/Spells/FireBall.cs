using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBall : Spell
{
    public FireBall()
    {
        m_ID = (int)GameVariables.SPELL_NAMES.FireBall;
        m_SpellImage = Resources.Load<GameObject>("FireBallImage").GetComponent<Image>();
    }

    /*
     *  To perform the fireball spell, you need 3 fire.
     */
    public override int CheckIfSpellAvailable(Inventory inventory)
    {
        //The division by 3 here comes from the fact that we need 3 fire to perform this spell
        int amount = inventory.GetNrOfFireElements() / 3;
        return amount;
    }
}
