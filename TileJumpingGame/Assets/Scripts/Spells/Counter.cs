using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : Spell
{
    public Projectile m_Projectile;
    public GameObject m_ChargedUpEffect;

    public override void InitSpell()
    {
        spellCastCost.Add(Element.Fire, 1);
        m_ID = (int)GameVariables.SPELL_NAMES.Counter;
    }

    public override void CastSpell()
    {
        //TODO: Perform your spell here!
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if(!player.InCounterState())
        {
            Vector3 pos = player.transform.position + new Vector3(0, 0.3f, -0.08f);
            GameObject charge = Instantiate(m_ChargedUpEffect, pos, Quaternion.identity);

            charge.transform.parent = player.transform;
            player.SetCounterState(charge);
        }

        base.CastSpell();
    }
}
