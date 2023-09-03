using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bomb : Spell
{
    public BombObject m_BombObject;

    public override void InitSpell()
    {
        spellCastCost.Add(Element.Fire, 1); //3
        //spellCastCost.Add(Element.Earth, 2); //2
        m_ID = (int)GameVariables.SPELL_NAMES.Bomb;
    }

    public override void CastSpell()
    {
        //TODO: Perform your spell here!

        Tile playerTile = GameObject.FindWithTag("Player").GetComponent<Player>().CurrentTile;
        Vector3 pos = playerTile.transform.position + new Vector3(0, 0, -0.05f);

        Instantiate(m_BombObject, pos, Quaternion.identity).Init(playerTile);
        base.CastSpell();
    }
}
