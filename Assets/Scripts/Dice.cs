using System;
using System.Collections;
using PawnNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dice : MonoBehaviour
{
    // Dice Variable
    private Sprite[] _diceRenderedSide;
    private SpriteRenderer _diceSpriteRenderer;
    public static int DiceRollingResult;
    public static int SixCounts;
    public static bool DiceClickable;
    
    public int DiceMax = 6;
    public int DiceMin = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize variable
        SixCounts = 0;
        DiceClickable = true;

        // Rendering Dice sprite with images front Asset
        _diceSpriteRenderer = GetComponent<SpriteRenderer>();
        _diceRenderedSide = Resources.LoadAll<Sprite>("Dice/");
        
    }
    private void OnMouseDown() {
        // Call the function `RollTheDice` with Mouse is down on this Component.
        if (DiceClickable){
            StartCoroutine("RollTheDice");    
        }
    }

    private void Update()
    {
        Debug.Log(SixCounts);
    }

    // Rolling the dice.
    private IEnumerator RollTheDice(){
        // Initialize the variable
        int diceRandomSide = 0;
        DiceRollingResult = 0;
        
        // Get random value between 0 and 5 20 times for rolling dice.
        for(int i=0; i <= 20; i++){
            diceRandomSide = Random.Range(DiceMin,DiceMax);
            _diceSpriteRenderer.sprite = _diceRenderedSide[diceRandomSide];
            yield return new WaitForSeconds(0.05f);
        }
        
        // After rolling loop end return the last dice index and add 1 to get its value.
        // (Because index start with 0 and value start with 1)
        DiceRollingResult = diceRandomSide + 1;
        
        // Check if result is six
        if(DiceRollingResult != 6)
            DiceClickable = false;
        else
            SixCounts++;

        // Check Turn Updater
        GameController.TurnUpdater();
    }
}
