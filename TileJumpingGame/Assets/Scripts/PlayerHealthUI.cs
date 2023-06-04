using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;
    public GameObject heart;

    private List<GameObject> hearts;
    private float xHeartOffset = 50f;

    public void SetMaxHealth(int maxHealth)
    {
        hearts = new List<GameObject>();
        for (int i = 0; i < maxHealth; i++)
        {
            var newHeart = Instantiate(heart, new Vector3(transform.position.x + xHeartOffset * i, transform.position.y, 0), Quaternion.identity);
            newHeart.transform.parent = transform;
            hearts.Add(newHeart);
        }
    }

    public void SetCurrentHealth(int currentHealth)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].GetComponent<Image>().sprite = fullHeart;
            }
            else
            {
                hearts[i].GetComponent<Image>().sprite = emptyHeart;
            }
        }
    }
}
