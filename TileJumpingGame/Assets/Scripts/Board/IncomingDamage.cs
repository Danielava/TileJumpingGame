using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomingDamage : MonoBehaviour
{
    public float delay;
    public float timer;

    public bool active;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            timer += Time.deltaTime;

            var ratio = timer / delay;
            transform.localScale = new Vector3(ratio, ratio, ratio);
            if (ratio >= 1)
            {
                Destroy(gameObject);
            }
        }

    }

    public void Init(float delay)
    {
        active = true;
        this.delay = delay;

        transform.localScale = Vector3.zero;
    }
}
