using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Vector
{
    
    private float x;
    private float y;

    public Vector(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
    public float getX()
    {
        return this.x;
    }
    public float getY()
    {
        return this.y;
    }
    public void setX(float x)
    {
        this.x = x;
    }
    public void setY(float y)
    {
        this.y = y;
    }
    public void addVector(float x2, float y2)
    {
        this.x = x2 + x;
        this.y = y2 + y;
    }
    public double dotProduct(float x2, float y2)
    {
        return (x * x2) + (y * y2);
    }
    public void multiply(float magnitude){
        this.x = this.x * magnitude;
        this.y = this.y * magnitude;
    }
    public void subtractVector(float x2, float y2)
    {
        x = x - x2;
        y = y - y2;
    }
    public float getEulerAngle(){
        
        float rawAngle = Mathf.Atan(y/x);
        rawAngle = rawAngle*180/Mathf.PI;
        rawAngle = Mathf.Abs(rawAngle);
        

        if(x>0 && y>0){
            return 270+rawAngle;
        }
        if(y>0 && x<0){

            return 90-rawAngle;

        }
        if(y<0 && x<0){

            return 90+rawAngle;
        }
        if(y<0 && x>0){
            return 270-rawAngle;
        }
        else return rawAngle;


    }
    public void normalize()
    {
        float mag = (float)Math.Sqrt(getMagSquared());
        x = x / mag;
        y = y / mag;

    }
    
    public float getMagSquared()
    {
        return x * x + y * y;
    }

    public string toString(){

        return ("[" + x + "  " + y + "]");

    }


    
}
