using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sprint : Spell
{
    public override void InitSpell()
    {
        spellCastCost.Add(Element.Earth, 2);
        m_ID = (int)GameVariables.SPELL_NAMES.Sprint;
    }

    public override void CastSpell(Inventory inventory)
    {
        //TODO: Perform your spell here!
        base.CastSpell(inventory);
    }
}
