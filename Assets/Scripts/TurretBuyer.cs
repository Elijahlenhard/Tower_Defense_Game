using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretBuyer : MonoBehaviour
{
    public GameObject turret;
    public GameObject sniperTurret;
    public Camera camera2;
    public float turretCost =10;
    public float sniperTurretCost =20;
    public Canvas priceDisplay;
    public Canvas sniperPriceDisplay;
    
    // Start is called before the first frame update
    void Start()
    {
        priceDisplay.GetComponentInChildren<Text>().text = "Price: " + (int)turretCost;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 position3 = camera2.ScreenToWorldPoint(Input.mousePosition);
            float x = position3.x*1.333333f;
            float y = position3.y*1.333333f;

            //Debug.Log("Curret mouse position is: " + x + ", " + y);
        if(Input.GetMouseButtonDown(0)){
            Debug.Log("x: " + x + "     y: " + y);
            if(y<-4)
                {
                    
                    GameObject controller = GameObject.FindGameObjectWithTag("ItemController");
                    
                if(x>8f && x<9f){
                    if(controller.GetComponent<ResourceTracker>().gold >= turretCost){
                
                        Debug.Log("Turret was bought!!!");
                        controller.GetComponent<ResourceTracker>().gold -= (int)turretCost;
                        turretCost = turretCost*Constants.TURRET_PRICE_MARKUP;
                        priceDisplay.GetComponentInChildren<Text>().text = "Price: " + (int)turretCost;
                        
                        

                        MoveTurret turretMover = controller.GetComponent<MoveTurret>();

                        

                        GameObject instantiatedTurret = Instantiate(turret, new Vector3(8.5f, -3.5f, 0), new Quaternion());

                        turretMover.isHeld = true;
                        turretMover.heldObject = instantiatedTurret;
                        turretMover.timeSincePickedUp =0;
                    }
                


                }
                if(x>4f && x<5f){
                    if(controller.GetComponent<ResourceTracker>().gold >= sniperTurretCost){
                
                        Debug.Log("Sniper Turret was bought!!!");

                        controller.GetComponent<ResourceTracker>().gold -= (int)sniperTurretCost;
                        sniperTurretCost = sniperTurretCost*Constants.TURRET_PRICE_MARKUP;
                        sniperPriceDisplay.GetComponentInChildren<Text>().text = "Price: " + (int)sniperTurretCost;
                        
                        

                        MoveTurret turretMover = controller.GetComponent<MoveTurret>();

                        

                        GameObject instantiatedTurret = Instantiate(sniperTurret, new Vector3(8.5f, -3.5f, 0), new Quaternion());

                        turretMover.isHeld = true;
                        turretMover.heldObject = instantiatedTurret;
                        turretMover.timeSincePickedUp =0;
                    }
                }

            }
        }
        
    }
}
