using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class InteractionManager : MonoBehaviour {

    [SerializeField]
    private Text interactionTitle;

    [SerializeField]
    private Image interactionIMG;

    List<Button> buttons;

    [SerializeField]
    private Button button0;
    private Text button0Txt;

    [SerializeField]
    private Button button1;
    private Text button1Txt;

    [SerializeField]
    private Button button2;
    private Text button2Txt;

    [SerializeField]
    private NPCHolder NPCHold;
  
	// Use this for initialization
	void Start () {
        
        setButtons();
        NPCHold.setCurrentNPC(0);
        switchNPC();
       

	}

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.V))
        {
            addQuest(NPCHold.getCurrentNPC());
            
        }
    }


    //Method for switching currentNPC, current is everything!
    public void switchNPC()
    {
        interactionTitle.text = NPCHold.getCurrentNPC().name;
        interactionIMG.sprite = NPCHold.getCurrentNPC().image;
        button0Txt.text = NPCHold.getCurrentNPC().buttonText0;
        button1Txt.text = NPCHold.getCurrentNPC().buttonText1;
        button2Txt.text = NPCHold.getCurrentNPC().buttonText2;

        disableButtons();

        for(int i = 0; i < NPCHold.getCurrentNPC().buttonAmount; i++)
        {
            buttons[i].gameObject.SetActive(true);
        }
    }

    //Set buttonTxts to correspond to correct text object
    void setButtons()
    {
        buttons = new List<Button>();
       
        buttons.Add(button0);
        buttons.Add(button1);
        buttons.Add(button2);

        button0Txt = button0.transform.GetChild(0).GetComponent<Text>();
        button1Txt = button1.transform.GetChild(0).GetComponent<Text>();
        button2Txt = button2.transform.GetChild(0).GetComponent<Text>();

        disableButtons();
    }

    void refreshButtons()
    {
        button0Txt.text = NPCHold.getCurrentNPC().buttonText0;
        button1Txt.text = NPCHold.getCurrentNPC().buttonText1;
        button2Txt.text = NPCHold.getCurrentNPC().buttonText2;
    }


    //Method to disable all buttons
    void disableButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }

    }

    //Method for adding a quest (only questAmount +1) for an NPC
    public void addQuest(NonPlayerCharacter npc)
    {
        string parsed;
        for(int i = 0; i < npc.buttonAmount; i++)
        {
            parsed = (buttons[i].transform.GetChild(0).GetComponent<Text>().text).Substring(0, 3);
            if (parsed == "Que")
            {
                npc.questAmount++;
                npc.setButtonText(i, "Quests " + "(" + npc.questAmount + ")");
                refreshButtons();
               // Debug.Log("done");
            }
        }          
    }

    //And removing it (-1)
    public void removeQuest(NonPlayerCharacter npc)
    {
        npc.questAmount--;
        npc.setButtonText(0, "Quests " + "(" + npc.questAmount + ")");
        button0Txt.text = npc.buttonText0;
        Debug.Log(npc.buttonText0);

    }
}
