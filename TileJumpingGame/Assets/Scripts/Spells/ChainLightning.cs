using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChainLightning : Spell
{
    public GameObject m_LightningProjectile;

    public override void InitSpell()
    {
        spellCastCost.Add(Element.Fire, 1);
        m_ID = (int)GameVariables.SPELL_NAMES.ChainLightning;
    }

    public override void CastSpell()
    {
        //TODO: Perform your spell here!
        Vector3 pos = GameObject.FindWithTag("Player").transform.position + new Vector3(0, 0, -0.5f);
        Instantiate(m_LightningProjectile, pos, Quaternion.identity);
        base.CastSpell();
    }
}
