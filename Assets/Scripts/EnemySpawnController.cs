using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnController : MonoBehaviour
{
    public int totalSpawns = 0;
    public int tempTotalToSpawn = 200;
    public float timeSinceSpawn = 4;
    public bool midWave = true;
    public int[,] waveData;
    public int currentWave = 0;
    public Canvas waveDisplay;
    public Camera camera2;

    public GameObject enemy;


    // Start is called before the first frame update
    void Start()
    {
        waveData = WaveData.construct();
    }

    // Update is called once per frame
    void Update()
    {




        waveDisplay.GetComponentInChildren<Text>().text = "Wave: " + currentWave;
        
        if(timeSinceSpawn > waveData[2,currentWave] && totalSpawns< waveData[0,currentWave]){
            midWave=true;
            timeSinceSpawn = 0;
            enemy.GetComponent<EnemyController>().health = waveData[1,currentWave];
            Instantiate(enemy, new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z), new Quaternion());
            totalSpawns++;
        } 
        if(totalSpawns >= waveData[0,currentWave]){
            midWave=false;
        }
            
        


        Vector3 mouseLocation = camera2.ScreenToWorldPoint(Input.mousePosition);

        if(Input.GetMouseButtonDown(0)){
            if(mouseLocation.x > 7 && mouseLocation.x<9 && mouseLocation.y> 2 && mouseLocation.y < 2.7){
                
                if(waveData[0,currentWave] == totalSpawns){
                    totalSpawns = 0;
                    currentWave++;
                    
                    GameObject[] turretControllers  = GameObject.FindGameObjectsWithTag("Turret");

                    for(int i =0; i<turretControllers.Length; i++){

                        //Resets the dmg contributed for this round and add to the total.

                        turretControllers[i].GetComponent<TurretTurret>().dmgContributedTotal += turretControllers[i].GetComponent<TurretTurret>().dmgContributed;
                        turretControllers[i].GetComponent<TurretTurret>().dmgContributed = 0;

                    }
                    
                }
            }
        }

        

        


        timeSinceSpawn = timeSinceSpawn + Time.deltaTime;
        
        

        
    }
}
