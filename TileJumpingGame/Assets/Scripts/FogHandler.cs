using UnityEngine;

public class FogHandler : MonoBehaviour
{
    private Material material;
    public GameObject Player;

    public int VisionRadius;

    float duration = 2.0f;
    float startTime;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            IncreaseLight();
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            DecreaseLight();
        }


        material.SetVector("_CharacterPosition", Player.transform.position);



        UpdateLight();
    }

    public void ActivateFog()
    {
        startTime = Time.time;
        VisionRadius = 3;
    }

    public void SetPosition()
    {

    }

    private void UpdateLight()
    {
        //material.SetFloat("_HardCutOff", VisionRadius + 1);
        //material.SetFloat("_SoftCutoff", VisionRadius);
        //float shininess = Mathf.PingPong(Time.time, 1.0f);

        float t = (Time.time - startTime) / duration;
        float lerp = Mathf.Lerp(material.GetFloat("_HardCutOff"), VisionRadius  + 1, t);
        float lerp2 = Mathf.Lerp(material.GetFloat("_SoftCutoff"), VisionRadius, t);

        material.SetFloat("_HardCutOff", lerp);
        material.SetFloat("_SoftCutoff", lerp2);
    }

    public void IncreaseLight()
    {
        VisionRadius++;
        startTime = Time.time;
    }

    public void DecreaseLight()
    {
        VisionRadius--;

        startTime = Time.time;
    }
}
