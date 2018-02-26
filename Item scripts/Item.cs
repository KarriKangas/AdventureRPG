using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

public class Item{

    //Item luokka, tätä käytetään jokaisen itemin pohjana
    public string Title { get; set; }
    public gearType Type { get; set; }
    public gearPrefix Prefix { get; set; }
    public gearSuffix Suffix { get; set; }
    public Sprite Sprite { get; set; }
    public int minDamage { get; set; }
    public int maxDamage { get; set; }
    public int Health { get; set; }
    public int Energy { get; set; }
    public int Level { get; set; }
    public string ID { get; set; }
    public bool hasPre, hasSuf;

    public float armor { get; set; }
    public float lifeSteal { get; set; }
    public float healthRegen { get; set; }
    public float energyRegen { get; set; }
    public float experienceBonus { get; set; }
    public float goldBonus { get; set; }
    public float lootQuantity { get; set; }
    public float lootQuality { get; set; }

    public int price { get; set; }


    public bool Usable { get; set; }
    public bool Stackable { get; set; }
    public int Value { get; set; }
    public int BuffAmount { get; set; }

    public string DescriptionName { get; set; }
    public string DescriptionAmount { get; set; }

    private List<string> rarities;
    public string Rarity { get; set; }
    


    //WEAPON Konstruktori joss tyyppi (knife), prefix (dusty) ja suffix (of the sheep)

    public Item(gearType type, gearPrefix prefix, gearSuffix suffix)
    {
        this.Type = type;

        this.Prefix = prefix;
        this.hasPre = true;

        this.Suffix = suffix;
        this.hasSuf = true;

        this.Title = prefix.PrefixName + " " + type.TypeName + " of the " + suffix.SuffixName;
        this.minDamage = prefix.minDamage + type.minDamage + suffix.minDamage;
        this.maxDamage = prefix.maxDamage + type.maxDamage + suffix.maxDamage;
        this.Health =  prefix.Health + type.Health + suffix.Health;
        this.Energy = prefix.Energy + type.Energy + suffix.Energy;       
        this.Level = prefix.Level + type.Level + suffix.Level;
        this.ID = prefix.ID.ToString() + type.ID.ToString() + suffix.ID.ToString();
        this.Sprite = Resources.Load<Sprite>("Sprites/Items/Weapons/" + type.TypeName);

        this.armor = type.armor + prefix.armor + suffix.armor;
        this.lifeSteal = type.lifeSteal + prefix.lifeSteal + suffix.lifeSteal;
        this.healthRegen = type.healthRegen + prefix.healthRegen + suffix.healthRegen;
        this.energyRegen = type.energyRegen + prefix.energyRegen + suffix.energyRegen;
        this.experienceBonus = type.experienceBonus + prefix.experienceBonus + suffix.experienceBonus;
        this.goldBonus = type.goldBonus + prefix.goldBonus + suffix.goldBonus;
        this.lootQuantity = type.lootQuantity + prefix.lootQuantity + suffix.lootQuantity;
        this.lootQuality = type.lootQuality + prefix.lootQuality + suffix.lootQuality;






    price = (type.Level + 1 + prefix.Level + suffix.Level)*3;
        buildWeaponDescription();
    }

    //WEAPON Konstruktori joss tyyppi (knife) ja prefix (dusty)
    public Item(gearType type, gearPrefix prefix)
    {
        this.Type = type;

        this.Prefix = prefix;
        this.hasPre = true;

        this.Suffix = null;
        this.hasSuf = false;

        this.Title = prefix.PrefixName + " " + type.TypeName;
        this.minDamage = prefix.minDamage + type.minDamage;
        this.maxDamage = prefix.maxDamage + type.maxDamage;
        this.Health = prefix.Health + type.Health;
        this.Energy = prefix.Energy + type.Energy;
        this.Level = prefix.Level + type.Level;
        this.ID = prefix.ID.ToString() + type.ID.ToString() + "000";
        this.Sprite = Resources.Load<Sprite>("Sprites/Items/Weapons/" + type.TypeName);

        this.armor = type.armor + prefix.armor;
        this.lifeSteal = type.lifeSteal + prefix.lifeSteal;
        this.healthRegen = type.healthRegen + prefix.healthRegen;
        this.energyRegen = type.energyRegen + prefix.energyRegen;
        this.experienceBonus = type.experienceBonus + prefix.experienceBonus;
        this.goldBonus = type.goldBonus + prefix.goldBonus;
        this.lootQuantity = type.lootQuantity + prefix.lootQuantity;
        this.lootQuality = type.lootQuality + prefix.lootQuality;

        price = (type.Level + 1 + prefix.Level)*2;
        buildWeaponDescription();
    }

    //WEAPON Konstruktori joss tyyppi (knife) ja suffix (of the wolf)
    public Item(gearType type, gearSuffix suffix)
    {
        this.Type = type;

        this.Prefix = null;
        this.hasPre = false;

        this.Suffix = suffix;
        this.hasSuf = true;
        this.Title = type.TypeName + " of the " + suffix.SuffixName;
        this.minDamage =  type.minDamage + suffix.minDamage;
        this.maxDamage =  type.maxDamage + suffix.maxDamage;
        this.Health = type.Health + suffix.Health;
        this.Energy = type.Energy + suffix.Energy;       
        this.Level = type.Level + suffix.Level;
        this.ID = "000" + type.ID.ToString() + suffix.ID.ToString();
        this.Sprite = Resources.Load<Sprite>("Sprites/Items/Weapons/" + type.TypeName);

        this.armor = type.armor + suffix.armor;
        this.lifeSteal = type.lifeSteal + suffix.lifeSteal;
        this.healthRegen = type.healthRegen + suffix.healthRegen;
        this.energyRegen = type.energyRegen + suffix.energyRegen;
        this.experienceBonus = type.experienceBonus + suffix.experienceBonus;
        this.goldBonus = type.goldBonus + suffix.goldBonus;
        this.lootQuantity = type.lootQuantity + suffix.lootQuantity;
        this.lootQuality = type.lootQuality + suffix.lootQuality;

        price = (type.Level +1  + suffix.Level)*2;
        buildWeaponDescription();
    }

    //WEAPON Konstruktori joss tyyppi (knife)
    public Item(gearType type)
    {
        this.Type = type;

        this.Prefix = null;
        this.hasPre = false;
        
        this.Suffix = null;
        this.hasSuf = false;

        this.Title =  type.TypeName;
        this.minDamage = type.minDamage;
        this.maxDamage = type.maxDamage;
        this.Health = type.Health;
        this.Energy = type.Energy;
        this.Level = type.Level;
        this.ID = "000" + type.ID.ToString() + "000";
        this.Sprite = Resources.Load<Sprite>("Sprites/Items/Weapons/" + type.TypeName);

        this.armor = type.armor;
        this.lifeSteal = type.lifeSteal;
        this.healthRegen = type.healthRegen;
        this.energyRegen = type.energyRegen;
        this.experienceBonus = type.experienceBonus;
        this.goldBonus = type.goldBonus;
        this.lootQuantity = type.lootQuantity;
        this.lootQuality = type.lootQuality;

        price = (type.Level + 1)*2;
        buildWeaponDescription();


    }

    //USABLE Konstruktori
    public Item(string title, int value, int buffAmount, bool usable, bool stackable, int id)
    {
        this.Title = title;
        this.Value = value;
        this.BuffAmount = buffAmount;
        this.Usable = usable;
        this.Stackable = stackable;
        this.ID = id.ToString();

        buildUsableDescription();
        this.Sprite = Resources.Load<Sprite>("Sprites/Items/Usables/" + title);

        this.price = value;
    }

    //CONTAINER Konstruktori
    public Item(string title, int value, int id)
    {
        this.Title = title;
        this.Value = value;
        this.ID = id.ToString();

        DescriptionName = "Click to open!";
        this.Sprite = Resources.Load<Sprite>("Sprites/Items/Usables/" + title);

        this.price = value;
    }

    //Konstruktori joka luo tyhjän esineen
    public Item()
    {
        this.ID = "-1";
    }


    //count rarity metodi, ottaa parametreina Level (eli Type.Level) ja Rarity (eli prefix.level+type.level+suffix.level) JA AINA RARITY LISTA (tehdään aina weaponItemHandlerissa)
    public void countRarity(float rarity, List<string> rarities)
    {
       /* Rarity = rarities[0];
        int itemLevel = Type.Level+5;

       
        for (int i = 0; i < rarities.Count; i++)
        {
            

            //if item rarity is bigger than   FORMULA:  ((LEVEL/5) + 1 ) * 3+i + (itemLevel/5) 
            if (rarity > (((itemLevel / 5) + 1)) * (3 + i) + (itemLevel/2))
            {
                Rarity = rarities[i];
                Debug.Log("Rarity is " + rarity + " and in text " + rarities[i]);
                //Debug.Log("Formula gives: " + ((((itemLevel / 5) + 1)) * (3 + i)));
            }
        }
       // Debug.Log("Counted rarity for " + Title + " it is " + Rarity);*/


    }

    private void buildWeaponDescription()
    {
        string descriptionStatName = "";
        string descriptionStatAmount = "";

        if (minDamage != 0)
        {
            descriptionStatName = "Damage:";
            descriptionStatAmount = minDamage + "-" + maxDamage;
        }
        if (armor != 0)
        {
            descriptionStatName += "Armor:";
            descriptionStatAmount += "" + armor;
        }
        if (Health != 0)
        {
            descriptionStatName += "<color=green>\nHealth:</color>";
            descriptionStatAmount += "<color=green>\n" + (Health) + "</color>";
        }
        if(Energy != 0)
        {
            descriptionStatName += "<color=green>\nEnergy:</color>";
            descriptionStatAmount += "<color=green>\n" + (Energy) + "</color>";
        }       
        if (lifeSteal != 0)
        {
            descriptionStatName += "<color=green>\nLife steal:</color>";
            descriptionStatAmount += "<color=green>\n" + (lifeSteal * 100) + "%</color>";
        }
        if (healthRegen != 0)
        {
            descriptionStatName += "<color=green>\nHealth regeneration:</color>";
            descriptionStatAmount += "<color=green>\n" + (healthRegen) + "/turn</color>";
        }
        if (energyRegen != 0)
        {
            descriptionStatName += "<color=green>\nEnergy regeneration:</color>";
            descriptionStatAmount += "<color=green>\n" + (energyRegen) + "/turn</color>";
        }
        if (experienceBonus != 0)
        {
            descriptionStatName += "<color=green>\nExperience bonus:</color>";
            descriptionStatAmount += "<color=green>\n" + (experienceBonus * 100) + "%</color>";
        }
        if (goldBonus != 0)
        {
            descriptionStatName += "<color=green>\nGold bonus:</color>";
            descriptionStatAmount += "<color=green>\n" + (goldBonus * 100) + "%</color>";
        }
        if (lootQuality != 0)
        {
            descriptionStatName += "<color=green>\nLoot quality:</color>";
            descriptionStatAmount += "<color=green>\n" + (lootQuality * 100) + "%</color>";
        }
        if (lootQuantity != 0)
        {
            descriptionStatName += "<color=green>\nLoot quantity:</color>";
            descriptionStatAmount += "<color=green>\n" + (lootQuantity * 100) + "%</color>";
        }

        descriptionStatName += "\nItem level:";
        descriptionStatAmount += "\n" + (Level);
        descriptionStatName += "<color=yellow>\nSell value:</color>";
        descriptionStatAmount += "<color=yellow>\n" + (price) + "</color>";

        this.DescriptionName = descriptionStatName;
        this.DescriptionAmount = descriptionStatAmount;
    }

    private void buildUsableDescription()
    {
        string titleToLower = Title.ToLower();
        if (titleToLower.Contains("health"))
        {
            DescriptionName = "Restores " + BuffAmount + " points of health";
        }else if (titleToLower.Contains("energy"))
        {
            DescriptionName = "Restores " + BuffAmount + " points of energy";
        }
        DescriptionName += "<color=yellow>\nSell value:       " + Value + "</color>";
    }



}
