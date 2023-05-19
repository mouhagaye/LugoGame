using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pions : MonoBehaviour
{
    public GameObject points;
    private int[] pointsIndex = new int[72];
    private int nextIndex = 0;
    private int i;
    private int currentIndex;
    public Transform currentPoint;
    public Transform destination;
    public float moveSpeed = 2f;
    int distance = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (i = 0; i <= 71; i++){
        pointsIndex[i] = i;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Dice.result;
    }
    private void OnMouseDown(){
        // StartCoroutine("Move");
            StartCoroutine("Move");
    }
    IEnumerator Move(){
        // for(int i = 0; i < 72; i++){
        //     if((transform.position - points.transform.GetChild(i).position).sqrMagnitude <= 0.05f ){
        //         currentIndex = i;
        //         Debug.Log("position actuel :"+currentIndex);

        //          break;
        //      }
        // }
        for (int i = 0; i <= distance; i++){
            nextIndex = (currentIndex + i)%71;
            if(nextIndex == 11 || nextIndex == 29 || nextIndex == 47){
                currentIndex += 5;
            }
            destination = points.transform.GetChild(nextIndex);
            // Debug.Log("next index :"+(currentIndex+distance));
                while ((transform.position - destination.position).sqrMagnitude > 0.001f) 
                    {   
                    transform.position = Vector2.MoveTowards(transform.position, destination.position, moveSpeed*Time.deltaTime );
                }
            yield return new WaitForSeconds(0.5f);
        }
        currentIndex = nextIndex;
            // Debug.Log("destionation: "+destination.position +"position: "+transform.position);

    }
}
