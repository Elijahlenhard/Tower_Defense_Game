using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTurret : MonoBehaviour
{

    public float timeSinceSlow = 0;

    public string turretType = "";

    public int completedItems = 0;
    public float baseDmg = 20;
    public float damage = 10;
    public float baseAttackSpeed = 1;
    private float missleSpeed = 3;
    public float attackSpeed = 1;
    public float projectileSpeed = 1;
    public float timeSinceAttack =0;


    private bool noEnemies = true;

    public bool reved = false;
    public int revedAttacks = 0;

    
    
    public int accelerator = 0;
    public int powerCore = 0;
    public int ammo = 0;
    public int goggles = 0;
    public int fuel = 0;


    public Dictionary<string, int> combinedItems = new Dictionary<string, int>(); //not currently used!!!! might swap to this system later
    
    
    public bool oneAccelerator = false;
    public bool onePowerCore = false;
    public bool oneAmmo = false;
    public bool oneGoggles =false;
    public bool oneFuel = false;

    public float dmgContributed = 0;
    public float dmgContributedTotal = 0;





    
    
    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        
        //pow power
        //amm ammo
        //gog goggles
        //acc Accelerator
        combinedItems.Add("pow_pow", 0);
        combinedItems.Add("pow_amm", 0);
        combinedItems.Add("pow_gog", 0);
        combinedItems.Add("pow_acc", 0);

        combinedItems.Add("amm_amm", 0);
        combinedItems.Add("amm_gog", 0);
        combinedItems.Add("amm_acc", 0);

        combinedItems.Add("gog_gog", 0);
        combinedItems.Add("gog_acc", 0);
        
        combinedItems.Add("acc_acc", 0);


        
    
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceSlow = timeSinceSlow+Time.deltaTime;
        
        //Debug.Log(reved + " is reved");
        if(revedAttacks == Constants.GOG_ACC_BONUS_ATTACKS){
            //Debug.Log(revedAttacks + " reved attacks " + reved + " is reved");
            reved = false;
            revedAttacks = 0;
        }
        
        Vector closest = getVectorToClosestTarget();
        timeSinceAttack = timeSinceAttack+Time.deltaTime;
        if(timeSinceAttack >= 1/(baseAttackSpeed+baseAttackSpeed*accelerator*Constants.ACCELERATOR_BONUS) 
        && !noEnemies
        || 
        (!noEnemies 
        && combinedItems["acc_acc"] >= 1 
        && timeSinceAttack>=(1/(baseAttackSpeed+baseAttackSpeed*accelerator*Constants.ACCELERATOR_BONUS)*1/Constants.ACC_ACC_ATTACKSPEED_RATIO)))
        {
            
            timeSinceAttack = 0;
            Debug.Log("I attacked!!!");
            fire(false);
            

        }
    }

    public void fire(bool bonus){
            Vector closest = getVectorToClosestTarget();
            closest.normalize();

        
            

            projectile.GetComponent<ProjectileController>().vx =  missleSpeed * (float)closest.getX();
            projectile.GetComponent<ProjectileController>().vy =  missleSpeed * (float)closest.getY();
            if(combinedItems["pow_gog"] >= 1){
                 projectile.GetComponent<ProjectileController>().vx =  (missleSpeed + Constants.POWER_GOG_BONUS*combinedItems["pow_gog"])  * (float)closest.getX();
                 projectile.GetComponent<ProjectileController>().vy =  (missleSpeed + Constants.POWER_GOG_BONUS*combinedItems["pow_gog"])  * (float)closest.getY();
            }

            projectile.GetComponent<ProjectileController>().parentTurret = this.gameObject;
            projectile.GetComponent<ProjectileController>().speed = projectileSpeed;
            float slowTimeBonus;
            if(attackSpeed<Constants.MAX_SLOW_SPEED_STACK){
                slowTimeBonus = attackSpeed;
            }else{
                slowTimeBonus= Constants.MAX_SLOW_SPEED_STACK;
            }
            if(timeSinceSlow+slowTimeBonus>Constants.TIME_BETWEEN_POW_ACC_SLOW && combinedItems["pow_acc"] >=1){
                projectile.GetComponent<ProjectileController>().willSlow = true;
                timeSinceSlow = 0;
                
            }else{
                projectile.GetComponent<ProjectileController>().willSlow = false;
            }

            if(bonus){
                projectile.GetComponent<ProjectileController>().isBonus = true;
            } else {
                projectile.GetComponent<ProjectileController>().isBonus = false;
            }

            Instantiate(projectile, new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z), new Quaternion());

            float newAngle = 0;

            newAngle= closest.getEulerAngle();
            

            Vector3 currentRotation = this.transform.localEulerAngles;

            transform.Rotate(new Vector3(0, 0, newAngle-currentRotation.z), Space.Self);
    }

    private Vector getVectorToClosestTarget(){
        
        float x1 = this.transform.localPosition.x;
        float y1 = this.transform.localPosition.y;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Length<1) {
            noEnemies = true;
            return new Vector(1, 1);
        }else {
            noEnemies = false;
        }
        Vector[] vectorsToEnemies = new Vector[enemies.Length];
        int indexOfSmallest = 0;
        for(int i =0; i< enemies.Length; i++){
            
            vectorsToEnemies[i] = new Vector(enemies[i].transform.localPosition.x - x1 , enemies[i].transform.localPosition.y - y1);
            if(vectorsToEnemies[i].getMagSquared() < vectorsToEnemies[indexOfSmallest].getMagSquared()){
                indexOfSmallest = i;
            }
        }

        return vectorsToEnemies[indexOfSmallest];
    }
}
