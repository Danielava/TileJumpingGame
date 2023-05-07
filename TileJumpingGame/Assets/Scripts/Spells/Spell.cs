using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    protected int m_ID;
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
    public virtual bool CheckIfSpellAvailable(Inventory inventory)
    {
        return false;
    }
}
