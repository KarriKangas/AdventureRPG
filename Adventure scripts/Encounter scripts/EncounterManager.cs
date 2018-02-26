using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class EncounterManager : MonoBehaviour {

    [SerializeField]
    private GameObject EncounterPanel;


    private AdventureScript adventure;
    private Player player;
    private Inventory inventory;

    private Text Title;
    private Text ActionName;

    private List<Encounter> mainQuest = new List<Encounter>();
    private List<Encounter> encounters = new List<Encounter>();

    private Encounter currentEncounter;
    

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        adventure = GameObject.Find("AdventureManager").GetComponent<AdventureScript>();
        InitEncounterPanel();
        createEncounterList();
    }


    public void showEncounter()
    {
        pickEncounter();
        Title.text = currentEncounter.Title;
        ActionName.text = currentEncounter.ActionName;

        EncounterPanel.SetActive(true);
    }

    Encounter pickEncounter()
    {
        Encounter chosenEncounter;

        //if current main quest requires encounter do this---- TO DO

        //else
        Debug.Log("Choosing encounter...");

        chosenEncounter = encounters[Random.Range(0, encounters.Count)];
        currentEncounter = chosenEncounter;
        Debug.Log("Chose " + currentEncounter.Title + " with ID " + currentEncounter.ID);
        return currentEncounter;
    }

    //Method for initializing the components of encounter panel
    void InitEncounterPanel()
    {
        EncounterPanel.SetActive(true);
        Title = EncounterPanel.transform.Find("EncounterTitlePanel").GetChild(0).GetComponent<Text>();
        ActionName = EncounterPanel.transform.Find("EncounterButtonPanel").GetChild(0).GetChild(0).GetComponent<Text>();

        EncounterPanel.SetActive(false);
    }

    //Method to create an encounter and add it to the encounters list
    void createEncounter(string Title, string ActionName, int id)
    {
        Encounter createdEncounter = new Encounter(Title, ActionName, id);
        encounters.Add(createdEncounter);
               
    }

    //In this method, every single encounter is manually created
    //ID IS SUPER IMPORTANT, it is used in encounterButtonClicked to see what needs to be done
    void createEncounterList()
    {
        createEncounter("Treasure", "Take", 0);
        createEncounter("Fountain", "Drink", 1);
        createEncounter("Shrine", "Pray", 2);
    }


    //List of encounters that might happen, and what happens when they are clicked
    public void OnEncounterClick()
    {
        int id = currentEncounter.ID;
        Debug.Log("ID IS " + id);
        switch (id)
        {
            case (0):
                inventory.AddItem(new Item("Treasure", 500, 0001));
                Debug.Log("Treasure chest added!");
                break;

            case (1):
                fountainAction();
                Debug.Log("Health healed!");
                break;

            case (2):
                shrineAction();
                Debug.Log("Experience added!");
                break;


        }

        EncounterPanel.SetActive(false);
        adventure.startBattle();
    }

    public void walkAway()
    {
        EncounterPanel.SetActive(false);
        adventure.startBattle();
    }

    //Method that runs when player clicks Action on fountain, choose between healing 50% of health/energy
    private void fountainAction()
    {
        int random = Random.Range(0, 2);
        if(random == 0)
        {
            player.setCurrentHealth(player.getMaxHealth() / 2);
        }
        else if (random == 1)
        {
            player.setCurrentEnergy(player.getMaxEnergy() / 2);
        }
    }

    //When action is shrine, give player 20% of current experience required
    private void shrineAction()
    {
        player.addExperience(player.getExpReq() / 5);
    }
}
