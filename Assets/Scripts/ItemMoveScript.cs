using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMoveScript : MonoBehaviour
{
    public Camera camera2;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 position3 = camera2.ScreenToWorldPoint(Input.mousePosition);
            float x = position3.x*1.3333333f;
            float y = position3.y;//TODO remove modifier
        this.gameObject.transform.SetPositionAndRotation(new Vector3(x, y, 0), new Quaternion());
        
    }
}
