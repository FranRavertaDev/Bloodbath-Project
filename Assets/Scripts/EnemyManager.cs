using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //We define the stats and the ailments
    public CharacterStat Strength = new CharacterStat(5);
    public CharacterStat Dexterity = new CharacterStat(5);
    public CharacterStat Constitution = new CharacterStat(5);
    public CharacterStat Appearence = new CharacterStat(5);
    public CharacterStat Intelligence = new CharacterStat(5);
    public CharacterStat Wisdom = new CharacterStat(5);
    public CharacterStat Charisma = new CharacterStat(5);
    public CharacterStat Size = new CharacterStat(5);
    public CharacterStat Will = new CharacterStat(5);

    public AilmentManager ailmentManager = new AilmentManager(1.0f, false, 1.0f, false, 1.0f, false, 1.0f, false);

    public HealthCounter healthCounter = new HealthCounter(100, 100);

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
