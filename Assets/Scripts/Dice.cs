using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    private Sprite[] diceSides;
    private SpriteRenderer rend;
    public static int result;
    public static bool canClick;
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
        
    }
    private void OnMouseDown() {
        if(canClick){
            StartCoroutine("RollTheDice");
        }    
    }
    private IEnumerator RollTheDice(){
        int randomDiceSide = 0;
        int finalSide = 0;

        for(int i=0; i <= 20; i++){

            randomDiceSide = Random.Range(0,6);
            rend.sprite = diceSides[randomDiceSide];

            yield return new WaitForSeconds(0.05f);
        }

        finalSide = randomDiceSide + 1;
        result = finalSide;
        canClick = false;
        Pions.canMove = true;


    }
    void Update(){
        // Debug.Log(canClick);
    }

    // Update is called once per frame
    public static void updateTour(){
        switch (couleur_state)
            {
                case COULEUR_STATE.VERT:
                    couleur_state = COULEUR_STATE.JAUNE; 
                    break;
                case COULEUR_STATE.JAUNE:
                    couleur_state = COULEUR_STATE.BLEU;
                    break;
                case COULEUR_STATE.BLEU:
                    couleur_state = COULEUR_STATE.ROUGE;
                    break;
                case COULEUR_STATE.ROUGE:
                    couleur_state = COULEUR_STATE.VERT;
                    break;
            }
    }
    
}
