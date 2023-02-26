using UnityEngine;
using UnityEngine.Serialization;

public class PawnSet : MonoBehaviour
{
    public GameObject[] pawsGameObjects = new GameObject[4];
    public int pawnsIn;
    public int pawnsOut;
    
    
    // Start is called before the first frame update
    void Start()
    {
        pawnsIn = 4;
        pawnsOut = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
