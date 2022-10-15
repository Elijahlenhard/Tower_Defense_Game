using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceTracker : MonoBehaviour
{

    public int powerCore = 3;
    public int ammo = 2;
    public int goggles = 4;
    public int accelerator = 3;
    public int gold =0;
    public int mainHealth = 1000;
    public Canvas healthDisplay;

    public Canvas goldCounter;
    public Canvas powerCoreText;
    public Canvas ammoText;
    public Canvas gogglesText;
    public Canvas acceleratorText;
    public Canvas powerCorePrice;
    public Canvas ammoPrice;
    public Canvas gogglesPrice;
    public Canvas acceleratorPrice;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ItemSlammer slammer = this.gameObject.GetComponent<ItemSlammer>();
        goldCounter.GetComponentInChildren<Text>().text = "Gold: " + gold;
        powerCoreText.GetComponentInChildren<Text>().text = "Owned: " + powerCore + "";
        ammoText.GetComponentInChildren<Text>().text = "Owned: " +ammo + "";
        gogglesText.GetComponentInChildren<Text>().text = "Owned: " +goggles + "";
        acceleratorText.GetComponentInChildren<Text>().text = "Owned: " +accelerator + "";
        powerCorePrice.GetComponentInChildren<Text>().text = "Price: " + Constants.ITEM_PRICE*(1 + Constants.ITEM_PRICE_MARKUP*slammer.powerCoresBought) + "";
        ammoPrice.GetComponentInChildren<Text>().text = "Price: " + Constants.ITEM_PRICE*(1 + Constants.ITEM_PRICE_MARKUP*slammer.ammoBought) + "";
        gogglesPrice.GetComponentInChildren<Text>().text = "Price: " + Constants.ITEM_PRICE*(1 + Constants.ITEM_PRICE_MARKUP*slammer.gogglesBought) + "";
        acceleratorPrice.GetComponentInChildren<Text>().text = "Price: " + Constants.ITEM_PRICE*(1 + Constants.ITEM_PRICE_MARKUP*slammer.acceleratorsBought) + "";
        healthDisplay.GetComponentInChildren<Text>().text = "Health: " + mainHealth;

    }
    
        
}   
