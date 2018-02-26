using UnityEngine;
using System.Collections;

public class BlockerScript : MonoBehaviour {

    [SerializeField]
    private GameObject AdventuringBlock;

    [SerializeField]
    private GameObject AttackBlock;


    [SerializeField]
    private GameObject ActionPanelBlock;

    [SerializeField]
    private GameObject BehindBlock;

    [SerializeField]
    private GameObject Shop;

    [SerializeField]
    private GameObject Interaction;

    [SerializeField]
    private GameObject Map;

    [SerializeField]
    private GameObject Quests;


    public void activeBlock(string blockname, bool active)
    {
        switch (blockname)
        {
            case ("Adventuring"):
                AdventuringBlock.SetActive(active);
                break;

            case ("Actions"):
                ActionPanelBlock.SetActive(active);
                break;

            case ("Attack"):
                AttackBlock.SetActive(active);
                break;

            case ("Behind"):
                BehindBlock.SetActive(active);
                break;




        }

    }

    public void activePanel(string panelname, bool active)
    {
        switch (panelname)
        {
            case ("Shop"):
                Shop.SetActive(active);
                break;

            case ("Interaction"):
                Interaction.SetActive(active);
                break;

            case ("Map"):
                Map.SetActive(active);
                break;

            case ("Quests"):
                Quests.SetActive(active);
                break;






        }
    }
}
