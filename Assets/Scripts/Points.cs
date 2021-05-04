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
    public GameObject point;
    public int index;

    void Start()
    {
        V_actuel = 0;
        J_actuel = 0;
        R_actuel = 0;
        B_actuel = 0;
        point = GameObject.FindGameObjectWithTag("points");
        for (int i = 0; i < 76; i++){
            if(point.transform.GetChild(i).position == transform.position){
                index = i;
                break;
            }
        }

        
    }
    
  

    
    public void barrageCheck(GameObject currentPion){

        switch(currentPion.GetComponent<Pions>().pionCouleur){
            case Pions.COULEUR.VERT:
                if(currentPion.GetComponent<Pions>().isOut){
                    if(currentPion.GetComponent<Pions>().currentIndex == index){
                        transform.GetComponent<Points>().V_actuel++;
                    }
                }
            break;
            case Pions.COULEUR.JAUNE:
                if(currentPion.GetComponent<Pions>().isOut){
                    if(currentPion.GetComponent<Pions>().currentIndex == index){
                        transform.GetComponent<Points>().J_actuel++;
                    }
                }
            break;
            case Pions.COULEUR.BLEU:
                if(currentPion.GetComponent<Pions>().isOut){
                    if(currentPion.GetComponent<Pions>().currentIndex == index){
                        transform.GetComponent<Points>().B_actuel++;
                    }
                }
            break;
            case Pions.COULEUR.ROUGE:
                if(currentPion.GetComponent<Pions>().isOut){
                    if(currentPion.GetComponent<Pions>().currentIndex == index){
                        transform.GetComponent<Points>().R_actuel++;
                    }
                }
            break;
        }

        

    }
   

}