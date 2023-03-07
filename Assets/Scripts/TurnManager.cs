using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public bool battleFlag = false;

    public Character[] listCharacters;
    // Character[] listEnemies;

    // We create a sorted list to have the turns sorted via a key, the dexterity of the character
    public SortedList<float,Character> turnsList;
    
    
    private int enemyCounter = 0;


    private void Awake()

    {
        listCharacters = GameObject.FindObjectsOfType<Character>();
        //listEnemies = GameObject.FindObjectsOfType<Character>();
        foreach (Character obj in listCharacters) 
        { 
            turnsList.Add(obj.Dexterity.Value,obj); 
            if ( obj.CompareTag("Enemy") == true ) {enemyCounter++; }
        }

        battleFlag= true;

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( enemyCounter == 0 ) { battleFlag = false; }
    }


}
