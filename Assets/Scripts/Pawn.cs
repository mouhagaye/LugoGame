
using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace PawnNamespace
{ 
    // Setting All possible pawn's color.
    public enum PawnColor {Green, Yellow, Blue, Red}
    
    public class Pawn : MonoBehaviour
    {
        // Pawn attribute.
        public PawnColor currentPawnColor;
        public int[] pawnUnAllowedCoordinates;
        public int pawnInitialCase;
        public bool pawnIsBlock;
        public bool pawnIsInside;
        public bool pawnIsOut;
        
        // Set pawn color and Initialize Pawn Attribute according to Color.
        public void SetPawnColor(PawnColor newPawnColor){
            this.currentPawnColor = newPawnColor;
            this.pawnIsBlock = true;
            this.pawnIsInside = true;
            this.InitPawn();
        }
        
        // Init Pawn
        public void InitPawn()
        {
            switch (this.currentPawnColor)
            {
                case PawnColor.Green:
                    this.pawnInitialCase = 0;
                    this.pawnUnAllowedCoordinates = new int[] { 11, 30, 49 };
                    break;
                case PawnColor.Yellow:
                    this.pawnInitialCase = 57;
                    this.pawnUnAllowedCoordinates = new int[] { 11, 30, 68 };
                    break;
            }
        }

        private void Update()
        {
            if (GameController.CurrentTurnColor != this.currentPawnColor)
            {
                this.pawnIsBlock = true;
            }
        }
    }
}