using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;
using System;



public class itemInformationDatabase : MonoBehaviour {

    //Tämä on TÄRKEÄ luokka. Täällä rakennetaan kaikki prefix, suffix ja tyyppi tietokannat
    //Näiden tietokantojen pohjalta rakennetaan jokainen item
    //Ensin määritellään jokainen lista ("database") ja vastaavasti niihin kuuluva JsonData
    //Kaikki data haetaan Start() metodissa .JSON tiedostoista jotka löytyvät pelin kansiosta.


    public List <gearPrefix> weaponPrefixDatabase = new List<gearPrefix>();
    private JsonData prefixData;
    public List <gearSuffix> weaponSuffixDatabase = new List<gearSuffix>();
    private JsonData suffixData;
    public List <gearType> weaponTypeDatabase = new List<gearType>();

    public List<gearPrefix> gearPrefixDatabase = new List<gearPrefix>();
    public List<gearType> gearTypeDatabase = new List<gearType>();
    public List<gearSuffix> gearSuffixDatabase = new List<gearSuffix>();

    private JsonData weaponTypeData;
    private JsonData gearTypeData;
    private JsonData usables;
    public List<Item> usableDatabase = new List<Item>();
    
   // public List<Item> weaponDatabase = new List<Item>();
   // public List<Item> gearDatabase = new List<Item>();

    public List<string> weaponPrefixNames = new List<string>();
    public List<string> weaponSuffixNames = new List<string>();
    public List<gearType> weaponTypeNames = new List<gearType>();
    public List<gearType> gearTypeNames = new List<gearType>();


    int prefixAmount, suffixAmount, gearTypeAmount, weaponTypeAmount;

    WebPlayerData webData;
    int currentMinDamage;
    int currentMaxDamage;
    int currentHP;
    int currentEnergy;
    int currentLevel;
    int currentID;
    int addingCounter;

    float currentArmor;
    float currentLifeSteal;
    float currentHealthRegen;
    float currentEnergyRegen;
    float currentExperienceBonus;
    float currentGoldBonus;
    float currentLootQuantity;
    float currentLootQuality;

    int HPplus;
    int Energyplus;
    int damagePlus;

    float ArmorPlus;
    float LifeStealPlus;
    float HealthRegenPlus;
    float EnergyRegenPlus;
    float ExperienceBonusPlus;
    float GoldBonusPlus;
    float LootQuantityPlus;
    float LootQualityPlus;


    void Start()
    {


        //Tässä luodaan JsonDatat jokaiselle datalle erikseen
#if UNITY_STANDALONE
        prefixData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/prefixes.json"));
        suffixData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/suffixes.json"));
        weaponTypeData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/types.json"));
        usables = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/usables.json"));
        gearTypeData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/gearTypes.json"));

         prefixAmount = prefixData.Count;
        suffixAmount = suffixData.Count;
        weaponTypeAmount = weaponTypeData.Count;
        gearTypeAmount = gearTypeData.Count;

        constructUsables();
        constructAllDatas();

#elif UNITY_WEBGL
        webData = GameObject.Find("WebPlayerHolder").GetComponent<WebPlayerData>();
        webData.buildItemDatas();

        prefixAmount = weaponPrefixNames.Count;
        suffixAmount = weaponSuffixNames.Count;
        weaponTypeAmount = weaponTypeNames.Count;
        gearTypeAmount = gearTypeNames.Count;

        Debug.Log("Prefixes: " + prefixAmount);
        Debug.Log("Suffixes: " + suffixAmount);
        Debug.Log("Weapon Types: " + weaponTypeAmount);
        Debug.Log("Gear Types: " + gearTypeAmount);
        constructAllDatas();


#endif


        //addWeaponsToItemDatabase();
       // addGearToItemDatabase();

    }

    void constructAllDatas()
    {
        
        constructWeaponFixes();
        constructWeapons();
        constructGearFixes();
        constructGear();
        

    }

    //Method for construction prefix and suffix data
    void constructWeaponFixes()
    {
        resetCurrents();

        gearPrefix tempPrefix;
        gearSuffix tempSuffix;

        for (int i = 0; i < prefixAmount; i++)
        {
            //Täydennetään prefixDatabase, sinne on helppo lisätä tavaraa, muokkaa vain JSON tiedosto
            //Depending on verison (Standalone/WEBgl) take name either from 
#if UNITY_STANDALONE
            tempPrefix = new gearPrefix(prefixData[i]["name"].ToString(), currentMinDamage, currentMaxDamage, currentHP, currentEnergy, currentLevel, currentID, currentLifeSteal);
#elif UNITY_WEBGL
            tempPrefix = new gearPrefix(weaponPrefixNames[i], currentMinDamage, currentMaxDamage, currentHP, currentEnergy, currentLevel, currentID, currentLifeSteal);
#endif

            setWeaponFixStats(i);

            currentID++;


            weaponPrefixDatabase.Add(tempPrefix);
        }
        //Debug.Log("Weapon prefixes generated " + weaponPrefixDatabase.Count);

        resetCurrents();
        currentHP = 5;
        currentMaxDamage = 0;
        for (int i = 0; i < suffixAmount; i++)
        {
            //Täydennetään suffixDatabase, sinne on helppo lisätä tavaraa, muokkaa vain JSON tiedosto
#if UNITY_STANDALONE
            tempSuffix = new gearSuffix(suffixData[i]["name"].ToString(), currentMinDamage, currentMaxDamage, currentHP, currentEnergy, currentLevel, currentID, currentLifeSteal);
#elif UNITY_WEBGL
            tempSuffix = new gearSuffix(weaponSuffixNames[i], currentMinDamage, currentMaxDamage, currentHP, currentEnergy, currentLevel, currentID, currentLifeSteal);
#endif
            

            setWeaponFixStats(i);

            currentID++;

            weaponSuffixDatabase.Add(tempSuffix);
        }
    }
    void constructWeapons()
    {
        gearType tempType;

        resetCurrents();
        currentMinDamage = 1;
        currentMaxDamage = 3;
        for (int i = 0; i < weaponTypeAmount; i++)
        {
            //Täydennetään typeDatabase, sinne on helppo lisätä tavaraa, muokkaa vain JSON tiedosto
#if UNITY_STANDALONE
            tempType = new gearType(weaponTypeData[i]["name"].ToString(), weaponTypeData[i]["slot"].ToString(), currentMinDamage, currentMaxDamage, currentHP, currentEnergy, currentLevel, currentID);
#elif UNITY_WEBGL
            tempType = new gearType(weaponTypeNames[i].TypeName, weaponTypeNames[i].EquipSlot, currentMinDamage, currentMaxDamage, currentHP, currentEnergy, currentLevel, currentID);
#endif

            setWeaponFixStats(i);
            setWeaponStats(i);

            currentLevel += 3;

            weaponTypeDatabase.Add(tempType);
        }
    }
    void constructGearFixes()
    {
        resetCurrents();
        currentArmor = 3;
        currentMaxDamage = 0;
        gearPrefix tempPrefix;
        gearSuffix tempSuffix;

        for (int i = 0; i < prefixAmount; i++)
        {
            //Täydennetään prefixDatabase, sinne on helppo lisätä tavaraa, muokkaa vain JSON tiedosto
#if UNITY_STANDALONE
            tempPrefix = new gearPrefix(prefixData[i]["name"].ToString(), currentMinDamage, currentMaxDamage, currentHP, currentEnergy, currentLevel, currentID, currentArmor, currentHealthRegen, currentEnergyRegen, currentExperienceBonus, currentGoldBonus, currentLootQuantity, currentLootQuality);
#elif UNITY_WEBGL
            tempPrefix = new gearPrefix(weaponPrefixNames[i], currentMinDamage, currentMaxDamage, currentHP, currentEnergy, currentLevel, currentID, currentArmor, currentHealthRegen, currentEnergyRegen, currentExperienceBonus, currentGoldBonus, currentLootQuantity, currentLootQuality);
#endif
            

            setGearFixStats(i);

            currentID++;


            gearPrefixDatabase.Add(tempPrefix);
        }

        resetCurrents();

        currentHP = 5;
        currentMaxDamage = 0;
        for (int i = 0; i < suffixAmount; i++)
        {
            //Täydennetään suffixDatabase, sinne on helppo lisätä tavaraa, muokkaa vain JSON tiedosto
#if UNITY_STANDALONE
             tempSuffix = new gearSuffix(suffixData[i]["name"].ToString(), currentMinDamage, currentMaxDamage, currentHP, currentEnergy, currentLevel, currentID, currentArmor, currentHealthRegen, currentEnergyRegen, currentExperienceBonus, currentGoldBonus, currentLootQuantity, currentLootQuality);
#elif UNITY_WEBGL
            tempSuffix = new gearSuffix(weaponSuffixNames[i], currentMinDamage, currentMaxDamage, currentHP, currentEnergy, currentLevel, currentID, currentArmor, currentHealthRegen, currentEnergyRegen, currentExperienceBonus, currentGoldBonus, currentLootQuantity, currentLootQuality);
#endif

           

            setGearFixStats(i);

            currentID++;

            gearSuffixDatabase.Add(tempSuffix);
        }
    }
    void constructGear()
    {
        gearType tempType;

        resetCurrents();
        currentArmor = 3;
        currentMaxDamage = 0;
        for (int i = 0; i < gearTypeAmount; i++)
        {
            //Täydennetään typeDatabase, sinne on helppo lisätä tavaraa, muokkaa vain JSON tiedosto
#if UNITY_STANDALONE
            tempType = new gearType(gearTypeData[i]["name"].ToString(), gearTypeData[i]["slot"].ToString(), currentMinDamage, currentMaxDamage, currentHP, currentEnergy, currentLevel, currentID, currentLifeSteal, currentHealthRegen, currentEnergyRegen, currentExperienceBonus, currentGoldBonus, currentLootQuantity, currentLootQuality, currentArmor);
#elif UNITY_WEBGL
            tempType = new gearType(gearTypeNames[i].TypeName, gearTypeNames[i].EquipSlot, currentMinDamage, currentMaxDamage, currentHP, currentEnergy, currentLevel, currentID, currentLifeSteal, currentHealthRegen, currentEnergyRegen, currentExperienceBonus, currentGoldBonus, currentLootQuantity, currentLootQuality, currentArmor);
#endif

            setGearFixStats(i);
            setGearStats(i);

            if(i%4 == 0)
            {
                currentLevel += 3;
            }

            gearTypeDatabase.Add(tempType);
        }

        
    }

    //Method for resetting current bonuses, use always when starting building from 0
    //Build prefixes -> reset -> build suffixes -> reset ...
    void resetCurrents()
    {
        currentMinDamage = 0;
        currentMaxDamage = 1;
        currentHP = 0;
        currentEnergy = 0;
        currentLevel = 1;
        currentID = 100;
        addingCounter = 0;

        currentArmor = 0;
        currentLifeSteal = 0;
        currentHealthRegen = 0;
        currentEnergyRegen = 0;
        currentExperienceBonus = 0;
        currentGoldBonus = 0;
        currentLootQuantity = 0;
        currentLootQuality = 0;


        HPplus = 5;
        Energyplus = 3;
        damagePlus = 1;

        ArmorPlus = 3;
        LifeStealPlus = 0.03f;
        HealthRegenPlus = 1f;
        EnergyRegenPlus = 1f;
        ExperienceBonusPlus = 0.1f;
        GoldBonusPlus = 0.05f;
        LootQuantityPlus = 0.1f;
        LootQualityPlus = 0.05f;
    }

    //Set bonus and base stats for prefixes and suffixes
    void setWeaponFixStats(int i)
    {
        //For each level, adding counter++ and add a base stat (hp, energy, min/max damage)
        if (addingCounter == 0)
        {
            currentHP += HPplus;
            addingCounter++;
        }
        else if (addingCounter == 1)
        {
            currentEnergy += Energyplus ;
            addingCounter++;
        }
        else if (addingCounter == 2)
        {
            currentMinDamage += damagePlus ;
            addingCounter++;
        }
        else if (addingCounter == 3)
        {
            currentMaxDamage += damagePlus ;
            addingCounter = 0;
        }

        //Each 5th level, add lifesteal
        if (i % 5 == 0)
        {
            currentLifeSteal += LifeStealPlus;
            LifeStealPlus += 0.03f;
        }
        else
        {
            currentLifeSteal = 0;
        }

        if (i % 3 == 0)
        {
            currentLevel++;
        }

    }

    //Set bonus and base stats for gear
    void setGearFixStats(int i)
    {
        //For each level, adding counter++ and add a base stat (hp, energy, min/max damage)
        if (addingCounter == 0)
        {
            currentHP += HPplus;
            addingCounter++;
        }
        else if (addingCounter == 1)
        {
            currentEnergy += Energyplus;
            addingCounter++;
        }
        else if (addingCounter == 2)
        {
            currentArmor += ArmorPlus;
            addingCounter++;
        }

        //Each even item, add healthregen
        if (i % 2 == 0 && i > 5)
        {
            currentHealthRegen += HealthRegenPlus;
            
        }
        else
        {
            currentHealthRegen = 0;
        }

        //Each uneven item, add energyregen
        if (i % 2 == 1 && i > 5)
        {
            currentEnergyRegen += EnergyRegenPlus;
            
        }
        else
        {
            currentEnergyRegen = 0;
        }

        //For each 3 items, add expBonus
        if (i % 3 == 0 && i > 10)
        {
            currentExperienceBonus += ExperienceBonusPlus;
            ExperienceBonusPlus += 0.01f;
        }
        else
        {
            currentExperienceBonus = 0;
        }

        //For each 4 items, add goldBonus
        if (i % 4 == 0 && i > 10)
        {
            currentGoldBonus += GoldBonusPlus;
            GoldBonusPlus += 0.01f;
        }
        else
        {
            currentGoldBonus = 0;
        }

        //For each 5 items, add lootQuality
        if (i % 5 == 0 && i > 15)
        {
            currentLootQuality += LootQualityPlus;
            LootQualityPlus += 0.02f;
        }
        else
        {
            currentLootQuality = 0;
        }

        //For each 6 items, add lootQuantity
        if (i % 6 == 0 && i > 15)
        {
            currentLootQuantity += LootQuantityPlus;
            LootQuantityPlus += 0.03f;
        }
        else
        {
            currentLootQuantity = 0;
        }

        if (i % 3 == 0)
        {
            currentLevel++;
        }
    }

    void setGearStats(int i)
    {
        currentArmor += 2;
    }

    //Set bonus and base stats for weapons
    void setWeaponStats(int i)
    {
        currentMinDamage += 2;
        currentMaxDamage += 5;
    }

    void constructUsables()
    {
        Item tempItem;
        for (int i = 0; i < usables.Count; i++)
        {
            tempItem = new Item(usables[i]["name"].ToString(), (int)usables[i]["value"], (int)usables[i]["buffAmount"], (bool)usables[i]["usable"], (bool)usables[i]["stackable"], (int)usables[i]["ID"]);
            
            usableDatabase.Add(tempItem);
        }
    }


    //metodi joka lisää kaikki aseet completeItemDatabasee
    // i = lisätään pelkkä tyyppi
    // i+j = lisätään prefix+tyyppi
    // i+j+k = lisätään prefix+tyyppi+suffix
    // i+h = lisätään tyyppi+suffix
   /* void addWeaponsToItemDatabase()
    {
        Debug.Log("weaponTypeDatabase.Count = " + weaponTypeDatabase.Count);
        Debug.Log("weaponPrefixDatabase.Count = " + weaponPrefixDatabase.Count);
        Debug.Log("weaponSuffixDatabase.Count = " + weaponSuffixDatabase.Count);

        for (int i = 0; i < weaponTypeDatabase.Count; i++)
        {
            weaponDatabase.Add(new Item(weaponTypeDatabase[i]));
            
            for (int j = 0; j < weaponPrefixDatabase.Count; j++)
            {
                weaponDatabase.Add(new Item(weaponTypeDatabase[i], weaponPrefixDatabase[j]));
                
                for (int k = 0; k < weaponSuffixDatabase.Count; k++)
                {
                    weaponDatabase.Add(new Item(weaponTypeDatabase[i], weaponPrefixDatabase[j], weaponSuffixDatabase[k]));
                    
                }
                
            }
            for(int h = 0; h < weaponSuffixDatabase.Count; h++)
            {
                weaponDatabase.Add(new Item(weaponTypeDatabase[i], weaponSuffixDatabase[h]));
                

            }
        }

        Debug.Log("Weapon amount: "+weaponDatabase.Count);

    }*/

   /* void addGearToItemDatabase()
    {
        for (int i = 0; i < gearTypeDatabase.Count; i++)
        {
            gearDatabase.Add(new Item(gearTypeDatabase[i]));

            for (int j = 0; j < gearPrefixDatabase.Count; j++)
            {
                gearDatabase.Add(new Item(gearTypeDatabase[i], gearPrefixDatabase[j]));

                for (int k = 0; k < gearSuffixDatabase.Count; k++)
                {
                    gearDatabase.Add(new Item(gearTypeDatabase[i], gearPrefixDatabase[j], gearSuffixDatabase[k]));

                }

            }
            for (int h = 0; h < gearSuffixDatabase.Count; h++)
            {
                gearDatabase.Add(new Item(gearTypeDatabase[i], gearSuffixDatabase[h]));


            }
        }

        Debug.Log("Gear amount: " + gearDatabase.Count);

    }*/

    public gearType pickWeaponTypeByLevel(int level)
    {
        int minLevel = level - 1;
        int maxLevel = level + 1;
        gearType toReturn = weaponTypeDatabase[0];
        if(level > weaponTypeDatabase[weaponTypeDatabase.Count - 1].Level)
        {
            return weaponTypeDatabase[weaponTypeDatabase.Count - 1];
        }
        for(int i = 0; i < weaponTypeDatabase.Count; i++)
        {
            if(weaponTypeDatabase[i].Level >= minLevel && weaponTypeDatabase[i].Level <= maxLevel)
            {
                toReturn = weaponTypeDatabase[i];
                return toReturn;
            }
            
        }
        return toReturn;
    }
    public gearPrefix pickWeaponPrefixByLevel(int level)
    {
        int minLevel = level - 1;
        int maxLevel = level + 1;
        int random = UnityEngine.Random.Range(0, 4);
        gearPrefix toReturn = weaponPrefixDatabase[0];
        if (level > weaponPrefixDatabase[weaponPrefixDatabase.Count - 1].Level)
        {
            return weaponPrefixDatabase[weaponPrefixDatabase.Count - 1];
        }
        for (int i = 0; i < weaponPrefixDatabase.Count; i++)
        {
            if (weaponPrefixDatabase[i].Level >= minLevel && weaponPrefixDatabase[i].Level <= maxLevel)
            {
                toReturn = weaponPrefixDatabase[i+random];
                
                return toReturn;
            }

        }
        return toReturn;
    }
    public gearSuffix pickWeaponSuffixByLevel(int level)
    {
        int minLevel = level - 1;
        int maxLevel = level + 1;
        int random = UnityEngine.Random.Range(0, 4);
        gearSuffix toReturn = weaponSuffixDatabase[0];
        if (level > weaponSuffixDatabase[weaponSuffixDatabase.Count - 1].Level)
        {
            return weaponSuffixDatabase[weaponSuffixDatabase.Count - 1];
        }
        for (int i = 0; i < weaponSuffixDatabase.Count; i++)
        {
            if (weaponSuffixDatabase[i].Level >= minLevel && weaponSuffixDatabase[i].Level <= maxLevel)
            {
                toReturn = weaponSuffixDatabase[i+ random];
                
                return toReturn;
            }

        }
        return toReturn;
    }

    public gearType pickGearTypeByLevel(int level)
    {
        int minLevel = level - 1;
        int maxLevel = level + 1;
        int random = UnityEngine.Random.Range(0, 5);
        gearType toReturn = gearTypeDatabase[0];
        if (level > gearTypeDatabase[gearTypeDatabase.Count - 1].Level)
        {
            return gearTypeDatabase[gearTypeDatabase.Count - 1];
        }
        for (int i = 0; i < gearTypeDatabase.Count; i++)
        {
            if (gearTypeDatabase[i].Level >= minLevel && gearTypeDatabase[i].Level <= maxLevel)
            {
                //Debug.Log(gearTypeDatabase[i + random].TypeName);
                toReturn = gearTypeDatabase[i+ random];
                return toReturn;
            }

        }
        return toReturn;
    }
    public gearPrefix pickGearPrefixByLevel(int level)
    {
        int minLevel = level - 1;
        int maxLevel = level + 1;
        int random = UnityEngine.Random.Range(0, 4);
        gearPrefix toReturn = gearPrefixDatabase[0];
        if (level > gearPrefixDatabase[gearPrefixDatabase.Count - 1].Level)
        {
            return gearPrefixDatabase[gearPrefixDatabase.Count - 1];
        }
        for (int i = 0; i < gearPrefixDatabase.Count; i++)
        {
            if (gearPrefixDatabase[i].Level >= minLevel && gearPrefixDatabase[i].Level <= maxLevel)
            {
                toReturn = gearPrefixDatabase[i + random];

                return toReturn;
            }

        }
        return toReturn;
    }
    public gearSuffix pickGearSuffixByLevel(int level)
    {
        int minLevel = level - 1;
        int maxLevel = level + 1;
        int random = UnityEngine.Random.Range(0, 4);
        gearSuffix toReturn = gearSuffixDatabase[0];
        if (level > gearSuffixDatabase[gearSuffixDatabase.Count - 1].Level)
        {
            return gearSuffixDatabase[gearSuffixDatabase.Count - 1];
        }
        for (int i = 0; i < gearSuffixDatabase.Count; i++)
        {
            if (gearSuffixDatabase[i].Level >= minLevel && gearSuffixDatabase[i].Level <= maxLevel)
            {
                toReturn = gearSuffixDatabase[i + random];

                return toReturn;
            }

        }
        return toReturn;
    }


    //getBlaBlaByID- metodit palauttavat halutun asian oikean ID:n perusteella, suffix, type, prefix = obsolete?
    /*public Item getItemByID(int id)
    {
        for(int i = 0; i < weaponDatabase.Count; i++)
        {
            if(Int32.Parse(weaponDatabase[i].ID) == id)
            {
                return weaponDatabase[i];
            }
        }
        Debug.Log("first ID "+ weaponDatabase[0].ID);
        return new Item();
    }


    public gearSuffix getSuffixByID(int id)
    {
        for (int i = 0; i < weaponSuffixDatabase.Count; i++)
        {
            if (weaponPrefixDatabase[i].ID == id)
            {
                return weaponSuffixDatabase[i];
            }
        }
        return null;
    }

    public gearType getTypeByID(int id)
    {
        for (int i = 0; i < weaponTypeDatabase.Count; i++)
        {
            if (weaponTypeDatabase[i].ID == id)
            {
                return weaponTypeDatabase[i];
            }
        }
        return null;
    }

    public gearPrefix getPrefixByID(int id)
    {
        Debug.Log("searching for prefix");
        for (int i = 0; i < weaponPrefixDatabase.Count; i++)
        {
            if (weaponPrefixDatabase[i].ID == id)
            {
                return weaponPrefixDatabase[i];
            }
        }
        return null;
    }*/

    //Metodi jolla rakennetaan Item IDn perusteella, obsolete?
    /*public Item buildItemID(string id)
    {
       
        if (Int32.Parse(id.Substring(0, 1)) == 0 && Int32.Parse(id.Substring(6, 1)) == 0) //Parsetaan IDstä ensimmäinen ja seitsemäs alkio (012 345 678) ja jos ne ovat 0, ei ole prefix eikä suffix
        {
            string typeSub = id.Substring(3, 3);
            int typeInt = Int32.Parse(typeSub);

            gearType type = getTypeByID(typeInt);

            return new Item(type);
        }


        else if (Int32.Parse(id.Substring(0, 1)) > 0 && Int32.Parse(id.Substring(6, 1)) == 0) //Tässä prefix mutta ei suffix
        {
            string prefixSub = id.Substring(0, 3);
            int prefixInt = Int32.Parse(prefixSub);

            string typeSub = id.Substring(3, 3);
            int typeInt = Int32.Parse(typeSub);

            gearPrefix prefix = getPrefixByID(prefixInt);
            gearType type = getTypeByID(typeInt);

            return new Item(type, prefix);


        }




        else if (Int32.Parse(id.Substring(0, 1)) == 0 && Int32.Parse(id.Substring(6, 1)) > 0)//Tässä suffix mutta ei prefix
        {
            string suffixSub = id.Substring(6, 3);
            int suffixInt = Int32.Parse(suffixSub);

            string typeSub = id.Substring(3, 3);
            int typeInt = Int32.Parse(typeSub);

            gearSuffix suffix = getSuffixByID(suffixInt);
            gearType type = getTypeByID(typeInt);
            //Debug.Log("created type:" + type.TypeName);
            //Debug.Log("created suffix: " + suffix.SuffixName);
            return new Item(type, suffix);

        }
        else // ja tässä suffix ja prefix
        {

            string prefixSub = id.Substring(0, 3);
            int prefixInt = Int32.Parse(prefixSub);
           
            string typeSub = id.Substring(3, 3);
            int typeInt = Int32.Parse(typeSub);

            string sufSub = id.Substring(6, 3);
            int suffixInt = Int32.Parse(sufSub);

            gearPrefix prefix = getPrefixByID(prefixInt);
            gearType type = getTypeByID(typeInt);
            gearSuffix suffix = getSuffixByID(suffixInt);

            return new Item(type, prefix, suffix);            
        }
    
}*/

}
