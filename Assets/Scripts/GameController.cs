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
        // Debug
        Debug.Log(CurrentTurnColor);
    }

    public static void UpdateTurn()
    {
       switch (CurrentTurnColor)
        {
            case PawnColor.Green:
                CurrentTurnColor = PawnColor.Yellow;
                break;
            case PawnColor.Yellow:
                CurrentTurnColor = PawnColor.Green;
                break;
            
        }
    }

}
