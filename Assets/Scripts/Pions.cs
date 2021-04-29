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
    public static int Pout;


    public static GameObject VERT;
    public static GameObject JAUNE;
    public static GameObject ROUGE;
    public static GameObject BLEU;

    private Transform currentPion;
    public static GameObject dice;






    public static bool Vhome;
    public static bool Jhome;
    public static bool Bhome;
    public static bool Rhome;


    public int Vindex;
    public int Jindex;
    public int Bindex;
    public int Rindex;
    public bool walking;




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

    public static int Verts;
    public static int Jaunes;
    public static int Rouges;
    public static int Bleus;



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
    private SpriteRenderer sprite;
    public int barrageIndex;
    public static bool Phome;
    public static bool P_free; 
    public bool dontRun;
    public static int Pin;

     

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

        Verts = 4;
        Jaunes = 4;
        Rouges = 4;
        Bleus = 4;

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

        sprite = GetComponent<SpriteRenderer>();
        sprite.sortingOrder = 0;
        dice = GameObject.FindGameObjectWithTag("Dice");
        
        
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
            if(pionCouleur==currentTour){
                sprite.sortingOrder = 1;
                transform.position = new Vector3(transform.position.x, transform.position.y, 0.005f);

            }
            else{
                sprite.sortingOrder = 0;
                transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);

            }
        
        
        updateTour();
        Debug.Log("V_catched :"+V_catched);

    }
    //////////////////////////////////////////////// ON MOUSE DOWN  ///////////////////////////
    private void OnMouseDown(){
        BarrageBlock = false;
        dice.GetComponent<Dice>().barrer();

        if(Dice.six >= 2 && BarrageBlock == true){
            Dice.six = Dice.six - 2;
            BarrageBlock = false;

        }
        dontRun = false;
        if(isOut){
            
            runcheck();
        }
        

        if (currentTour == pionCouleur && canMove && (!HomeTrigger) && !block && !BarrageBlock && !dontRun){
            StartCoroutine("Move");
        }
      
    }
    /////////////////////////////////////////////////////   MOVE    ///////////////////////////////////
    IEnumerator Move(){
        debutCase = false;
        sprite.sortingOrder = 1;
        switch(pionCouleur){
            case COULEUR.VERT:
                Pout = Vout;
                P_free = Dice.V_free;
                Phome = Vhome;
                Pin = Vin;
            break;
            case COULEUR.JAUNE:
                Pout = Jout;
                P_free = Dice.J_free;
                Phome = Jhome;
                Pin = Jin;
            break;
            case COULEUR.ROUGE:
                Pout = Rout;
                P_free = Dice.R_free;
                Phome = Rhome;
                Pin = Rin;
            break;
            case COULEUR.BLEU:
                Pout = Bout;
                P_free = Dice.B_free;
                Phome = Bhome;
                Pin = Bin;
            break;
        }

         if(!isOut && Dice.six != 0 && !catched){
            //  switch(pionCouleur){
            //      case COULEUR.VERT:
            //         if(Pions.points.transform.GetChild(Vinitial).gameObject.GetComponent<Points>().R_actuel)
            //      break;
            //      case COULEUR.JAUNE:
            //         currentIndex = Jinitial;
            //      break;
            //      case COULEUR.ROUGE:
            //         currentIndex = Rinitial;
            //      break;
            //      case COULEUR.BLEU:
            //         currentIndex = Binitial;
            //      break;
            //  }
                isOut = true;
                Dice.six--;
                 switch (pionCouleur)
                {
                    case COULEUR.VERT:

                        currentIndex = Vinitial;
                        Vout++;
                        Vin--;
                        points.transform.GetChild(Vinitial).GetComponent<Points>().V_actuel++;
                        Pout = Vout;
                        P_free = Dice.V_free;
                        Phome = Vhome;
                        Pin = Vin;               
                        break;

                    case COULEUR.JAUNE:
                        currentIndex = Jinitial;
                        Jout++;
                        Jin--;
                        points.transform.GetChild(Jinitial).GetComponent<Points>().J_actuel++;
                        Pout = Jout;
                        P_free = Dice.J_free;
                        Phome = Jhome;
                        Pin = Jin;
                        break;

                    case COULEUR.ROUGE:

                        Pout = Rout;
                        P_free = Dice.R_free;
                        Phome = Rhome;
                        Pin = Rin;
                        currentIndex = Rinitial;
                        Rout++;
                        Rin--;
                        points.transform.GetChild(Rinitial).GetComponent<Points>().R_actuel++;


                        break;
                    case COULEUR.BLEU:

                        currentIndex = Binitial;
                        Bout++;
                        Bin--;
                        points.transform.GetChild(Binitial).GetComponent<Points>().B_actuel++;
                        Pout = Bout;
                        P_free = Dice.B_free;
                        Phome = Bhome;
                        Pin = Bin;

                        break;
                }
                catchPion();
                superposition();
                dice.GetComponent<Dice>().barrer();
                dice.GetComponent<Dice>().CheckTour();

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
                superposition();
                sprite.sortingOrder = 0;
                departIndex = currentIndex;
                dice.GetComponent<Dice>().CheckTour();
                Debug.Log(Pout);

            }
        
        if(catched && Dice.six != 0){
            if((Dice.six == 1 && (!P_free || Pin == 0)) || Dice.six > 1){
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

                dice.GetComponent<Dice>().CheckTour();
                if((Pout == 0 || !P_free || !Phome)&&Dice.six == 0){
                    dice.GetComponent<Dice>().updateTour();
                    Dice.canClick = true;
                    canMove = false;
                }

            }
            else{
                block = true;
                yield return new WaitForSeconds(0.1f);
            }
        }
        
        departIndex = currentIndex;
        ////////////////////////    IS OUT  //////////////////////////////

        if(isOut && !debutCase && !dontRun && !BarrageBlock){
            dice.GetComponent<Dice>().barrer();
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
                superposition();

                



            }
            else{
            if(Dice.six >= 1){
                
                if(!dontRun){
                    distance = 6;
                    Dice.six--;
                }
                else{
                    canMove = false;
                }
                //dontRun = false;
                
            }
            else{
                distance = Dice.result;
            }
            nextIndex = currentIndex;
            for (int i = 0; i <= distance; i++){

                nextIndex = (currentIndex + i)%76;
                switch (pionCouleur)
                {
                    case COULEUR.VERT:
                        
                        if(nextIndex == 12 || nextIndex == 31 || nextIndex == 50){
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
                        if(nextIndex == 12 || nextIndex == 31 || nextIndex == 69){
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
                        if(nextIndex == 69 || nextIndex == 31 || nextIndex == 50){
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
                        if(nextIndex == 12 || nextIndex == 69 || nextIndex == 50){
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
                nextIndex = (currentIndex + i)%76;
                destination = points.transform.GetChild(nextIndex);
                    while ((transform.position - destination.position).sqrMagnitude >= 0.001f) 
                        {
                        transform.position = Vector2.MoveTowards(transform.position, destination.position, moveSpeed*Time.deltaTime ); 
                        
                    }
                    if(i < distance){
                        walking = true;
                    }
                    else{
                        walking = false;
                    }
                yield return new WaitForSeconds(0.5f);
            }
                currentIndex = nextIndex;
                catchPion();
                superposition();
                dice.GetComponent<Dice>().barrer();
                //runcheck();
                if (Dice.six==0 && distance != 6 ){
                dice.GetComponent<Dice>().updateTour();
                Dice.canClick = true;
                canMove = false;
            }

                


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
                            Verts--;
                            break;
                        case COULEUR.JAUNE:
                            Jout--;
                            Jaunes--;
                            break;
                        case COULEUR.ROUGE:
                            Rout--;
                            Rouges--;
                            break;
                        case COULEUR.BLEU:
                            Bout--;
                            Bleus--;
                            break;
                    }
            }
///////////////////////////////////////////////////////////////////
            
    
            transform.position = new Vector3(transform.position.x,transform.position.y,0.0f);
            
        }
         for(int j = 0 ; j < 4 ; j++){
                currentPion = VERT.transform.GetChild(j);
                currentPion.gameObject.GetComponent<Pions>().block = false;
                currentPion.gameObject.GetComponent<Pions>().dontRun = false;


                currentPion = JAUNE.transform.GetChild(j);
                currentPion.gameObject.GetComponent<Pions>().block = false;
                currentPion.gameObject.GetComponent<Pions>().dontRun = false;


                currentPion = BLEU.transform.GetChild(j);
                currentPion.gameObject.GetComponent<Pions>().block = false;
                currentPion.gameObject.GetComponent<Pions>().dontRun = false;


                currentPion = ROUGE.transform.GetChild(j);
                currentPion.gameObject.GetComponent<Pions>().block = false;
                currentPion.gameObject.GetComponent<Pions>().dontRun = false;

                 
        }
        
        if(isOut){
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
        }
        destination.GetComponent<Points>().barrageCheck(transform.gameObject);
        sprite.sortingOrder = 0;
        //runcheck();
        


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
                    
                    
                    currentPion = JAUNE.transform.GetChild(i);
                    if(currentPion.GetComponent<Pions>().isOut){
                        if(currentIndex == currentPion.gameObject.GetComponent<Pions>().currentIndex){
                            currentPion.position = new Vector3(Random.Range(-max,-min)/10 , Random.Range(max,min)/10, 0);
                            currentPion.gameObject.GetComponent<Pions>().currentIndex = -1;
                            currentPion.gameObject.GetComponent<Pions>().isOut = false;
                            currentPion.gameObject.GetComponent<Pions>().catched = true;
                            points.transform.GetChild(currentIndex).GetComponent<Points>().J_actuel--;


                            Jout--;
                            J_catched++;

                        }
                    }
                    currentPion = ROUGE.transform.GetChild(i);
                    if(currentPion.GetComponent<Pions>().isOut){
                        if(currentIndex == currentPion.gameObject.GetComponent<Pions>().currentIndex){
                            currentPion.position = new Vector3(Random.Range(-max,-min)/10 , Random.Range(max,min)/10, 0);
                            currentPion.gameObject.GetComponent<Pions>().currentIndex = -1;
                            currentPion.gameObject.GetComponent<Pions>().isOut = false;
                            currentPion.gameObject.GetComponent<Pions>().catched = true;
                            points.transform.GetChild(currentIndex).GetComponent<Points>().R_actuel--;
                            Rout--;
                            R_catched++;

                        }
                    }
                    currentPion = BLEU.transform.GetChild(i);
                    if(currentPion.GetComponent<Pions>().isOut){
                        if(currentIndex == currentPion.gameObject.GetComponent<Pions>().currentIndex){
                            currentPion.position = new Vector3(Random.Range(-max,-min)/10 , Random.Range(max,min)/10, 0);
                            currentPion.gameObject.GetComponent<Pions>().currentIndex = -1;
                            currentPion.gameObject.GetComponent<Pions>().isOut = false;
                            currentPion.gameObject.GetComponent<Pions>().catched = true;
                            points.transform.GetChild(currentIndex).GetComponent<Points>().B_actuel--;
                    

                        Bout--;
                        B_catched++;

                        }
                    }          
                }
            break;

            case COULEUR.JAUNE:
                for(int i = 0 ; i < 4 ; i++){
                   currentPion = VERT.transform.GetChild(i);
                    if(currentPion.GetComponent<Pions>().isOut){
                        if(currentIndex == currentPion.gameObject.GetComponent<Pions>().currentIndex){
                            currentPion.position = new Vector3(Random.Range(-max,-min)/10 , Random.Range(-min,-max)/10, 0);
                            currentPion.gameObject.GetComponent<Pions>().currentIndex = -1;
                            currentPion.gameObject.GetComponent<Pions>().isOut = false;
                            currentPion.gameObject.GetComponent<Pions>().catched = true;
                            points.transform.GetChild(currentIndex).GetComponent<Points>().V_actuel--;


                            Vout--;
                            V_catched++;

                        }
                    }
                    currentPion = ROUGE.transform.GetChild(i);
                    if(currentPion.GetComponent<Pions>().isOut){
                        if(currentIndex == currentPion.gameObject.GetComponent<Pions>().currentIndex){
                            currentPion.position = new Vector3(Random.Range(-max,-min)/10 , Random.Range(-min,-max)/10, 0);
                            currentPion.gameObject.GetComponent<Pions>().currentIndex = -1;
                            currentPion.gameObject.GetComponent<Pions>().isOut = false;
                            currentPion.gameObject.GetComponent<Pions>().catched = true;
                            points.transform.GetChild(currentIndex).GetComponent<Points>().R_actuel--;


                            Rout--;
                            R_catched++;

                        }
                    }
                    currentPion = BLEU.transform.GetChild(i);
                    if(currentPion.GetComponent<Pions>().isOut){
                        if(currentIndex == currentPion.gameObject.GetComponent<Pions>().currentIndex){
                            currentPion.position = new Vector3(Random.Range(-max,-min)/10 , Random.Range(-min,-max)/10, 0);
                            currentPion.gameObject.GetComponent<Pions>().currentIndex = -1;
                            currentPion.gameObject.GetComponent<Pions>().isOut = false;
                            currentPion.gameObject.GetComponent<Pions>().catched = true;
                            points.transform.GetChild(currentIndex).GetComponent<Points>().B_actuel--;


                            Bout--;
                            B_catched++;

                        }
                    }          
                }
            break;

            case COULEUR.ROUGE:
                for(int i = 0 ; i < 4 ; i++){
                    
                    currentPion = VERT.transform.GetChild(i);
                    if(currentPion.GetComponent<Pions>().isOut){
                        if(currentIndex == currentPion.gameObject.GetComponent<Pions>().currentIndex){
                            currentPion.position = new Vector3(Random.Range(min,max)/10 , Random.Range(min,max)/10, 0);
                            currentPion.gameObject.GetComponent<Pions>().currentIndex = -1;
                            currentPion.gameObject.GetComponent<Pions>().isOut = false;
                            currentPion.gameObject.GetComponent<Pions>().catched = true;
                            points.transform.GetChild(currentIndex).GetComponent<Points>().V_actuel--;


                            Vout--;
                            V_catched++;

                        }
                    }
                    currentPion = JAUNE.transform.GetChild(i);
                    if(currentPion.GetComponent<Pions>().isOut){
                        if(currentIndex == currentPion.gameObject.GetComponent<Pions>().currentIndex){
                            currentPion.position = new Vector3(Random.Range(min,max)/10 , Random.Range(min,max)/10, 0);
                            currentPion.gameObject.GetComponent<Pions>().currentIndex = -1;
                            currentPion.gameObject.GetComponent<Pions>().isOut = false;
                            currentPion.gameObject.GetComponent<Pions>().catched = true;
                            points.transform.GetChild(currentIndex).GetComponent<Points>().J_actuel--;


                            Jout--;
                            J_catched++;

                        }
                    }
                    currentPion = BLEU.transform.GetChild(i);
                    if(currentPion.GetComponent<Pions>().isOut){
                        if(currentIndex == currentPion.gameObject.GetComponent<Pions>().currentIndex){
                            currentPion.position = new Vector3(Random.Range(min,max)/10 , Random.Range(min,max)/10, 0);
                            currentPion.gameObject.GetComponent<Pions>().currentIndex = -1;
                            currentPion.gameObject.GetComponent<Pions>().isOut = false;
                            currentPion.gameObject.GetComponent<Pions>().catched = true;
                            points.transform.GetChild(currentIndex).GetComponent<Points>().B_actuel--;


                            Bout--;
                            B_catched++;

                        }
                    }        
                }
            break;
            case COULEUR.BLEU:
                for(int i = 0 ; i < 4 ; i++){
                    
                    currentPion = VERT.transform.GetChild(i);
                    if(currentPion.GetComponent<Pions>().isOut){
                        if(currentIndex == currentPion.gameObject.GetComponent<Pions>().currentIndex){
                            currentPion.position = new Vector3(Random.Range(min,max)/10 , Random.Range(-min,-max)/10, 0);
                            currentPion.gameObject.GetComponent<Pions>().currentIndex = -1;
                            currentPion.gameObject.GetComponent<Pions>().isOut = false;
                            currentPion.gameObject.GetComponent<Pions>().catched = true;
                            points.transform.GetChild(currentIndex).GetComponent<Points>().B_actuel--;


                            Vout--;
                            V_catched++;

                        }
                    }
                    currentPion = ROUGE.transform.GetChild(i);
                    if(currentPion.GetComponent<Pions>().isOut){
                        if(currentIndex == currentPion.gameObject.GetComponent<Pions>().currentIndex){
                            currentPion.position = new Vector3(Random.Range(min,max)/10 , Random.Range(-min,-max)/10, 0);
                            currentPion.gameObject.GetComponent<Pions>().currentIndex = -1;
                            currentPion.gameObject.GetComponent<Pions>().isOut = false;
                            currentPion.gameObject.GetComponent<Pions>().catched = true;
                            points.transform.GetChild(currentIndex).GetComponent<Points>().R_actuel--;


                            Rout--;
                            R_catched++;

                        }
                    }
                    currentPion = JAUNE.transform.GetChild(i);
                    if(currentPion.GetComponent<Pions>().isOut){
                        if(currentIndex == currentPion.gameObject.GetComponent<Pions>().currentIndex){
                            currentPion.position = new Vector3(Random.Range(min,max)/10 , Random.Range(-min,-max)/10, 0);
                            currentPion.gameObject.GetComponent<Pions>().currentIndex = -1;
                            currentPion.gameObject.GetComponent<Pions>().isOut = false;
                            currentPion.gameObject.GetComponent<Pions>().catched = true;
                            points.transform.GetChild(currentIndex).GetComponent<Points>().J_actuel--;


                            Jout--;
                            J_catched++;

                        }
                    }          
                }
            break;

            
        }
    }
    ///////////////////////////////// SUPERPOSITION ////////////////////////////////////////
    void superposition(){
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
                    
                }
            break;

            
        }
    }
    public void runcheck(){
        barrageIndex = currentIndex;
        if(Dice.six == 1 ){
        switch (pionCouleur){
            case COULEUR.VERT:
            if((Vin > 0 || V_catched > 0)){    
                    for(int i = 0; i <= (Dice.six*6 +Dice.result); i++ ){

                        if(barrageIndex == 12 || barrageIndex == 31 || barrageIndex == 50){
                            barrageIndex += 6;
                        
                        }
                        

                        barrageIndex = barrageIndex %76;
                        if(Pions.points.transform.GetChild(barrageIndex).gameObject.GetComponent<Points>().J_actuel >=  2){
                            dontRun = true;
                            break;
                        }
                        if (Pions.points.transform.GetChild(barrageIndex).gameObject.GetComponent<Points>().R_actuel >=  2){
                            dontRun = true;
                            break;
                        }
                        if(Pions.points.transform.GetChild(barrageIndex).gameObject.GetComponent<Points>().B_actuel >=  2){
                            dontRun = true;
                            break;
                        }
                        barrageIndex++;
                        
                    }
                }
            
            break;
            case COULEUR.JAUNE:    
                if((Jin > 0 || J_catched > 0)){    
                    
                    for(int i = 0; i <= (Dice.six*6 +Dice.result); i++ ){

                        if(barrageIndex == 12 || barrageIndex == 31 || barrageIndex == 69){
                            barrageIndex += 6;
                        
                        }
                        

                        barrageIndex = barrageIndex %76;
                        if(Pions.points.transform.GetChild(barrageIndex).gameObject.GetComponent<Points>().V_actuel >=  2){
                            dontRun = true;
                            break;
                        }
                        if (Pions.points.transform.GetChild(barrageIndex).gameObject.GetComponent<Points>().R_actuel >=  2){
                            dontRun = true;
                            break;
                        }
                        if(Pions.points.transform.GetChild(barrageIndex).gameObject.GetComponent<Points>().B_actuel >=  2){
                            dontRun = true;
                            break;
                        }
                        barrageIndex++;
                        
                    }

                }
            break;
            case COULEUR.BLEU:    
                if((Bin > 0 || B_catched > 0)){    
                
                    for(int i = 0; i <= (Dice.six*6 +Dice.result); i++ ){

                        if(barrageIndex == 12 || barrageIndex == 50 || barrageIndex == 69){
                            barrageIndex += 6;
                        
                        }
                        

                        barrageIndex = barrageIndex %76;
                        if(Pions.points.transform.GetChild(barrageIndex).gameObject.GetComponent<Points>().V_actuel >=  2){
                            dontRun = true;
                            break;
                        }
                        if (Pions.points.transform.GetChild(barrageIndex).gameObject.GetComponent<Points>().R_actuel >=  2){
                            dontRun = true;
                            break;
                        }
                        if(Pions.points.transform.GetChild(barrageIndex).gameObject.GetComponent<Points>().J_actuel >=  2){
                            dontRun = true;
                            break;
                        }
                        barrageIndex++;
                        
                    }
                }
            
            break;           

            case COULEUR.ROUGE:
                if((Rin > 0 || R_catched > 0)){    
                
                    for(int i = 0; i <= (Dice.six*6 +Dice.result); i++ ){

                        if(barrageIndex == 50 || barrageIndex == 31 || barrageIndex == 69){
                            barrageIndex += 6;
                        
                        }
                        

                        barrageIndex = barrageIndex %76;
                        if(Pions.points.transform.GetChild(barrageIndex).gameObject.GetComponent<Points>().V_actuel >=  2){
                            dontRun = true;
                            break;
                        }
                        if (Pions.points.transform.GetChild(barrageIndex).gameObject.GetComponent<Points>().J_actuel >=  2){
                            dontRun = true;
                            break;
                        }
                        if(Pions.points.transform.GetChild(barrageIndex).gameObject.GetComponent<Points>().B_actuel >=  2){
                            dontRun = true;
                            break;
                        }
                        barrageIndex++;
                        
                    }
                }
            
            break;                     
        }
    }
    }
    
}

