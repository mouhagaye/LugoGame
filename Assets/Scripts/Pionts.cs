using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pionts : MonoBehaviour
{
    // Start is called before the first frame update
    public static int[] barrangeCheck = new int[76];
    public Transform CurrentPion;
    public Transform TempPion;

    void Start()
    {
        for (int i = 0; i <= 75; i++){
            barrangeCheck[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 4; i ++){
            TempPion = Pions.VERT.transform.GetChild(i);
            if((transform.position - TempPion.position).sqrMagnitude <= 0.002f){
                CurrentPion = Pions.VERT.transform.GetChild(i);
            }
            TempPion = Pions.JAUNE.transform.GetChild(i);
            if((transform.position - TempPion.position).sqrMagnitude <= 0.002f){
                CurrentPion = Pions.JAUNE.transform.GetChild(i);
            }
            TempPion = Pions.ROUGE.transform.GetChild(i);
            if((transform.position - TempPion.position).sqrMagnitude <= 0.002f){
                CurrentPion = Pions.ROUGE.transform.GetChild(i);
            }
            TempPion = Pions.BLEU.transform.GetChild(i);
            if((transform.position - TempPion.position).sqrMagnitude <= 0.002f){
                CurrentPion = Pions.BLEU.transform.GetChild(i);
            }

        }
    }
}
