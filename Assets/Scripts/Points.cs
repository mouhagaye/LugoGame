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

    private Transform currentPion;


    void Start()
    {
        V_actuel = 0;
        J_actuel = 0;
        R_actuel = 0;
        B_actuel = 0;
        
    }
    
  

    
    public void barrageCheck(GameObject currentPion){

        switch(currentPion.GetComponent<Pions>().pionCouleur){
            case Pions.COULEUR.VERT:
                if(currentPion.GetComponent<Pions>().isOut){
                    if((currentPion.transform.position - transform.position).sqrMagnitude <= 0.002f){
                        transform.GetComponent<Points>().V_actuel++;
                    }
                    else{
                        transform.GetComponent<Points>().V_actuel--;
                    }
                }
            break;
            case Pions.COULEUR.JAUNE:
                if(currentPion.GetComponent<Pions>().isOut){
                    if((currentPion.transform.position - transform.position).sqrMagnitude <= 0.002f){
                        transform.GetComponent<Points>().J_actuel++;
                    }
                    else{
                        transform.GetComponent<Points>().J_actuel--;
                    }
                }
            break;
            case Pions.COULEUR.BLEU:
                if(currentPion.GetComponent<Pions>().isOut){
                    if((currentPion.transform.position - transform.position).sqrMagnitude <= 0.002f){
                        transform.GetComponent<Points>().B_actuel++;
                    }
                    else{
                        transform.GetComponent<Points>().B_actuel--;
                    }
                }
            break;
            case Pions.COULEUR.ROUGE:
                if(currentPion.GetComponent<Pions>().isOut){
                    if((currentPion.transform.position - transform.position).sqrMagnitude <= 0.002f){
                        transform.GetComponent<Points>().R_actuel++;
                    }
                    else{
                        transform.GetComponent<Points>().R_actuel--;
                    }
                }
            break;
        }

        

    }
   

}