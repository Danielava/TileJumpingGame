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

    public override void CastSpell()
    {
        //TODO: Perform your spell here!
        CharacterController characterController = GameObject.FindWithTag("Player").GetComponent<CharacterController>();
        characterController.Teleport();
        base.CastSpell();
    }
}
