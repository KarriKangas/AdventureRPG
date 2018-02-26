using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FloatingNumbers : MonoBehaviour {

    Color enemyColor, playerColor, goldColor, expColor, lifestealColor, HPregColor, EnergyregColor;
    Vector3 screenMiddle;
    Vector3 playerPosition, enemyPosition, goldPosition, expPosition;
    float speed;
    int textLocChange;
    GameObject floatingText;
    GameObject floatingTextHolder;

    // Use this for initialization
    void Start () {
        floatingText = Resources.Load<GameObject>("Prefabs/FloatingText");
        floatingTextHolder = GameObject.Find("FloatingTexts");

        //set position variables for texts
        setPositions();

        //set color variables for texts
        setColors();
        
        speed = 0.80f;

    }
	
	
	void Update () {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).position = new Vector3(transform.GetChild(i).position.x, transform.GetChild(i).position.y + speed);
            
        }
	
	}

    public void createText(float value, string textCase)
    {
        
        StartCoroutine(wait(value, textCase));

        
    }

    IEnumerator wait(float value, string textCase)
    {
        //set toInstantiate to be a floatingText prefab
        GameObject toInstantiate = floatingText;
        GameObject instance;

        //Set the text to be displayed
        toInstantiate.GetComponent<Text>().text = "" + value;


        //The base for creating a text
        //Different cases represent different places where texts occur, damages, gold, experience...
        switch (textCase)
        {

            case ("player"):
                //Set color accordingly
                toInstantiate.GetComponent<Text>().color = playerColor;

                //Set position accordingly
                toInstantiate.GetComponent<RectTransform>().localPosition = playerPosition;

                //Instantiate           
                instance = Instantiate(toInstantiate);

                //Set scale
                instance.transform.localScale= new Vector3(1, 1, 1);

                //Set parent
                instance.transform.SetParent(floatingTextHolder.transform);

                //Set name
                instance.name = "pDmg";

                //Wait for 1.5s
                yield return new WaitForSeconds(1.5f);

                //Destroy the correct text using the findChild method
                Destroy(transform.GetChild(findChild("pDmg")).gameObject);

                break;


            case ("enemy"):
                toInstantiate.GetComponent<Text>().color = enemyColor;
                toInstantiate.GetComponent<RectTransform>().localPosition = enemyPosition;
                instance = Instantiate(toInstantiate);
                instance.transform.localScale = new Vector3(1, 1, 1);
                instance.transform.SetParent(floatingTextHolder.transform);
                instance.name = "eDmg";

                yield return new WaitForSeconds(1.5f);
                Destroy(transform.GetChild(findChild("eDmg")).gameObject);
                break;


            case ("gold"):
                toInstantiate.GetComponent<Text>().text += "g";
                toInstantiate.GetComponent<Text>().color = goldColor;
                toInstantiate.GetComponent<RectTransform>().localPosition = goldPosition;
                instance = Instantiate(toInstantiate);
                instance.transform.localScale = new Vector3(1, 1, 1);
                instance.transform.SetParent(floatingTextHolder.transform);
                instance.name = "gold";

                yield return new WaitForSeconds(1.5f);
                Destroy(transform.GetChild(findChild("gold")).gameObject);
                break;

            case ("experience"):
                toInstantiate.GetComponent<Text>().text += "xp";
                toInstantiate.GetComponent<Text>().color = expColor;

                toInstantiate.GetComponent<RectTransform>().localPosition = expPosition;
                instance = Instantiate(toInstantiate);
                instance.transform.localScale = new Vector3(1, 1, 1);
                instance.transform.SetParent(floatingTextHolder.transform);
                instance.name = "exp";

                yield return new WaitForSeconds(1.5f);
                Destroy(transform.GetChild(findChild("exp")).gameObject);
                break;

            case ("lifesteal"):
                yield return new WaitForSeconds(0.2f);
                toInstantiate.GetComponent<Text>().text += " stolen";
                toInstantiate.GetComponent<Text>().color = lifestealColor;

                toInstantiate.GetComponent<RectTransform>().localPosition = playerPosition;
   
                instance = Instantiate(toInstantiate);

                instance.transform.localScale = new Vector3(1, 1, 1);

                instance.transform.SetParent(floatingTextHolder.transform);

                instance.name = "lifesteal";

                yield return new WaitForSeconds(1.5f);
               
                Destroy(transform.GetChild(findChild("lifesteal")).gameObject);

                break;

            case ("HPregen"):
                toInstantiate.GetComponent<Text>().text += "hp";
                toInstantiate.GetComponent<Text>().color = HPregColor;

                toInstantiate.GetComponent<RectTransform>().localPosition = playerPosition;

                instance = Instantiate(toInstantiate);

                instance.transform.localScale = new Vector3(1, 1, 1);

                instance.transform.SetParent(floatingTextHolder.transform);

                instance.name = "HPregen";

                yield return new WaitForSeconds(1.5f);

                Destroy(transform.GetChild(findChild("lifesteal")).gameObject);

                break;

            case ("Energyregen"):
                yield return new WaitForSeconds(0.2f);
                toInstantiate.GetComponent<Text>().text += " energy";
                toInstantiate.GetComponent<Text>().color = EnergyregColor;

                toInstantiate.GetComponent<RectTransform>().localPosition = playerPosition;

                instance = Instantiate(toInstantiate);

                instance.transform.localScale = new Vector3(1, 1, 1);

                instance.transform.SetParent(floatingTextHolder.transform);

                instance.name = "HPregen";

                yield return new WaitForSeconds(1.5f);

                Destroy(transform.GetChild(findChild("lifesteal")).gameObject);

                break;
        }


    }

    //color variable set method
    private void setColors()
    {
        enemyColor = new Color(0.7f, 0, 0);
        playerColor = new Color(0, 0, 0.7f);
        lifestealColor = new Color(0.4f ,0, 0.4f);
        HPregColor = new Color(0.6f, 1, 0.2f);
        EnergyregColor = new Color(0.2f, 0.6f, 1);
        goldColor = new Color(0.9f, 0.675f, 0);
        expColor = new Color(0, 0.6f, 0.2f);
    }

    //position variable set method
    private void setPositions()
    {
        //Find screen middle and use it for damage positions
        screenMiddle = new Vector3(((Screen.width) / 2) - 25, (Screen.height) / 2);
        textLocChange = Screen.width / 15;

        playerPosition = new Vector3(screenMiddle.x + textLocChange, screenMiddle.y);
        enemyPosition = new Vector3(screenMiddle.x - textLocChange, screenMiddle.y);

        //Find gold/exp positions based on their GameObjects
        goldPosition = GameObject.Find("GoldText").transform.position;
        goldPosition = new Vector3(goldPosition.x, goldPosition.y);
        expPosition = GameObject.Find("ExperienceBar").transform.position;

    }

    //Method for finding the right floating number depending on it's type
    private int findChild(string type)
    {

        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).name == type)
            {
                return i;
            }
        }
        return 0;
    }
}
