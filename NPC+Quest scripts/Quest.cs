using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class Quest{


    private string title;

    private string description;

    private int id;


    public float XPreward { get; set; }
    public int goldReward { get; set; }

    public int currentObjAmount { get; set; }
    public int totalObjAmount { get; set; }
    
    public string objective { get; set; }
    private string toCollect { get; set; }
    public EnemyType toKill { get; set; }


    //Quest constructor without ObjectiveType
    public Quest(string Title, string Description, float XPrew, int Goldrew, string Obj, int totalObjAmount, int ID)
    {
        this.title = Title;
        this.description = Description;
        this.XPreward = XPrew;
        this.goldReward = Goldrew;
        this.totalObjAmount = totalObjAmount;
        this.objective = Obj;
        this.id = ID;
    }

    //Quest constructor objectivetype = Kill
    public Quest(string Title, string Description, float XPrew, int Goldrew, string Obj, int totalObjAmount, EnemyType toKillType, int ID)
    {
        this.title = Title;
        this.description = Description;
        this.XPreward = XPrew;
        this.goldReward = Goldrew;
        this.totalObjAmount = totalObjAmount;
        this.objective = Obj;
        this.toKill = toKillType;
        this.id = ID;

    }

    //Quest constructor objectivetype = Collect
    public Quest(string Title, string Description, float XPrew, int Goldrew, string Obj, int ObjAmount, string toCollectType, int ID)
    {
        this.title = Title;
        this.description = Description;
        this.XPreward = XPrew;
        this.goldReward = Goldrew;
        this.objective = Obj;
        this.toCollect = toCollectType;
        this.id = ID;

    }

    public Quest()
    {
        this.id = -1;
        this.currentObjAmount = 0;
    }

    public int giveID()
    {
        return id;
    }

    public void setID(int ID)
    {
        this.id = ID;
    }

    public string getDescription()
    {
        return description;

    }

    public string getTitle()
    {
        return title;

    }
    
}
