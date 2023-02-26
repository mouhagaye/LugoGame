using System.Collections;
using PawnNamespace;
using UnityEngine;

public class PawnUtils: MonoBehaviour
{
    // Board Variables
    public GameObject boardCases;
    private static readonly int _boardCaseCount = 72;
    private readonly int[] _boardCasesCoordinates = new int[_boardCaseCount];
    
    // Pawn Variables
    public float pawnMoveSpeed = 2f;
    private int _pawnRunNextCoordinate;
    private int _pawnCurrentCoordinate;
    private int _pawnMoveDistance;
    public Transform pawnDestinationCase;
    public PawnColor pawnPawnColor;
    private readonly Pawn _pawnObject = new Pawn();
    
    // Start is called before the first frame update
    void Start()
    {
        // Set color to pawn object and init all pawn attribute
        _pawnObject.SetPawnColor(pawnPawnColor);
        _pawnCurrentCoordinate = _pawnObject.PawnInitialCase;
        this.GetComponent<Renderer>().material.color = _pawnObject.PawnPawnColorCode;

        for (int i = 0; i <= (_boardCaseCount - 1); i++){
            _boardCasesCoordinates[i] = i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get pawn run distance from dice.
        _pawnMoveDistance = Dice.DiceRealValue;
    }
    
    // Called when mouse is down in Pawn game object.
    private void OnMouseDown(){
        // StartCoroutine("Move");
            StartCoroutine(nameof(MovePawn));
    }
    
    // Moving Pawn
    IEnumerator MovePawn(){
        // Loop to move pawn case by case.
        for (int i = 0; i <= _pawnMoveDistance; i++){
            
            // pawn's next index should be between 0 and board point count.
            _pawnRunNextCoordinate = (_pawnCurrentCoordinate + i) % (_boardCaseCount - 1);
            
            // Jump by 5 case to avoid pawns run on disallowed case.

            foreach (var unAllowedCoordination in _pawnObject.PawnUnAllowedCoordinates)
            {
                if (_pawnRunNextCoordinate == unAllowedCoordination){
                    _pawnCurrentCoordinate += 5;
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

    }
}
