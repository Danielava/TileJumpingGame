using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetHealth(float currentHealth, float maxHealth)
    {
        GetComponent<Slider>().value = currentHealth / maxHealth;
    }
}
