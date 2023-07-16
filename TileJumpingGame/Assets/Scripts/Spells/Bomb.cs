using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bomb : Spell
{
    public BombObject m_BombObject;

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

        Vector3 pos = GameObject.FindWithTag("Player").transform.position;// + new Vector3(0, 0, -0.05f);
        Tile playerTile = GameObject.FindWithTag("Player").GetComponent<Player>().CurrentTile;

        Instantiate(m_BombObject, pos, Quaternion.identity).Init(playerTile);
        base.CastSpell();
    }
}
