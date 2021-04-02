using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    private Sprite[] diceSides;
    private SpriteRenderer rend;
    public static int result;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        diceSides = Resources.LoadAll<Sprite>("Dice/");
        
    }
    private void OnMouseDown() {
        StartCoroutine("RollTheDice");    
    }
    private IEnumerator RollTheDice(){
        int randomDiceSide = 0;
        int finalSide = 0;

        for(int i=0; i <= 20; i++){

            randomDiceSide = Random.Range(0,5);
            rend.sprite = diceSides[randomDiceSide];

            yield return new WaitForSeconds(0.05f);
        }

        finalSide = randomDiceSide + 1;
        result = finalSide;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
