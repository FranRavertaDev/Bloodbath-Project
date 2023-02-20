using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public enum AilmentType { Poison, Burn, Cold, Bleed }

public class AilmentManager
{   
    //Constructor: defines all the resistances and invulnerabilities
    public AilmentManager(float poisonRes, bool poisonInv, float burnRes, bool burnInv, float coldnRes, bool coldInv, float bleedRes, bool bleedInv) 
    {
        poisonResistance = poisonRes; poisonInvulnerability = poisonInv;
        burnResistance = burnRes; burnInvulnerability = burnInv;
        coldResistance= coldnRes; coldInvulnerability= coldInv;
        bleedResistance= bleedRes; bleedInvulnerability= bleedInv;
    }


    //We define the flags of the possible ailments
    public bool isPoisoned = false;
    public bool isBurned = false;
    public bool isCold = false;
    public bool isBleeding = false;

    //We add counters to know what status affects the pc/npc
    private int poisonCounter = 0;
    private int bleedCounter = 0;
    private int coldCounter = 0;
    private int burnCounter = 0;

    //We define resistances and invulnerabilities
    public float poisonResistance;
    public float burnResistance;
    public float coldResistance;
    public float bleedResistance;

    public bool poisonInvulnerability;
    public bool burnInvulnerability;
    public bool coldInvulnerability;
    public bool bleedInvulnerability;

    public List<Ailment> ailmentList = new List<Ailment>();

    public void AddAilment(Ailment ailment)
    {
        /* We add an ailment and we increase the counter of the ailment and switch the flag to true. */
        ailmentList.Add(ailment);
        switch (ailment.ailmentType)
        {
            case AilmentType.Poison: poisonCounter++; isPoisoned = true; break;
            case AilmentType.Cold: coldCounter++; isCold = true; break;
            case AilmentType.Bleed: bleedCounter++; isBleeding = true; break;
            case AilmentType.Burn: burnCounter++; isBurned = true; break;
        }
    }

    public void ApplyAilments()
    {
        /* Whe apply the ailments. If the time of the ailment is zero, we have to erase it from the list and
         * make the counter of the ailment one less unit. If it equals zero, then it is no longer affected
         * 
         */

        float resistance = 1.0f;
        bool invulnerability = false;

        // We iterate the list with a backward for loop to erase the elements that we dont want anymore (we cant in a forward one because the index gets altered)
        for (int i = ailmentList.Count - 1; i >= 0; i--)
        {
            switch (ailmentList[i].ailmentType)
            {
                case AilmentType.Poison: resistance = poisonResistance; invulnerability = poisonInvulnerability; break;
                case AilmentType.Cold: resistance = coldResistance; invulnerability = coldInvulnerability; break;
                case AilmentType.Burn: resistance = burnResistance; invulnerability = burnInvulnerability; break;
                case AilmentType.Bleed: resistance = bleedResistance; invulnerability = bleedInvulnerability; break;
            }

            ailmentList[i].ApplyEffect(resistance, invulnerability);
            
            if (ailmentList[i].turnCounter == 0)
            {
                switch (ailmentList[i].ailmentType)
                {
                    case AilmentType.Poison:
                        poisonCounter--;
                        if (poisonCounter == 0) { isPoisoned = false; }
                        break;
                    case AilmentType.Cold:
                        coldCounter--;
                        if (coldCounter == 0) { isCold = false; }
                        break;
                    case AilmentType.Burn:
                        burnCounter--;
                        if (burnCounter == 0) { isBurned = false; }
                        break;
                    case AilmentType.Bleed:
                        bleedCounter--;
                        if (bleedCounter == 0) { isBleeding = false; }
                        break;
                }
            }
            ailmentList.RemoveAt(i);
        }

    }

    public void RemoveAilment(Ailment ailment)
    {
        ailmentList.Remove(ailment);
    }

    public void RemoveAllAilments()
    {
        ailmentList.Clear();
    }

    public void RemoveAilmentByType(AilmentType type)
    {
        for (int i = ailmentList.Count - 1; i >= 0; i--)
        {
            if (ailmentList[i].ailmentType == type) 
            {
                ailmentList.RemoveAt(i);
            }
        }
    }

}

public class Ailment 
{   
    public AilmentType ailmentType;

    public float baseDamage;
    public int turnCounter;

    public Ailment(AilmentType ailmentType, float baseDamage, int turnCounter)
    {
        this.ailmentType = ailmentType;
        this.baseDamage = baseDamage;
        this.turnCounter = turnCounter;
    }

    //public void SetTurnCounter(int counter) { turnCounter = counter; }
    //public int GetTurnCounter() { return turnCounter; }

    //public void SetBaseDamage(float damage) { baseDamage = damage; }
    //public float GetBaseDamage() { return baseDamage; }


    public void ApplyEffect(float resistance, bool invulnerability)
    {
        switch (ailmentType)
        {
            case AilmentType.Poison: ApplyPoisonEffect(resistance, invulnerability); break;
            case AilmentType.Burn: ApplyBurnEffect(resistance, invulnerability); break;
            case AilmentType.Bleed: ApplyBleedEffect(resistance, invulnerability); break;
            case AilmentType.Cold: ApplyColdEffect(resistance, invulnerability); break;

            default: 
                break;
        }

        turnCounter--;

    }

    private void ApplyPoisonEffect(float resistance, bool invulnerability)
    {
        //define functionality
    }

    private void ApplyBurnEffect(float resistance, bool invulnerability)
    {
        //define functionality
    }

    private void ApplyColdEffect(float resistance, bool invulnerability)
    {
        //defina functionality
    }

    private void ApplyBleedEffect(float resistance, bool invulnerability)
    {
        //define functionality
    }
}

