using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class AdventureBuilderButtons : MonoBehaviour {


    AdventureBuilder AdventureBuilder;

    string battleAmount, lootAmount;

    int difficulty;
    Sprite notClickedButton;
    Sprite clickedButton;
	// Use this for initialization
	void Start () {
        //Set the very first difficulty+battle amount to be displayed
        difficulty = 1;
        battleAmount = "2-3";
        
        AdventureBuilder = GameObject.Find("AdventureBuildPanel").GetComponent<AdventureBuilder>();

        notClickedButton = Resources.Load<Sprite>("Sprites/UI/notClickedButton");

        
        clickedButton = Resources.Load<Sprite>("Sprites/UI/ClickedButton");

        updateInfo();
    }
	


    public void difficultyButtonName()
    {
        AdventureBuilder.currentDifficulty = transform.parent.name;
        Debug.Log(AdventureBuilder.currentDifficulty);

        changeImages("difficulty");
        transform.parent.GetComponent<Image>().sprite = clickedButton;

        updateInfo();
    }

    public void lengthButtonName()
    {
        AdventureBuilder.currentLength = transform.parent.name;
        Debug.Log(AdventureBuilder.currentLength);

        changeImages("length");
        transform.parent.GetComponent<Image>().sprite = clickedButton;

        updateInfo();
    }


    private void updateInfo()
    {
        setDifficulty();
        setBattleAmount();
        setLootAmount();

        AdventureBuilder.setDifficulty();
        AdventureBuilder.setLength();

        AdventureBuilder.DestinationDescription = @"Level: " + (AdventureBuilder.location.getLevel() + difficulty-1)+ "-" + (AdventureBuilder.nextLocation.getLevel() + difficulty-1) +
            "\nBattles: " + battleAmount +
            "\nLoot: " + lootAmount;

        AdventureBuilder.setTexts();
    }

    private void setDifficulty()
    {
        if (AdventureBuilder.currentDifficulty == "Easy")
        {
            difficulty = 1;
        }
        else if (AdventureBuilder.currentDifficulty == "Medium")
        {
            difficulty = 2;
        }
        else if (AdventureBuilder.currentDifficulty == "Hard")
        {
            difficulty = 4;
        }
    }

    private void setBattleAmount()
    {
        if (AdventureBuilder.currentLength == "Short")
        {
            battleAmount = "2-3";
        } else if (AdventureBuilder.currentLength == "Normal")
        {
            battleAmount = "3-5";
        } else if (AdventureBuilder.currentLength == "Long")
        {
            battleAmount = "5-7";
        }
    }

    private void setLootAmount()
    {
        int lengthLoot=0, difficultyLoot=0;
        if(AdventureBuilder.currentLength == "Short")
        {
            lengthLoot = 1;
        }
        else if (AdventureBuilder.currentLength == "Normal")
        {
            lengthLoot = 2;
        }
        else if (AdventureBuilder.currentLength == "Long")
        {
            lengthLoot = 4;
        }

        if (AdventureBuilder.currentDifficulty == "Easy")
        {
            difficultyLoot = 1;
        }
        else if (AdventureBuilder.currentDifficulty == "Medium")
        {
            difficultyLoot = 2;
        }
        else if (AdventureBuilder.currentDifficulty == "Hard")
        {
            difficultyLoot = 4;
        }

        int totalLoot = lengthLoot + difficultyLoot;
        if(totalLoot < 3)
        {
            lootAmount = "Very Poor";
        }else if(totalLoot == 3)
        {
            lootAmount = "Poor";
        }else if(totalLoot == 4){
            lootAmount = "Mediocre";
        }else if(totalLoot == 5){
            lootAmount = "Good";
        }
        else if (totalLoot == 6)
        {
            lootAmount = "Very Good";
        }
        else if (totalLoot == 8)
        {
            lootAmount = "Excellent";
        }


    }

    private void changeImages(string DiffLeng)
    {
        if(DiffLeng == "difficulty")
        {
            for(int i = 0; i < AdventureBuilder.difficultyButtons.Count; i++)
            {
                AdventureBuilder.difficultyButtons[i].GetComponent<Image>().sprite = notClickedButton;
            }
        }

        if (DiffLeng == "length")
        {
            for (int i = 0; i < AdventureBuilder.lengthButtons.Count; i++)
            {
                AdventureBuilder.lengthButtons[i].GetComponent<Image>().sprite = notClickedButton;
            }
        }
    }


}
