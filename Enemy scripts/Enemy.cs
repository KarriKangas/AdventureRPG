using UnityEngine;
using System.Collections;

public class Enemy {

    //Enemy luokka, tätä käytetään jokaisen vastustajan pohjana
    public string Title { get; set; }
    public string Description { get; set; }
    public EnemyType Type { get; set; }
    public EnemyPrefix Prefix { get; set; }
    public Sprite Sprite { get; set; }
    public int minDamage { get; set; }
    public int maxDamage { get; set; }
    public int Health { get; set; }
    public int Difficulty { get; set; }
    public string ID { get; set; }
    public bool hasPre;

    //Konstruktori jossa prefix
    public Enemy(EnemyType type, EnemyPrefix prefix)
    {
        this.Type = type;

        this.Prefix = prefix;
        this.hasPre = true;

        this.Title = prefix.PrefixName + " " + type.TypeName;
        this.minDamage = prefix.minDamage + type.minDamage;
        this.maxDamage = prefix.maxDamage + type.maxDamage;
        this.Health = prefix.Health + type.Health;
        this.Difficulty = prefix.Difficulty + type.Difficulty;

        this.ID = prefix.ID.ToString() + type.ID.ToString();
        this.Sprite = Resources.Load<Sprite>("Sprites/Enemies/" + type.TypeName);

        this.Description = "Type: " + Type.toString() + "\nDifficulty: " + Difficulty + "\nDamage: " + minDamage +"-" + maxDamage + "\nHealth: " + Health;
    }

    //Konstruktori jossa pelkkä tyyppi
    public Enemy(EnemyType type)
    {
        this.Type = type;

        this.Title =  type.TypeName;
        this.minDamage = type.minDamage;
        this.maxDamage =  type.maxDamage;
        this.Health = type.Health;
        this.Difficulty = type.Difficulty;

        this.ID = type.ID.ToString();
        this.Sprite = Resources.Load<Sprite>("Sprites/Enemies/" + type.TypeName);

        this.Description = "Type: " + Type.toString() + "\nDifficulty: " + Difficulty + "\nDamage: " + minDamage + "-"  + maxDamage + "\nHealth: " + Health;
    }

    public void resetDescription()
    {
        this.Description = "Type: " + Type.toString() + "\nDifficulty: " + Difficulty + "\nDamage: " + minDamage + "-" + maxDamage + "\nHealth: " + Health;
    }

    

}
