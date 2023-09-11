using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    private Tile m_Tile;

    public void Init(Tile tile)
    {
        m_Tile = tile;
    }

    //Return the tile that this boulder is standing on
    public Tile GetTile()
    {
        return m_Tile;
    }
}
