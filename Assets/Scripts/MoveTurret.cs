using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveTurret : MonoBehaviour
{
    public bool isHeld = false;
    public int timeSincePickedUp = 0;
    public GameObject heldObject;
    public Camera camera2;
    public Canvas itemChecker;
    
    public GameObject itemController;

    public GameObject enemySpawner;
    // Start is called before the first frame update
    void Start()
    {
        enemySpawner= GameObject.FindGameObjectWithTag("EnemySpawner");
        itemController = GameObject.FindGameObjectWithTag("ItemController");
    }

    // Update is called once per frame
    void Update()
    {
        if(timeSincePickedUp<10){
            timeSincePickedUp++;
        }
        
        if(Input.GetMouseButtonDown(0)&& !isHeld && itemController.GetComponent<ItemSlammer>().wasHeld >4 && !enemySpawner.GetComponent<EnemySpawnController>().midWave){
            
            GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
            Vector3 position3 = camera2.ScreenToWorldPoint(Input.mousePosition);
            float x = position3.x;
            float y = position3.y;

            for(int i =0; i < turrets.Length; i++){
               if(x > turrets[i].transform.localPosition.x-.5 &&x < turrets[i].transform.localPosition.x+.5){
                   if(y > turrets[i].transform.localPosition.y-.5 &&y < turrets[i].transform.localPosition.y+.5){
                    Debug.Log("You clicked on a turret!!!!");
                        heldObject = turrets[i];
                        isHeld = true;
                   }
               }
            }
            return;
        }

        if(Input.GetMouseButtonDown(1)&& !isHeld && itemController.GetComponent<ItemSlammer>().wasHeld >4){
            
            GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
            Vector3 position3 = camera2.ScreenToWorldPoint(Input.mousePosition);
            float x = position3.x;
            float y = position3.y;

            for(int i =0; i < turrets.Length; i++){
               if(x > turrets[i].transform.localPosition.x-.5 &&x < turrets[i].transform.localPosition.x+.5){
                   if(y > turrets[i].transform.localPosition.y-.5 &&y < turrets[i].transform.localPosition.y+.5){
                    Debug.Log("You clicked on a turret!!!!");
                        string items = "Items on this turret: ";
                        TurretTurret trt = turrets[i].GetComponent<TurretTurret>();
                        Dictionary<string, int> item = trt.combinedItems;

                        int powers = trt.powerCore;
                        int ammos = trt.ammo;
                        int goggles = trt.goggles;
                        int accels = trt.accelerator;
                        
                        if(item["pow_pow"] >= 1){
                            items+= "(Power + Power) ";
                            if(item["pow_pow"] > 1){
                                items+= "(Power + Power) ";
                            }
                            powers--;
                            powers--;

                        }
                        if(item["pow_amm"] >= 1){
                            items+= "(Power + Ammo) ";
                            powers--;
                            ammos--;
                        }
                        if(item["pow_gog"] >= 1){
                            items+= "(Power + Goggles) ";
                            powers--;
                            goggles--;
                        }
                        if(item["pow_acc"] >= 1){
                            items+= "(Power + Accelerator) ";
                            powers--;
                            accels--;
                        }
                        if(item["amm_amm"] >= 1){
                            items+= "(Ammo + Ammo) ";
                            ammos--;
                            ammos--;
                        }
                        if(item["amm_gog"] >= 1){
                            items+= "(Ammo + Goggles) ";
                            ammos--;
                            goggles--;
                        }
                        if(item["amm_acc"] >= 1){
                            items+= "(Ammo + Accelerator) ";
                            ammos--;
                            goggles--;
                        }
                        if(item["gog_gog"] >= 1){
                            items+= "(Goggles + Goggles) ";
                            goggles--;
                            goggles--;
                        }
                        if(item["gog_acc"] >= 1){
                            items+= "(Goggles + Accelerator) ";
                            goggles--;
                            accels--;
                        }
                        if(item["acc_acc"] >= 1){
                            items+= "(Accelerator + Accelerator) ";
                            accels--;
                            accels--;
                        }

                        if(powers == 1){
                            items+= " Power";
                        }
                        if(ammos == 1){
                            items+= " Ammo";
                        }
                        if(goggles ==1){
                            items+= " Goggles";
                        }
                        if(accels == 1 ) {
                            items+= " Acclerator";
                        }
                        
                        items+= "Damage this Round: " + trt.dmgContributed;




                        itemChecker.GetComponentInChildren<Text>().text = items;
                   }
               }
            }
            return;
        }

        
        if(isHeld){
            
            Vector3 position3 = camera2.ScreenToWorldPoint(Input.mousePosition);
            float x = position3.x;
            float y = position3.y;
            heldObject.transform.SetPositionAndRotation(new Vector3(x, y, 0), new Quaternion());
            //heldObject.transform.Translate(x-heldObject.transform.localPosition.x, y-heldObject.transform.localPosition.y, 0);
        }



        if(Input.GetMouseButtonDown(0)&& isHeld && timeSincePickedUp >5){
            Vector3 position3 = camera2.ScreenToWorldPoint(Input.mousePosition);
            float x = position3.x;
            float y = position3.y;

            x = Mathf.RoundToInt(x);
            y = Mathf.RoundToInt(y);
             heldObject.transform.Translate(x-heldObject.transform.localPosition.x, y-heldObject.transform.localPosition.y, 0);
            isHeld= false;
            heldObject = null;
        }
        
    }
}
