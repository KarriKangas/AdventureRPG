using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Tooltip : MonoBehaviour {

    private Item item;
    private string descriptionName;
    private string descriptionAmount;
    private string title;
    private GameObject tooltip;

    void Start()
    {

        tooltip = GameObject.Find("Tooltip");
        tooltip.SetActive(false);
    }

    //If tooltip is active, update it's position to mousePosition
    void Update()
    {
        
        if (tooltip.activeSelf == true && Input.mousePosition.x >Screen.width/2)
        {
            tooltip.transform.position = Input.mousePosition;
        }
        else if (tooltip.activeSelf == true) {
            //Scale and mouse cursor, have to add 1.2 multiplier
            tooltip.transform.position = Input.mousePosition - new Vector3((tooltip.GetComponent<RectTransform>().rect.x)*1.2f, 0,0);
            
        }
    }

    //Tooltip activate method which is called when mouse is hovered over an item
	public void Activate(Item item)
    {
        this.item = item;
        constructDataString();
        tooltip.SetActive(true);
    }

    //And deactivate method called when mouse is hovered off an item
    public void Deactivate()
    {
        tooltip.SetActive(false);
    }

    //Tooltip construction method, constructs item title+description based on item and color based on it's rarity
    public void constructDataString()
    {
        Color color = UnityEngine.Color.white;
        if(item.Rarity == "Common")
        {
            //color = new Color(0.5f, 0.5f, 0.5f); 
        }else if (item.Rarity == "Uncommon")
        {
            color = new Color(0, 0.6f, 0);
        }
        else if (item.Rarity == "Rare")
        {
            color = new Color(0, 0.3f, 0.7f);
        }
        else if (item.Rarity == "Epic")
        {
            color = new Color(0.6f, 0, 0.6f);
        }
        else if (item.Rarity == "Legendary")
        {
            color = new Color(1f,  0.6f, 0);
        }

        title = "<b>" + item.Title + "</b>";
        descriptionName = "" + item.DescriptionName;
        descriptionAmount = "" + item.DescriptionAmount;
        tooltip.transform.GetChild(0).GetComponent<Text>().color = color;
        tooltip.transform.GetChild(0).GetComponent<Text>().text = title;
        tooltip.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text = descriptionName;
        tooltip.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text = descriptionAmount;
    }

}
