using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pions : MonoBehaviour
{
    public GameObject points;
    private int[] pointsIndex = new int[76];
    private int nextIndex = 0;
    private int i;
    public int currentIndex;
    public static bool canMove;
    public bool block;

    public GameObject currentPoint;
    public Transform destination;
    public Transform home;
    public bool isOut;
    public bool isHoming;
    public bool catched;


    public static GameObject VERT;
    public static GameObject JAUNE;
    public static GameObject ROUGE;
    public static GameObject BLEU;

    public Transform currentPion;



    public static bool VertBlock;
    public static bool JauneBlock;
    public static bool BleuBlock;
    public static bool RougeBlock;

    public static bool Vhome;
    public static bool Jhome;
    public static bool Bhome;
    public static bool Rhome;

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

    public bool HomeTrigger;
     

    // Start is called before the first frame update
    void Start()
    {
        Vinitial = 0;
        Jinitial = 57;
        Rinitial = 19;
        Binitial = 38;

        Vin = 4;
        Jin = 4;
        Rin = 4;
        Bin = 4;

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


        Vhome = false;
        Jhome = false;
        Rhome = false;
        Bhome = false;

        VERT = GameObject.FindGameObjectWithTag("VERT");
        JAUNE = GameObject.FindGameObjectWithTag("JAUNE");
        ROUGE = GameObject.FindGameObjectWithTag("ROUGE");
        BLEU = GameObject.FindGameObjectWithTag("BLEU");


        catched = false;
        isHoming = false;
        block = false;
    }

    // Update is called once per frame
    void Update()
    {
        

        switch(pionCouleur)
        {
                case COULEUR.VERT:
                    home = points.transform.GetChild(74);
                    if(currentIndex > 68 && !isHoming){
                             isHoming = true;
                    }
                    HomeTrigger = false;
                    if(currentIndex + Dice.result > 75){
                        HomeTrigger = true;
                     }
                    break;
                case COULEUR.JAUNE:
                    home = points.transform.GetChild(55);
                    if(currentIndex < 56 && currentIndex > 49 && !isHoming){
                             isHoming = true;
                    }
                    HomeTrigger = false;
                    if((currentIndex + Dice.result > 55) && (currentIndex < 56)){
                        HomeTrigger = true;
                     }
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
                             canMove=false;
                             Dice.canClick = true;
                    switch(pionCouleur)
                      {
                          case COULEUR.VERT:
                              Vout--;
                              break;
                          case COULEUR.JAUNE:
                              Jout--;
                              break;
                          case COULEUR.ROUGE:
                              Bout--;
                              break;
                          case COULEUR.BLEU:
                              Rout--;
                              break;
                      }
                     Dice.updateTour();
            }
        
        updateTour();
        Debug.Log(Vhome);

      Debug.Log(currentTour);
    //   Debug.Log("VERT SORTIES :"+Vout);


        
    }
    private void OnMouseDown(){

        if (currentTour == pionCouleur && canMove && (!HomeTrigger)){
            StartCoroutine("Move");
        }
    }
    IEnumerator Move(){
         
        distance = Dice.result;
         if(!isOut && distance == 6){
                distance = 0;
                isOut = true;

                 switch (pionCouleur)
                {
                    case COULEUR.VERT:
                    if (currentIndex == Vinitial)
                        {
                            Vout = Vout+1;
                            Vin = Vin - 1;
                        }
                        
                        break;
                    case COULEUR.JAUNE:
                        if(currentIndex == Jinitial){
                            Jout = Jout+1;
                            Jin = Jin - 1;
                        }

                        break;
                    case COULEUR.ROUGE:
                        if(currentIndex == Rinitial){
                        Rout = Rout+1;
                        Rin = Rin - 1;

                        }

                        break;
                    case COULEUR.BLEU:
                        if(currentIndex == Binitial){
                            Bout = Bout+1;
                            Bin = Bin - 1;

                        }
                        break;
                }
            }
        if(catched){
                distance = 0;
                catched = false;
                isOut=false;
                switch(pionCouleur){
                    case COULEUR.VERT:
                        currentIndex = Vinitial;
                        transform.position = new Vector3(Random.Range(-30,-10)/10 , Random.Range(30,10)/10, 0);
                    break;
                    case COULEUR.JAUNE:
                        currentIndex = Jinitial;
                        transform.position = new Vector3(Random.Range(-30,-10)/10 , Random.Range(-20,-40)/10, 0);
                    break;
                    case COULEUR.ROUGE:
                        currentIndex = Rinitial;
                        transform.position = new Vector3(Random.Range(-30,-10)/10 , Random.Range(30,10)/10, 0);
                    break;
                    case COULEUR.BLEU:
                        currentIndex = Binitial;
                        transform.position = new Vector3(Random.Range(-30,-10)/10 , Random.Range(30,10)/10, 0);
                    break;
                }
            }
        if(isOut){
           
            for (int i = 0; i <= distance; i++){
                nextIndex = (currentIndex + i)%76;


                switch (pionCouleur)
                {
                    case COULEUR.VERT:
                        
                        if(nextIndex == 11 || nextIndex == 30 || nextIndex == 49){
                            currentIndex += 6;
                            }
                        Vindex = currentIndex + distance;
                        
                        break;
                    case COULEUR.JAUNE:
                        if(nextIndex == 11 || nextIndex == 30 || nextIndex == 68){
                            currentIndex += 6;
                            }
                        Jindex = currentIndex + distance;

                        break;
                    case COULEUR.ROUGE:
                        if(nextIndex == 68 || nextIndex == 30 || nextIndex == 49){
                            currentIndex += 6;
                            }
                            Rindex = currentIndex + distance;
                            if(currentIndex == Rinitial){
                            Rout = Rout+1;
                            Rin = Rin - 1;

                            }

                        break;
                    case COULEUR.BLEU:
                        if(nextIndex == 11 || nextIndex == 68 || nextIndex == 49){
                            currentIndex += 6;
                            }
                        Bindex = currentIndex + distance;
                        if(currentIndex == Binitial){
                            Bout = Bout+1;
                            Bin = Bin - 1;

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

            
        }
        if (Dice.result != 6){
                Dice.updateTour();
            }
            Dice.canClick = true;
            canMove = false;
        catchPion();        
    

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

    void homeCheck(){
        switch(pionCouleur){
            case COULEUR.VERT:
            Vhome = false;
             for(int i = 0 ; i < 4 ; i++){
                    currentPion = VERT.transform.GetChild(i);
                    if(currentPion.gameObject.GetComponent<Pions>().isOut && !isHoming && !currentPion.gameObject.GetComponent<Pions>().catched){
                        break;
                    }
                    if(currentPion.gameObject.GetComponent<Pions>().currentIndex + Dice.result > 75 && Dice.result != 6){
                        Vhome = true;
                    }
             }
             break;
             case COULEUR.JAUNE:
             for(int i = 0 ; i < 4 ; i++){
                    currentPion = JAUNE.transform.GetChild(i);
                    if((currentPion.gameObject.GetComponent<Pions>().currentIndex + Dice.result) < 55 && currentPion.gameObject.GetComponent<Pions>().isOut) {
                        Jhome = false;
                        break;
                    }
                    if((currentPion.gameObject.GetComponent<Pions>().currentIndex + Dice.result) > 55 && currentPion.gameObject.GetComponent<Pions>().currentIndex < 55){
                        Jhome = true;
                    }
             }
             break;

        }
    }


    void catchPion(){
        switch(pionCouleur){
            case COULEUR.VERT:
                for(int i = 0 ; i < 4 ; i++){
                    currentPion = JAUNE.transform.GetChild(i);
                    if((transform.position - currentPion.position).sqrMagnitude <= 0.002f ){
                        currentPion.position = new Vector3(Random.Range(-30,-10)/10 , Random.Range(30,10)/10, 0);
                        currentPion.gameObject.GetComponent<Pions>().currentIndex = Jinitial;
                        currentPion.gameObject.GetComponent<Pions>().isOut = false;
                        currentPion.gameObject.GetComponent<Pions>().catched = true;

                        Jout--;

                    }
                    currentPion = ROUGE.transform.GetChild(i);
                    if((transform.position - currentPion.position).sqrMagnitude <= 0.002f ){
                        currentPion.position = new Vector3(Random.Range(-30,-10)/10 , Random.Range(30,10)/10, 0);
                        currentPion.gameObject.GetComponent<Pions>().currentIndex = Rinitial;
                        currentPion.gameObject.GetComponent<Pions>().isOut = false;
                        currentPion.gameObject.GetComponent<Pions>().catched = true;
                        Rout--;

                    }
                    currentPion = BLEU.transform.GetChild(i);
                    if((transform.position - currentPion.position).sqrMagnitude <= 0.002f ){
                        currentPion.position = new Vector3(Random.Range(-30,-10)/10 , Random.Range(30,10)/10, 0);
                        currentPion.gameObject.GetComponent<Pions>().currentIndex = Binitial;
                        currentPion.gameObject.GetComponent<Pions>().isOut = false;
                        currentPion.gameObject.GetComponent<Pions>().catched = true;

                        Bout--;

                    }          
                }
            break;

            case COULEUR.JAUNE:
                for(int i = 0 ; i < 4 ; i++){
                    currentPion = VERT.transform.GetChild(i);
                    if((transform.position - currentPion.position).sqrMagnitude <= 0.002f ){
                        currentPion.position = new Vector3(Random.Range(-30,-10)/10 , Random.Range(-20,-40)/10, 0);
                        currentPion.gameObject.GetComponent<Pions>().currentIndex = Jinitial;
                        currentPion.gameObject.GetComponent<Pions>().isOut = false;
                        currentPion.gameObject.GetComponent<Pions>().catched = true;

                        Vout--;

                    }
                    currentPion = ROUGE.transform.GetChild(i);
                    if((transform.position - currentPion.position).sqrMagnitude <= 0.002f ){
                        currentPion.position = new Vector3(Random.Range(-30,-10)/10 , Random.Range(-20,-40)/10, 0);
                        currentPion.gameObject.GetComponent<Pions>().currentIndex = Rinitial;
                        currentPion.gameObject.GetComponent<Pions>().isOut = false;
                        currentPion.gameObject.GetComponent<Pions>().catched = true;

                        Rout--;

                    }
                    currentPion = BLEU.transform.GetChild(i);
                    if((transform.position - currentPion.position).sqrMagnitude <= 0.002f ){
                        currentPion.position = new Vector3(Random.Range(-30,-10)/10 , Random.Range(-20,-40)/10, 0);
                        currentPion.gameObject.GetComponent<Pions>().currentIndex = Vinitial;
                        currentPion.gameObject.GetComponent<Pions>().isOut = false;
                        currentPion.gameObject.GetComponent<Pions>().catched = true;

                        Bout--;

                    }          
                }
            break;

            
        }
    }
}

