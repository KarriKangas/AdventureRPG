using UnityEngine;
using System.Collections;

//Scripti joka vahtii ruudulla olevien (EI INTERACTION PANELIN) nappuloiden painalluksia, täällä kaikkea pientä kuten näytä/piilota Inn, Map, Quests
public class ButtonActions : MonoBehaviour {

    [SerializeField]
    private GameObject map;

    [SerializeField]
    private GameObject interaction;

    [SerializeField]
    private GameObject quests;

    [SerializeField]
    private GameObject character;


    //OnClick -metodit kaikille nappuloille erikseen, jos näkyvillä-piilota, jos piilossa-näytä
    public void OnClickInteraction()
    {
        ShowHidePanels("interaction");
        if (interaction.activeSelf == false)
            interaction.SetActive(true);

        else
            interaction.SetActive(false);


    }

    public void OnClickMap()
    {
        ShowHidePanels("map");
        if (map.activeSelf == false)
            map.SetActive(true);

        else
            map.SetActive(false);
        
      
    }

    public void OnClickQuests()
    {
        ShowHidePanels("quests");
        if (quests.activeSelf == false)
            quests.SetActive(true);

        else
            quests.SetActive(false);

    }

    public void OnClickCharacter()
    {
        ShowHidePanels("character");
        if (character.activeSelf == false)
            character.SetActive(true);

        else
            character.SetActive(false);
    }


    //Tämä metodi piilottaa oikeat panelit riippuen painetusta panelista
    //Esim jos kyseessä Inn, piilota map ja quests jne.
    void ShowHidePanels(string place)
    {
        switch (place)
        {
            case ("interaction"):
                if (map.activeSelf == true)
                    map.SetActive(false);

                if (quests.activeSelf == true)
                    quests.SetActive(false);

                if (character.activeSelf == true)
                    character.SetActive(false);


                break;

            case ("map"):
                if (interaction.activeSelf == true)
                    interaction.SetActive(false);

                if (quests.activeSelf == true)
                    quests.SetActive(false);

                if (character.activeSelf == true)
                    character.SetActive(false);

                break;

            case ("quests"):
                if (map.activeSelf == true)
                    map.SetActive(false);

                if (interaction.activeSelf == true)
                    interaction.SetActive(false);

                if (character.activeSelf == true)
                    character.SetActive(false);

                break;

            case ("character"):
                if (map.activeSelf == true)
                    map.SetActive(false);

                if (interaction.activeSelf == true)
                    interaction.SetActive(false);

                if (quests.activeSelf == true)
                    quests.SetActive(false);

                break;
        }
    }
}
