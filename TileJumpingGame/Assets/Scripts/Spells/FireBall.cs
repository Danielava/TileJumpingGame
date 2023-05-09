using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBall : Spell
{
    // Start is called before the first frame update
    /*
    void Start()
    {
        m_ID = (int)GameVariables.SPELL_NAMES.FireBall;
        m_SpellImage = (Image)Resources.Load("Assets/Sprites/Spell_Images/FireBallImage");
        Debug.Log("FIREBALL SPELL IMAGE: " + m_SpellImage);
    }
    */

    public FireBall()
    {
        m_ID = (int)GameVariables.SPELL_NAMES.FireBall;
        m_SpellImage = Resources.Load<GameObject>("FireBallImage").GetComponent<Image>();
        Debug.Log("FIREBALL SPELL IMAGE: " + m_SpellImage);
    }

    /*
     *  To perform the fireball spell, you need 3 fire.
     */
    public override bool CheckIfSpellAvailable(Inventory inventory)
    {
        if (inventory.GetNrOfFireElements() >= 1)
        {
            return true;
        }
        return false;
    }
}
