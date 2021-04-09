using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pions : MonoBehaviour
{
    public GameObject points;
    private int[] pointsIndex = new int[76];
    private int nextIndex = 0;
    private int i;
    private int currentIndex;
    public static bool canMove;
    public Transform currentPoint;
    public Transform destination;
    public Transform home;

    public float moveSpeed = 2f;
    int distance = 0;
    public enum COULEUR
    {
        VERT,
        JAUNE,
        ROUGE,
        BLEU
    }
    public COULEUR pionCouleur;
    public COULEUR currentTour;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <= 75; i++){
        pointsIndex[i] = i;
        }
        switch (pionCouleur)
        {
            case COULEUR.VERT:
                currentIndex = 0;
                break;
            case COULEUR.JAUNE:
                currentIndex = 57;
                break;
            case COULEUR.ROUGE:
                currentIndex = 19;
                break;
            case COULEUR.BLEU:
                currentIndex = 38;
                break;
        }

        currentTour = COULEUR.ROUGE;
        canMove = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        

        switch(pionCouleur)
        {
                case COULEUR.VERT:
                    home = points.transform.GetChild(74);
                    break;
                case COULEUR.JAUNE:
                    home = points.transform.GetChild(55);
                    break;
                case COULEUR.ROUGE:
                    home = points.transform.GetChild(17);
                    break;
                case COULEUR.BLEU:
                    home = points.transform.GetChild(36);
                    break;
            }
             if((transform.position - home.position).sqrMagnitude <= 0.005f){
                             transform.gameObject.SetActive(false);
                             Debug.Log("Home na !!!!!!!!!!!");
                     } 

        updateTour();
        

        Debug.Log(currentTour);

        
    }
    private void OnMouseDown(){
        if (currentTour == pionCouleur && canMove){
            StartCoroutine("Move");
        }
    }
    IEnumerator Move(){
         
        distance = Dice.result;
        for (int i = 0; i <= distance; i++){
            nextIndex = (currentIndex + i)%76;

            switch (pionCouleur)
            {
                case COULEUR.VERT:
                    if(nextIndex == 11 || nextIndex == 30 || nextIndex == 49){
                        currentIndex += 6;
                        }
                    break;
                case COULEUR.JAUNE:
                    if(nextIndex == 11 || nextIndex == 30 || nextIndex == 68){
                        currentIndex += 6;
                        }
                    break;
                case COULEUR.ROUGE:
                    if(nextIndex == 68 || nextIndex == 30 || nextIndex == 49){
                        currentIndex += 6;
                        }
                    break;
                case COULEUR.BLEU:
                    if(nextIndex == 11 || nextIndex == 68 || nextIndex == 49){
                        currentIndex += 6;
                        }
                    break;
            }

            destination = points.transform.GetChild(nextIndex);
                while ((transform.position - destination.position).sqrMagnitude >= 0.001f) 
                    {   
                    transform.position = Vector2.MoveTowards(transform.position, destination.position, moveSpeed*Time.deltaTime ); 
                    
                }
            yield return new WaitForSeconds(0.5f);
        }
       
        currentIndex = nextIndex;

        if (distance != 6){
            Dice.updateTour();
        }
        Dice.canClick = true;
        canMove = false;


    

    }
    void updateTour(){
        switch(Dice.couleur_state){
            case Dice.COULEUR_STATE.VERT:
                currentTour = COULEUR.VERT;
                break;
            case Dice.COULEUR_STATE.JAUNE:
                currentTour = COULEUR.JAUNE;
                break;
            case Dice.COULEUR_STATE.ROUGE:
                currentTour = COULEUR.ROUGE;
                break;
            case Dice.COULEUR_STATE.BLEU:
                currentTour = COULEUR.BLEU;
                break;
        }
    }
}
// void updateTour(){
//         switch(Dice.couleur_state){
//             case Dice.COULEUR_STATE.VERT:
//                 if(transform.gameObject.active){
//                     currentTour = COULEUR.VERT;
//                 }
//                 else{
//                     currentTour = COULEUR.JAUNE;
//                 }
//                 break;
//             case Dice.COULEUR_STATE.JAUNE:
//                 if(transform.gameObject.active){
//                     currentTour = COULEUR.JAUNE;
//                 }
//                 else{
//                     currentTour = COULEUR.BLEU;
//                 }
//                 break;
//             case Dice.COULEUR_STATE.ROUGE:
//                 if(transform.gameObject.active){
//                     currentTour = COULEUR.ROUGE;
//                 }
//                 else{
//                     currentTour = COULEUR.VERT;
//                 }
//                 break;
//             case Dice.COULEUR_STATE.BLEU:
//                 if(transform.gameObject.active){
//                     currentTour = COULEUR.BLEU;
//                 }
//                 else{
//                     currentTour = COULEUR.ROUGE;
//                 }
//                 break;
//         }
//     }
// }
