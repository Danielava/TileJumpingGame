using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class ChainLightning : Spell
{
    public ChainLightingProjectile m_LightningProjectile;

    public override void InitSpell()
    {
        spellCastCost.Add(Element.Fire, 1);
        m_ID = (int)GameVariables.SPELL_NAMES.ChainLightning;
    }

    public override void CastSpell()
    {
        //StartCoroutine(CastChtinainLighg());
        Vector3 playerPos = GameObject.FindWithTag("Player").transform.position + new Vector3(0, 0, -0.5f);
        m_LightningProjectile.InitiateChainLighting(playerPos);
        
        base.CastSpell();
    }
}
