using UnityEngine;
using System.Collections;

public class EnemyType{

	//EnemyType luokka määrää vihollisen tyypin.

    public string TypeName { get; set; }
    public int minDamage { get; set; }
    public int maxDamage { get; set; }
    public int Health { get; set; }
    public int Difficulty { get; set; }
    public int ID { get; set; }
    
    public EnemyType(string name,int minDmg, int maxDmg, int hp, int diff, int id)
    {
        this.TypeName = name;
        this.minDamage = minDmg;
        this.maxDamage = maxDmg;
        this.Health = hp;
        this.Difficulty = diff;
        this.ID = id;
    }

    public EnemyType(string name)
    {
        this.TypeName = name;
    }

    public string toString()
    {
        return this.TypeName;
    }

}
