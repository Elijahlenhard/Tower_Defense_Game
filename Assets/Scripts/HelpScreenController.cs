using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpScreenController : MonoBehaviour
{
    public Canvas itemHelpScreen;
    public Camera camera2;
    private bool itemHelperDisplayed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        
        Vector3 position3 = camera2.ScreenToWorldPoint(Input.mousePosition);
            float x = position3.x;
            float y = position3.y;

        if(x> -5 && x<-4 && y>-5 && y< -4){
            if(!itemHelperDisplayed){
            
            Instantiate(itemHelpScreen, new Vector3(3, -.4f,-5), new Quaternion());
            itemHelperDisplayed = true;
            }

        } else {
            itemHelperDisplayed = false;

            GameObject intItemHelp = GameObject.FindGameObjectWithTag("ItemHelpDisplay");
            Destroy(intItemHelp);
        }



        
    }
}
