using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
     // Start is called before the first frame update
    public static int[] barrageArray = new int[76];
    // public Transform currentPion;

    void Start()
    {
        for (int i = 0; i <= 75; i++){
            barrageArray[i] = 0;
        }
    }
    
  

    // Update is called once per frame
    void Update()
    {
        
    }
    // public void barrageCheck(){
    //     for(int i = 0; i < 76; i++){
    //         for(int j = 0; j < 4; j++){
    //             switch(Pions.currentTour){
    //                 case Pions.COULEUR.VERT:
    //                     currentPion = Pions.VERT.transform.GetChild(j);
    //                     if(currentPion.GetComponent<Pions>().isOut){
    //                         if((currentPion.position - transform.GetChild(i).position).sqrMagnitude <= 0.002f){
    //                         transform.GetChild(i).GetComponent<Points>().V_actuel++;
    //                         break;
    //                         }
    //                         else{
    //                             transform.GetChild(i).GetComponent<Points>().V_actuel--;
    //                             break;
    //                         }
    //                     }
    //                  break;
    //                 case Pions.COULEUR.JAUNE:
    //                     currentPion = Pions.JAUNE.transform.GetChild(j);
    //                     if(currentPion.GetComponent<Pions>().isOut){
    //                         if((currentPion.position - transform.GetChild(i).position).sqrMagnitude <= 0.002f){
    //                         transform.GetChild(i).GetComponent<Points>().J_actuel++;
    //                         break;
    //                         }
    //                         else{
    //                             transform.GetChild(i).GetComponent<Points>().J_actuel--;
    //                             break;
    //                         }
    //                     }
    //                 break;
    //                 }
                
    //         }
    //     }
   
    // }
}
