using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class TurnManager : MonoBehaviour
{

    public GameObject[] listPlayers;
    public GameObject[] listEnemies;

    public List<GameObject> turnsList;


    private void Awake()

    {
        listPlayers = GameObject.FindGameObjectsWithTag("Player");
        listEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in listPlayers) { turnsList.Add(obj); }
        foreach (GameObject obj in listEnemies) { turnsList.Add(obj); }

        turnsList.Sort( (x,y) => x.GetComponent<PlayerManager>().Dexterity.Value.CompareTo(y.GetComponent<EnemyManager>().Dexterity.Value) );

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
