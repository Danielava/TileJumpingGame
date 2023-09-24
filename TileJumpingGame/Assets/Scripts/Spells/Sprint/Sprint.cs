using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sprint : Spell
{

    public GameObject explosion;

    public override void InitSpell()
    {
        spellCastCost.Add(Element.Earth, 2);
        m_ID = (int)GameVariables.SPELL_NAMES.Sprint;
    }

    public override void CastDirectionalSpell(Direction direction)
    {
        //TODO: Perform your spell here!
        CharacterController characterController = GameObject.FindWithTag("Player").GetComponent<CharacterController>();
        characterController.Teleport(direction);
        Instantiate(explosion, GameObject.FindWithTag("Player").transform);
    }

}
