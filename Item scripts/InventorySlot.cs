using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

//Scripti joka liitettynä jokaiseen slottiin
public class InventorySlot : MonoBehaviour, IDropHandler
{
    private Tooltip tooltip;
    private Inventory inventory;
    private Player player;
    private ShopManager shop;
    public int slotID;
    
    private Sprite rarityUncommon;
    private Sprite rarityRare;
    private Sprite rarityEpic;
    private Sprite rarityLegendary;
    private Sprite rarityCommon;
    private Sprite noImage;


    void Update()
    {
        setRarityImage("Slot Panel");
        setRarityImage("EquipmentSlotPanel");
        
        //Mouse2 pressed IF shop window is open AND we right clicked an item in inventory
        if (shop.checkIfSelling() && Input.GetMouseButtonDown(1) && !Input.GetMouseButton(0) && transform.parent.name == "Slot Panel" && inventory.slots[slotID].transform.childCount > 0 && inventory.slots[slotID].transform.GetChild(0).GetComponent<ItemData>().hovering)
        {
            sellItem();
        }
        else if (Input.GetMouseButtonDown(1) && !Input.GetMouseButton(0))
        {
            
            //Metodi jolla katsotaan mouse2 nappulan painalluksia itemien päällä ja equipataan ne
            //Jos parent name = "Slot Panel" ELI item on inventoryssa JA slotin childCount > 0 ELI slotissa on itemi JA hoverin==true eli hiiri on kyseisen itemin päällä

            if (transform.parent.name == "Slot Panel" && inventory.slots[slotID].transform.childCount >0 && inventory.slots[slotID].transform.GetChild(0).GetComponent<ItemData>().hovering)
            {
                ItemData pressedItem = inventory.slots[slotID].transform.GetChild(0).GetComponent<ItemData>();

                if (pressedItem.item.Usable == true)
                {
                    useItem();
                }
                else if (pressedItem.item.Type != null && pressedItem.item.Type.EquipSlot == "1h")
                {
                    equip1h();
                }
                else if (pressedItem.item.Type != null && pressedItem.item.Type.EquipSlot == "Head")
                {
                    equipGear(2);
                }
                else if (pressedItem.item.Type != null && pressedItem.item.Type.EquipSlot == "Torso")
                {
                    equipGear(3);
                }
                else if (pressedItem.item.Type != null && pressedItem.item.Type.EquipSlot == "Hands")
                {
                    equipGear(4);
                }
                else if (pressedItem.item.Type != null && pressedItem.item.Type.EquipSlot == "Legs")
                {
                    equipGear(5);
                }
                else if (pressedItem.item.Type != null && pressedItem.item.Type.EquipSlot == "Feet")
                {
                    equipGear(6);
                }
            }


            //Jos parent name = "EquipmentSlotPanel" ELI item on equipattuna JA slotin childCount > 1 ELI slotissa on itemi JA hoverin==true eli hiiri on kyseisen itemin päällä
            else if (transform.parent.name == "EquipmentSlotPanel" && inventory.equipmentSlots[slotID - inventory.slotAmount].transform.childCount > 0 && inventory.equipmentSlots[slotID - inventory.slotAmount].transform.GetChild(0).GetComponent<ItemData>().hovering)
            {
                unEquipItem();
            }

            player.updateGearStats();

        }

        
    }


    void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        tooltip = inventory.GetComponent<Tooltip>();
        player = GameObject.Find("Player").GetComponent<Player>();
        shop = inventory.shop;
        noImage = Resources.Load<Sprite>("Sprites/noImage");
        rarityUncommon = Resources.Load<Sprite>("Sprites/Items/SlotColor/Uncommon");
        rarityRare = Resources.Load<Sprite>("Sprites/Items/SlotColor/Rare");
        rarityEpic = Resources.Load<Sprite>("Sprites/Items/SlotColor/Epic");
        rarityLegendary = Resources.Load<Sprite>("Sprites/Items/SlotColor/Legendary");
        rarityCommon = Resources.Load<Sprite>("Sprites/Items/SlotColor/Common");


    }

    //OnDrop metodi - kun esineen tiputtaa OnDragista
    public void OnDrop(PointerEventData eventData)
    {
        //Otetaan dragatun itemin item data eventDatasta
        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();

        if (droppedItem.dragged)
        {

            Debug.Log("this fired");

            if(slotID == -3)
            {
                Debug.Log("this fired");
            }

            //Jos kyseessä on Invetoryssa oleva slot (eli ei esim equipment)
            if (transform.tag == "Inventory")
            {
                OnDropInventory(droppedItem);
                player.updateGearStats();
            }

            if (transform.tag == "Equipment")
            {

                OnDropEquipment(droppedItem);
                player.updateGearStats();
            }

            
        }
        droppedItem.dragged = false;

        
    }


    //metodi jota kutsutaan kun item pudotetaan inventoryslottiin
    private void OnDropInventory(ItemData droppedItem)
    {
        int equipItemSlot = slotID - inventory.slotAmount;
        int equipGameObjectSlot = droppedItem.slotNum - inventory.slotAmount;

        //Jos itemi slotissa = -1 
        if (inventory.items[slotID].ID == (-1).ToString())
        {

            //Tyhjennä slot missä item oli
            if (droppedItem.transform.parent.name == "Inventory Item")
            {
                inventory.items[droppedItem.slotNum] = new Item();
            }else if(droppedItem.transform.parent.name == "Equipment Item")
            {
                inventory.equippedItems[droppedItem.slotNum - inventory.slotAmount] = new Item();
            }
            //Ja lisää item slottiin johon se dropataan (eli tähän slottiin)
            inventory.items[slotID] = droppedItem.item;

            //Ja vaihda itemin slotNum oikeaksi
            droppedItem.slotNum = slotID;
        }
        //Jos item pudotetaan johonkin muuhun slottiin
        else if (droppedItem.slotNum != slotID)
        {                
            //Otetaan TÄMÄN slotin 0:s lapsi eli item (eli se jonka päälle pudotetaan)
            Transform item = this.transform.GetChild(0);
            //Jos item on lähtöisin inventorysta
            if (droppedItem.transform.parent.name == "Inventory Item")
            {
                
                
                //Otetaan sen itemin slotNum ja vaihdetaan se dropatun itemin slotNumiksi
                item.GetComponent<ItemData>().slotNum = droppedItem.slotNum;

                //Ja vaihdetaan parent
                item.transform.SetParent(inventory.slots[droppedItem.slotNum].transform);

                //Ja position
                item.transform.position = inventory.slots[droppedItem.slotNum].transform.position;

                //Ja päivitetään backendi
                inventory.items[droppedItem.slotNum] = item.GetComponent<ItemData>().item;
                inventory.items[slotID] = droppedItem.item;

                //Sitten tehdään kaikki sama dropattavalle itemille
                droppedItem.slotNum = slotID;
                droppedItem.transform.SetParent(this.transform);
                droppedItem.transform.position = this.transform.position;
            }
            //Jos item on lähtöisin equip slotista
            else if (droppedItem.transform.parent.name == "Equipment Item")
            {
                if (inventory.items[slotID].Type != null &&inventory.items[slotID].Type.EquipSlot == droppedItem.item.Type.EquipSlot)
                {
                    inventory.equippedItems[equipGameObjectSlot] = new Item();

                    //Ja vaihdetaan parent
                    item.transform.SetParent(inventory.equipmentSlots[equipGameObjectSlot].transform);

                    //Ja position
                    item.transform.position = inventory.equipmentSlots[equipGameObjectSlot].transform.position;

                    //päivitetään vielä inventory backend
                    item.GetComponent<ItemData>().slotNum = droppedItem.slotNum;

                    inventory.equippedItems[equipGameObjectSlot] = item.GetComponent<ItemData>().item;

                    inventory.items[slotID] = droppedItem.item;

                    //Sitten tehdään kaikki sama dropattavalle itemille
                    droppedItem.slotNum = slotID;
                    droppedItem.transform.SetParent(this.transform);
                    droppedItem.transform.position = this.transform.position;

                    Debug.Log("Dropped item is now in slot" + droppedItem.slotNum);
                    Debug.Log("New item is now in slot " + slotID);
                }
            }

        }
    }

    //metodi jota kutsutaan kun item pudotetaan equipment slottiin
    private void OnDropEquipment(ItemData droppedItem)
    {
        int equipItemSlot = slotID - inventory.slotAmount;
        int equipGameObjectSlot = droppedItem.slotNum - inventory.slotAmount;

        Debug.Log("id is " +inventory.equippedItems[equipItemSlot].ID);
        if (droppedItem.item.Type != null)
        {
            //Jos itemi slotissa = -1    (eli ei ole itemiä)    
            if (inventory.equippedItems[equipItemSlot].ID == (-1).ToString() && checkIfItemFits(droppedItem))
            {
                
                //Tyhjennä slot missä item oli
                //Jos Item on lähtöisin inventorysta ( eli parent on slot panel ) 

                if (droppedItem.transform.parent.name == "Inventory Item")
                {
                    inventory.items[droppedItem.slotNum] = new Item();
                }
                //Jos taas Item lähtöisin Equipmentista ( eli parent on EquipmentSlotPanel )
                else if (droppedItem.transform.parent.name == "Equipment Item")
                {
                    inventory.equippedItems[equipGameObjectSlot] = new Item();
                }

                //Ja lisää item slottiin johon se dropataan (eli tähän slottiin)
                inventory.equippedItems[equipItemSlot] = droppedItem.item;

                //Ja vaihda itemin slotNum oikeaksi
                droppedItem.slotNum = slotID;

            }
            //Jos item pudotetaan johonkin muuhun slottiin
            else if (droppedItem.slotNum != slotID  && checkIfItemFits(droppedItem))
            {

                //Otetaan TÄMÄN slotin 1:nen lapsi eli item (eli se jonka päälle pudotetaan) 1 Koska equipmenteissa Imaget
                Transform item = this.transform.GetChild(0);

                Debug.Log(item.GetComponent<ItemData>().item.Type.EquipSlot);
                Debug.Log(droppedItem.item.Type.EquipSlot);
                //Jos itemien tyyppi on sama (eli käytännössä tämä ei tee muuta kuin sallii 1h itemien paikan vaihdon keskenään, miksi? jaa-a, pakko olla)
                if (item.GetComponent<ItemData>().item.Type.EquipSlot == droppedItem.item.Type.EquipSlot)
                {
                    //Otetaan sen itemin slotNum ja vaihdetaan se dropatun itemin slotNumiksi
                    item.GetComponent<ItemData>().slotNum = droppedItem.slotNum;

                    //Jos pudotetava Item on lähtöisin inventorysta
                    if (droppedItem.transform.parent.name == "Inventory Item")
                    {
                        inventory.items[droppedItem.slotNum] = new Item();

                        //Otetaan sen itemin slotNum ja vaihdetaan se dropatun itemin slotNumiksi
                        item.GetComponent<ItemData>().slotNum = droppedItem.slotNum;

                        //Ja vaihdetaan parent
                        item.transform.SetParent(inventory.slots[droppedItem.slotNum].transform);

                        //Ja position
                        item.transform.position = inventory.slots[droppedItem.slotNum].transform.position;

                        //Ja päivitetään backendi
                        inventory.items[droppedItem.slotNum] = item.GetComponent<ItemData>().item;
                        inventory.equippedItems[equipItemSlot] = droppedItem.item;

                        //Sitten tehdään kaikki sama dropattavalle itemille
                        droppedItem.slotNum = slotID;
                        droppedItem.transform.SetParent(this.transform);
                        droppedItem.transform.position = this.transform.position;

                    }
                    //Jos taas Item lähtöisin Equipmentista ( eli parent on EquipmentSlotPanel )
                    else if (droppedItem.transform.parent.name == "Equipment Item")
                    {
                        inventory.equippedItems[equipGameObjectSlot] = new Item();
                        

                        
                        //Ja vaihdetaan parent
                        item.transform.SetParent(inventory.equipmentSlots[equipGameObjectSlot].transform);

                        //Ja position
                        item.transform.position = inventory.equipmentSlots[equipGameObjectSlot].transform.position;

                        //päivitetään vielä inventory backend
                        
                        inventory.equippedItems[equipGameObjectSlot] = item.GetComponent<ItemData>().item;
                        inventory.equippedItems[equipItemSlot] = droppedItem.item;

                        //Sitten tehdään kaikki sama dropattavalle itemille
                        droppedItem.slotNum = slotID;
                        droppedItem.transform.SetParent(this.transform);
                        droppedItem.transform.position = this.transform.position;
                    }
                    

                    
                }

            }
        }
    }

    private bool checkIfItemFits(ItemData droppedItem)
    {
      string equipSlot = droppedItem.item.Type.EquipSlot;

    //Jos kyseinen slotti on 15 tai 16, tarkista on dropped item ase!
      if (slotID == 15 || slotID == 16)
        {
            if (equipSlot == "1h") return true;
        }
      //Jos kyseinen slotti on 17, tarkista onko item kypärä!
       else if (slotID == 17)
        {
            if (equipSlot == "Head") return true;
        }
        //Jos kyseinen slotti on 18, tarkista onko item torso!
        else if (slotID == 18)
        {
            if (equipSlot == "Torso") return true;
        }
        //Jos kyseinen slotti on 19, tarkista onko item hanskat!
        else if (slotID == 19)
        {
            if (equipSlot == "Hands") return true;
        }
        //Jos kyseinen slotti on 20, tarkista onko item housut!
        else if (slotID == 20)
        {
            if (equipSlot == "Legs") return true;
        }
        //Jos kyseinen slotti on 21, tarkista onko item kengätä!
        else if (slotID == 21)
        {
            if (equipSlot == "Feet") return true;
        }
        return false;

    }

    //Metodi jota käytetään 1h aseiden equippakuseen
    private void equip1h()
    {

        //Equip item = equipattava itemi
        ItemData equipItem = inventory.slots[slotID].transform.GetChild(0).GetComponent<ItemData>();

        //equipTransform on equipattavan itemin transform
        Transform equipTransform = inventory.slots[slotID].transform.GetChild(0);

        equipItem.hovering = false;
        
        //Jos equippedItems[0], eli Right hand slotti on tyhjä, laita itemi sinne
        if(inventory.equippedItems[0].ID == "-1")
        {
            equipItemInSlot(0, equipItem, equipTransform);

            
            //Ja tyhjennetään backend viittaus inventorysta
            inventory.items[slotID] = new Item();
        }
        
        
        //Jos taas equippedItems[1], eli left hand slotti on tyhjä, laita itemi sinne
        else if(inventory.equippedItems[1].ID == "-1")
        {
            equipItemInSlot(1, equipItem, equipTransform);
            
            
            
            //Ja tyhjennetään backend viittaus inventorysta
            inventory.items[slotID] = new Item();
        }
       
        
        
        //Jos molemmat slotit ovat täynnä, laita item slottiin Right hand [0]
        //Ja laita righthandissa ollut item inventoryyn slottiin josta item otettiin pois
        else
        {
            swapItems(0, equipItem, equipTransform);
        }

    }

    //Metodi jolla equipataan kaikki muu gear
    private void equipGear(int slot)
    {
        //Equip item = equipattava itemi
        ItemData equipItem = inventory.slots[slotID].transform.GetChild(0).GetComponent<ItemData>();
        
        //equipTransform on equipattavan itemin transform
        Transform equipTransform = inventory.slots[slotID].transform.GetChild(0);
        
        equipItem.hovering = false;

        //Jos equippedItems[0], eli Right hand slotti on tyhjä, laita itemi sinne
        if (inventory.equippedItems[slot].ID == "-1")
        {
            equipItemInSlot(slot, equipItem, equipTransform);

            //Ja tyhjennetään backend viittaus inventorysta
            inventory.items[slotID] = new Item();
        }
        //Jos gearin equip slotissa on jotain
        //Swappaa itemit keskenään
        else
        {
            swapItems(slot, equipItem, equipTransform);
        }
    }

    //Metodi jolla equipataan ase inventorysta tiettyyn slottiin
    private void equipItemInSlot(int slot, ItemData equipItem, Transform equipTransform)
    {

        //Poistetaan tooltip näkyvistä, sillä item muutta tässä sijaintia
        tooltip.Deactivate();


        //Ensin muutetaan equipItemin slot numero
        equipItem.slotNum = inventory.equipmentSlots[slot].GetComponent<InventorySlot>().slotID;


        //Sitten muutetaan inventoryn backend 
        inventory.equippedItems[slot] = equipItem.item;


        //Muutetaan parentti equipmenttiin
        equipTransform.SetParent(inventory.equipmentSlots[slot].transform);


        //Ja muutetaan position myös sinne
        equipTransform.position = inventory.equipmentSlots[slot].transform.position;
    }

    private void swapItems(int slot, ItemData equipItem, Transform equipTransform)
    {
        //itemToSwap = equip slotissa jo oleva swapatta itemi joka laitetaan inventoryy sille paikalle mistä equippatava itemi lähtee
        ItemData itemToSwap = inventory.equipmentSlots[slot].transform.GetChild(0).GetComponent<ItemData>();

        //transformToSwap on swapattavan itemin transformi
        Transform transformToSwap = inventory.equipmentSlots[slot].transform.GetChild(0);

        //vaihdetaan swapattavan itemin slotNum backendissa
        itemToSwap.slotNum = equipItem.slotNum;

        //Ja vaihdetaan parent ja position inventoryyn
        transformToSwap.SetParent(equipTransform.parent);
        transformToSwap.position = equipTransform.parent.position;

        //Kutsutaan equipItemInSlot metodia 
        equipItemInSlot(slot, equipItem, equipTransform);

        inventory.items[slotID] = itemToSwap.item;

        
    }

    private void unEquipItem()
    {
        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (inventory.items[i].ID == "-1")
            {
               
                //Fetch itemdata for the item we are unequipping
                ItemData unEquipItem = inventory.equipmentSlots[slotID - inventory.slotAmount].transform.GetChild(0).GetComponent<ItemData>();
                
                //And the transform for that item
                Transform unEquipTransform = inventory.equipmentSlots[slotID - inventory.slotAmount].transform.GetChild(0).transform;
              
                //Set slotNum in itemdata for item we are unequipping
                unEquipItem.slotNum = i;

                //set backend inventory items correctly
                inventory.items[i] = unEquipItem.item;
                Debug.Log("fired");
                //set transform parent and position
                unEquipTransform.SetParent(inventory.slots[i].transform);
                Debug.Log("fired");
                unEquipTransform.position = inventory.slots[i].transform.position;
                Debug.Log("fired");
                //And correct equippedItems backend
                inventory.equippedItems[slotID - inventory.slotAmount] = new Item();

                break;
            }
        }
    }

    private void useItem()
    {
        ItemData itemBeingUsed = inventory.slots[slotID].transform.GetChild(0).GetComponent<ItemData>();
        string itemTitle = itemBeingUsed.item.Title.ToLower();
        if (itemTitle.Contains("potion"))
        {
            if (itemTitle.Contains("health") && player.getCurrentHealth() != player.getMaxHealth())
            {
                Debug.Log("Player current health before potion " + player.getCurrentHealth());
                player.setCurrentHealth(itemBeingUsed.item.BuffAmount);
                Debug.Log("player healed for " + itemBeingUsed.item.BuffAmount);
                Debug.Log("Player current health after potion " + player.getCurrentHealth());
                if (itemBeingUsed.amount == 1)
                {
                    
                    inventory.items[slotID] = new Item();
                    Destroy(inventory.slots[slotID].transform.GetChild(0).gameObject);
                    tooltip.Deactivate();
                }
                else
                {
                    itemBeingUsed.amount--;
                    itemBeingUsed.transform.GetChild(1).GetComponent<Text>().text = ""+ itemBeingUsed.amount;
                }
            }
        }
    }

    private void sellItem()
    {
        ItemData itemBeingSold = inventory.slots[slotID].transform.GetChild(0).GetComponent<ItemData>();
        if(itemBeingSold.amount == 1)
        {
            inventory.items[slotID] = new Item();
            player.addGold(itemBeingSold.item.price);
            Destroy(inventory.slots[slotID].transform.GetChild(0).gameObject);
            tooltip.Deactivate();
        }
        else
        {
            itemBeingSold.amount--;
            itemBeingSold.transform.GetChild(1).GetComponent<Text>().text = "" + itemBeingSold.amount;
            player.addGold(itemBeingSold.item.price);
        }

    }

    //Set the rarity image for a slot based on the item in the slot
    private void setRarityImage(string panel)
    {

        if (transform.parent.name == panel && transform.childCount > 0)
        {
            if (transform.GetChild(0).GetComponent<ItemData>().item.Rarity == "Uncommon")
            {
                transform.GetChild(0).GetComponent<Image>().sprite = rarityUncommon;
            }
            else if (transform.GetChild(0).GetComponent<ItemData>().item.Rarity == "Rare")
            {
                transform.GetChild(0).GetComponent<Image>().sprite = rarityRare;
            }
            else if (transform.GetChild(0).GetComponent<ItemData>().item.Rarity == "Epic")
            {
                transform.GetChild(0).GetComponent<Image>().sprite = rarityEpic;
            }
            else if (transform.GetChild(0).GetComponent<ItemData>().item.Rarity == "Legendary")
            {
                transform.GetChild(0).GetComponent<Image>().sprite = rarityLegendary;
            }
            else
            {
                transform.GetChild(0).GetComponent<Image>().sprite = rarityCommon;
            }
        }

    }


}
