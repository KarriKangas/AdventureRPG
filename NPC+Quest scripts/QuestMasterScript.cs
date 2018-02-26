using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class QuestMasterScript : MonoBehaviour
{

    public List<Quest> QuestList;

    [SerializeField]
    private GameObject QuestListPanel;

    [SerializeField]
    private GameObject QuestInstance;

    [SerializeField]
    private GameObject Scrollbar;

    [SerializeField]
    private GameObject QuestContent;

    [SerializeField]
    private Player player;

    [SerializeField]
    private GameObject QuestRewardPanel;

    private GameObject questContinue, questNext;
    
    private Quest testQuest;

    EnemyInformationDatabase enemyDatabase;
    private List<EnemyType> enemyTypes;
    float minDiff, maxDiff;
    float questBaseXP;
    int questBaseGold;
    List<string> killList, collectList, collectItemList;

    //The "size" of the size changes of quest panel+quests when more quests than 3 are added
    private float QLPanelSizeChange;
    int counter;

    private List<Quest> tempCompletedQuests;
    private int totalTempCompleted;
    private int currentTempCompleted;
    
    void Start()
    {
        enemyDatabase = GameObject.Find("EnemyScriptHolder").GetComponent<EnemyInformationDatabase>();
        QuestList = new List<Quest>();
        prepareLists();

        QLPanelSizeChange =QuestContent.GetComponent<GridLayoutGroup>().cellSize.y + QuestListPanel.transform.GetComponent<GridLayoutGroup>().spacing.y;

        questContinue = QuestRewardPanel.transform.Find("continueButton").gameObject;
        questNext = QuestRewardPanel.transform.Find("nextButton").gameObject;


        minDiff = 0.7f;
        maxDiff = 1.3f;
        questBaseXP = 25;
        questBaseGold = 10;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {

            addQuest(generateQuest(Random.Range(1,100)));
        }

    }


    //Metodi jolla lisätään questi
    public void addQuest(Quest quest)
    {
        //Instansioidaan uusi questi Unityn objektiksi, tehdään siitä questlistpanelin lapsi ja nimetään se tyyliin "Quest0", asetetaan myös koko oikeaksi
        GameObject QuestToAdd = QuestInstance;
        GameObject tempQuest;
        tempQuest = Instantiate(QuestInstance, new Vector3(0, 0), Quaternion.identity) as GameObject;
        tempQuest.name = ("Quest" + QuestListPanel.transform.childCount);
        tempQuest.transform.SetParent(QuestListPanel.transform);
        tempQuest.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);



        //Lisää questi varsinaiseen koodissa olevaan questien listaan
        QuestList.Add(quest);
        //Jos questeja on enemmän kuin kaksi, muutetaan questlistpanelin kokoa ja sen sijaintia oikein
        if (QuestList.Count > 3)
        {
            QuestListPanel.GetComponent<RectTransform>().sizeDelta += new Vector2(0, QuestListPanel.GetComponent<RectTransform>().localScale.y + QLPanelSizeChange);
            QuestListPanel.GetComponent<RectTransform>().localPosition -= new Vector3(0, QLPanelSizeChange);
            //Ja kaikkia questeja liikutetaan sen mukaan
            for (int i = 0; i < QuestList.Count; i++)
            {
                QuestListPanel.transform.GetChild(i).GetComponent<RectTransform>().position += new Vector3(0, QLPanelSizeChange);
            }
        }
        updateQuestInfo(tempQuest, quest);

        if(Scrollbar.activeSelf == true)
        {
            Scrollbar.GetComponent<Scrollbar>().value = 1;
        }

    }



    //Questin title ja description päivitys metodi
    void updateQuestInfo(GameObject questObject, Quest quest)
    {
        questObject.transform.GetChild(0).GetComponent<Text>().text = quest.getTitle();
        questObject.transform.GetChild(1).GetComponent<Text>().text = quest.getDescription();


    }




    //Generate a quest, only parameter is difficulty, everything else will be assigned by random!
    public Quest generateQuest(float difficulty)
    {
        string objective = pickObjective();
        EnemyType questEnemyType;
        float xpReward;
        int goldReward;
        string title;
        string description;
       // string collectType;
        int objectiveAmount = Random.Range(3, 10);
        Quest randomQuest = new Quest();
        if (objective == "Kill")
        {
            questEnemyType = pickEnemyType(difficulty);
            xpReward = 2*((questBaseXP)  + (objectiveAmount * (3*questEnemyType.Difficulty)));
            goldReward = questBaseGold + (int)(difficulty * Random.Range(minDiff, maxDiff)) + (objectiveAmount * questEnemyType.Difficulty);
            title = questEnemyType.toString() + " " + assembleTitle(objective);

            //Description kasaa koko questin kuvauksen eli esim.
            //                "Kill"          " 5 "                    "Rats"                                             "50"                               "10"
            description = (objective + " " + randomQuest.currentObjAmount  +"/" + objectiveAmount + " " + questEnemyType.toString() + "s\nReward: " + Mathf.FloorToInt(xpReward) + " exp, " + goldReward + " gold");
            randomQuest = new Quest(title, description, xpReward, goldReward, objective, objectiveAmount, questEnemyType, QuestList.Count);
            Debug.Log("Generated a quest where totalObjAmount is  " + objectiveAmount);
            return randomQuest;
        }

        /*if(objective == "Collect")
        {
            collectType = chooseCollect();
            xpReward = (questBaseXP * (2 * difficulty))  + 3*(objectiveAmount);
            goldReward = questBaseGold + (int)(difficulty * Random.Range(minDiff, maxDiff)) + 3 * (objectiveAmount);
            title = collectType + " " + assembleTitle(objective);
            
            description = (objective + " " + objectiveAmount + " " + collectType + "\nReward: " + Mathf.FloorToInt(xpReward) + " exp, " + goldReward + " gold");
            randomQuest = new Quest(title, description, xpReward, goldReward, objective, objectiveAmount, QuestList.Count);
            return randomQuest;
        }*/








        return null;

    }
    

    //Method which is called every time a quest is completed
    public void progressKillQuest(EnemyType type)
    {
        currentTempCompleted = 0;
        totalTempCompleted = 0;
        tempCompletedQuests = new List<Quest>();
        //Debug.Log(QuestList.Count);
        //Scan through every quest in questlist
        for(int i  = 0;  i < QuestList.Count; i++)
        {
            //If killed enemy type == the type of enemy needed by the quest
            if (QuestList[i].toKill == type)
            {

                //Update the amount of killed enemies
                QuestList[i].currentObjAmount++;
                Debug.Log("fired");
                //If currentAmount == totalAmount, means quest is complede, add rewards and display reward tab
                if (QuestList[i].currentObjAmount >= QuestList[i].totalObjAmount)
                {
                    Debug.Log("Quest " + i + " seems to be completed");
                    //Add the completed quest to completed list and calculate the total amount of quests completed at once
                    tempCompletedQuests.Add(QuestList[i]);

                    totalTempCompleted = tempCompletedQuests.Count;

                    //Update quest (Because it is displayed in QuestRewardPanel)
                    updateQuestProgress(i);
                                                       
                }
                else {

                    Debug.Log("calling updateQuest");
                    updateQuestProgress(i);
                }
            }
        }
        if (totalTempCompleted > 0)
        {

            buildQuestReward();

            QuestRewardPanel.SetActive(true);
        }
    }

    //Method to updateQuestProgress TEXT in questlistpanel
    private void updateQuestProgress(int id)
    {
        
        //Create a slicedDescription, which is the reward part of the original description
        string slicedDescription;
        
        //Find index of line-change \n
        int index = QuestListPanel.transform.GetChild(id).GetChild(1).GetComponent<Text>().text.IndexOf("\n");
        
        //And set slicedDescription correctly
        slicedDescription = QuestListPanel.transform.GetChild(id).GetChild(1).GetComponent<Text>().text.Remove(0, index);
        
        //Set the "newBeginning" correctly: objective (not changed) currentObjAmount (changed part!)  totalObjAmount (not changed) and type to kill (not changed)
        string newBeginning = QuestList[id].objective + " " + QuestList[id].currentObjAmount + "/" + QuestList[id].totalObjAmount + " " + QuestList[id].toKill.TypeName;
       
        //And finally add newBeginning and the old sliced reward part together
        string finalString = newBeginning + slicedDescription;
   
        //And set the text correctly
        QuestListPanel.transform.GetChild(id).GetChild(1).GetComponent<Text>().text = finalString;

    }

    //Method to set texts for questrewardpanel
    private void buildQuestReward()
    {
        Quest currentQuest = tempCompletedQuests[0];
        int questAmount = QuestList.Count;
        if (tempCompletedQuests.Count == 1)
        {
            

            questNext.SetActive(false);
            questContinue.SetActive(true);

            //Add gold
            player.addGold(currentQuest.goldReward);

            //Add experience
            player.addExperience((int)currentQuest.XPreward);


            GameObject completedQuest = new GameObject();
            Debug.Log("getting child " + currentQuest.giveID() + " of QuestContent");
            completedQuest = QuestListPanel.transform.GetChild(currentQuest.giveID()).gameObject;

            Debug.Log("setting the parent to questRewardPanel");
            completedQuest.transform.SetParent(QuestRewardPanel.transform.GetChild(0));

            Debug.Log("adjusting position to questRewardPanel");
            completedQuest.transform.position = completedQuest.transform.parent.position;



            QuestList.Remove(currentQuest);
            for (int i = currentQuest.giveID(); i < QuestList.Count; i++)
            {
                QuestList[i].setID(QuestList[i].giveID() - 1);                
                
            }

           
            

        }
        else {
            currentTempCompleted++;
            questContinue.SetActive(false);
            questNext.transform.GetChild(0).GetComponent<Text>().text = "Next (" + currentTempCompleted + "/" + totalTempCompleted + ")";
            questNext.SetActive(true);

            //Add gold
            player.addGold(currentQuest.goldReward);

            //Add experience
            player.addExperience((int)currentQuest.XPreward);

            GameObject completedQuest = new GameObject();
            Debug.Log("getting child " + currentQuest.giveID() + " of QuestContent");
            completedQuest = QuestListPanel.transform.GetChild(currentQuest.giveID()).gameObject;

            Debug.Log("setting the parent to questRewardPanel");
            completedQuest.transform.SetParent(QuestRewardPanel.transform.GetChild(0));

            Debug.Log("adjusting position to questRewardPanel");
            completedQuest.transform.position = completedQuest.transform.parent.position;

            



            QuestList.Remove(currentQuest);
            for (int i = currentQuest.giveID(); i < QuestList.Count; i++)
            {
                QuestList[i].setID(QuestList[i].giveID() - 1);

            }

        }

    }

    public void nextQuestClick()
    {
        Destroy(QuestRewardPanel.transform.GetChild(0).GetChild(0).gameObject);
        tempCompletedQuests.RemoveAt(0);
        buildQuestReward();

    }


    //Method to assemble quest title from given parameters
    string assembleTitle(string objective)
    {
        


        if(objective == "Kill")
        {
            return killList[Random.Range(0, killList.Count)];

        }
        if (objective == "Collect")
        {
            return collectList[Random.Range(0, collectList.Count)];
        }
        return null;

    }





    //Metodi jolla valitaan enemyType questia varten
    EnemyType pickEnemyType(float difficulty)
    {
        Debug.Log("picking enemy type");
        EnemyType lastType = null;
        float pickedDiff = (Random.Range(minDiff, maxDiff)) * difficulty;
        int easyHard = Random.Range(0, 1);
        foreach (EnemyType type in enemyDatabase.enemyTypeDatabase)
        {
            Debug.Log(type.toString());
            if (type.Difficulty > pickedDiff)
            {
                if (easyHard == 0 && lastType != null)
                {
                    Debug.Log("Returned: " + lastType.toString());
                    return lastType;
                }
                else 
                {
                    Debug.Log("Returned: " + type.toString());
                    return type;
                }

            
            }
            lastType = type;



        }
        return lastType;
    }




    //method to pick quest objective (currently only kill)
    string pickObjective()
    {
        List<string> objectives = new List<string>();
        objectives.Add("Kill");
        //objectives.Add("Collect");

        return objectives[Random.Range(0, objectives.Count)];
    }




    //Method to choose item to collect (not impl)
    string chooseCollect()
    {
        return collectItemList[Random.Range(0, collectItemList.Count)];


    }



    void prepareLists()
    {
        killList = new List<string>();
        killList.Add("Trouble");
        killList.Add("Difficulty");
        killList.Add("Issue");
        killList.Add("Concern");
        killList.Add("Annoyance");
        killList.Add("Adventure");
        killList.Add("Errand");
        killList.Add("Task");
        killList.Add("Duty");

        collectList = new List<string>();
        collectList.Add("Adventure");
        collectList.Add("Errand");
        collectList.Add("Task");
        collectList.Add("Duty");

        collectItemList = new List<string>();
        collectItemList.Add("Insect eyes");
        collectItemList.Add("Mammal guts");
        collectItemList.Add("Wings");
        collectItemList.Add("Slime");
        

    }
}

    





