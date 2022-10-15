using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProjectileController : MonoBehaviour
{
    public float vx = 0;
    public bool revedAttack = false;
    public bool willSlow = false;
    public float speed =1;
    public float vy = 0;
    private bool isCrit = false;

    public bool isBonus = false;

    public float damage = 0;
    public float onHitDamage =0;
    public Boolean hasPierced = false;
    public float timeSincePierce = 0f; //Temp maybe but this might be final

    public GameObject parentTurret;
    
    // Start is called before the first frame update
    void Start()
    {
        TurretTurret parent = parentTurret.GetComponent<TurretTurret>();

        if(parent.turretType.Equals("Sniper")){
            this.gameObject.GetComponent<Renderer>().material.color= Color.magenta;
        }
        if(parent.turretType.Equals("Base")){
            this.gameObject.GetComponent<Renderer>().material.color= Color.cyan;
        }

        
        if(willSlow){
            this.gameObject.GetComponent<Renderer>().material.color= new Color(0, 4, 50);
        }
       
        
        
        damage += parent.damage;
        if(parent.combinedItems["acc_acc"] >=1){
            damage = damage*Constants.ACC_ACC_DMG_RATIO*parent.combinedItems["acc_acc"];
        }
        if(parent.combinedItems["amm_amm"] >=1){
            float dmgToAdd = onHitDamage + Constants.AMM_AMM_DMG_PER_WAVE * GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawnController>().currentWave;
            if(dmgToAdd>60){
                dmgToAdd = 60;
            }
            onHitDamage +=dmgToAdd;
        }
        damage += damage*Constants.POWER_CORE_BONUS*parent.powerCore;

        onHitDamage += Constants.AMMO_DMG*parent.ammo;
        if(parent.combinedItems["pow_amm"]>=1){
        onHitDamage += (Constants.POW_AMM_BONUS_PER_INCREMENT*parent.combinedItems["pow_amm"])*(parent.damage/Constants.POW_AMM_INCREMENT)*damage;
        }
        if(parent.combinedItems["acc_acc"] >=1){
            onHitDamage = onHitDamage*Constants.ACC_ACC_ONHIT_DMG_RATIO*parent.combinedItems["acc_acc"];
        }

        damage = damage+onHitDamage;

        if(parent.combinedItems["pow_gog"] >=1){
            damage = damage*(1+ Constants.POW_GOG_DMG_BONUS_PERCENT*(parent.projectileSpeed + Constants.POWER_GOG_BONUS*parent.combinedItems["pow_gog"])
            *parent.combinedItems["pow_gog"]);
        }

        
        

        
        System.Random random = new System.Random();
        float critTest = (float)random.NextDouble();
        //Debug.Log(critTest);
       
        if(critTest < Constants.GOGGLE_CRIT_CHANCE*parent.goggles || parent.combinedItems["gog_gog"] > 0){
            isCrit = true;
            
            damage = damage*2;
            if(parent.combinedItems["amm_gog"] > 0){
                damage = damage*parent.combinedItems["amm_gog"]*Constants.AMM_GOG_BONUS_CRIT;
            }
        }
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(timeSincePierce < 5 && hasPierced){
            timeSincePierce = timeSincePierce+Time.deltaTime;
        }
        TurretTurret parent = parentTurret.GetComponent<TurretTurret>();

        

        float x1 = this.transform.localPosition.x;
        float y1 = this.transform.localPosition.y;

        if(Mathf.Abs(x1) >9 || Mathf.Abs(y1) > 6){
            
            Destroy(this.gameObject);
            
            
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Vector[] vectorsToEnemies = new Vector[enemies.Length];
        for(int i =0; i< enemies.Length; i++){
            vectorsToEnemies[i] = new Vector(enemies[i].transform.localPosition.x - x1 , enemies[i].transform.localPosition.y - y1);
        }
        //Debug.Log("Number of amm_acc : " + parent.combinedItems["amm_acc"]);
        
        for (int i =0; i < vectorsToEnemies.Length; i++){
            if(vectorsToEnemies[i].getMagSquared() < 1f && (timeSincePierce>.5f || !hasPierced)){
                //Debug.Log("Projectile made second hit with hasPierced as "+ hasPierced + " timeSincePierce as " + timeSincePierce + " and amm_acc " + parent.combinedItems["amm_acc"]);
                EnemyController Enemy = enemies[i].GetComponent<EnemyController>();
                Enemy.damage(damage, "projectile", parent);
                parent.dmgContributed += damage;
                if(willSlow){
                    Enemy.applySlow();
                    
                }
                if(parent.combinedItems["pow_pow"] >=1){
                    Enemy.applyBurn(parent, Enemy.maxHealth*Constants.POW_POW_DMG_PER_SECOND_PERCENT + Constants.POW_POW_DMG_PER_SECOND, Constants.POW_POW_BURN_DURRATION);
                }
                
                //Debug.Log(revedAttack);

                if(parent.combinedItems["gog_acc"]>=1  && isCrit && !isBonus){
                  
                  parent.fire(true);
                  
                }
                
                if(parent.combinedItems["amm_acc"]>=1 && !hasPierced){
                   // Debug.Log("Projectile made first hit");
                    hasPierced = true;
                    damage = damage*Constants.PIERCE_DMG;
                    
                    timeSincePierce = 0;
                    break;
                }else {
                    //Debug.Log("projectile was distroyed with haspierced as" + hasPierced + " timeSincePierce as " + timeSincePierce + " and amm_acc " + parent.combinedItems["amm_acc"]);
                    Destroy(this.gameObject);
                }
                
            }
        }

        this.transform.Translate((vx*Time.deltaTime*speed), (vy*Time.deltaTime*speed),0);
        
    }


}
