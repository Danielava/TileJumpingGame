using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bomb : Spell
{
    public Bomb(string spellname)
    {
        spellCastCost.Add(Element.Fire, 3);
        spellCastCost.Add(Element.Earth, 2);
        m_SpellName = spellname;
        m_ID = (int)GameVariables.SPELL_NAMES.Bomb;
        m_SpellImage = Resources.Load<GameObject>("BombImage").GetComponent<Image>();
    }

    public override void CastSpell(Inventory inventory)
    {
        //TODO: Perform your spell here!
        GameObject.FindWithTag("Boss").GetComponent<Boss>().TakeDamage(20);
        base.CastSpell(inventory);
    }
}
