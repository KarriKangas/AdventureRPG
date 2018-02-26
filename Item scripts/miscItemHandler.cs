using UnityEngine;
using System.Collections;
using System;
public class miscItemHandler : MonoBehaviour {

    private itemInformationDatabase database;

    // Use this for initialization
    void Start () {
        database = GameObject.Find("Inventory").GetComponent<itemInformationDatabase>();
    }
	

    public Item generateUsable(int rarity)
    {
        Item tempItem = new Item();
        //int random = UnityEngine.Random.Range(-5, 10);
        //rarity += random;

        tempItem = database.usableDatabase[0];
        for (int i = 0; i < database.usableDatabase.Count; i++)
        {
            
            if(rarity >= database.usableDatabase[i].Value)
            {
                
                tempItem = database.usableDatabase[i];
            }
        }
        
        return tempItem;



    }


    Item findItemByID(int id)
    {
        for(int i = 0; i < database.usableDatabase.Count; i++)
        {
            if(Int32.Parse(database.usableDatabase[i].ID) == id)
            {
                return database.usableDatabase[i];
            }
        }
        return new Item();
    }
}
