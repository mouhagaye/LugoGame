using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    private Sprite[] diceSides;
    private SpriteRenderer rend;
    public static int result;
    public static bool canClick;
    public static bool FinPartie;

    public int randomDiceSide = 0;
    public int finalSide = 0;
    public Transform currentPion;
    public int index = 0;


    public enum COULEUR_STATE{
        VERT,
        JAUNE,
        BLEU,
        ROUGE
    }
    public static COULEUR_STATE couleur_state;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        diceSides = Resources.LoadAll<Sprite>("Dice/");
        couleur_state = COULEUR_STATE.VERT;
        canClick = true;
        FinPartie = false;
        
    }
    private void OnMouseDown() {
        if(canClick){
            StartCoroutine("RollTheDice");
        }    
    }
    private IEnumerator RollTheDice(){

        for(int i=0; i <= 20; i++){

            randomDiceSide = Random.Range(0,6);
            rend.sprite = diceSides[randomDiceSide];

            yield return new WaitForSeconds(0.05f);
        }

        finalSide = randomDiceSide + 1;
        result = finalSide;
        canClick = false;
        Pions.canMove = true;

        
        // Debug.Log(couleur_state);

        switch(couleur_state){
            case COULEUR_STATE.VERT:
                Pions.Vhome = false;
                    for(int i = 0 ; i < 4 ; i++){
                        currentPion = Pions.VERT.transform.GetChild(i);
                        if(currentPion.gameObject.GetComponent<Pions>().currentIndex + result < 75 && currentPion.gameObject.GetComponent<Pions>().isOut){
                            Pions.Vhome = true;
                            break;
                        }
                    }
                if((Pions.Vout == 0  || !Pions.Vhome) && result != 6 ){

                    updateTour();
                    canClick = true;
                    Pions.canMove = false;
                }
            break;
            case COULEUR_STATE.JAUNE:
            Pions.Jhome = false;
              for(int i = 0 ; i < 4 ; i++){
                    currentPion = Pions.JAUNE.transform.GetChild(i);
                    if(currentPion.gameObject.GetComponent<Pions>().currentIndex > 56){
                        index = currentPion.gameObject.GetComponent<Pions>().currentIndex - 57;
                    }
                    else{
                        index = currentPion.gameObject.GetComponent<Pions>().currentIndex - 57 + 75;
                    }
                    Debug.Log(index);
                     if(index + result < 75 && currentPion.gameObject.GetComponent<Pions>().isOut){
                         Pions.Jhome = true;
                         break;
                     }
                }
                if((Pions.Jout == 0 || !Pions.Jhome )&& result != 6 ){
                    updateTour();
                    canClick = true;
                    Pions.canMove = false;
                }
            break;
            case COULEUR_STATE.ROUGE:
                if((Pions.Rout == 0 && result != 6 ) ||  Pions.Jindex < 17 && (Pions.Rindex + result > 17)){
                    updateTour();
                    canClick = true;
                    Pions.canMove = false;
                }
            break;
            case COULEUR_STATE.BLEU:
                if((Pions.Bout == 0 && result != 6 ) || Pions.Jindex < 36 && (Pions.Bindex + result > 36)){
                    updateTour();
                    canClick = true;
                    Pions.canMove = false;
                }
            break;
        }


    }
    void Update(){
        // Debug.Log(canClick);
    }

    // Update is called once per frame
    public static void updateTour(){
        switch (couleur_state)
            {
                case COULEUR_STATE.VERT:
                    if (!Pions.JauneBlock){
                        couleur_state = COULEUR_STATE.JAUNE;
                    }
                    else if (!Pions.BleuBlock){
                        couleur_state = COULEUR_STATE.BLEU;
                    }
                    else if (!Pions.RougeBlock){
                        couleur_state = COULEUR_STATE.ROUGE;
                    }
                    else{
                        FinPartie = true;
                    }

                    break;
                case COULEUR_STATE.JAUNE:
                    if (!Pions.BleuBlock){
                        couleur_state = COULEUR_STATE.BLEU;
                    }
                    else if (!Pions.RougeBlock){
                        couleur_state = COULEUR_STATE.ROUGE;
                    }
                    else if (!Pions.VertBlock){
                        couleur_state = COULEUR_STATE.VERT;
                    }
                    else{
                        FinPartie = true;
                    }
                    break;
                case COULEUR_STATE.BLEU:
                    if (!Pions.RougeBlock){
                        couleur_state = COULEUR_STATE.ROUGE;
                    }
                    else if (!Pions.VertBlock){
                        couleur_state = COULEUR_STATE.VERT;
                    }
                    else if (!Pions.JauneBlock){
                        couleur_state = COULEUR_STATE.JAUNE;
                    }
                    else{
                        FinPartie = true;
                    }
                    break;
                case COULEUR_STATE.ROUGE:
                    if (!Pions.VertBlock){
                        couleur_state = COULEUR_STATE.VERT;
                    }
                    else if (!Pions.JauneBlock){
                        couleur_state = COULEUR_STATE.JAUNE;
                    }
                    else if (!Pions.BleuBlock){
                        couleur_state = COULEUR_STATE.BLEU;
                    }
                    else{
                        FinPartie = true;
                    }
                    break;
            }
    }
    
}
