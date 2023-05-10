using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellPanel : MonoBehaviour
{
    private List<Spell> m_Spells;
    private RectTransform m_RectTransform;
    private float m_PanelWidth;
    // Start is called before the first frame update
    void Awake()
    {
        m_Spells = new List<Spell>();

        m_RectTransform = gameObject.GetComponent<RectTransform>();
        m_PanelWidth = m_RectTransform.rect.width; //Position 0 will be in the middle of the panel. m_PanelWidth/2 will be the right edge, -m_PanelWidth/2 will be left edge!
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

        //You need to loop through all panel spells to beautifully sort them visually.
        ArangeSpellsVisually();
    }

    //Loop through all SpellPanel's children and arrange them visually in the panel!
    private void ArangeSpellsVisually()
    {
        float distanceBetweenSpellImages = 70.0f;
        float spellCoverageLength = distanceBetweenSpellImages * Mathf.Max(0, transform.childCount - 1);
        float rightMostPos = spellCoverageLength / 2.0f;
        float leftMostPos = -rightMostPos;

        int idx = 0;
        foreach (Transform child in transform)
        {
            child.GetComponent<Image>().rectTransform.anchoredPosition = new Vector2(leftMostPos + idx * distanceBetweenSpellImages, 0.0f);
            idx++;
        }
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
