using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float vx = 1;

    public float timeBurning = 0;
    public float timeSinceLastTick = 0;
    public float burnDamage = 0;
    public float burnLength =0;
    public bool isBurning = false;


    public float speedMultiplier = 1f;
    public float vy = 0;
    public float health =100;
    public float maxHealth;
    public double goldDropMax = 10;
    public int goldDropMin = 5;
    private float percentItemDropChance = .35f;
    int goldDrop = 0;
    private bool isSlowed = false;
    private float timeSlowed =0;
    public Canvas healthBar;
    public Canvas test;
    public Canvas damageText;
    public Canvas test2;
    public static int killsWithNoDrop = 0;

    public TurretTurret burnedBy;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
       test = Instantiate(healthBar, this.transform.localPosition, this.transform.localRotation);
        test.transform.Translate(.35f, -.7f,0);
       
    }

    // Update is called once per frame
    void Update()
    {
       
        if(isBurning){
            //this.GetComponent<Renderer>().material.color = Color.red; 
            timeBurning = timeBurning+Time.deltaTime;
            if(timeSinceLastTick >=1){
            
                damage(burnDamage, "burn", burnedBy);
                burnedBy.dmgContributed += burnDamage;
                
                if(timeBurning>burnLength){
                    timeBurning =0;
                    isBurning = false;
                }
                timeSinceLastTick = 0;
            }else{
                timeSinceLastTick += Time.deltaTime;
            }
        } else{
            //this.GetComponent<Renderer>().material.color = Color.black;
        }

        if(isSlowed){
            timeSlowed+= Time.deltaTime;
            if(timeSlowed> Constants.POW_ACC_SLOW_DURATION){
                isSlowed=false;
            }
        }
        
        
        float x = transform.localPosition.x;
        float y = transform.localPosition.y;
        if(x> 6.5 && y>4){
            //Debug.Log("first");
            vx =0;
            vy =-1;
        }
        if(x>6.5 && y<1.5){
            //Debug.Log("sceondt");
            vx =-1;
            vy =0;
        }
        if(x<-3.5 && y<1.5){
            //Debug.Log("third");
            vx =0;
            vy = -1;
        }
        if(y< -3.5){
            //Debug.Log("four!");
            vx= 1;
            vy =0;
        }
        if(y<3.5 && x>9.5){
            Debug.Log("you took dmg!!");
            int dmg = (int)((health/maxHealth)*100);
            ResourceTracker tracker = GameObject.FindGameObjectWithTag("ItemController").GetComponent<ResourceTracker>();
            tracker.mainHealth = tracker.mainHealth-dmg;
            Destroy(this.gameObject);
        }
        

        test.GetComponentInChildren<Text>().text = health + "";

        if(isSlowed){
            speedMultiplier = Constants.POW_ACC_SLOW_PERCENT;
        } else{
            speedMultiplier = 1;
        }

        this.transform.Translate((vx*Time.deltaTime)*speedMultiplier, (vy*Time.deltaTime)*speedMultiplier,0);
        test.transform.Translate((vx*Time.deltaTime)*speedMultiplier, (vy*Time.deltaTime)*speedMultiplier,0);
        System.Random rand = new System.Random();
        goldDrop = (int)(rand.NextDouble()*(goldDropMax-goldDropMin)) + goldDropMin-1;
       
    }
    public void applySlow(){
        isSlowed =true;
        timeSlowed = 0;
    }
    public void applyBurn(TurretTurret parent, float dmg, float duration){
        isBurning = true;
        burnDamage = dmg;
        burnLength = duration;
        timeBurning = 0;
        burnedBy = parent;
    
    }

    public void damage(float dmg, string source, TurretTurret turret){
        this.health = health - dmg;
        

        test2 = Instantiate(damageText, 
        new Vector3(this.transform.localPosition.x, this.transform.localPosition.y-.5f, this.transform.localPosition.z), this.transform.localRotation);
        dmg =dmg*100;
        Mathf.RoundToInt(dmg);
        float roundedDmg = dmg/100f;;
        
        test2.GetComponentInChildren<Text>().text = roundedDmg + "";
        if(source.Equals("burn")){
            test2.GetComponentInChildren<Text>().color = Color.cyan;
        }

        if(health<=0){

            System.Random rand = new System.Random();
            
            ResourceTracker tracker = GameObject.FindGameObjectWithTag("ItemController").GetComponent<ResourceTracker>();
            tracker.gold += goldDrop;

            float giveItem = (float)rand.NextDouble();
            Debug.Log("item giver is " +giveItem + " and dropchance is " + (percentItemDropChance + percentItemDropChance*((Constants.ITEM_DROP_PITY)*killsWithNoDrop)));
            if(giveItem < percentItemDropChance + percentItemDropChance*((Constants.ITEM_DROP_PITY)*killsWithNoDrop)){
                killsWithNoDrop = 0;
                
                float itemChoser = (float)rand.NextDouble();
                

                if(itemChoser >.75){
                    tracker.powerCore++;
                }else if (itemChoser > .5){
                    tracker.ammo++;
                }else if (itemChoser>.25){
                    tracker.goggles++;
                }else{
                    tracker.accelerator++;
                }
                Debug.Log("current pity is " + killsWithNoDrop*Constants.ITEM_DROP_PITY);

            }else{
                killsWithNoDrop++;
                Debug.Log("current pity is " + killsWithNoDrop*Constants.ITEM_DROP_PITY);
            }


            Destroy(this.gameObject);
            Destroy(test.gameObject);
        }
    }
}
