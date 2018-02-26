using UnityEngine;
using System.Collections;

public class gearType
{

    //Item Type luokka, tämä määrää itemin tyypin ja on osa jokaista itemiä
    //tyyppejä mm. Dagger, Sword, Axe...

    public string TypeName { get; set; }
    public string EquipSlot { get; set; }
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
    

    //Constructor for WEAPON TYPES
    public gearType(string name,  string slot, int minDmg, int maxDmg, int hp, int energy, int level, int id)
    {
        this.TypeName = name;
        this.EquipSlot = slot;
        this.minDamage = minDmg;
        this.maxDamage = maxDmg;
        this.Health = hp;
        this.Energy = energy;
        this.Level = level;
        this.ID = id;

    }

    //Constructor for GEAR TYPES
    public gearType(string name, string slot, int minDmg, int maxDmg, int hp, int energy, int level, int id, float LifeSteal, float HealthRegen, float EnergyRegen, float ExperienceBonus, float GoldBonus, float LootQuantity, float LootQuality, float Armor)
    {
        this.TypeName = name;
        this.EquipSlot = slot;
        this.minDamage = minDmg;
        this.maxDamage = maxDmg;
        this.Health = hp;
        this.Energy = energy;
        this.Level = level;
        this.ID = id;

        this.lifeSteal = LifeSteal;
        this.healthRegen = HealthRegen;
        this.energyRegen = EnergyRegen;
        this.experienceBonus = ExperienceBonus;
        this.goldBonus = GoldBonus;
        this.lootQuantity = LootQuantity;
        this.lootQuality = LootQuality;
        this.armor = Armor;
    }

    

    public gearType()
    {

    }

    public gearType(string name, string slot)
    {
        this.TypeName = name;
        this.EquipSlot = slot;
    }
}
