using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;




public class EnemyInformationDatabase : MonoBehaviour {

    [SerializeField]
    private WebPlayerData webData;

    public List<EnemyPrefix> enemyPrefixDatabase = new List<EnemyPrefix>();
    private JsonData enemyPrefixData;

    public List<EnemyType> enemyTypeDatabase = new List<EnemyType>();
    private JsonData enemyTypeData;

    public List<string> enemyPrefixNames = new List<string>();
    public List<string> enemyTypeNames = new List<string>();

    int typeCount, prefixCount;


    int currentMinDamage;
    int currentMaxDamage;
    int currentHP;
    int currentLevel;
    int currentID;
    int addingCounter;

    int HPplus;
    int damagePlus;

    void Start()
    {
        
        //Tässä luodaan JsonDatat jokaiselle datalle erikseen
#if UNITY_STANDALONE
        enemyPrefixData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/enemyPrefixes.json"));       
        enemyTypeData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/enemyTypes.json"));
        typeCount = enemyTypeData.Count;
        prefixCount = enemyPrefixData.Count;

        constructDatas();
        

#elif UNITY_WEBGL
        webData.initWebData();      
        webData.buildEnemyDatas();

        typeCount = enemyTypeNames.Count;
        prefixCount = enemyPrefixNames.Count;

        constructDatas();

#endif


        printAllEnemies();



    }

    void constructDatas()
    {

        resetCurrents();

        EnemyPrefix tempPrefix;      
        EnemyType tempType;


        for (int i = 0; i < prefixCount; i++)
        {
            //Täydennetään prefixDatabase, sinne on helppo lisätä tavaraa, muokkaa vain JSON tiedosto
#if UNITY_STANDALONE
            tempPrefix = new EnemyPrefix(enemyPrefixData[i]["name"].ToString(), currentMinDamage, currentMaxDamage, currentHP, currentLevel, currentID);
#elif UNITY_WEBGL
            tempPrefix = new EnemyPrefix(enemyPrefixNames[i], currentMinDamage, currentMaxDamage, currentHP, currentLevel, currentID);
#endif


            setFixStats(i);

            currentID++;

            enemyPrefixDatabase.Add(tempPrefix);
        }

        resetCurrents();
        currentHP = 15;
        currentMinDamage = 1;
        currentMaxDamage = 3;
        for (int i = 0; i < typeCount; i++)
        {
            //Täydennetään typeDatabase, sinne on helppo lisätä tavaraa, muokkaa vain JSON tiedosto
#if UNITY_STANDALONE
            tempType = new EnemyType(enemyTypeData[i]["name"].ToString(), currentMinDamage, currentMaxDamage, currentHP, currentLevel, currentID);
#elif UNITY_WEBGL
            tempType= new EnemyType(enemyTypeNames[i], currentMinDamage, currentMaxDamage, currentHP, currentLevel, currentID);
#endif

            setFixStats(i);
            setTypeStats(i);

            currentID++;

            currentLevel += 3;

            enemyTypeDatabase.Add(tempType);
        }
    }

    void resetCurrents()
    {
        currentMinDamage = 0;
        currentMaxDamage = 1;
        currentHP = 0;
        currentLevel = 1;
        currentID = 100;
        addingCounter = 0;

        HPplus = 5;
        damagePlus = 1;
    }

    void setFixStats(int i)
    {
        if (addingCounter == 0)
        {
            currentHP += HPplus;
            addingCounter++;
        }
        else if (addingCounter == 1)
        {
            currentMinDamage += damagePlus;
            addingCounter++;
        }
        else if (addingCounter == 2)
        {
            currentMaxDamage += damagePlus;
            addingCounter = 0;
        }

        if (i % 3 == 0)
        {
            currentLevel++;
        }
    }

    void setTypeStats(int i)
    {
        currentHP += 20;
        currentMinDamage += 5;
        currentMaxDamage += 10;
    }

    //metodi joka printtaa kaikkien vihollisten nimet
    void printAllEnemies()
    {
        int count = 1;
        for (int i = 0; i < enemyTypeDatabase.Count; i++)
        {
            //Debug.Log("Enemy number: " + count + " is a " + enemyTypeDatabase[i].TypeName);
            count++;
            for (int j = 0; j < enemyPrefixDatabase.Count; j++)
            {
                 //Debug.Log("Enemy number: " + count + " is: " + enemyPrefixDatabase[j].PrefixName + " " + enemyTypeDatabase[i].TypeName);
                count++;
               

            }
        }
        //Debug.Log("Prefix amount: " + enemyPrefixDatabase.Count);
       // Debug.Log("Enemy amount: " + count);
    }
}
