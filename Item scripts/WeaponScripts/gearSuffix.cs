using UnityEngine;
using System.Collections;

public class gearSuffix {

    //Item Suffix luokka, tämä määrää itemin suffixin, eli jälkiliitten ja on osa jokaista itemiä
    //suffix mm. Child, Squirrel, Rabbit...

    public string SuffixName { get; set; }
    public int minDamage { get; set; }
    public int maxDamage { get; set; }

    public int Health { get; set; }
    public int Energy { get; set; }
    public int Level { get; set; }
    public int ID { get; set; }

    public float lifeSteal { get; set; }
    public float healthRegen { get; set; }
    public float energyRegen { get; set; }
    public float experienceBonus { get; set; }
    public float goldBonus { get; set; }
    public float lootQuantity { get; set; }
    public float lootQuality { get; set; }
    public float armor { get; set; }

    //Constructor for WEAPON SUFFIXES
    public gearSuffix(string name, int minDmg, int maxDmg, int hp, int energy, int level, int id, float LifeSteal)
    {
        this.SuffixName = name;
        this.minDamage = minDmg;
        this.maxDamage = maxDmg;
        this.Health = hp;
        this.Energy = energy;
        this.Level = level;
        this.ID = id;
        this.lifeSteal = LifeSteal;
    }

    //Constructor for GEAR SUFFIXES
    public gearSuffix(string name, int minDmg, int maxDmg, int hp, int energy, int level, int id, float Armor, float HealthRegen, float EnergyRegen, float ExperienceBonus, float GoldBonus, float LootQuantity, float LootQuality)
    {
        this.SuffixName = name;
        this.minDamage = minDmg;
        this.maxDamage = maxDmg;
        this.Health = hp;
        this.Energy = energy;
        this.Level = level;
        this.ID = id;

        this.armor = Armor;
        this.healthRegen = HealthRegen;
        this.energyRegen = EnergyRegen;
        this.experienceBonus = ExperienceBonus;
        this.goldBonus = GoldBonus;
        this.lootQuantity = LootQuantity;
        this.lootQuality = LootQuality;
        
    }

    public gearSuffix()
    {

    }

    public gearSuffix(string name)
    {
        this.SuffixName = name;
    }
}
