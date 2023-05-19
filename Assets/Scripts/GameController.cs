using PawnNamespace;
using UnityEditor;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Pawn relative variable
    public static PawnColor CurrentTurnColor;

    // Start is called before the first frame update
    void Start()
    {
        CurrentTurnColor = PawnColor.Green;
    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.Log(CurrentTurnColor);
        Debug.Log("Current pawns set PAWN IN : " + PawnUtils.CurrentPawnSets.pawnsIn);
        Debug.Log("Current pawns set PAWN OUT : " + PawnUtils.CurrentPawnSets.pawnsOut);
    }

    public static void DiceTurnUpdater() {
        if (PawnUtils.CurrentPawnSets.pawnsOut == 0 && Dice.SixCounts == 0)
        {
            TurnSwitcher();
        }
    }
    public static void PawnTurnUpdater() {
        if (Dice.SixCounts == 0)
        {
            TurnSwitcher();
        }
    }
    
    public static void TurnSwitcher()
    {
       switch (CurrentTurnColor)
        {
            case PawnColor.Green:
                CurrentTurnColor = PawnColor.Yellow;
                Dice.DiceClickable = true;
                break;
            case PawnColor.Yellow:
                CurrentTurnColor = PawnColor.Green;
                Dice.DiceClickable = true;
                break;
            
        }
    }

}
