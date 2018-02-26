using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class AdventureBuilder : MonoBehaviour {



    private int difficultyBonus;
    private int adventureLength;

    public string currentDifficulty, currentLength;

    [SerializeField]
    GameObject DestinationInfoPanel;

    [SerializeField]
    Image DestinationImage;

    [SerializeField]
    public LocationManager LocationManager;

    [SerializeField]
    private BlockerScript blocker;

    public AdventureScript AdventureManager;

    public string DestinationInfoTitle;
    private Text DestinationInfoTitleText;

    public string DestinationDescription;
    private Text DestinationDescriptionText;

    [SerializeField]
    public List<Button> difficultyButtons, lengthButtons;

    public Location location;
    public Location nextLocation;


    public void Initialize()
    {
        DestinationInfoTitleText = DestinationInfoPanel.transform.GetChild(0).GetComponent<Text>();
        DestinationDescriptionText = DestinationInfoPanel.transform.GetChild(1).GetComponent<Text>();

        location = LocationManager.LocationList[0].GetComponent<Location>();
        nextLocation = LocationManager.LocationList[1].GetComponent<Location>();

        currentDifficulty = "Easy";
        currentLength = "Short";
    }

    public void setTexts()
    {
        DestinationInfoTitleText.text = DestinationInfoTitle;
        DestinationDescriptionText.text = DestinationDescription;
    }
    //Called in adventure builder buttons every time a button is pressed
    public void setDifficulty()
    {
        string difficulty = currentDifficulty;
        

        //Switch difficulty depending on which button is pressed
        //Easy = 1;
        //Normal = 3;
        //Hard = 5;
        switch (difficulty)
        {
            case ("Easy"):
                
                difficultyBonus = 1;
                break;

            case ("Medium"):
                difficultyBonus = 3;
                break;

            case ("Hard"):
                difficultyBonus = 5;
                break;

        }

        
    }

    //Called in adventure builder buttons every time a button is pressed
    public void setLength()
    {
        string length = currentLength;
        
        //Switch length depending on which button is pressed
        //Easy = 2-3;
        //Normal = 3-5;
        //Hard = 5-7;
        switch (length)
        {
            case ("Short"):
                adventureLength = Random.Range(2, 4);
                break;

            case ("Normal"):
                adventureLength = Random.Range(4, 6);
                break;

            case ("Long"):
                adventureLength = Random.Range(6, 8);
                break;



        }
        
    }

    public void showBuilder()
    {
        DestinationInfoTitle = AdventureManager.player.location.name;
        DestinationDescription = @"Level: " + (location.getLevel()) + "-" + (nextLocation.getLevel()) +
            "\nBattles: 2-3"  +
            "\nLoot: Very Poor";
        setTexts();
        transform.gameObject.SetActive(true);
        closePanels();

    }

    //Method to close panels behind the activated one
    private void closePanels()
    {
        blocker.activePanel("Shop", false);
        blocker.activePanel("Interaction", false);
        blocker.activePanel("Map", false);
        blocker.activePanel("Quests", false);
    }

    //Returns difficulty of current adventure
    public int getDifficulty()
    {        
        return difficultyBonus;
    }

    //Returns the length of current adventure
    public int getLength()
    {       
        return adventureLength;
    }

}
