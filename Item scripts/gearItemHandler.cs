using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class gearItemHandler : MonoBehaviour {
    private itemInformationDatabase database;



    private List<string> rarities;
    private string currentItemRarity;
    float lootChance = 1f;
    // Use this for initialization
    void Start () {
        database = GameObject.Find("Inventory").GetComponent<itemInformationDatabase>();

        rarities = new List<string>();
        rarities.Add("Common");     //0
        rarities.Add("Uncommon");   //1
        rarities.Add("Rare");       //2
        rarities.Add("Epic");       //3
        rarities.Add("Legendary");  //4


        /*Item tempItem;
        int Uncommonamount = 0;
        int rareAmount = 0;
        int epicAmount = 0;
        int legendaryAmount = 0;

        for(int i = 0; i < 1000; i++)
        {
            tempItem = generateBase(30);
            
            Debug.Log(tempItem.Rarity);
            if(tempItem.Rarity == "Uncommon")
            {
                Uncommonamount++;
            }
            if(tempItem.Rarity == "Rare")
            {
                rareAmount++; 
            }
            if(tempItem.Rarity == "Epic")
            {
                epicAmount++;
            }
            if(tempItem.Rarity == "Legendary")
            {
                legendaryAmount++;
            }
        }

        Debug.Log("Uncommons: " + Uncommonamount + "\nRares: " + rareAmount + "\nEpics: " + epicAmount + "\nLegendaries: " + legendaryAmount);*/

       
 
    }

    //Method for generating a weapon using level as base
    //level = item base
    //rarityBonus = prefix + suffix level multiplier
    //rarityStageBonus = item rarity aka common uncommon rare epic legendary multiplier
    public Item generateBaseWeapon(int level, int rarity, float rarityBonus, float rarityStageBonus)
    {
        Item generatedItem;
        gearPrefix pickedPrefix;
        gearSuffix pickedSuffix;
        //Pick a weapon type using chooseType method
        gearType pickedType = chooseWeaponType(level);

        //Assign item a rarity (common - legendary), and set it's "level" accordinly ("level" used as base for prefix and suffix)
        rarity = (int) assignItemRarity(rarity, rarityStageBonus);

        //Temporarily generate an Item based on the type
        generatedItem = new Item(pickedType);

        //Choose wether the item has pre/suf-fixes
        setPrefixSuffix(generatedItem);
        
        //If it has a prefix
        if (generatedItem.hasPre && !generatedItem.hasSuf)
        {
            pickedPrefix = chooseWeaponPrefix(rarity * rarityBonus);
            generatedItem = new Item(pickedType, pickedPrefix);
        }
        //If it has a suffix
        else if (generatedItem.hasSuf&& !generatedItem.hasPre)
        {
            pickedSuffix = chooseWeaponSuffix(rarity * rarityBonus);
            generatedItem = new Item(pickedType, pickedSuffix);
        }
        //If it has both
        else if (generatedItem.hasPre && generatedItem.hasSuf)
        {
            //if an item has both, rarity is increased a bit (level is not divided by 2(amount of fixes) but with 1.75 instead)
            pickedPrefix = chooseWeaponPrefix((rarity / 1.75f) * rarityBonus);
            pickedSuffix = chooseWeaponSuffix((rarity / 1.75f) * rarityBonus);
            generatedItem = new Item(pickedType, pickedPrefix, pickedSuffix);

        }

        //And set the rarity of the generated item
        generatedItem.Rarity = currentItemRarity;

        
        
        return generatedItem;
    }

    public Item generateBaseGear(int level, int rarity, float rarityBonus, float rarityStageBonus)
    {
        Item generatedItem;
        gearPrefix pickedPrefix;
        gearSuffix pickedSuffix;
        //Pick a weapon type using chooseType method
        gearType pickedType = chooseGearType(level);

        //Assign item a rarity (common - legendary), and set it's "level" accordinly ("level" used as base for prefix and suffix)
        rarity = (int)assignItemRarity(rarity, rarityStageBonus);

        //Temporarily generate an Item based on the type
        generatedItem = new Item(pickedType);

        //Choose wether the item has pre/suf-fixes
        setPrefixSuffix(generatedItem);

        //If it has a prefix
        if (generatedItem.hasPre && !generatedItem.hasSuf)
        {
            pickedPrefix = chooseGearPrefix(rarity * rarityBonus);
            generatedItem = new Item(pickedType, pickedPrefix);
        }
        //If it has a suffix
        else if (generatedItem.hasSuf && !generatedItem.hasPre)
        {
            pickedSuffix = chooseGearSuffix(rarity * rarityBonus);
            generatedItem = new Item(pickedType, pickedSuffix);
        }
        //If it has both
        else if (generatedItem.hasPre && generatedItem.hasSuf)
        {
            //if an item has both, rarity is increased a bit (level is not divided by 2(amount of fixes) but with 1.75 instead)
            pickedPrefix = chooseGearPrefix((rarity / 1.75f) * rarityBonus);
            pickedSuffix = chooseGearSuffix((rarity / 1.75f) * rarityBonus);
            generatedItem = new Item(pickedType, pickedPrefix, pickedSuffix);

        }

        //And set the rarity of the generated item
        //Rarity Image is set in InventorySlot class
        generatedItem.Rarity = currentItemRarity;



        return generatedItem;
    }

    //Metodi joka muuttaa itemin stringiksi, aika yksinkertainen
    string itemToString(Item item)
    {
        string s = "";
        if (item.hasPre == false && item.hasSuf == false)
        {
            s = "" + item.Type.TypeName;
        }
        else if (item.hasSuf == false)
        {
            s = "" + item.Prefix.PrefixName + " " + item.Type.TypeName;

        }
        else if(item.hasPre == false)
        {
            s = "" + item.Type.TypeName + " of the " + item.Suffix.SuffixName;

        }
        else {
            s = "" + item.Prefix.PrefixName + " " + item.Type.TypeName + " of the " + item.Suffix.SuffixName;
        }
        return s;
    }

    //Metodi joka päättää onko itemille prefix, suffix vai ei mitään
    private void setPrefixSuffix(Item item)
    {
        int minRange = 0;
        if(currentItemRarity != "Common")
        {
            minRange++;
        }
        int random = Random.Range(minRange, 4);
        if(random == 1)
        {
            item.hasPre = true;
            
        }
        else if(random == 2)
        {
            item.hasSuf = true;
            
        }
        else if(random == 3)
        {
            item.hasPre = true;
            item.hasSuf = true;
            
        }
    }


    private gearType chooseWeaponType(int level)
    {

        int random = Random.Range(1, 101);

        //Valitsee +/-, 80% chance for +1, 20% chance for -1
        int multiplier = pickMultiplier();


        //More than 52 for +-1
        if (random > 52 && random <= 78)
        {
            level += (multiplier * 1);
        }

        //more than 78 for +-2
        else if (random > 78 && random <= 91)
        {
            level += (multiplier * 2);
        }

        //more than 91 for +-3
        else if (random > 91 && random <= 97)
        {
            level += (multiplier * 3);
            //Debug.Log("EpicTYPE!!! created");
        }
        //more than 97 for +-4
        else if (random > 97)
        {
            level += (multiplier * 4);
            //Debug.Log("LegendaryTYPE!!! created");
        }


        gearType chosenType = database.pickWeaponTypeByLevel(level);

        return chosenType;

    }
    private gearPrefix chooseWeaponPrefix(float level)
    {
        //float random = Random.Range(-2f, 2f);
        gearPrefix chosenPrefix;
        float itemRarity = level;
        float randomDifference;

        if(currentItemRarity == "Common")
        {
            int randomBadPrefix = Random.Range(0, (int)itemRarity);
            chosenPrefix = database.pickWeaponPrefixByLevel(randomBadPrefix);
            return chosenPrefix;
        }
        
        if (currentItemRarity == "Uncommon")
        {
            randomDifference = Random.Range(1, 1.25f);
            itemRarity += 2;
            chosenPrefix = database.pickWeaponPrefixByLevel((int)itemRarity);
            return chosenPrefix;
        }

        else if (currentItemRarity == "Rare")
        {
            randomDifference = Random.Range(1.25f, 1.5f);
            itemRarity += 4;
            chosenPrefix = database.pickWeaponPrefixByLevel((int)itemRarity);
            return chosenPrefix;
        }

        else if (currentItemRarity == "Epic")
        {
            randomDifference = Random.Range(1.5f, 1.75f);
            itemRarity += 6;
            chosenPrefix = database.pickWeaponPrefixByLevel((int)itemRarity);
            return chosenPrefix;
        }

        else if (currentItemRarity == "Legendary")
        {
            randomDifference = Random.Range(1.75f, 2f);
            itemRarity += 8;
            chosenPrefix = database.pickWeaponPrefixByLevel((int)itemRarity);

            return chosenPrefix;
        }

        //never returned null
        return null;
    }
    private gearSuffix chooseWeaponSuffix(float level)
    {
        //float random = Random.Range(-2f, 2f);
        gearSuffix chosenSuffix;

        float itemRarity = level;
        float randomDifference;

        if (currentItemRarity == "Common")
        {
            int randomBadSuffix = Random.Range(0, (int) itemRarity);
            chosenSuffix = database.pickWeaponSuffixByLevel(randomBadSuffix);
            return chosenSuffix;
        }

        else if(currentItemRarity == "Uncommon")
        { 
        
            randomDifference = Random.Range(1, 1.25f);
            itemRarity += 2;
            chosenSuffix = database.pickWeaponSuffixByLevel((int)itemRarity);
            return chosenSuffix;
        }

        else if (currentItemRarity == "Rare")
        {
            randomDifference = Random.Range(1.25f, 1.5f);
            itemRarity += 4;
            chosenSuffix = database.pickWeaponSuffixByLevel((int)itemRarity);
            return chosenSuffix;
        }
        else if (currentItemRarity == "Epic")
        {
            randomDifference = Random.Range(1.5f, 1.75f);
            itemRarity += 6;
            chosenSuffix = database.pickWeaponSuffixByLevel((int)itemRarity);

            return chosenSuffix;
        }

        else if (currentItemRarity == "Legendary")
        {
            randomDifference = Random.Range(1.75f, 2f);
            itemRarity += 8;
            chosenSuffix = database.pickWeaponSuffixByLevel((int)itemRarity);

            return chosenSuffix;
        }
        //never returned null
        return null;
    }



    private gearType chooseGearType(int level)
    {

        int random = Random.Range(1, 101);

        //Valitsee +/-, 80% chance for +1, 20% chance for -1
        int multiplier = pickMultiplier();


        //More than 52 for +-1
        if (random > 52 && random <= 78)
        {
            level += (multiplier * 1);
        }

        //more than 78 for +-2
        else if (random > 78 && random <= 91)
        {
            level += (multiplier * 2);
        }

        //more than 91 for +-3
        else if (random > 91 && random <= 97)
        {
            level += (multiplier * 3);
            //Debug.Log("EpicTYPE!!! created");
        }
        //more than 97 for +-4
        else if (random > 97)
        {
            level += (multiplier * 4);
            //Debug.Log("LegendaryTYPE!!! created");
        }


        gearType chosenType = database.pickGearTypeByLevel(level);

        return chosenType;

    }
    private gearPrefix chooseGearPrefix(float level)
    {
        //float random = Random.Range(-2f, 2f);
        gearPrefix chosenPrefix;
        float itemRarity = level;
        float randomDifference;

        if (currentItemRarity == "Common")
        {
            int randomBadPrefix = Random.Range(0, (int)itemRarity);
            chosenPrefix = database.pickGearPrefixByLevel(randomBadPrefix);
            return chosenPrefix;
        }

        if (currentItemRarity == "Uncommon")
        {
            randomDifference = Random.Range(1, 1.25f);
            itemRarity += 2;
            chosenPrefix = database.pickGearPrefixByLevel((int)itemRarity);
            return chosenPrefix;
        }

        else if (currentItemRarity == "Rare")
        {
            randomDifference = Random.Range(1.25f, 1.5f);
            itemRarity += 4;
            chosenPrefix = database.pickGearPrefixByLevel((int)itemRarity);
            return chosenPrefix;
        }

        else if (currentItemRarity == "Epic")
        {
            randomDifference = Random.Range(1.5f, 1.75f);
            itemRarity += 6;
            chosenPrefix = database.pickGearPrefixByLevel((int)itemRarity);
            return chosenPrefix;
        }

        else if (currentItemRarity == "Legendary")
        {
            randomDifference = Random.Range(1.75f, 2f);
            itemRarity += 8;
            chosenPrefix = database.pickGearPrefixByLevel((int)itemRarity);

            return chosenPrefix;
        }

        //never returned null
        return null;
    }
    private gearSuffix chooseGearSuffix(float level)
    {
        //float random = Random.Range(-2f, 2f);
        gearSuffix chosenSuffix;

        float itemRarity = level;
        float randomDifference;

        if (currentItemRarity == "Common")
        {
            int randomBadSuffix = Random.Range(0, (int)itemRarity);
            chosenSuffix = database.pickGearSuffixByLevel(randomBadSuffix);
            return chosenSuffix;
        }

        else if (currentItemRarity == "Uncommon")
        {

            randomDifference = Random.Range(1, 1.25f);
            itemRarity += 2;
            chosenSuffix = database.pickGearSuffixByLevel((int)itemRarity);
            return chosenSuffix;
        }

        else if (currentItemRarity == "Rare")
        {
            randomDifference = Random.Range(1.25f, 1.5f);
            itemRarity += 4;
            chosenSuffix = database.pickGearSuffixByLevel((int)itemRarity);
            return chosenSuffix;
        }
        else if (currentItemRarity == "Epic")
        {
            randomDifference = Random.Range(1.5f, 1.75f);
            itemRarity += 6;
            chosenSuffix = database.pickGearSuffixByLevel((int)itemRarity);

            return chosenSuffix;
        }

        else if (currentItemRarity == "Legendary")
        {
            randomDifference = Random.Range(1.75f, 2f);
            itemRarity += 8;
            chosenSuffix = database.pickGearSuffixByLevel((int)itemRarity);

            return chosenSuffix;
        }
        //never returned null
        return null;
    }









    //Metodi joka päättää itemin rarityn, 52% = common, 26% = uncommon, 13% = rare, 7% = epic, 2% = legendary
    float assignItemRarity(int level, float rarityBonus) 
    {
        float random = Random.Range(0f, 1f) * (rarityBonus);
        float randomDifference;
        float itemRarity = level;

        //Common spawn above this % 47          (60% chance)
        float commonChance = 0.40f * lootChance;
        //Uncommons spawn between these % 21-47 (25% chance)
        float uncommonChance = 0.15f * lootChance;
        //Rares spawn between these % 9-21      (11% chance)
        float rareChance = 0.04f * lootChance;
        //Epics spawn between these % 3-9       (3% chance)
        float epicChance = 0.01f * lootChance;
        //Legendaries spawn under this % 3      (1% chance)


        //If random is BIGGER than commonChance, spawn Common
        if (random >= commonChance)
        {
            randomDifference = Random.Range(1.8f, 2.2f);
            itemRarity = ((level / 5) + 1) * randomDifference;
            currentItemRarity = "Common";
            
        }
        //If random is BIGGER than uncommonChance, spawn Uncommon
        else if (random < commonChance && random >= uncommonChance)
        {
            randomDifference = Random.Range(2.8f, 3.2f);
            itemRarity = ((level / 5) + 1) * randomDifference;
            currentItemRarity = "Uncommon";
        }

        //If random is BIGGER than rareChance, spawn Rare
        else if (random < uncommonChance && random >= rareChance)
        {
            randomDifference = Random.Range(3.8f, 4.2f);
            itemRarity = ((level / 5) + 1) * randomDifference;
            currentItemRarity = "Rare";
        }

        //If random is BIGGER than epicChance, spawn Epic
        else if (random < rareChance && random > epicChance)
        {
            randomDifference = Random.Range(4.8f, 5.2f);
            itemRarity = ((level / 5) + 1) * randomDifference;
            currentItemRarity = "Epic";
        }

        //If random is SMALLER than every else chance, spawn legendary!
        else if (random < epicChance)
        {
            randomDifference = Random.Range(5.8f, 6.2f);
            itemRarity = ((level / 5) + 1) * randomDifference;
            currentItemRarity = "Legendary";
        }
        //If level is not enough, create common item
        else
        {
            randomDifference = Random.Range(1.8f, 2.2f);
            itemRarity = ((level / 5) + 1) * randomDifference;
            currentItemRarity = "Common";
        }



        //Debug.Log("created an item with level" + level +" and it has "+itemRarity+" rarity");

        return itemRarity;
       
    }


    int pickMultiplier()
    {
        int random = Random.Range(0, 5);

        if (random == 0) return -1;
        else return 1;
    }




}
