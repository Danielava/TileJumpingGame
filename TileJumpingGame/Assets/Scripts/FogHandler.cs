using UnityEngine;

public class FogHandler : MonoBehaviour
{
    private Material material;
    public GameObject Player;

    public int VisionRadius;
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
    }

    public void SetPosition()
    {

    }

    private void UpdateLight()
    {
        material.SetFloat("_HardCutOff", VisionRadius * 2);
        material.SetFloat("_SoftCutoff", VisionRadius);
    }

    public void IncreaseLight()
    {
        VisionRadius++;
        UpdateLight();
    }

    public void DecreaseLight()
    {
        VisionRadius++;
        UpdateLight();
    }
}
