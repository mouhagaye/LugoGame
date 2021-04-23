using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    // Start is called before the first frame update
    // public static int[] barrageArray = new int[76];
    // public Transform CurrentPion;
    public int V_actuel;
    public int J_actuel;
    public int R_actuel;
    public int B_actuel;

    public Transform currentPion;


    void Start()
    {
        V_actuel = 0;
        J_actuel = 0;
        R_actuel = 0;
        B_actuel = 0;
        
    }
    
  

    
    public void barrageCheck(){
    for(int j = 0; j < 4; j++){
        switch(Pions.currentTour){
            case Pions.COULEUR.VERT:
                currentPion = Pions.VERT.transform.GetChild(j);
                if(currentPion.GetComponent<Pions>().isOut){
                    if((currentPion.position - transform.position).sqrMagnitude <= 0.002f){
                    transform.GetComponent<Points>().V_actuel++;
                    break;
                    }
                    
                }
                break;
            case Pions.COULEUR.JAUNE:
                currentPion = Pions.JAUNE.transform.GetChild(j);
                if(currentPion.GetComponent<Pions>().isOut){
                    if((currentPion.position - transform.position).sqrMagnitude <= 0.002f){
                    transform.GetComponent<Points>().J_actuel++;
                    break;
                    }
                }
            break;
            case Pions.COULEUR.BLEU:
                currentPion = Pions.BLEU.transform.GetChild(j);
                if(currentPion.GetComponent<Pions>().isOut){
                    if((currentPion.position - transform.position).sqrMagnitude <= 0.002f){
                    transform.GetComponent<Points>().J_actuel++;
                    break;
                    }
                }
            break;case Pions.COULEUR.ROUGE:
                currentPion = Pions.ROUGE.transform.GetChild(j);
                if(currentPion.GetComponent<Pions>().isOut){
                    if((currentPion.position - transform.position).sqrMagnitude <= 0.002f){
                    transform.GetComponent<Points>().J_actuel++;
                    break;
                    }
                }
            break;
            }
        
        }
    }
   

}