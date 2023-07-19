using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellPanel : MonoBehaviour
{
    private List<Spell> m_Spells;
    private RectTransform m_RectTransform;
    private float m_PanelWidth;

    public GameObject m_Cursor;
    private int m_CurrentSelectedSpell; //The spell the cursor is hovering at!
    private Inventory m_Inventory;

    public Text m_Text;

    // Start is called before the first frame update
    void Awake()
    {
        m_Spells = new List<Spell>();

        m_RectTransform = gameObject.GetComponent<RectTransform>();
        m_PanelWidth = m_RectTransform.rect.width; //Position 0 will be in the middle of the panel. m_PanelWidth/2 will be the right edge, -m_PanelWidth/2 will be left edge!

        m_Cursor.SetActive(false);
        m_CurrentSelectedSpell = -1;

        m_Cursor = Instantiate(m_Cursor, new Vector2(0.0f, 0.0f), Quaternion.identity);
        m_Cursor.transform.parent = this.transform;
        m_Cursor.tag = "Cursor";
    }
    //Make sure to call this in the Inventory Awake function!
    public void SetInventory(Inventory inv)
    {
        m_Inventory = inv;
    }

    public void AddSpellToPanel(int spellIndex)
    {
        Spell spell = GameVariables.instance.SPELLS[spellIndex];
        m_Spells.Add(spell);
        RenderSpellToPanel(spell);
    }

    public void UpdateSpellPanelNumbers()
    {
        int childIdx = 0;
        foreach (Transform child in transform)
        {
            if (child.tag == "Cursor")
            {
                continue;
            }

            int spellIndex = m_Spells[childIdx].GetSpellID();
            int nrOfTheSpellAvailable = GameVariables.instance.SPELLS[spellIndex].CheckIfSpellAvailable(m_Inventory);
            m_Inventory.m_AvailablePlayerSpellsPrevious[spellIndex] = nrOfTheSpellAvailable;

            //Update spell text number
            child.transform.GetChild(0).GetComponent<Text>().text = "" + nrOfTheSpellAvailable;
            childIdx++;
        }

        int childCount = transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            if (transform.GetChild(i).tag == "Cursor")
            {
                continue;
            }

            int nrOfTheSpellAvailable;
            int.TryParse(transform.GetChild(i).GetChild(0).GetComponent<Text>().text, out nrOfTheSpellAvailable);

            if (nrOfTheSpellAvailable == 0)
            {
                RemoveSpellFromPanel(i);
            }
        }
    }

    // Update is called once per frame
    /*
     *  TODO! This cursor will iterate through our spell list. But make it instead iterate the children of SpellPanel to prevent sync/race condition issues.
     */
    void Update()
    {
        if(m_Spells.Count <= 0)
        {
            m_Cursor.SetActive(false);
            m_CurrentSelectedSpell = -1;
        }
        else
        {
            m_Cursor.SetActive(true);
        }

        if(m_Spells.Count == 1)
        {
            MoveCursor(0);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveCursor(m_CurrentSelectedSpell - 1);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveCursor(m_CurrentSelectedSpell + 1);
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(m_CurrentSelectedSpell >= 0)
            {
                CastSpell(m_CurrentSelectedSpell);
            }
        }
    }

    /*
     * Cast the spell, make sure to remove it from the list, re-render the spellPanel and also adjust the cursor!
     */
    private void CastSpell(int spellNr)
    {
        m_Spells[spellNr].PickSpell();
    }

    private void RemoveSpellFromPanel(int spellNr)
    {
        //If nrOfSpellLeft has reached 0, then we want to destroy this spell completely
        m_Spells.RemoveAt(spellNr);

        //Destroy spell child from the panel
        GameObject spellobj = transform.GetChild(spellNr).gameObject;
        /*
         * TODO: WARNING!! This Destroy might not be immidiate and therefore transform.childCount might give unreliable results.. 
         * If there are bugs in the future, this might be the reason!       
        */
        DestroyImmediate(spellobj);

        ArangeSpellsVisually(false);

        //adjust cursor
        int nextSpellIdx = m_CurrentSelectedSpell;
        if (nextSpellIdx >= m_Spells.Count)
        {
            nextSpellIdx = m_Spells.Count - 1;
        }
        MoveCursor(nextSpellIdx);
    }

    private void MoveCursor(int spellListIdx)
    {
        m_Cursor.GetComponent<Image>().rectTransform.SetAsLastSibling();
        if (spellListIdx >= m_Spells.Count || spellListIdx < 0)
        {
            return;
        }

        //Get children object of spellpanel!
        Vector3 spell_pos = transform.GetChild(spellListIdx).GetComponent<Image>().rectTransform.anchoredPosition;
        Vector3 cursor_offset = new Vector3(20.0f, -30.0f, 0.0f);
        m_Cursor.GetComponent<Image>().rectTransform.anchoredPosition = new Vector3(spell_pos.x, spell_pos.y, spell_pos.z) + cursor_offset;
        m_CurrentSelectedSpell = spellListIdx;
    }

    private void RenderSpellToPanel(Spell spell)
    {
        Image image = Instantiate(spell.GetSpellImage(), new Vector2(-45.0f, 0.0f), Quaternion.identity);
        //Add a text object to our image as well.
        Text spellNumberText = Instantiate(m_Text);
        spellNumberText.rectTransform.anchoredPosition = image.rectTransform.anchoredPosition - new Vector2(-image.rectTransform.rect.width/2.0f, image.rectTransform.rect.height/2.0f);
        spellNumberText.text = "" + 1;
        spellNumberText.transform.parent = image.transform;

        image.transform.parent = this.transform;

        //You need to loop through all panel spells to beautifully sort them visually.
        ArangeSpellsVisually(true);
    }

    //Loop through all SpellPanel's children and arrange them visually in the panel!
    private void ArangeSpellsVisually(bool adjustCursor)
    {
        float distanceBetweenSpellImages = 70.0f;
        float spellCoverageLength = distanceBetweenSpellImages * Mathf.Max(0, transform.childCount - 2); //-2 because we don't want to count cursor as the child!
        float rightMostPos = spellCoverageLength / 2.0f;
        float leftMostPos = -rightMostPos;

        int idx = 0;
        foreach (Transform child in transform)
        {
            if (child.tag == "Cursor")
            {
                continue;
            }

            child.GetComponent<Image>().rectTransform.anchoredPosition = new Vector2(leftMostPos + idx * distanceBetweenSpellImages, 0.0f);
            idx++;
        }

        if(adjustCursor)
        {
            //--------Adjust cursor--------
            if ((m_CurrentSelectedSpell == -1) && (m_Spells.Count == 1))
            {
                m_CurrentSelectedSpell = 0;
            }
            MoveCursor(m_CurrentSelectedSpell);
            //-----------------------------
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
}
