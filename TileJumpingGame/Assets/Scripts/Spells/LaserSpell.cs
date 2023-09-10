using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserSpell : Spell
{
    public Laser m_Laser;

    public override void InitSpell()
    {
        spellCastCost.Add(Element.Fire, 3);
        m_ID = (int)GameVariables.SPELL_NAMES.Laser;
    }

    public override void CastSpell()
    {
        base.CastSpell();
    }
}
