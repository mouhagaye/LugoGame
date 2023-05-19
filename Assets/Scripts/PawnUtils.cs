using System.Collections;
using PawnNamespace;
using UnityEngine;

public class PawnUtils: MonoBehaviour
{
    // Board Variables
    public GameObject boardCases;
    private static readonly int BoardCaseCount = 76;
    private readonly int[] _boardCasesCoordinates = new int[BoardCaseCount];
    
    // Pawn Variables
    public float pawnMoveSpeed = 2f;
    private int _pawnRunNextCoordinate;
    private int _pawnCurrentCoordinate;
    private int _pawnMoveDistance;
    public Transform pawnDestinationCase;
    public PawnColor pawnColor;
    private Pawn _pawnObject;
    
    // PawnSet Variable
    public PawnSet greenPawnSets;
    public PawnSet yellowPawnSets;
    
    public static PawnSet CurrentPawnSets;
    
    // Start is called before the first frame update
    void Start()
    {
        // Set Pawn object to current game object.
        _pawnObject = this.gameObject.AddComponent<Pawn>();
        
        // Set color to pawn object and init all pawn attribute
        _pawnObject.SetPawnColor(pawnColor);
        _pawnCurrentCoordinate = _pawnObject.pawnInitialCase;
        _pawnObject.pawnIsInside = true;
        _pawnObject.pawnIsOut = false;
        
        // Set Board case coordinates
        for (int i = 0; i <= (BoardCaseCount - 1); i++){
            _boardCasesCoordinates[i] = i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        // Update current pawn set
        switch (GameController.CurrentTurnColor)
        {
            case PawnColor.Green:
                CurrentPawnSets = greenPawnSets;
                break;
            case PawnColor.Yellow:
                CurrentPawnSets = yellowPawnSets;
                break;
        }

        if (this.pawnColor != GameController.CurrentTurnColor)
            _pawnObject.pawnIsBlock = true;
        
        Debug.Log("Dice clickagle: " + Dice.DiceClickable);
        
    }

    // Called when mouse is down in Pawn game object.
    private void OnMouseDown(){
        // Move if current tour allowed it.
        if(pawnCanRun()) {
            StartCoroutine(nameof(MovePawn));
            
        }
    }

    private bool pawnCanRun()
    {
        if (_pawnObject.currentPawnColor == GameController.CurrentTurnColor ||
            !Dice.DiceClickable)
            return true;
        else
            return false;
    }
    

    // Moving Pawn
    IEnumerator MovePawn(){

        if (_pawnObject.pawnIsInside && Dice.SixCounts > 0) {
            // Decrement six count
            Dice.SixCounts--;
            _pawnObject.pawnIsInside = false;
            _pawnObject.pawnIsOut = true;
            CurrentPawnSets.pawnsIn--;
            CurrentPawnSets.pawnsOut++;
            _pawnCurrentCoordinate = _pawnObject.pawnInitialCase;

        }
        else if (Dice.SixCounts > 1)
        {
            _pawnMoveDistance = 6;
            Dice.SixCounts--;
        }
        else if (Dice.SixCounts == 1)
        {
            _pawnMoveDistance = Dice.DiceRollingResult + 6;
        }
        else
        {
            // Get pawn run distance from dice.
            _pawnMoveDistance = Dice.DiceRollingResult;
        }

        // Loop to move pawn case by case.
        for (int i = 0; i <= _pawnMoveDistance; i++){
            
            // pawn's next index should be between 0 and board point count.
            _pawnRunNextCoordinate = (_pawnCurrentCoordinate + i) % BoardCaseCount;
            
            // Jump by 5 case to avoid pawns run on disallowed case.

            foreach (var unAllowedCoordination in _pawnObject.pawnUnAllowedCoordinates)
            {
                if (_pawnRunNextCoordinate == unAllowedCoordination){
                    _pawnCurrentCoordinate += 6;
                }
            }
            // Setting the destination case.
            pawnDestinationCase = boardCases.transform.GetChild(_pawnRunNextCoordinate);
            
            // Move toward destination while not close to destination.
            while ((transform.position - pawnDestinationCase.position).sqrMagnitude > 0.001f){
                transform.position = Vector2.MoveTowards(transform.position, pawnDestinationCase.position, pawnMoveSpeed*Time.deltaTime );
            }
            yield return new WaitForSeconds(0.5f);
        }  
        
        
        // Update pawn coordinate.
        _pawnCurrentCoordinate = _pawnRunNextCoordinate;
        
        
        // Update Dice clickability
        GameController.PawnTurnUpdater();
    }
}
