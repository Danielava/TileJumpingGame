using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellPanel : MonoBehaviour
{
    private List<Spell> m_Spells;
    // Start is called before the first frame update
    void Awake()
    {
        m_Spells = new List<Spell>();
    }

    public void AddSpellToPanel(Spell spell)
    {
        m_Spells.Add(spell);
        RenderSpellToPanel(spell);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RenderSpellToPanel(Spell spell)
    {
        Image image = Instantiate(spell.GetSpellImage(), new Vector2(-45.0f, 0.0f), Quaternion.identity);

        image.transform.parent = this.transform;

        //TODO: Calculate the image positions, don't hardcode them!
        image.rectTransform.anchoredPosition = new Vector2(-45.0f, 0.0f);
    }

    /*
     * UNUSED FOR NOW!!
     */
    public void RenderSpellPanel()
    {
        for(int i = 0; i < m_Spells.Count; i++)
        {
            //Image image = m_Spells[i].GetSpellImage();
            Image image = Instantiate(m_Spells[i].GetSpellImage(), new Vector3(0, 0, 10), Quaternion.identity);
            image.transform.parent = this.transform;
        }
    }

    void InvokeSpell(int spellNr)
    {
        m_Spells[spellNr].CastSpell();
        m_Spells.RemoveAt(spellNr);
    }
}
