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
    public Transform currentPoint;
    public Transform destination;
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

        currentTour = COULEUR.VERT;
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Dice.result;
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
        Debug.Log(currentTour);

        
    }
    private void OnMouseDown(){
        if (currentTour == pionCouleur){
            StartCoroutine("Move");
        }
    }
    IEnumerator Move(){
         
       
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
        Debug.Log("la distance est ega la :"+distance);

        Debug.Log("Dokhba parei ");
        if (distance != 6){
            Dice.updateTour();
        }

    

    }
    void updateTour(){
        switch (currentTour)
            {
                case COULEUR.VERT:
                    currentTour = COULEUR.JAUNE; 
                    break;
                case COULEUR.JAUNE:
                    currentTour = COULEUR.BLEU;
                    break;
                case COULEUR.ROUGE:
                    currentTour = COULEUR.VERT;
                    break;
                case COULEUR.BLEU:
                    currentTour = COULEUR.ROUGE;
                    break;
            }
    }
}
