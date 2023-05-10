using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bomb : Spell
{
    public Bomb()
    {
        m_ID = (int)GameVariables.SPELL_NAMES.Bomb;
        m_SpellImage = Resources.Load<GameObject>("BombImage").GetComponent<Image>();
    }

    /*
     *  To perform the bomb spell, you need 3 fire and 2 earth.
     */
    public override int CheckIfSpellAvailable(Inventory inventory)
    {
        int amountFire = inventory.GetNrOfFireElements() / 3; //The division by 3 here comes from the fact that we need 3 fire to perform this spell
        int amountEarth = inventory.GetNrOfEarthElements() / 2; //The division by 2 here comes from the fact that we need 2 earth to perform this spell
        return Mathf.Min(amountEarth, amountFire);
    }
}
