using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    private Sprite[] diceSides;
    private SpriteRenderer rend;
    public Text TextUI;
    public static int result;
    public static bool canClick;
    public static bool FinPartie;

    public int randomDiceSide = 0;
    public int finalSide = 0;
    public Transform currentPion;
    public int index = 0;
    public GameObject arrow;


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
        TextUI.text = "C'est parti !";
        arrow = GameObject.FindGameObjectWithTag("arrow");
        arrow.SetActive(true);
        
    }
    void Update(){
        switch(couleur_state){
            case COULEUR_STATE.VERT:
                TextUI.text = "tour de VERT";
                TextUI.color = Color.green;
            break;
            case COULEUR_STATE.JAUNE:
                TextUI.text = "tour de JAUNE";
                TextUI.color = Color.yellow;
            break;
            case COULEUR_STATE.BLEU:
                TextUI.text = "tour de BLEU";
                TextUI.color = Color.blue;
            break;case COULEUR_STATE.ROUGE:
                TextUI.text = "tour de ROUGE";
                TextUI.color = Color.red;
            break;
        }
        arrow.SetActive(canClick);
    }
    private void OnMouseDown() {
        arrow.SetActive(false);
        if(canClick){
            StartCoroutine("RollTheDice");
        }    
    }
    private IEnumerator RollTheDice(){

        for(int i=0; i <= 20; i++){

            randomDiceSide = Random.Range(3,6);
            rend.sprite = diceSides[randomDiceSide];

            yield return new WaitForSeconds(0.05f);
        }

        finalSide = randomDiceSide + 1;
        result = finalSide;
        canClick = false;
        Pions.canMove = true;
        arrow.SetActive(false);

        
        // Debug.Log(couleur_state);

        switch(couleur_state){
            case COULEUR_STATE.VERT:

                Pions.Vhome = false;
                    for(int i = 0 ; i < 4 ; i++){
                        currentPion = Pions.VERT.transform.GetChild(i);
                        if(!currentPion.gameObject.GetComponent<Pions>().isHoming && currentPion.gameObject.GetComponent<Pions>().isOut){
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
            
                     if(!currentPion.gameObject.GetComponent<Pions>().isHoming && currentPion.gameObject.GetComponent<Pions>().isOut){
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
            Pions.Rhome = false;
              for(int i = 0 ; i < 4 ; i++){
                    currentPion = Pions.ROUGE.transform.GetChild(i);
                     if(!currentPion.gameObject.GetComponent<Pions>().isHoming && currentPion.gameObject.GetComponent<Pions>().isOut){
                    
                         Pions.Rhome = true;
                         break;
                     }
                }
                if((Pions.Rout == 0 || !Pions.Rhome )&& result != 6 ){
                    updateTour();
                    canClick = true;
                    Pions.canMove = false;
                }
            break;
            case COULEUR_STATE.BLEU:
                Pions.Bhome = false;
              for(int i = 0 ; i < 4 ; i++){
                    currentPion = Pions.BLEU.transform.GetChild(i);
                     if(!currentPion.gameObject.GetComponent<Pions>().isHoming && currentPion.gameObject.GetComponent<Pions>().isOut){
                    
                         Pions.Bhome = true;
                         break;
                     }
                }
                if((Pions.Bout == 0 || !Pions.Bhome )&& result != 6 ){
                    updateTour();
                    canClick = true;
                    Pions.canMove = false;
                }
            break;
        }


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
