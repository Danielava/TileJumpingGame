using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bomb : Spell
{
    public override void InitSpell()
    {
        spellCastCost.Add(Element.Fire, 3);
        spellCastCost.Add(Element.Earth, 2);
        m_ID = (int)GameVariables.SPELL_NAMES.Bomb;
    }

    public override void CastSpell()
    {
        //TODO: Perform your spell here!
        GameObject.FindWithTag("Boss").GetComponent<Boss>().TakeDamage(20);
        base.CastSpell();
    }
}
