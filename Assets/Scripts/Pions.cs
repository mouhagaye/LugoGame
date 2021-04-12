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
    public GameObject currentPoint;
    public Transform destination;
    public Transform home;
    public bool isOut;

    public static bool VertBlock;
    public static bool JauneBlock;
    public static bool BleuBlock;
    public static bool RougeBlock;
    public static int Vindex;
    public static int Jindex;
    public static int Bindex;
    public static int Rindex;



    public float moveSpeed = 2f;
    int distance = 0;
    public enum COULEUR
    {
        VERT,
        JAUNE,
        ROUGE,
        BLEU,
        FIN
    }
    public COULEUR pionCouleur;
    public COULEUR currentTour;

    public int Vinitial;
    public int Jinitial;
    public int Rinitial;
    public int Binitial;

    public int Vin;
    public int Jin;
    public int Rin;
    public int Bin;

    public static int Vout;
    public static int Jout;
    public static int Rout;
    public static int Bout;


     

    // Start is called before the first frame update
    void Start()
    {
        Vinitial = 0;
        Jinitial = 57;
        Rinitial = 19;
        Binitial = 38;

        Vin = 1;
        Jin = 1;
        Rin = 1;
        Bin = 1;

        Vout = 0;
        Jout = 0;
        Rout = 0;
        Bout = 0;

        isOut = false;

        for (int i = 0; i <= 75; i++){
            pointsIndex[i] = i;
        }
        switch (pionCouleur)
        {
            case COULEUR.VERT:
                currentIndex = Vinitial;
                break;
            case COULEUR.JAUNE:
                currentIndex = Jinitial;
                break;
            case COULEUR.ROUGE:
                currentIndex = Rinitial;
                break;
            case COULEUR.BLEU:
                currentIndex = Binitial;
                break;
        }

        currentTour = COULEUR.ROUGE;
        canMove = true;
        VertBlock = false;
        JauneBlock = false;
        RougeBlock = true;
        BleuBlock = true;

        
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
                    switch(pionCouleur)
                    {
                        case COULEUR.VERT:
                            VertBlock = true;
                            break;
                        case COULEUR.JAUNE:
                            JauneBlock = true;
                            break;
                        case COULEUR.ROUGE:
                            BleuBlock = true;
                            break;
                        case COULEUR.BLEU:
                            RougeBlock = true;
                            break;
                    }
                     Dice.updateTour();
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
                    if(Vout == 0){
                        distance = 0;
                    }
                    Vindex = currentIndex + distance;
                    if(currentIndex == Vinitial){
                        Vout = Vout+1;
                    }
                    
                    break;
                case COULEUR.JAUNE:
                    if(nextIndex == 11 || nextIndex == 30 || nextIndex == 68){
                        currentIndex += 6;
                        }
                    if(Jout == 0){
                        distance = 0;
                    }
                    Jindex = currentIndex + distance;
                    if(currentIndex == Jinitial){
                        Jout = Jout+1;
                    }

                    break;
                case COULEUR.ROUGE:
                    if(nextIndex == 68 || nextIndex == 30 || nextIndex == 49){
                        currentIndex += 6;
                        }
                        Rindex = currentIndex + distance;
                        if(currentIndex == Rinitial){
                        Rout = Rout+1;
                        }

                    break;
                case COULEUR.BLEU:
                    if(nextIndex == 11 || nextIndex == 68 || nextIndex == 49){
                        currentIndex += 6;
                        }
                    Bindex = currentIndex + distance;
                    if(currentIndex == Binitial){
                        Bout = Bout+1;
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

        if (Dice.result != 6){
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

