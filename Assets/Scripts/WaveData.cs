using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static int[,] construct(){

        //[0][n] is number of enemies for wave n
        //[1][n] is health of enemies for wave n
        //[2][n] is time between spawn for wave n
        //NOT IMPLEMENTED [3][n] is speed of enemies in wave n
        //[4][n] 

        int[,]  arr = new int[5,200];

        for(int i =0; i< 20; i++){
            arr[3,i] = 1;
        }

        


        for(int i =0; i<200; i++){
            


            arr[0,i] = i/3 + 2;
            arr[1,i] = (int)Mathf.Pow(125*(i+1), 1.05f);

            arr[2,i] = 5;
            arr[3,i] = 1;
            arr[4,i] = 1;
            if(i>10){
                arr[1,i] = (int)Mathf.Pow(175*(i+1), 1.075f);
            }
            


        }
        

        return arr;


    }
}
