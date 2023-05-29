using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBall : Spell
{
    public FireBall(string spellname)
    {
        spellCastCost.Add(Element.Fire, 3);
        m_SpellName = spellname;
        m_ID = (int)GameVariables.SPELL_NAMES.FireBall;
        m_SpellImage = Resources.Load<GameObject>("FireBallImage").GetComponent<Image>();
    }

    public override void CastSpell(Inventory inventory)
    {
        //TODO: Perform your spell here!
        GameObject.FindWithTag("Boss").GetComponent<Boss>().TakeDamage(10);
        base.CastSpell(inventory);
    }
}
