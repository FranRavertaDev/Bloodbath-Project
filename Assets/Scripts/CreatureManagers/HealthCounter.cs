using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCounter 
{

    private float minHealth = 0;
    private float maxHealth = 100;
    private float health = 100;

    public HealthCounter(float currentHealth, float maxH)
    {
        health = currentHealth;
        maxHealth = maxH;
    }

    public void ChangeMinHealth(float newMin)
    {
        minHealth= newMin;
    }

    public void ChangeMaxHealth(float newMax)
    {
        maxHealth= newMax;
    }

    public void ChangeHealth(float newHealth)
    {
        health = newHealth;
    }

    public float GetHealth()
    {
        return health;
    }

}
