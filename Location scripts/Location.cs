using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

//Script for each location instance
public class Location: MonoBehaviour{

    [SerializeField]
    private string name;

    [SerializeField]
    private string tooltip;

    [SerializeField]
    private GameObject tooltipObject;

    [SerializeField]
    private int Level;

    [SerializeField]
    private string interactionName;

    public int id { get; set; }


    public bool available { get; set; }

    public string getTooltip()
    {
        return tooltip;
    }

    public string getName()
    {
        return name;
    }

    public int getLevel()
    {
        return Level;
    }

    public string getInteraction()
    {
        return interactionName;
    }



}
