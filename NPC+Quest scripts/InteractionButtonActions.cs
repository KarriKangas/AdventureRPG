using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class InteractionButtonActions : MonoBehaviour {

    
    private NPCHolder NPCHolder;
    private InteractionManager Interactions;
    private QuestMasterScript QuestMaster;
    private Player player;
    [SerializeField]
    private GameObject shopPanel;



    string actionName;

    void Start()
    {
        
        NPCHolder = GameObject.Find("NPCManager").GetComponent<NPCHolder>();
        Interactions = GameObject.Find("InteractionManager").GetComponent<InteractionManager>();
        QuestMaster = GameObject.Find("QuestScriptHolder").GetComponent<QuestMasterScript>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void OnClick()
    {
        actionName = (transform.GetComponent<Text>().text).Substring(0, 3).ToLower(); 
        
        Debug.Log(actionName);
        switch(actionName)
        {
            case ("que"):
                if(NPCHolder.getCurrentNPC().questAmount > 0)
                {
                    QuestMaster.addQuest(QuestMaster.generateQuest(player.location.getLevel()));
                    Interactions.removeQuest(NPCHolder.getCurrentNPC());
                }
                break;

            case ("sho"):
                shopPanel.SetActive(true);
                break;
               
            
            
        }
    }
    
}
