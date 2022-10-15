using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private float timeAlive =0;
    public float Vy = -.5f;
    // Start is called before the first frame update
    void Start()
    {
        float offset = 0;
        System.Random rand = new System.Random();
        if(rand.NextDouble() > .5){
            offset = offset + (float)rand.NextDouble();
        } else{
            offset = offset - (float)rand.NextDouble();
        }
        
        this.transform.SetPositionAndRotation(new Vector3(this.transform.localPosition.x+offset, this.transform.localPosition.y, this.transform.localPosition.z), new Quaternion());

        
    }

    // Update is called once per frame
    void Update()
    {
        timeAlive = timeAlive + Time.deltaTime;
        if(timeAlive>1.5){
            Destroy(this.gameObject);
        }
        
        this.transform.Translate(0, Vy*Time.deltaTime, 0);


    }
}
