using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodCounter 
{
    private float maxBlood = 100;
    private float blood = 100;
    private float minBlood = 0;


    public BloodCounter(float currentBlood, float maxB) 
    { 
        blood= currentBlood;
        maxBlood= maxB;
    }


    public void ChangeMinBlood(float newMin)
    {
        minBlood= newMin;
    }

    public void ChangeMaxBlood(float newMax)
    {
        maxBlood= newMax;
    }

    public void ChangeBlood(float newBlood)
    {
        if (newBlood < minBlood) { blood = minBlood; }
        else if (newBlood > maxBlood) { blood = maxBlood; }
        else { blood = newBlood; }
    }

    public float GetBlood()
    {
        return blood;
    }

}
