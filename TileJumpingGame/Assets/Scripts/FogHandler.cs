using UnityEngine;

public class FogHandler : MonoBehaviour
{
    private Material material;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        material.SetVector("_CharacterPosition", Player.transform.position);
    }

    public void SetPosition()
    {

    }
}
