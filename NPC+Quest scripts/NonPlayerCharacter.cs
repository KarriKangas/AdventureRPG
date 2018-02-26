using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NonPlayerCharacter{

    public string name { get; set; }
    public Sprite image { get; set; }
    public int questAmount { get; set; }
    public int buttonAmount { get; set; }
    public string buttonText0 { get; set; }
    public string buttonText1 { get; set; }
    public string buttonText2 { get; set; }
    public int id { get; set; }
    public int level { get; set; }

    public List<Item> shopItems = new List<Item>();
    public List<Item> buybackItems = new List<Item>(); 

    public NonPlayerCharacter(string Name, Sprite Image, int ButtonAmount, int ID)
    {
        this.name = Name;
        this.image = Image;
        this.buttonAmount = ButtonAmount;
        this.id = ID;
    }

    public NonPlayerCharacter()
    {
        this.id = -1;
    }

    public void setButtonText(int buttonNum, string text)
    {
        if(buttonNum == 0)
        {
            buttonText0 = text;
        }
        if (buttonNum == 1)
        {
            buttonText1 = text;
        }
        if (buttonNum == 2)
        {
            buttonText2 = text;
        }

    }

}
