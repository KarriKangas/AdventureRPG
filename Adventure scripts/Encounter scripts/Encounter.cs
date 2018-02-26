using UnityEngine;
using System.Collections;

//This class is an instance of an encounter and every single encounter on adventures is constructed here
public class Encounter {

    //Must for UI
    public string Title { get; set; }
    public Sprite Sprite { get; set; }
    public string ActionName { get; set; }  // For example if encouner == chest -> ActionName = Open, if encounter == fountain -> ActionName = Drink
    
    //Must for backend
    public int ID { get; set; }

    //½ Must for backend
    public int level { get; set; }

    //Optional, maybe needed in the future?
    /*public bool interactable { get; set; }
    public int exp { get; set; }
    public int gold { get; set; }
    public int heal { get; set; }
    public int energy { get; set; }*/

    public Encounter(string title, string action, int id)
    {
        this.Title = title;
        this.ActionName = action;
        this.ID = id;

        this.Sprite = Resources.Load<Sprite>("Sprites/Encounters");

    }
    



}
