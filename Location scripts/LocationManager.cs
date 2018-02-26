using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

//Location manager luokka sisältää lokaatioiden näyttöä, muuttoa, päivitystä yms.
public class LocationManager : MonoBehaviour {

    private Text tooltip;
    private NPCHolder NPCManager;
    private InteractionManager InteractionManager;
    private Player Player;
    private string currentLoc;

    [SerializeField]
    public List<GameObject> LocationList;

    [SerializeField]
    private Text InteractionName;

    [SerializeField]
    private Text AdventureInfo;



	void Start () {
    
        Player = GameObject.Find("Player").GetComponent<Player>();
        NPCManager = GameObject.Find("NPCManager").GetComponent<NPCHolder>();
        InteractionManager = GameObject.Find("InteractionManager").GetComponent<InteractionManager>();
        //Asetetaan tooltipit
        setTooltips();
        //Näytetään lokaatio 0

        changeAvailability(0);

        //Vain testitarkoituksissa!
        /*for(int i = 0; i < 8; i++)
        {
           changeAvailability(i);
        }*/



        //Asetetaan pelaajan "current" lokaatio
        changeLocation(0);

    }
	

    //Näytetään/Piilotetaan lokaatiot kartassa sen mukaan mitkä niistä ovat pelaajalle "available"
    void showhideLocations()
    {
        for(int i = 0; i < LocationList.Count; i++)
        {
            if (LocationList[i].GetComponent<Location>().available != true)
            {
                LocationList[i].gameObject.SetActive(false);
            }
            else
            {
                LocationList[i].gameObject.SetActive(true);
            }
        }

    }

    //Tehdään tietystä lokaatiosta "available", toimii vain yhteen suuntaan koska lokaatioista ei koskaan tule unavailable kun ne on kerran saatu
    void changeAvailability(int locNum)
    {
        LocationList[locNum].GetComponent<Location>().available = true;
        showhideLocations();
    }

    //Metodi jota kutsutaan alussa, päivittää kaikkien lokaatioiden tooltipit pelissä näkyviksi!
    //(Eli haetaan tieto jokaisen lokaation Location scriptistä ja liitetään se gameobjectiin tekstinä)
    void setTooltips()
    {
        for(int i = 0; i < LocationList.Count; i++)
        {
            string name = LocationList[i].GetComponent<Location>().getName();
            string tooltip = LocationList[i].GetComponent<Location>().getTooltip();
            int Level = LocationList[i].GetComponent<Location>().getLevel();
            LocationList[i].GetComponent<Location>().id = i;
            LocationList[i].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = name + "\n" +tooltip + "\nLevel: " + Level;
            
        }
    }

    //Vaihdetaan pelaajan lokaatiota
    public void changeLocation(int locationNum)
    {

        Location tempLoc = LocationList[locationNum].GetComponent<Location>();
        currentLoc = tempLoc.name;


        //change player location
        Player.location = tempLoc;
        

        //Depending on location, choose level limit to be (currentLocLevel) to (nextLocLevel)
        //if locationNum+1 is equal to locationlist count it means we are in the last location and level limit can't be chosen from next level
        if (locationNum+1 == LocationList.Count)
        {

            AdventureInfo.text = "Location: " + tempLoc.name + "\nLevel: " + tempLoc.getLevel() + "-" + LocationList[locationNum].GetComponent<Location>().getLevel();
            
        }
        else
        {

            AdventureInfo.text = "Location: " + tempLoc.name + "\nLevel: " + tempLoc.getLevel() + "-" + LocationList[locationNum + 1].GetComponent<Location>().getLevel();
        }

        InteractionName.text = tempLoc.getInteraction();
        
        //Set the correct NPC reference
        NPCManager.setCurrentNPC(tempLoc.id);

        //And switch the current NPC as the portrayed NPC
        InteractionManager.switchNPC();
        //Debug.Log("Current NPC is now "+NPCManager.getCurrentNPC().name);
    }


}
