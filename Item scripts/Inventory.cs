using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    [SerializeField]
    GameObject equipmentSlotPanel;

    [SerializeField]
    public ShopManager shop;

    GameObject inventoryPanel;
    GameObject slotPanel;
    GameObject inventorySlot;
    GameObject inventoryItem;
    itemInformationDatabase database;
    gearItemHandler weaponItemHandler;
    miscItemHandler miscItemHandler;

    //Max amount of player inv slots
    public int slotAmount;

    //list of items in player inv
    public List<Item> items = new List<Item>();

    public List<Item> equippedItems = new List<Item>();

    //list of slots in player inv
    public List<GameObject> slots = new List<GameObject>();

    public List<GameObject> equipmentSlots = new List<GameObject>();

    [SerializeField]
    Sprite missingImage;

    void Start()
    {
        
        database = GetComponent<itemInformationDatabase>();

        slotAmount = 15;
        inventoryPanel = GameObject.Find("Inventory");
        slotPanel = inventoryPanel.transform.Find("Slot Panel").gameObject;
        weaponItemHandler = GameObject.Find("ItemScriptHolder").GetComponent<gearItemHandler>();
        miscItemHandler = GameObject.Find("ItemScriptHolder").GetComponent<miscItemHandler>();
        inventorySlot = Resources.Load<GameObject>("Prefabs/Slot");
        inventoryItem = Resources.Load<GameObject>("Prefabs/Item");


        buildInvSlots();
        buildEquipmentSlots();


        //AddItem(new Item(database.weaponTypeDatabase[0]));

        AddItemLevel(2, 2, "weapon");
        AddItemLevel(2, 2, "weapon");
        AddItemLevel(2, 2, "weapon");
        AddItemLevel(2, 2, "weapon");
        /*AddItemLevel(4, 1, "weapon");
        AddItemLevel(7, 1, "weapon");
        AddItemLevel(10, 1, "weapon");
        AddItemLevel(13, 1, "weapon");
        AddItemLevel(16, 1, "weapon");
        AddItem(new Item(new gearType("Test Shirt", "Torso", 1, 1, 1, 1, 1, 65)));
        AddItem(new Item(new gearType("Test Helmet", "Head", 1, 1, 1, 1, 1, 65)));
        AddItem(new Item(new gearType("Test Gloves", "Hands", 1, 1, 1, 1, 1, 65)));
        AddItem(new Item(new gearType("Test Pants", "Legs", 1, 1, 1, 1, 1, 65)));
        AddItem(new Item(new gearType("Test Boots", "Feet", 1, 1, 1, 1, 1, 65)));*/





        //AddItemLevel(0,0, "usable");
        // AddItemLevel(0,0, "usable");
    }

    //Metodi jolla lisätään inventoryyn esine
    public void AddItemLevel(int value, int rarity, string type)
    {
        Item itemToAdd = new Item();
        //Luodaan esine valuella
        if (type == "weapon")
        {
            itemToAdd = weaponItemHandler.generateBaseWeapon(value,rarity,1, 1);
        }else if(type == "usable")
        {
           itemToAdd = miscItemHandler.generateUsable(value);
        }

        //Tarkistetaan onko inventoryssa tilaa
        int slotToAdd = checkForSlot();

        //if lauseke jolla käsitellään itemien stackaus!
        //Jos item == stackable ja jos inventoryssa on jo sellainen
        if (itemToAdd.Stackable && checkInvForItem(itemToAdd))
        {
            for (int i = 0; i < items.Count; i++) {
                if (items[i].ID == itemToAdd.ID)
                {
                    //Otetaan kyseisen itemin itemdata
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();

                    //Ja lisätään siihen yksi
                    data.amount++;

                    //Ja päivitetään teksti
                    if(data.amount > 1)
                        data.transform.GetChild(1).GetComponent<Text>().text = "" + data.amount;
                    else
                        data.transform.GetChild(1).GetComponent<Text>().text = "";



                    Debug.Log("Stackable item added!");
                    break;
                }
            }
        }

        //Jos item ei stackkaa tai niitä ei vielä ole, lisätään ensimmäiseen tyhjään slottiin!
        else if(slotToAdd != -1)
        {
            //Lisätään item siihen slottiin
            items[slotToAdd] = itemToAdd;

            //Ja luodaan sille UI elementti (eli kuva)
            GameObject itemObj = Instantiate(inventoryItem);

            //Ja asetetaan position oikein
            itemObj.transform.position = Vector2.zero;

            //Ja asetetaan sen parent oikein
            itemObj.transform.SetParent(slots[slotToAdd].transform);

            //Otetaan luodusta prefabista sen data ja asetetaan DATASSA item = lisätty item ja määrä = 1
            ItemData data = slots[slotToAdd].transform.GetChild(0).GetComponent<ItemData>();
            data.item = itemToAdd;
            data.amount = 1;
            data.slotNum = slotToAdd;

            //Ja asetetaan oikea nimi
            itemObj.name = itemToAdd.Title;




            //Ja asetetaan oikea sprite
            if (itemToAdd.Sprite != null) itemObj.transform.Find("Image").GetComponent<Image>().sprite = itemToAdd.Sprite;
            else itemObj.transform.Find("Image").GetComponent<Image>().sprite = missingImage;

            //Ja vielä varmistetaan scale
            itemObj.transform.localScale = new Vector3(1, 1, 1);
        }



       
    }

    public void AddItem(Item item)
    {

        Item itemToAdd = item;
        //Tarkistetaan onko inventoryssa tilaa
        int slotToAdd = checkForSlot();

        //if lauseke jolla käsitellään itemien stackaus!
        //Jos item == stackable ja jos inventoryssa on jo sellainen
        if (itemToAdd.Stackable && checkInvForItem(itemToAdd))
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == itemToAdd.ID)
                {
                    //Otetaan kyseisen itemin itemdata
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();

                    //Ja lisätään siihen yksi
                    data.amount++;

                    //Ja päivitetään teksti
                    data.transform.GetChild(1).GetComponent<Text>().text = "" + data.amount;

                    Debug.Log("Stackable item added!");
                    break;
                }
            }
        }

        //Jos item ei stackkaa tai niitä ei vielä ole, lisätään ensimmäiseen tyhjään slottiin!
        else if (slotToAdd != -1)
        {
            //Lisätään item siihen slottiin
            items[slotToAdd] = itemToAdd;

            //Ja luodaan sille UI elementti (eli kuva)
            GameObject itemObj = Instantiate(inventoryItem);

            //Ja asetetaan sen parent oikein
            itemObj.transform.SetParent(slots[slotToAdd].transform);

            //Ja asetetaan position oikein
            itemObj.transform.position = (slots[slotToAdd].transform.position);

            //Otetaan luodusta prefabista sen data ja asetetaan DATASSA item = lisätty item ja määrä = 1
            ItemData data = slots[slotToAdd].transform.GetChild(0).GetComponent<ItemData>();
            data.item = itemToAdd;
            data.amount = 1;
            data.slotNum = slotToAdd;

            //Ja asetetaan oikea nimi
            itemObj.name = itemToAdd.Title;




            //Ja asetetaan oikea sprite
            if (itemToAdd.Sprite != null) itemObj.transform.Find("Image").GetComponent<Image>().sprite = itemToAdd.Sprite;
            else itemObj.transform.Find("Image").GetComponent<Image>().sprite = missingImage;

            //Ja vielä varmistetaan scale
            itemObj.transform.localScale = new Vector3(1, 1, 1);
        }
    }




    //Check inventory for free slot and return the first available one
    private int checkForSlot()
    {
        for(int i = 0; i < items.Count; i++)
        {
            if(Int32.Parse(items[i].ID) == -1)
            {
                return i;
            }
        }
        return -1;

    }

    //Methodi jolla tarkistetaan onko invetoryssa tietty itemi
    bool checkInvForItem(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ID == item.ID)
            {
                return true;
            }
        }
        return false;
    }

    //Metodi jolla rakennetaan inventoryn slotit
    private void buildInvSlots()
    {
        
        for (int i = 0; i < slotAmount; i++)
        {
            //Lisätään items- listaan tyhjä Item jokaiselle slotille
            items.Add(new Item());

            //Instantioidaan slot prefab jokaiselle slotille
            slots.Add(Instantiate(inventorySlot));

            //Asetetaan oikea parent ja varmistetaan että scale on kunnossa
            slots[i].transform.SetParent(slotPanel.transform);
            slots[i].transform.localScale = new Vector3(1, 1, 1);

            slots[i].name = "Slot " + i;

            slots[i].GetComponent<InventorySlot>().slotID = i;
        }
    }

    private void buildEquipmentSlots()
    {
        for(int i = 0; i < equipmentSlotPanel.transform.childCount; i++)
        {
            equippedItems.Add(new Item());

            equipmentSlots.Add(equipmentSlotPanel.transform.GetChild(i).gameObject);

            equipmentSlots[i].name = "Equipment slot " + i;

            equipmentSlots[i].GetComponent<InventorySlot>().slotID = i + slotAmount;

            
        }
    }

    public int hasFreeSlot()
    {
        for(int i = 0; i < items.Count; i++)
        {
            if(items[i].ID == "-1")
            {
                return i;
            }
        }
        return -1;
    }
}
