using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviour
{
    private Sprite[] _diceRenderedSide;
    private SpriteRenderer _diceSpriteRenderer;
    public static int DiceRealValue;
    public static int sixCounts;
    
    // Start is called before the first frame update
    void Start()
    {
        // Rendering Dice sprite with images front Asset
        _diceSpriteRenderer = GetComponent<SpriteRenderer>();
        _diceRenderedSide = Resources.LoadAll<Sprite>("Dice/");
        
    }
    private void OnMouseDown() {
        // Call the function `RollTheDice` with Mouse is down on this Component.
        StartCoroutine("RollTheDice");    
    }
    
    // Rolling the dice.
    private IEnumerator RollTheDice(){
        // Initialize the random dice value to 0
        int diceRandomSide = 0;
        
        // Get random value between 0 and 5 20 times for rolling dice.
        for(int i=0; i <= 20; i++){
            diceRandomSide = Random.Range(0,5);
            _diceSpriteRenderer.sprite = _diceRenderedSide[diceRandomSide];
            yield return new WaitForSeconds(0.05f);
        }
        
        // After rolling loop end return the last dice index and add 1 to get its value.
        // (Because index start with 0 and value start with 1)
        DiceRealValue = diceRandomSide + 1;
        
        // Check if result is six
        if (DiceRealValue == 6)
        {
            
        }

    }
}
