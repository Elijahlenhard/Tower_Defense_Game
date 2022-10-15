using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{



    public static float POWER_CORE_BONUS = .35f; 
    public static float AMMO_DMG = 10;
    public static float GOGGLE_CRIT_CHANCE = .33f;
    public static float ACCELERATOR_BONUS = .35f;

    public static float ITEM_PRICE = 10f;
    public static float ITEM_PRICE_MARKUP = .5f;

   



    public static float POW_POW_DMG_PER_SECOND_PERCENT = .015f; //Percent maxHealth damage on burn item
    public static float POW_POW_DMG_PER_SECOND = 10f; // base dmg per second on burn item
    public static float POW_POW_BURN_DURRATION = 3f; // burn durration for burn item

    public static float POW_AMM_BONUS_PER_INCREMENT = .1f; // Percent increase per 10 dmg
    public static float POW_AMM_INCREMENT = 10f; 

    public static float POWER_AMMO_BONUS = .3f; // base on-hit dmg bonus for power ammo

    public static float POWER_GOG_BONUS = (.4f); //Percent projectile speed increase
    public static float POW_GOG_DMG_BONUS_PERCENT = (.05f); //percent dmg increase for each 1 missle speed

    public static float POW_ACC_SLOW_PERCENT = 1 - (.35f); // percent slow
    public static float POW_ACC_SLOW_DURATION = 2f; // durration of slow
    public static float MAX_SLOW_SPEED_STACK = 2; //minimum time between slowing shots
    public static float TIME_BETWEEN_POW_ACC_SLOW = 4f; //base time between slowing shots

    

    public static float AMM_AMM_DMG_PER_WAVE = 1; // on-hit dmg gained per wave
    public static float AMM_AMM_DMG_MAX = 60; // max dmg gained though this stacking
    
    public static float AMM_GOG_BONUS_CRIT = 1.5f; // crit dmg with ammo goggles


    //Accelerator Accelerator
    public static float ACC_ACC_DMG_RATIO = .35f; //base dmg decrease with accacc
    public static float ACC_ACC_ONHIT_DMG_RATIO = .75f; // on-hit dmg decrease with acc
    public static float ACC_ACC_ATTACKSPEED_RATIO = 1.75f; //attack speed gained


    
    
    public static float PIERCE_DMG = .75f; //dmg percent for pierce shot

    public static int GOG_ACC_BONUS_ATTACKS = 2; //num of attacks with double speed

    public static float ITEM_DROP_PITY = 0.1f; // percent increase for each enemy killed with no item

    public static float TURRET_PRICE_MARKUP = 1.5f; // percent markup per turret bought
    
    
}
