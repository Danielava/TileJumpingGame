using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bomb : Spell
{
    private int fireCost = 3;
    private int earthCost = 2;

    public Bomb(string spellname)
    {
        m_SpellName = spellname;
        m_ID = (int)GameVariables.SPELL_NAMES.Bomb;
        m_SpellImage = Resources.Load<GameObject>("BombImage").GetComponent<Image>();
    }

    /*
     *  To perform the bomb spell, you need 3 fire and 2 earth.
     */
    public override int CheckIfSpellAvailable(Inventory inventory)
    {
        int amountFire = inventory.GetNrOfFireElements() / fireCost; //The division by 3 here comes from the fact that we need 3 fire to perform this spell
        int amountEarth = inventory.GetNrOfEarthElements() / earthCost; //The division by 2 here comes from the fact that we need 2 earth to perform this spell
        return Mathf.Min(amountEarth, amountFire);
    }

    public override void CastSpell(Inventory inventory)
    {
        inventory.IncrementEarth(-earthCost);
        inventory.IncrementFire(-fireCost);
        Debug.Log(m_SpellName + " casted!");
    }
}
