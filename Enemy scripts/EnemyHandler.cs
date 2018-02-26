using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemyHandler : MonoBehaviour {

    EnemyInformationDatabase database;
    List<Enemy> enemies;
    List<EnemyType> enemyTypes;
    List<EnemyPrefix> enemyPrefixes;

    float randomMultiplier;
    Enemy createdEnemy;
    Enemy createdBossEnemy;

    //Init variables
    void Start () {
        database = transform.GetComponent<EnemyInformationDatabase>();
        enemyTypes = database.enemyTypeDatabase;
        enemyPrefixes = database.enemyPrefixDatabase;

	}


    //Creates a pool of possible enemies for an encounter
    private void createEnemy(int level, float difficulty)
    {
        randomMultiplier = Random.Range(0.75f, 1.25f);
        List<EnemyPrefix> prefixPool = new List<EnemyPrefix>();
        int poolSize = 0;
        //Add difficultybonus
        difficulty = difficulty * randomMultiplier;
        //Create needed temp variables
        EnemyType tempType;
        EnemyPrefix tempPrefix;
        int prefixDifficulty;

        tempType = enemyTypes[0];
        //Run a foor loop to add all enemies of diffulty smaller than parameter difficulty
        //To a list of enemies
        for (int i = 0; i < enemyTypes.Count; i++)
        {
            if(enemyTypes[i].Difficulty <= level)
            {
                tempType = enemyTypes[i];
            }
        }

        prefixDifficulty = (int)difficulty - tempType.Difficulty;

        if(prefixDifficulty > 0)
        {
            tempPrefix = enemyPrefixes[0];
            for(int i = 0; i < enemyPrefixes.Count; i++)
            {
                if(enemyPrefixes[i].Difficulty <= prefixDifficulty)
                {
                    prefixPool.Add(enemyPrefixes[i]);
                    poolSize++;
                    //Debug.Log(enemyPrefixes[i].PrefixName + " added to pool");
                }
            }
            if(poolSize > 5) tempPrefix = prefixPool[Random.Range(poolSize-5, poolSize)];
            else tempPrefix = prefixPool[Random.Range(0, poolSize)];

            createdEnemy = new Enemy(tempType, tempPrefix);
        }
        else
        {
            Debug.Log("Creted an enemy with type " + tempType.TypeName);
            createdEnemy = new Enemy(tempType);
        }


    }

    private void createBossEnemy(int level, float difficulty)
    {
        randomMultiplier = Random.Range(0.75f, 1.25f);
        int bossBonusHealth = level * 5;
        int bossBonusDamage = level * 2;
        //Add difficultybonus
        difficulty = difficulty * randomMultiplier;
        //Create needed temp variables
        EnemyType tempType;
        EnemyPrefix tempPrefix;
        int prefixDifficulty;

        tempType = enemyTypes[0];
        //Run a foor loop to add all enemies of diffulty smaller than parameter difficulty
        //To a list of enemies
        for (int i = 0; i < enemyTypes.Count; i++)
        {
            if (enemyTypes[i].Difficulty <= level)
            {
                tempType = enemyTypes[i];
            }
        }

        prefixDifficulty = (int)difficulty - tempType.Difficulty;
        prefixDifficulty += Random.Range(-2, 2);
        if (prefixDifficulty > 0)
        {
            tempPrefix = enemyPrefixes[0];
            for (int i = 0; i < enemyPrefixes.Count; i++)
            {
                if (enemyPrefixes[i].Difficulty <= prefixDifficulty)
                {
                    tempPrefix = enemyPrefixes[i];
                }
            }
            Debug.Log("Creted a boss with type and prefix " + tempPrefix.PrefixName + " " + tempType.TypeName);
            createdBossEnemy = new Enemy(tempType, tempPrefix);
            setBossStats(bossBonusHealth, bossBonusDamage);
        }
        else
        {
            Debug.Log("Creted a boss enemy with type " + tempType.TypeName);
            createdBossEnemy = new Enemy(tempType);
            setBossStats(bossBonusHealth, bossBonusDamage);
        }
    }

    //Method to update boss stats and description
    private void setBossStats(int bonusHP, int bonusDMG)
    {
        createdBossEnemy.Health += bonusHP;
        createdBossEnemy.minDamage += bonusDMG;
        createdBossEnemy.maxDamage += bonusDMG;
        createdBossEnemy.Difficulty *= 2;
        createdBossEnemy.Title += " King";
        createdBossEnemy.resetDescription();
    }



    public Enemy returnEnemy(int level, float difficulty)
    {
        createEnemy(level, difficulty);
        return createdEnemy;

    }

    public Enemy returnBossEnemy(int level, float difficulty)
    {
        createBossEnemy(level, difficulty);
        return createdBossEnemy;
            
    }

    



}
