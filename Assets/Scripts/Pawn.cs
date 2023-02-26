
using UnityEngine;

namespace PawnNamespace
{ 
    // Setting All possible pawn's color.
    public enum PawnColor {Green, Yellow, Blue, Red}
    
    public class Pawn
    {
        // Pawn attribute.
        private PawnColor _currentPawnPawnColor;
        public int[] PawnUnAllowedCoordinates = new int[3];
        public int PawnInitialCase;
        public Color PawnPawnColorCode;
        public bool PawnIsOut;
        
        // Set pawn color and Initialize Pawn Attribute according to Color.
        public void SetPawnColor(PawnColor newPawnColor){
            this._currentPawnPawnColor = newPawnColor;
            this.InitPawn();
        }
        
        // Init Pawn
        public void InitPawn()
        {
            switch (this._currentPawnPawnColor)
            {
                case PawnColor.Green:
                    this.PawnInitialCase = 0;
                    this.PawnUnAllowedCoordinates = new int[] { 11, 29, 47 };
                    this.PawnPawnColorCode = new Color(55, 111, 55);
                    break;
                case PawnColor.Yellow:
                    this.PawnInitialCase = 57;
                    this.PawnUnAllowedCoordinates = new int[] { 11, 29, 47 };
                    this.PawnPawnColorCode = new Color(195, 176, 0);
                    break;
            }
        }

    }
}