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
    // float Vmin_x = -40;
    // float Vmax_x = -20;
    // float Bmin_x = 20;
    // float Bmax_x = 40;

    // float Vmin_y = 20;
    // float Vmax_y = 40;
    // float Bmin_y = -20;
    // float Bmax_y = -40;

    float min = 40;
    float max = 20;

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

    public static int Vin;
    public static int Jin;
    public static int Rin;
    public static int Bin;

    public static int Vout;
    public static int Jout;
    public static int Rout;
    public static int Bout;

    public static int V_catched;
    public static int J_catched;
    public static int R_catched;
    public static int B_catched;

    public bool HomeTrigger;
     

    // Start is called before the first frame update
    void Start()
    {
        Vinitial = 0;
        Jinitial = 57;
        Rinitial = 19;
        Binitial = 38;

        V_catched = 0;
        J_catched = 0;
        R_catched = 0;
        B_catched = 0;


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
        
        currentIndex = -1;

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
                    if(currentIndex < 18 && currentIndex > 11 && !isHoming){
                             isHoming = true;
                    }
                    HomeTrigger = false;
                    if((currentIndex + Dice.result > 17) && (currentIndex < 18)){
                        HomeTrigger = true;
                     }
                    break;
                case COULEUR.BLEU:
                    home = points.transform.GetChild(36);
                    if(currentIndex < 37 && currentIndex > 30 && !isHoming){
                             isHoming = true;
                    }
                    HomeTrigger = false;
                    if((currentIndex + Dice.result > 36) && (currentIndex < 37)){
                        HomeTrigger = true;
                     }
                    break;
            }
            

        
        updateTour();
       
        
    }
    private void OnMouseDown(){

        if (currentTour == pionCouleur && canMove && (!HomeTrigger) && !block){
            StartCoroutine("Move");
        }
    }
    IEnumerator Move(){
         
        distance = Dice.result;
         if(!isOut && distance == 6 && !catched){
                distance = 0;
                isOut = true;

                 switch (pionCouleur)
                {
                    case COULEUR.VERT:
                            currentIndex = Vinitial;
                            Vout = Vout+1;
                            Vin = Vin - 1;
                        
                        break;
                    case COULEUR.JAUNE:
                        currentIndex = Jinitial;
                            Jout = Jout+1;
                            Jin = Jin - 1;

                        break;
                    case COULEUR.ROUGE:
                        currentIndex = Rinitial;
                        Rout = Rout+1;
                        Rin = Rin - 1;

                        break;
                    case COULEUR.BLEU:
                        currentIndex = Binitial;
                            Bout = Bout+1;
                            Bin = Bin - 1;
                        break;
                }
            }
        if(catched && distance == 6){
                distance = 0;
                catched = false;
                isOut=false;
                
                switch(pionCouleur){
                    case COULEUR.VERT:
                        Vin++;
                        V_catched--;
                        currentIndex = Vinitial;
                        transform.position = new Vector3(Random.Range(-max,-min)/10 , Random.Range(max,min)/10, 0);
                    break;
                    case COULEUR.JAUNE:
                        Jin++;
                        J_catched--;
                        currentIndex = Jinitial;
                        transform.position = new Vector3(Random.Range(-max,-min)/10 , Random.Range(-max,-min)/10, 0);
                    break;
                    case COULEUR.ROUGE:
                        Rin++;
                        R_catched--;
                        currentIndex = Rinitial;
                        transform.position = new Vector3(Random.Range(max,min)/10 , Random.Range(max,min)/10, 0);
                    break;
                    case COULEUR.BLEU:
                        Bin++;
                        B_catched--;
                        currentIndex = Binitial;
                        transform.position = new Vector3(Random.Range(max,min)/10 , Random.Range(-max,-min)/10, 0);
                    break;
                }
                Dice.canClick = true;
                canMove = false;
                catchPion();
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

                        for(int j = 0 ; j < 4 ; j++){
                             currentPion = VERT.transform.GetChild(j);
                             currentPion.gameObject.GetComponent<Pions>().block = true;
                        }
                        block = false;
                        
                        break;
                    case COULEUR.JAUNE:
                        if(nextIndex == 11 || nextIndex == 30 || nextIndex == 68){
                            currentIndex += 6;
                            }
                        Jindex = currentIndex + distance;
                        for(int j = 0 ; j < 4 ; j++){
                             currentPion = JAUNE.transform.GetChild(j);
                             currentPion.gameObject.GetComponent<Pions>().block = true;
                        }
                        block = false;

                        break;
                    case COULEUR.ROUGE:
                        if(nextIndex == 68 || nextIndex == 30 || nextIndex == 49){
                            currentIndex += 6;
                            }
                            Rindex = currentIndex + distance;
                            for(int j = 0 ; j < 4 ; j++){
                             currentPion = ROUGE.transform.GetChild(j);
                             currentPion.gameObject.GetComponent<Pions>().block = true;
                            }
                            block = false;

                        break;
                    case COULEUR.BLEU:
                        if(nextIndex == 11 || nextIndex == 68 || nextIndex == 49){
                            currentIndex += 6;
                            }
                        Bindex = currentIndex + distance;
                        for(int j = 0 ; j < 4 ; j++){
                             currentPion = BLEU.transform.GetChild(j);
                             currentPion.gameObject.GetComponent<Pions>().block = true;
                        }
                        block = false;
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
            Dice.canClick = true;
            canMove = false;
            catchPion();
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
            }
             if (Dice.result != 6){
                Dice.updateTour();
            }
            
        }
         for(int j = 0 ; j < 4 ; j++){
                currentPion = VERT.transform.GetChild(j);
                currentPion.gameObject.GetComponent<Pions>().block = false;

                currentPion = JAUNE.transform.GetChild(j);
                currentPion.gameObject.GetComponent<Pions>().block = false;

                currentPion = BLEU.transform.GetChild(j);
                currentPion.gameObject.GetComponent<Pions>().block = false;

                currentPion = ROUGE.transform.GetChild(j);
                currentPion.gameObject.GetComponent<Pions>().block = false;
                 
        }

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



    void catchPion(){
        switch(pionCouleur){
            case COULEUR.VERT:
                for(int i = 0 ; i < 4 ; i++){
                    currentPion = JAUNE.transform.GetChild(i);
                    if((transform.position - currentPion.position).sqrMagnitude <= 0.002f ){
                        currentPion.position = new Vector3(Random.Range(-max,-min)/10 , Random.Range(max,min)/10, 0);
                        currentPion.gameObject.GetComponent<Pions>().currentIndex = -1;
                        currentPion.gameObject.GetComponent<Pions>().isOut = false;
                        currentPion.gameObject.GetComponent<Pions>().catched = true;

                        Jout--;
                        J_catched++;

                    }
                    currentPion = ROUGE.transform.GetChild(i);
                    if((transform.position - currentPion.position).sqrMagnitude <= 0.002f ){
                        currentPion.position = new Vector3(Random.Range(-max,-min)/10 , Random.Range(max,min)/10, 0);
                        currentPion.gameObject.GetComponent<Pions>().currentIndex = -1;
                        currentPion.gameObject.GetComponent<Pions>().isOut = false;
                        currentPion.gameObject.GetComponent<Pions>().catched = true;
                        Rout--;
                        R_catched++;

                    }
                    currentPion = BLEU.transform.GetChild(i);
                    if((transform.position - currentPion.position).sqrMagnitude <= 0.002f ){
                        currentPion.position = new Vector3(Random.Range(-max,-min)/10 , Random.Range(max,min)/10, 0);
                        currentPion.gameObject.GetComponent<Pions>().currentIndex = -1;
                        currentPion.gameObject.GetComponent<Pions>().isOut = false;
                        currentPion.gameObject.GetComponent<Pions>().catched = true;

                        Bout--;
                        B_catched++;

                    }          
                }
            break;

            case COULEUR.JAUNE:
                for(int i = 0 ; i < 4 ; i++){
                    currentPion = VERT.transform.GetChild(i);
                    if((transform.position - currentPion.position).sqrMagnitude <= 0.002f ){
                        currentPion.position = new Vector3(Random.Range(-max,-min)/10 , Random.Range(-min,-max)/10, 0);
                        currentPion.gameObject.GetComponent<Pions>().currentIndex = -1;
                        currentPion.gameObject.GetComponent<Pions>().isOut = false;
                        currentPion.gameObject.GetComponent<Pions>().catched = true;

                        Vout--;
                        J_catched++;

                    }
                    currentPion = ROUGE.transform.GetChild(i);
                    if((transform.position - currentPion.position).sqrMagnitude <= 0.002f ){
                        currentPion.position = new Vector3(Random.Range(-max,-min)/10 , Random.Range(-min,-max)/10, 0);
                        currentPion.gameObject.GetComponent<Pions>().currentIndex = -1;
                        currentPion.gameObject.GetComponent<Pions>().isOut = false;
                        currentPion.gameObject.GetComponent<Pions>().catched = true;

                        Rout--;
                        R_catched++;

                    }
                    currentPion = BLEU.transform.GetChild(i);
                    if((transform.position - currentPion.position).sqrMagnitude <= 0.002f ){
                        currentPion.position = new Vector3(Random.Range(-max,-min)/10 , Random.Range(-min,-max)/10, 0);
                        currentPion.gameObject.GetComponent<Pions>().currentIndex = -1;
                        currentPion.gameObject.GetComponent<Pions>().isOut = false;
                        currentPion.gameObject.GetComponent<Pions>().catched = true;

                        Bout--;
                        B_catched++;

                    }          
                }
            break;

            case COULEUR.ROUGE:
                for(int i = 0 ; i < 4 ; i++){
                    currentPion = VERT.transform.GetChild(i);
                    if((transform.position - currentPion.position).sqrMagnitude <= 0.002f ){
                        currentPion.position = new Vector3(Random.Range(min,max)/10 , Random.Range(min,max)/10, 0);
                        currentPion.gameObject.GetComponent<Pions>().currentIndex = -1;
                        currentPion.gameObject.GetComponent<Pions>().isOut = false;
                        currentPion.gameObject.GetComponent<Pions>().catched = true;

                        Vout--;
                        V_catched++;

                    }
                    currentPion = JAUNE.transform.GetChild(i);
                    if((transform.position - currentPion.position).sqrMagnitude <= 0.002f ){
                        currentPion.position = new Vector3(Random.Range(min,max)/10 , Random.Range(min,max)/10, 0);
                        currentPion.gameObject.GetComponent<Pions>().currentIndex = -1;
                        currentPion.gameObject.GetComponent<Pions>().isOut = false;
                        currentPion.gameObject.GetComponent<Pions>().catched = true;

                        Rout--;
                        R_catched++;

                    }
                    currentPion = BLEU.transform.GetChild(i);
                    if((transform.position - currentPion.position).sqrMagnitude <= 0.002f ){
                        currentPion.position = new Vector3(Random.Range(min,max)/10 , Random.Range(min,max)/10, 0);
                        currentPion.gameObject.GetComponent<Pions>().currentIndex = -1;
                        currentPion.gameObject.GetComponent<Pions>().isOut = false;
                        currentPion.gameObject.GetComponent<Pions>().catched = true;

                        Bout--;
                        B_catched++;

                    }          
                }
            break;
            case COULEUR.BLEU:
                for(int i = 0 ; i < 4 ; i++){
                    currentPion = VERT.transform.GetChild(i);
                    if((transform.position - currentPion.position).sqrMagnitude <= 0.002f ){
                        currentPion.position = new Vector3(Random.Range(min,max)/10 , Random.Range(-min,-max)/10, 0);
                        currentPion.gameObject.GetComponent<Pions>().currentIndex = -1;
                        currentPion.gameObject.GetComponent<Pions>().isOut = false;
                        currentPion.gameObject.GetComponent<Pions>().catched = true;

                        Vout--;
                        V_catched++;

                    }
                    currentPion = ROUGE.transform.GetChild(i);
                    if((transform.position - currentPion.position).sqrMagnitude <= 0.002f ){
                        currentPion.position = new Vector3(Random.Range(min,max)/10 , Random.Range(-min,-max)/10, 0);
                        currentPion.gameObject.GetComponent<Pions>().currentIndex = -1;
                        currentPion.gameObject.GetComponent<Pions>().isOut = false;
                        currentPion.gameObject.GetComponent<Pions>().catched = true;

                        Rout--;
                        R_catched++;

                    }
                    currentPion = JAUNE.transform.GetChild(i);
                    if((transform.position - currentPion.position).sqrMagnitude <= 0.002f ){
                        currentPion.position = new Vector3(Random.Range(min,max)/10 , Random.Range(-min,-max)/10, 0);
                        currentPion.gameObject.GetComponent<Pions>().currentIndex = -1;
                        currentPion.gameObject.GetComponent<Pions>().isOut = false;
                        currentPion.gameObject.GetComponent<Pions>().catched = true;

                        Bout--;
                        B_catched++;

                    }          
                }
            break;

            
        }
    }
}

