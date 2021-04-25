using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pions : MonoBehaviour
{
    public static GameObject points;
    private int[] pointsIndex = new int[76];
    
    public int nextIndex = 0;
    private int i;
    public int currentIndex;
    public int departIndex;
    public int marche;

    public static bool canMove;
    public bool block;
    public bool BarrageBlock;


    float min = 40;
    float max = 20;

    private GameObject currentPoint;
    public Transform destination;
    public Transform home;
    public bool isOut;
    public bool isHoming;
    public bool catched;
    public bool beginning;
    public int Pout;


    public static GameObject VERT;
    public static GameObject JAUNE;
    public static GameObject ROUGE;
    public static GameObject BLEU;

    private Transform currentPion;
    private Transform SiblingPion;






    public static bool Vhome;
    public static bool Jhome;
    public static bool Bhome;
    public static bool Rhome;


    public int Vindex;
    public int Jindex;
    public int Bindex;
    public int Rindex;




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
    public static COULEUR currentTour;

    public static int Vinitial;
    public static int Jinitial;
    public static int Rinitial;
    public static int Binitial;

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
    public bool debutCase;
    // private int barrageIndex;


     

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
        


        Vhome = false;
        Jhome = false;
        Rhome = false;
        Bhome = false;

        VERT = GameObject.FindGameObjectWithTag("VERT");
        JAUNE = GameObject.FindGameObjectWithTag("JAUNE");
        ROUGE = GameObject.FindGameObjectWithTag("ROUGE");
        BLEU = GameObject.FindGameObjectWithTag("BLEU");
        points = GameObject.FindGameObjectWithTag("points");




        catched = false;
        isHoming = false;
        block = false;
        BarrageBlock = false;
        Pout=0;

        
        
    }

    /////////////////////////////////////// UPDATE  ///////////////////////////////////
    void Update()
    {
        

        switch(pionCouleur)
        {
                case COULEUR.VERT:
                    
                    if(currentIndex > 68 && !isHoming){
                             isHoming = true;
                    }
                    HomeTrigger = false;
                    if((currentIndex + Dice.result > 75) && Dice.six== 0){
                        HomeTrigger = true;
                     }
                    ///////////////////////////////////////////////////////
                    

                    break;

                case COULEUR.JAUNE:
                    
                    if(currentIndex < 56 && currentIndex > 49 && !isHoming){
                             isHoming = true;
                    }
                    HomeTrigger = false;
                    if((currentIndex + Dice.result > 55) && (currentIndex < 56)  && Dice.six== 0){
                        HomeTrigger = true;
                     }
                    ///////////////////////////////////////////////////////

                     break;
                case COULEUR.ROUGE:
                    if(currentIndex < 18 && currentIndex > 11 && !isHoming){
                             isHoming = true;
                    }
                    HomeTrigger = false;
                    if((currentIndex + Dice.result > 17) && (currentIndex < 18)  && Dice.six== 0){
                        HomeTrigger = true;
                     }
                     ///////////////////////////////////////////////////////
                    
                    break;
                case COULEUR.BLEU:
                    if(currentIndex < 37 && currentIndex > 30 && !isHoming){
                             isHoming = true;
                    }
                    HomeTrigger = false;
                    if((currentIndex + Dice.result > 36) && (currentIndex < 37)  && Dice.six== 0){
                        HomeTrigger = true;
                     }
                     ///////////////////////////////////////////////////////
                    
                    break;
            }
        
        
        updateTour();
        Debug.Log("V_catched :"+V_catched);

    }
    //////////////////////////////////////////////// ON MOUSE DOWN  ///////////////////////////
    private void OnMouseDown(){

      

        if(Dice.six >= 2 && BarrageBlock == true){
            Dice.six = Dice.six - 2;
            BarrageBlock = false;

        }
        if (currentTour == pionCouleur && canMove && (!HomeTrigger) && !block && !BarrageBlock){
            StartCoroutine("Move");
        }
      
    }
    /////////////////////////////////////////////////////   MOVE    ///////////////////////////////////
    IEnumerator Move(){

        debutCase = false;


         if(!isOut && Dice.six != 0 && !catched){
                isOut = true;
                Dice.six--;
                 switch (pionCouleur)
                {
                    case COULEUR.VERT:
                            currentIndex = Vinitial;
                            Vout = Vout+1;
                            Vin--;
                            points.transform.GetChild(Vinitial).GetComponent<Points>().V_actuel++;
                            Pout = Vout;
                                        
                        break;
                    case COULEUR.JAUNE:
                        currentIndex = Jinitial;
                            Jout = Jout+1;
                            Jin--;
                            points.transform.GetChild(Jinitial).GetComponent<Points>().J_actuel++;
                            Pout = Jout;

                        break;
                    case COULEUR.ROUGE:
                        currentIndex = Rinitial;
                        Rout = Rout+1;
                        Rin--;
                        points.transform.GetChild(Rinitial).GetComponent<Points>().R_actuel++;
                        Pout = Rout;


                        break;
                    case COULEUR.BLEU:
                        currentIndex = Binitial;
                            Bout = Bout+1;
                            Bin--;
                            points.transform.GetChild(Binitial).GetComponent<Points>().B_actuel++;
                            Pout = Bout;

                        break;
                }
                catchPion();

                if(Pout > 1 || Dice.six >= 1){
                    debutCase = true;

                }
                destination = points.transform.GetChild(currentIndex);

                // destination.GetComponent<Points>().barrageCheck(transform.gameObject);
                while ((transform.position - destination.position).sqrMagnitude >= 0.001f) 
                        {   
                        transform.position = Vector2.MoveTowards(transform.position, destination.position, moveSpeed*Time.deltaTime ); 
                        
                    }
                yield return new WaitForSeconds(0.1f);
                catchPion();
               //departIndex = currentIndex;

            }
        
        if(catched && Dice.six != 0){
                Dice.six--;
                catched = false;
                isOut=false;
                BarrageBlock =false;
                
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
                
                yield return new WaitForSeconds(0.1f);
                

                

            }
        
        departIndex = currentIndex;
        ////////////////////////    IS OUT  //////////////////////////////
        if(isOut && !debutCase){
            transform.position = new Vector3(transform.position.x,transform.position.y,1.0f);
            if(isHoming && Dice.six > 0 ){
                switch(pionCouleur){
                    case COULEUR.VERT:
                        destination = points.transform.GetChild(68);   
                        transform.position = destination.position;
                        currentIndex = 68;
                        isHoming = false;
                    break;
                    case COULEUR.JAUNE:
                        destination = points.transform.GetChild(49);   
                        transform.position = destination.position;
                        currentIndex = 49;
                        isHoming = false;
                    break; 
                    case COULEUR.ROUGE:
                        destination = points.transform.GetChild(11);   
                        transform.position = destination.position;
                        currentIndex = 11;
                        isHoming = false;
                    break; 
                    case COULEUR.BLEU:
                        destination = points.transform.GetChild(30);   
                        transform.position = destination.position;
                        currentIndex = 30;
                        isHoming = false;
                    break;    

                }
                Dice.six--;
                catchPion();

                



            }
            else{
            if(Dice.six > 0){
                distance = 6;
                Dice.six--;
            }
            else{
                distance = Dice.result;
            }
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
                catchPion();

                


            }
            
            
            if((transform.position - home.position).sqrMagnitude <= 0.005f){

                transform.gameObject.SetActive(false);
                isOut = false;
                BarrageBlock = false;
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
            ///////////////////////////////////////////////////////////////////
            if (Dice.six==0 && distance != 6){
                Dice.updateTour();
                Dice.canClick = true;
                canMove = false;
            }
            transform.position = new Vector3(transform.position.x,transform.position.y,0.0f);
            
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
        
        switch(pionCouleur)
        {
            case COULEUR.VERT:
                points.transform.GetChild(departIndex).GetComponent<Points>().V_actuel--;
                break;
            case COULEUR.JAUNE:
                points.transform.GetChild(departIndex).GetComponent<Points>().J_actuel--;
                break;
            case COULEUR.ROUGE:
                points.transform.GetChild(departIndex).GetComponent<Points>().R_actuel--;
                break;
            case COULEUR.BLEU:
                points.transform.GetChild(departIndex).GetComponent<Points>().B_actuel--;
                break;

        }

        destination.GetComponent<Points>().barrageCheck(transform.gameObject);


    }
    ///////////////////////////////// UPDATE TOUR /////////////////////////////////
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


///////////////////////////////// CATCH PION ////////////////////////////////////////
    void catchPion(){
        switch(pionCouleur){
            case COULEUR.VERT:
                for(int i = 0 ; i < 4 ; i++){
                    currentPion = VERT.transform.GetChild(i);
                    if(currentPion.name == transform.gameObject.name){
                        continue;
                    }

                    if((transform.position - currentPion.position).sqrMagnitude <= 0.002f ){
                        currentPion.position = new Vector3(transform.position.x + ((Random.Range(0,2)*2 - 1 )* 0.02f), transform.position.y + ((Random.Range(0,2)*2 - 1 )* 0.11f), 0);

                    }
                    
                    currentPion = JAUNE.transform.GetChild(i);
                    if((transform.position - currentPion.position).sqrMagnitude <= 0.007f || (currentIndex == currentPion.gameObject.GetComponent<Pions>().currentIndex)){
                        currentPion.position = new Vector3(Random.Range(-max,-min)/10 , Random.Range(max,min)/10, 0);
                        currentPion.gameObject.GetComponent<Pions>().currentIndex = -1;
                        currentPion.gameObject.GetComponent<Pions>().isOut = false;
                        currentPion.gameObject.GetComponent<Pions>().catched = true;
                        points.transform.GetChild(currentIndex).GetComponent<Points>().J_actuel--;


                        Jout--;
                        J_catched++;

                    }
                    currentPion = ROUGE.transform.GetChild(i);
                    if((transform.position - currentPion.position).sqrMagnitude <= 0.007f || (currentIndex == currentPion.gameObject.GetComponent<Pions>().currentIndex)){
                        currentPion.position = new Vector3(Random.Range(-max,-min)/10 , Random.Range(max,min)/10, 0);
                        currentPion.gameObject.GetComponent<Pions>().currentIndex = -1;
                        currentPion.gameObject.GetComponent<Pions>().isOut = false;
                        currentPion.gameObject.GetComponent<Pions>().catched = true;
                        points.transform.GetChild(currentIndex).GetComponent<Points>().R_actuel--;
                        Rout--;
                        R_catched++;

                    }
                    currentPion = BLEU.transform.GetChild(i);
                    if((transform.position - currentPion.position).sqrMagnitude <= 0.007f || (currentIndex == currentPion.gameObject.GetComponent<Pions>().currentIndex)){
                        currentPion.position = new Vector3(Random.Range(-max,-min)/10 , Random.Range(max,min)/10, 0);
                        currentPion.gameObject.GetComponent<Pions>().currentIndex = -1;
                        currentPion.gameObject.GetComponent<Pions>().isOut = false;
                        currentPion.gameObject.GetComponent<Pions>().catched = true;
                        points.transform.GetChild(currentIndex).GetComponent<Points>().B_actuel--;


                        Bout--;
                        B_catched++;

                    }          
                }
            break;

            case COULEUR.JAUNE:
                for(int i = 0 ; i < 4 ; i++){

                    currentPion = JAUNE.transform.GetChild(i);
                    if(currentPion.name == transform.gameObject.name){
                        continue;
                    }

                    if((transform.position - currentPion.position).sqrMagnitude <= 0.002f ){
                        currentPion.position = new Vector3(transform.position.x + ((Random.Range(0,2)*2 - 1 )* 0.02f), transform.position.y + ((Random.Range(0,2)*2 - 1 )* 0.11f), 0);

                    }
                    //      }
                   currentPion = VERT.transform.GetChild(i);
                    if((transform.position - currentPion.position).sqrMagnitude <= 0.007f || (currentIndex == currentPion.gameObject.GetComponent<Pions>().currentIndex)){
                        currentPion.position = new Vector3(Random.Range(-max,-min)/10 , Random.Range(-min,-max)/10, 0);
                        currentPion.gameObject.GetComponent<Pions>().currentIndex = -1;
                        currentPion.gameObject.GetComponent<Pions>().isOut = false;
                        currentPion.gameObject.GetComponent<Pions>().catched = true;
                        points.transform.GetChild(currentIndex).GetComponent<Points>().V_actuel--;


                        Vout--;
                        V_catched++;

                    }
                    currentPion = ROUGE.transform.GetChild(i);
                    if((transform.position - currentPion.position).sqrMagnitude <= 0.007f || (currentIndex == currentPion.gameObject.GetComponent<Pions>().currentIndex)){
                        currentPion.position = new Vector3(Random.Range(-max,-min)/10 , Random.Range(-min,-max)/10, 0);
                        currentPion.gameObject.GetComponent<Pions>().currentIndex = -1;
                        currentPion.gameObject.GetComponent<Pions>().isOut = false;
                        currentPion.gameObject.GetComponent<Pions>().catched = true;
                        points.transform.GetChild(currentIndex).GetComponent<Points>().R_actuel--;


                        Rout--;
                        R_catched++;

                    }
                    currentPion = BLEU.transform.GetChild(i);
                    if((transform.position - currentPion.position).sqrMagnitude <= 0.007f || (currentIndex == currentPion.gameObject.GetComponent<Pions>().currentIndex)){
                        currentPion.position = new Vector3(Random.Range(-max,-min)/10 , Random.Range(-min,-max)/10, 0);
                        currentPion.gameObject.GetComponent<Pions>().currentIndex = -1;
                        currentPion.gameObject.GetComponent<Pions>().isOut = false;
                        currentPion.gameObject.GetComponent<Pions>().catched = true;
                        points.transform.GetChild(currentIndex).GetComponent<Points>().B_actuel--;


                        Bout--;
                        B_catched++;

                    }          
                }
            break;

            case COULEUR.ROUGE:
                for(int i = 0 ; i < 4 ; i++){
                    currentPion = ROUGE.transform.GetChild(i);
                    if(currentPion.name == transform.gameObject.name){
                        continue;
                    }

                    if((transform.position - currentPion.position).sqrMagnitude <= 0.002f ){
                        currentPion.position = new Vector3(transform.position.x + ((Random.Range(0,2)*2 - 1 )* 0.02f), transform.position.y + ((Random.Range(0,2)*2 - 1 )* 0.11f), 0);

                    }
                    currentPion = VERT.transform.GetChild(i);
                    if((transform.position - currentPion.position).sqrMagnitude <= 0.007f || (currentIndex == currentPion.gameObject.GetComponent<Pions>().currentIndex)){
                        currentPion.position = new Vector3(Random.Range(min,max)/10 , Random.Range(min,max)/10, 0);
                        currentPion.gameObject.GetComponent<Pions>().currentIndex = -1;
                        currentPion.gameObject.GetComponent<Pions>().isOut = false;
                        currentPion.gameObject.GetComponent<Pions>().catched = true;
                        points.transform.GetChild(currentIndex).GetComponent<Points>().V_actuel--;


                        Vout--;
                        V_catched++;

                    }
                    currentPion = JAUNE.transform.GetChild(i);
                    if((transform.position - currentPion.position).sqrMagnitude <= 0.007f || (currentIndex == currentPion.gameObject.GetComponent<Pions>().currentIndex)){
                        currentPion.position = new Vector3(Random.Range(min,max)/10 , Random.Range(min,max)/10, 0);
                        currentPion.gameObject.GetComponent<Pions>().currentIndex = -1;
                        currentPion.gameObject.GetComponent<Pions>().isOut = false;
                        currentPion.gameObject.GetComponent<Pions>().catched = true;
                        points.transform.GetChild(currentIndex).GetComponent<Points>().J_actuel--;


                        Jout--;
                        J_catched++;

                    }
                    currentPion = BLEU.transform.GetChild(i);
                    if((transform.position - currentPion.position).sqrMagnitude <= 0.007f || (currentIndex == currentPion.gameObject.GetComponent<Pions>().currentIndex)){
                        currentPion.position = new Vector3(Random.Range(min,max)/10 , Random.Range(min,max)/10, 0);
                        currentPion.gameObject.GetComponent<Pions>().currentIndex = -1;
                        currentPion.gameObject.GetComponent<Pions>().isOut = false;
                        currentPion.gameObject.GetComponent<Pions>().catched = true;
                        points.transform.GetChild(currentIndex).GetComponent<Points>().B_actuel--;


                        Bout--;
                        B_catched++;

                    }          
                }
            break;
            case COULEUR.BLEU:
                for(int i = 0 ; i < 4 ; i++){
                    currentPion = BLEU.transform.GetChild(i);

                    if(currentPion.name == transform.gameObject.name){
                        continue;
                    }

                    if((transform.position - currentPion.position).sqrMagnitude <= 0.002f ){
                        currentPion.position = new Vector3(transform.position.x + ((Random.Range(0,2)*2 - 1 )* 0.02f), transform.position.y + ((Random.Range(0,2)*2 - 1 )* 0.11f), 0);

                    }
                    currentPion = VERT.transform.GetChild(i);
                    if((transform.position - currentPion.position).sqrMagnitude <= 0.007f || (currentIndex == currentPion.gameObject.GetComponent<Pions>().currentIndex)){
                        currentPion.position = new Vector3(Random.Range(min,max)/10 , Random.Range(-min,-max)/10, 0);
                        currentPion.gameObject.GetComponent<Pions>().currentIndex = -1;
                        currentPion.gameObject.GetComponent<Pions>().isOut = false;
                        currentPion.gameObject.GetComponent<Pions>().catched = true;
                        points.transform.GetChild(currentIndex).GetComponent<Points>().B_actuel--;


                        Vout--;
                        V_catched++;

                    }
                    currentPion = ROUGE.transform.GetChild(i);
                    if((transform.position - currentPion.position).sqrMagnitude <= 0.007f || (currentIndex == currentPion.gameObject.GetComponent<Pions>().currentIndex)){
                        currentPion.position = new Vector3(Random.Range(min,max)/10 , Random.Range(-min,-max)/10, 0);
                        currentPion.gameObject.GetComponent<Pions>().currentIndex = -1;
                        currentPion.gameObject.GetComponent<Pions>().isOut = false;
                        currentPion.gameObject.GetComponent<Pions>().catched = true;
                        points.transform.GetChild(currentIndex).GetComponent<Points>().R_actuel--;


                        Rout--;
                        R_catched++;

                    }
                    currentPion = JAUNE.transform.GetChild(i);
                    if((transform.position - currentPion.position).sqrMagnitude <= 0.007f || (currentIndex == currentPion.gameObject.GetComponent<Pions>().currentIndex)){
                        currentPion.position = new Vector3(Random.Range(min,max)/10 , Random.Range(-min,-max)/10, 0);
                        currentPion.gameObject.GetComponent<Pions>().currentIndex = -1;
                        currentPion.gameObject.GetComponent<Pions>().isOut = false;
                        currentPion.gameObject.GetComponent<Pions>().catched = true;
                        points.transform.GetChild(currentIndex).GetComponent<Points>().J_actuel--;


                        Jout--;
                        J_catched++;

                    }          
                }
            break;

            
        }
    }
    
}

