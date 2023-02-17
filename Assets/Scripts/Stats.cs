using System.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class CharacterStat
/*
 * This class has the following methods: 
 * 1- AddModifier (StatModifier mod) : adds a new modifier to the list of modifiers, sorted by the order in which it is applied 
 * 2- RemoveModifier (StatModifier mod) : removes a modifier from the list
 * 3- Value : returns the stat considering all modifiers
 * 4- RemoveAllModifiersFromSource (object srce) : removes all the modifiers from a single source (item)
 * 
 * This class has the following variables:.
 * 1- baseValue : returns the base stat without the modifiers
 * 2- statModifiersRead : list of modifiers associated with a stat, read only.
 */
{
    // To be more easily extendable, all private methods and variables are changed to protected, and all properties and methods are changed to virtual

    public float baseValue; //This will be the value without modifiers
    protected float lastBaseValue = float.MinValue; // This will be used to store the last baseValue used, to check if there are changes in order to recalculate everything. 

    protected readonly List<StatModifier> statModifiers;  //This is gonna be a list
    public readonly ReadOnlyCollection<StatModifier> statModifiersRead;
    
    protected bool listHasChanges = true; //this flag is used to calculate the final value only if there are changes.
    protected float _value;               //value of the stat with all the modifiers


    public CharacterStat(float baseVal)
    {
        // The constructor initializes the stat with a baseValue and an empty List of modifiers.
        baseValue= baseVal;
        statModifiers = new List<StatModifier>();
        statModifiersRead = statModifiers.AsReadOnly(); // statModifiersReadable will be modified when statModifiers change, and it will be readable from outside
    }

    //Now we add functions that allows to add and remove modifiers to/from stats

    public virtual float Value      // Returns the final value of the stat
    {
        get 
        {
            if (listHasChanges || lastBaseValue != baseValue) // We recalculate if we changed the baseValue for any reason
            {
                lastBaseValue = baseValue;
                _value = CalculateFinalValue();
                listHasChanges = false;
            }
            return _value;
        } 
    } 
    public virtual void AddModifier (StatModifier mod)
    {
        listHasChanges = true;
        statModifiers.Add(mod);
        statModifiers.Sort(CompareModifierOrder); //We sort the modifiers given the order of importance. We give the method of ordering.
    }

    public virtual bool RemoveModifier (StatModifier mod)
    {
        if (statModifiers.Remove(mod)) //if  it removes, returns true, else returns false
        {
            listHasChanges = true;
            return true;
        }
        return false;
    }

    public virtual bool RemoveAllModifiersFromSource (object srce) //removes all modifiers from a single source
    {
        bool didRemove = false;

        for (int i = statModifiers.Count - 1; i >= 0; i--) // Look from the last to the first element of the list
        {
            if(statModifiers[i].source == srce )
            {
                listHasChanges = true;
                didRemove = true;
                statModifiers.RemoveAt(i);
            }
        }
        return didRemove;
    }


    protected virtual float CalculateFinalValue()
    {
        float finalValue = baseValue;
        float sumPercentAdd = 0; //This will hold the sum of the percentages of PercentAdd prior to multiply them to the stat. 

        for (int i = 0; i < statModifiers.Count; i++)
        {
            StatModifier mod= statModifiers[i];

            if(mod.modType == StatModType.Flat)
            {
                finalValue += statModifiers[i].value; //Adds the modifier to the final value
            } else if (mod.modType == StatModType.PercentMult)
            {
                finalValue *= 1+ mod.value;

            } else if (mod.modType == StatModType.PercentAdd)
            {

                sumPercentAdd += mod.value; // we start adding the modifiers

                //If we are at the end of the list, or if the next modifier is not of this type (as they are sorted)
                if ( i+1 >= statModifiers.Count || statModifiers[i+1].modType != StatModType.PercentAdd)
                {
                    finalValue *= 1 + sumPercentAdd;
                    sumPercentAdd = 0;
                } 


            }
            
        }

        return (float)Math.Round(finalValue,4);

    }

    protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        if(a.order < b.order) {return -1;} else if (a.order > b.order) {return 1;} else {return 0;}   
    }

}


// Class to define the modifiers

public enum StatModType { Flat = 100, PercentAdd = 200, PercentMult = 300 } // Definitions of different types of modifiers, with different mechanics
/* Flat is added directly to the stat; PercentMult multiplies the stat directly; PercentAdd are all summed and then multiply the stat.
 * The numbers make it possible to add thing in between in case of necesity.*/

public class StatModifier
{
    public readonly float value; // A readonly variable can be assigned a value only when declared
    public readonly StatModType modType;    // The type of modifier
    public readonly int order;              // The order of importance of the modifier
    public readonly object source;          // The idea is to track what thing added the modifier

    public StatModifier(float val, StatModType type, int orderVal, object srce)
    {
        // The constructor initializes the modifier with a value
        value = val;
        modType = type;
        order = orderVal;
        source = srce;
    }

    //Constructor given only value and type; the order is given an int related to the order in the StatModType enum:
    //public StatModifier(float val, StatModType type) : this(val, type, (int)type, null) { } 

    //Constructor given only value and type, order:
    //public StatModifier(float val, StatModType type) : this(val, type, order, null) { } 

    //Constructor given only value, type and source:
    //public StatModifier(float val, StatModType type) : this(val, type, (int)type, srce) { }



}


/* IDEA FOR CLASS ITEM WITH THIS IN MIND
 public class Item // Hypothetical item class
{
    public void Equip(Character c)
    {
        // We need to store our modifiers in variables before adding them to the stat.
        mod1 = new StatModifier(10, StatModType.Flat);
        mod2 = new StatModifier(0.1, StatModType.Percent);
        c.Strength.AddModifier(mod1);
        c.Strength.AddModifier(mod2);
    }
 
    public void Unequip(Character c)
    {
        // Here we need to use the stored modifiers in order to remove them.
        // Otherwise they would be "lost" in the stat forever.
        c.Strength.RemoveAllModifiersFromSource(this);
    }
}
 
 */