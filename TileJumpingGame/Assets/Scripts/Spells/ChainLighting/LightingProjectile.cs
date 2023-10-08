using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingProjectile : MonoBehaviour
{
    public void Start()
    {
        Destroy(gameObject, 1.0f);
    }
    /*
       TODO: The chain lighting should probably be stuck to any gameObject, meaning the endPos and startPos
       should be constantly updated in an Update function here and then the chainLighting should be adjusted accordingly.
       
       Likewise you would have to in an Update function in ChainLightingProjectile check so that as soon as
       an enemy dies or if the order of closest enemies etc changes, that the chain lighting should be adjusted there
       as well. But this would be a huge change and is really more fitted if we want a channeled chainlighting attack.
     */
    public void Init(Vector2 endPos)
    {
        GetComponent<Renderer>().sharedMaterial.SetFloat("_Height", 0.01f);

        Vector2 startPos = transform.position;
        Vector2 rotationVector = startPos - endPos;

        float angle = Vector2.SignedAngle(Vector2.up, rotationVector);
        transform.rotation = Quaternion.Euler(0, 0, angle);

        float dist = Vector2.Distance(startPos, endPos);
        float distRatio = dist / GetComponent<SpriteRenderer>().size.y;


        float heightRatio = 1.0f;
        //GetComponent<Renderer>().sharedMaterial.SetFloat("_Height", heightRatio); //This changes the value for ALL objects using this shader
        GetComponent<Renderer>().material.SetFloat("_Height", heightRatio);
        GetComponent<Renderer>().material.SetFloat("_UpperCutoff", 1.0f - distRatio);

        //You need to push it out in the y-dir since the control point is in the middle.
        float offset = 1.0f;
        transform.localPosition -= transform.up * (heightRatio * GetComponent<SpriteRenderer>().size.y/2.0f) * offset;

        transform.position = new Vector3(transform.position.x, transform.position.y, -0.05f);
        //if you want to add some value to any of the x,y,z values simply use
        //transform.Rotate (0,0,10);
    }
}
