using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Spell : MonoBehaviour
{
    protected int m_ID;
    protected Image m_SpellImage;

    public string m_SpellName;

    protected Dictionary<Element, int> spellCastCost = new Dictionary<Element, int>();
    // Start is called before the first frame update
    void Start()
    {
        m_ID = -1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
     * Every spell class will override this class and it
     * will return true if you have enough resources to perform
     * the spell.
     */
    public virtual int CheckIfSpellAvailable(Inventory inventory)
    {
        return spellCastCost.Select(e => inventory.GetElementCount(e.Key) / e.Value).Min();
    }

    public virtual void CastSpell(Inventory inventory)
    {
        inventory.ComputeAndShowAvailableSpells(); //The reason we call this here is because this will lead to this being called less times!
        foreach (var elementCost in spellCastCost)
        {
            inventory.IncrementElement(elementCost.Key, -elementCost.Value);
        }
    }

    public Image GetSpellImage()
    {
        return m_SpellImage;
    }
}
