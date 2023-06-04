using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum SpellType
{
    Normal, //Immidiate?
    Directional,
    Channel
}

public class Spell : MonoBehaviour
{
    protected int m_ID;
    public Image m_SpellImage;

    public string m_SpellName;

    protected Dictionary<Element, int> spellCastCost = new Dictionary<Element, int>();

    public SpellType m_SpellType = SpellType.Normal;

    //Has to be called by GameVariable!
    public virtual void InitSpell() {}

    /*
     * Every spell class will override this class and it
     * will return true if you have enough resources to perform
     * the spell.
     */
    public virtual int CheckIfSpellAvailable(Inventory inventory)
    {
        return spellCastCost.Select(e => inventory.GetElementCount(e.Key) / e.Value).Min();
    }

    public virtual void CastSpell()
    {

    }

    public virtual void CastDirectionalSpell(Direction direction)
    {

    }

    public void PickSpell()
    {
        switch(m_SpellType)
        { 
            case SpellType.Directional:
                GameObject.FindWithTag("Player").GetComponent<CharacterController>().PrepareSpell(this);
                break;
            default:
                GameObject.FindWithTag("Player").GetComponent<Player>().CastSpell(this);
                break;
        }
    }

    public Dictionary<Element, int> GetSpellCastCost()
    {
        return spellCastCost;
    }

    public Image GetSpellImage()
    {
        return m_SpellImage;
    }
}
