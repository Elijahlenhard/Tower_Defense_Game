using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlammer : MonoBehaviour
{

    public bool itemIsHeld = false; 
    public int wasHeld = 0;
    public Camera camera2;
    public GameObject heldItem;
    public GameObject powerCore;
    public GameObject ammo;
    public GameObject goggles;
    public GameObject accelerator;
    private bool replacedThisFrame = false;

    public int powerCoresBought = 0;
    public int ammoBought = 0;
    public int gogglesBought = 0;
    public int acceleratorsBought = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        replacedThisFrame = false;
        if(itemIsHeld) wasHeld = 0;
        if(wasHeld <5 && !itemIsHeld){
            wasHeld++;
        }
        
        if(itemIsHeld && Input.GetMouseButtonDown(0)){
            Vector3 position3 = camera2.ScreenToWorldPoint(Input.mousePosition);
            float x = position3.x;
            float y = position3.y;

            GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
            Vector[] turretLocations = new Vector[turrets.Length];

            for(int i = 0; i < turrets.Length; i++){
                turretLocations[i] = new Vector((turrets[i].transform.localPosition.x), (turrets[i].transform.localPosition.y));
            }
            if(y>-5 && y< -4 &&x>-9 && x<-5){
                bool itemReplaced = false;
                if(x>-9 && x<-8 && heldItem.name.Equals("PowerCore")){
                        this.GetComponent<ResourceTracker>().powerCore++;
                        itemReplaced = true;
                    }
                    if(x>-8&& x<-7&&heldItem.name.Equals("Ammo")){
                        this.GetComponent<ResourceTracker>().ammo++;
                        itemReplaced = true;
                    }
                    if(x>-7 && x< -6&&heldItem.name.Equals("Goggles")) {
                        this.GetComponent<ResourceTracker>().goggles++;
                        itemReplaced = true;
                    }
                    if(x>-6 &&x<-5 && (heldItem.name.Equals("Accelerator"))) {
                        this.GetComponent<ResourceTracker>().accelerator++;
                        itemReplaced = true;
                    }
                    if(itemReplaced){
                    GameObject item2 = GameObject.FindGameObjectWithTag("HeldItem");
                    Destroy(item2.gameObject);
                    itemIsHeld = false;
                    replacedThisFrame = true;
                    }
                    
                }
                
            for(int i = 0; i < turrets.Length; i++){
                if(x<turretLocations[i].getX()+.5f && x>turretLocations[i].getX()-.5f && y>turretLocations[i].getY()-.5f &&y<turretLocations[i].getY()+.5f){
                    
                    bool itemSlammed = false;

                    if(heldItem.name.Equals("PowerCore")){
                        itemSlammed = slamPowerCore(turrets[i]);
                    }
                    if(heldItem.name.Equals("Ammo")){
                        itemSlammed = slamAmmo(turrets[i]);
                    }
                    if(heldItem.name.Equals("Goggles")){
                       itemSlammed = slamGoggles(turrets[i]);
                    }
                    if(heldItem.name.Equals("Accelerator")){
                        itemSlammed = slamAccelerator(turrets[i]);
                    }
                    
                    if(itemSlammed){
                    GameObject item2 = GameObject.FindGameObjectWithTag("HeldItem");
                    Destroy(item2.gameObject);
                    itemIsHeld = false;
                    }

                  
                }
            }
            
        }
        
        if(Input.GetMouseButtonDown(0)&& !itemIsHeld && !replacedThisFrame){
            
            Vector3 position3 = camera2.ScreenToWorldPoint(Input.mousePosition);
            float x = position3.x;
            float y = position3.y;
            if(y<-4)
            {
                if(x>-9 && x<-8){
                    itemIsHeld = pickUpPowerCore();
                }
                if(x>-8&& x<-7){
                    itemIsHeld = pickUpAmmo();
                }
                if(x>-7 && x< -6) {
                    itemIsHeld = pickUpGoggles();
                }
                if(x>-6 &&x<-5) {
                    itemIsHeld = pickUpAccelerator();
                }
            }




        }

    }

    bool slamPowerCore(GameObject turret){
        TurretTurret controller = turret.GetComponent<TurretTurret>();
        if(turret.GetComponent<TurretTurret>().completedItems>=2){
            return false;
        }

        if(controller.onePowerCore){
            controller.onePowerCore = false;
            controller.combinedItems["pow_pow"]++;
            controller.powerCore++;
            turret.GetComponent<TurretTurret>().completedItems++;
            return true;
        }
        if(controller.oneAmmo){
            controller.oneAmmo = false;
            controller.combinedItems["pow_amm"]++;
            controller.powerCore++;
            turret.GetComponent<TurretTurret>().completedItems++;
            return true;
        }
        
        if(controller.oneGoggles){
            controller.oneGoggles = false;
            controller.combinedItems["pow_gog"]++;
            controller.powerCore++;
            turret.GetComponent<TurretTurret>().completedItems++;
            return true;
        }
        if(controller.oneAccelerator){
            controller.oneAccelerator = false;
            controller.combinedItems["pow_acc"]++;
            controller.powerCore++;
            turret.GetComponent<TurretTurret>().completedItems++;
            return true;
        }

        //TODO: more ifs to check for all item combos!!!

        controller.powerCore++;
        controller.onePowerCore = true;
        return true;

    }
    bool slamAmmo(GameObject turret){
        TurretTurret controller = turret.GetComponent<TurretTurret>();

        if(controller.completedItems>=2){
            return false;
        }

        if(controller.onePowerCore){
            controller.onePowerCore = false;
            controller.combinedItems["pow_amm"]++;
            controller.ammo++;
            turret.GetComponent<TurretTurret>().completedItems++;
            return true;
        }
        if(controller.oneAmmo){
            controller.oneAmmo = false;
            controller.combinedItems["amm_amm"]++;
            controller.ammo++;
            turret.GetComponent<TurretTurret>().completedItems++;
            return true;
        }
        if(controller.oneGoggles){
            controller.oneGoggles = false;
            controller.combinedItems["amm_gog"]++;
            controller.ammo++;
            turret.GetComponent<TurretTurret>().completedItems++;
            return true;
        }
        if(controller.oneAccelerator){
            controller.oneAccelerator = false;
            controller.combinedItems["amm_acc"]++;
            controller.ammo++;
            turret.GetComponent<TurretTurret>().completedItems++;
            return true;
        }


        controller.ammo++;
        controller.oneAmmo = true;
        return true;

    }
    bool slamGoggles (GameObject turret){
        TurretTurret controller = turret.GetComponent<TurretTurret>();

        if(controller.completedItems>=2){
            return false;
        }
        if(controller.onePowerCore){
            controller.onePowerCore = false;
            controller.combinedItems["pow_gog"]++;
            controller.goggles++;
            turret.GetComponent<TurretTurret>().completedItems++;
            return true;
        }
        if(controller.oneAmmo){
            controller.oneAmmo = false;
            controller.combinedItems["amm_gog"]++;
            controller.goggles++;
            turret.GetComponent<TurretTurret>().completedItems++;
            return true;
        }
        if(controller.oneGoggles){
            controller.oneGoggles = false;
            controller.combinedItems["gog_gog"]++;
            controller.goggles++;
            turret.GetComponent<TurretTurret>().completedItems++;
            return true;
        }
        if(controller.oneAccelerator){
            controller.oneAccelerator = false;
            controller.combinedItems["gog_acc"]++;
            controller.goggles++;
            turret.GetComponent<TurretTurret>().completedItems++;
            return true;
        }

        controller.goggles++;
        controller.oneGoggles = true;
        return true;

    }
    bool slamAccelerator(GameObject turret){

        TurretTurret controller = turret.GetComponent<TurretTurret>();

        if(controller.completedItems>=2){
            return false;
        }
        if(controller.onePowerCore){
            controller.onePowerCore = false;
            controller.combinedItems["pow_acc"]++;
            controller.accelerator++;
            turret.GetComponent<TurretTurret>().completedItems++;
            return true;
        }
        if(controller.oneAmmo){
            controller.oneAmmo = false;
            controller.combinedItems["amm_acc"]++;
            controller.accelerator++;
            turret.GetComponent<TurretTurret>().completedItems++;
            return true;
        }
        if(controller.oneGoggles){
            controller.oneGoggles = false;
            controller.combinedItems["gog_acc"]++;
            controller.accelerator++;
            turret.GetComponent<TurretTurret>().completedItems++;
            return true;
        }
        if(controller.oneAccelerator){
            controller.oneAccelerator = false;
            controller.combinedItems["acc_acc"]++;
            controller.accelerator++;
            turret.GetComponent<TurretTurret>().completedItems++;
            return true;
        }

        controller.accelerator++;
        controller.oneAccelerator = true;
        return true;


    }
    bool pickUpPowerCore (){
        

        if(this.GetComponent<ResourceTracker>().powerCore > 0){
            heldItem = powerCore;
            Instantiate(heldItem, new Vector3(-8.5f, -3.5f, 0), new Quaternion());
            this.GetComponent<ResourceTracker>().powerCore--;
            
            return true;
        } else 
        if(this.GetComponent<ResourceTracker>().gold >= Constants.ITEM_PRICE*(1 + Constants.ITEM_PRICE_MARKUP*powerCoresBought)){
            heldItem = powerCore;
            Instantiate(heldItem, new Vector3(-8.5f, -3.5f, 0), new Quaternion());
            this.GetComponent<ResourceTracker>().gold -= (int)(Constants.ITEM_PRICE*(1 + Constants.ITEM_PRICE_MARKUP*powerCoresBought));
            powerCoresBought++;
            return true;
        }

        return false;
    }
    bool pickUpAmmo() {

        if(this.GetComponent<ResourceTracker>().ammo > 0){
            heldItem = ammo;
            Instantiate(heldItem, new Vector3(-7.5f, -3.5f, 0), new Quaternion());
            this.GetComponent<ResourceTracker>().ammo--;
            
            return true;
        }
         if(this.GetComponent<ResourceTracker>().gold >= Constants.ITEM_PRICE*(1 + Constants.ITEM_PRICE_MARKUP*ammoBought)){
            heldItem = ammo;
            Instantiate(heldItem, new Vector3(-8.5f, -3.5f, 0), new Quaternion());
            this.GetComponent<ResourceTracker>().gold -= (int)(Constants.ITEM_PRICE*(1 + Constants.ITEM_PRICE_MARKUP*ammoBought));
            ammoBought++;
            return true;
        }
        else return false;
    }
    bool pickUpGoggles() {
        if(this.GetComponent<ResourceTracker>().goggles > 0){
            heldItem = goggles;
            Instantiate(heldItem, new Vector3(-7.5f, -3.5f, 0), new Quaternion());
            this.GetComponent<ResourceTracker>().goggles--;
            
            return true;
        }
         if(this.GetComponent<ResourceTracker>().gold >= Constants.ITEM_PRICE*(1 + Constants.ITEM_PRICE_MARKUP*gogglesBought)){
            heldItem = goggles;
            Instantiate(heldItem, new Vector3(-8.5f, -3.5f, 0), new Quaternion());
            
           
            this.GetComponent<ResourceTracker>().gold -= (int)(Constants.ITEM_PRICE*(1 +Constants.ITEM_PRICE_MARKUP*gogglesBought));
             gogglesBought++;
            return true;
        }
        else return false;

    }
    bool pickUpAccelerator(){
        if(this.GetComponent<ResourceTracker>().accelerator > 0){
            heldItem = accelerator;
            Instantiate(heldItem, new Vector3(-7.5f, -3.5f, 0), new Quaternion());
            this.GetComponent<ResourceTracker>().accelerator--;
            
            return true;
        }
        if(this.GetComponent<ResourceTracker>().gold >= Constants.ITEM_PRICE*(1 + Constants.ITEM_PRICE_MARKUP*acceleratorsBought)){
            heldItem = accelerator;
            Instantiate(heldItem, new Vector3(-8.5f, -3.5f, 0), new Quaternion());
            this.GetComponent<ResourceTracker>().gold -= (int)(Constants.ITEM_PRICE*(1 + Constants.ITEM_PRICE_MARKUP*acceleratorsBought));
            acceleratorsBought++;
            return true;
        }
        else return false;


    }


}
