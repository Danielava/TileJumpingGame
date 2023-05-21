using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBall : Spell
{
    private int fireCost = 3;

    public FireBall(string spellname)
    {
        m_SpellName = spellname;
        m_ID = (int)GameVariables.SPELL_NAMES.FireBall;
        m_SpellImage = Resources.Load<GameObject>("FireBallImage").GetComponent<Image>();
    }

    /*
     *  To perform the fireball spell, you need 3 fire.
     */
    public override int CheckIfSpellAvailable(Inventory inventory)
    {
        //The division by 3 here comes from the fact that we need 3 fire to perform this spell
        int amount = inventory.GetNrOfFireElements() / fireCost;
        return amount;
    }

    public override void CastSpell(Inventory inventory)
    {
        //TODO: Perform your spell here!

        inventory.IncrementFire(-fireCost);
        base.CastSpell(inventory);
    }
}
