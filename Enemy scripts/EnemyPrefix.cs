using UnityEngine;
using System.Collections;

public class EnemyPrefix{

    public string PrefixName { get; set; }
    public int minDamage { get; set; }
    public int maxDamage { get; set; }
    public int Health { get; set; }
    public int Difficulty { get; set; }
    public int ID { get; set; }

    public EnemyPrefix(string name,int minDmg, int maxDmg, int hp, int diff, int id)
    {
        this.PrefixName = name;
        this.minDamage = minDmg;
        this.maxDamage = maxDmg;
        this.Health = hp;
        this.Difficulty = diff;
        this.ID = id;
    }

    public EnemyPrefix(string name)
    {
        this.PrefixName = name;
    }

}

