using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ShopManager : MonoBehaviour {

    [SerializeField]
    private GameObject shopItemPrefab;

    [SerializeField]
    private GameObject shopContent;

    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    private NPCHolder NPCmanager;

    [SerializeField]
    private Text shopTimer;
    private int secondsLeft;
    private int minutesLeft;

    private NonPlayerCharacter currentShopNPC;

    private GameObject Title;
    private Sprite missingImage;
    private Sprite rarityCommon;
    private Sprite rarityUncommon;
    private Sprite rarityRare;
    private Sprite rarityEpic;
    private Sprite rarityLegendary;
    private Sprite noImage;

    private bool isSelling;

    public void Initialize()
    {
        missingImage = Resources.Load<Sprite>("Sprites/missItem");
        noImage = Resources.Load<Sprite>("Sprites/noImage");
        rarityCommon = Resources.Load<Sprite>("Sprites/Items/SlotColor/Common");
        rarityUncommon = Resources.Load<Sprite>("Sprites/Items/SlotColor/Uncommon");
        rarityRare = Resources.Load<Sprite>("Sprites/Items/SlotColor/Rare");
        rarityEpic = Resources.Load<Sprite>("Sprites/Items/SlotColor/Epic");
        rarityLegendary = Resources.Load<Sprite>("Sprites/Items/SlotColor/Legendary");
        
        currentShopNPC = NPCmanager.getCurrentNPC();
        
    }

    void Update()
    {
        if(currentShopNPC != NPCmanager.getCurrentNPC())
        {
            currentShopNPC = NPCmanager.getCurrentNPC();
            updateShopItems();
            Debug.Log("NPC Updated");
        }

        setTime();

    }

    private void setTime()
    {
        secondsLeft = (int)NPCmanager.getTimer();
        if (secondsLeft >= 60)
        {
            minutesLeft = secondsLeft / 60;
            if(secondsLeft - (minutesLeft * 60) > 9)
                shopTimer.text = minutesLeft + ":" +(secondsLeft -(minutesLeft*60));
            else
                shopTimer.text = minutesLeft + ":0" + (secondsLeft - (minutesLeft * 60));
        }
        else
        {
            if(secondsLeft > 9)
                shopTimer.text =   "0:" + (secondsLeft);
            else
                shopTimer.text = "0:0" + (secondsLeft);
        }
    }


    //What is needed:
    //Current NPC, with a LEVEL of items he spawns
    //-- has a list of items he sells (+buyback list)
    //Method to add an item to shopContent (similar to loot) FROM NPCs item list
    //OnMouse2 click (done in inventorySlot script)
    //-- if gold > item.price -> buy
    //--- add item to inventory, remove item from shop
    //OnDrop (done in inventorySlot script)
    //-- remove item from inventory, add item to currentNPC buyback list!
    //-- add gold (item price)

    //Method to clearAll items

    //DynamicShopInventory 
    //--Every 3 battles, resuffle inventory
    //--- clearAll items
    //--- Choose an amount of items 5-10, create these items with NPC level
    //--- Add them to item list

    //On town switch, switch current NPC



    public void AddItemToShop(Item item)
    {

        //Instantioidaan uusi item prefab
        GameObject shopItemObj = Instantiate(shopItemPrefab);


        //Ja asetetaan parent oikein
        shopItemObj.transform.SetParent(shopContent.transform);

        //Asetetaan sen position vastaamaan parentin keskusta
        shopItemObj.transform.position = shopItemObj.transform.parent.position;

        //Ja scale kuntoon
        shopItemObj.transform.localScale = new Vector3(1, 1, 1);

        //Otetaan itemData
        ItemData Data = shopItemObj.transform.Find("Item").GetComponent<ItemData>();

        //Ja datan.item oikein
        Data.item = item;

        //Itemin slot num "-3" kun item on kaupassa!
        Data.slotNum = -3;


        //Ja objectin nimi oikein editorissa
        shopItemObj.name = item.Title;


        //Ja itemin image oikeaksi, jos itemillä ei ole sprite, laita missingImage
        if (Data.item.Sprite != null) shopItemObj.transform.Find("Item").Find("Image").GetComponent<Image>().sprite = Data.item.Sprite;
        else shopItemObj.transform.Find("Item").Find("Image").GetComponent<Image>().sprite = missingImage;

        //Ja set description ja rarity
        setShopItemDescription(shopItemObj);
        setShopItemRarity(shopItemObj);

    }

    //Set shopItem description asettaa esineiden descriptionin oikeaksi
    private void setShopItemDescription(GameObject shopItemObj)
    {
        string itemName = shopItemObj.transform.Find("Item").GetComponent<ItemData>().item.Title;
        //Shop Price = itemPrice*2
        string itemPrice = (shopItemObj.transform.Find("Item").GetComponent<ItemData>().item.price*2).ToString();
        shopItemObj.transform.Find("Description").GetComponent<Text>().text = itemName;
        shopItemObj.transform.Find("Price").GetChild(0).GetComponent<Text>().text = "Price: " +itemPrice;
    }

    //showShop näyttää shop paneelin
    public void showShop(bool show)
    {
        if (show)
            transform.gameObject.SetActive(true);
        else
            transform.gameObject.SetActive(false);
    }

    //Clear shop metodi tyhjentää shopin kaikki itemit
    public void clearShop()
    {
        for (int i = 0; i < shopContent.transform.childCount; i++)
        {
            Destroy(shopContent.transform.GetChild(i).gameObject);
        }
    }

    public void updateShopItems()
    {

        clearShop();
        for(int i = 0; i < currentShopNPC.shopItems.Count; i++)
        {
            AddItemToShop(currentShopNPC.shopItems[i]);
        }
    }

    //setShopItemRarity määrää rarity-kuvan joka näkyy uncommon - legendary itemeissä
    private void setShopItemRarity(GameObject shopItemObj)
    {
        //Debug.Log("setting loot rarity");

        if (shopItemObj.transform.Find("Item").GetComponent<ItemData>().item.Rarity == "Uncommon")
        {
            shopItemObj.transform.Find("Item").GetComponent<Image>().sprite = rarityUncommon;
        }
        else if (shopItemObj.transform.Find("Item").GetComponent<ItemData>().item.Rarity == "Rare")
        {
            shopItemObj.transform.Find("Item").GetComponent<Image>().sprite = rarityRare;
        }
        else if (shopItemObj.transform.Find("Item").GetComponent<ItemData>().item.Rarity == "Epic")
        {
            shopItemObj.transform.Find("Item").GetComponent<Image>().sprite = rarityEpic;
        }
        else if (shopItemObj.transform.Find("Item").GetComponent<ItemData>().item.Rarity == "Legendary")
        {
            shopItemObj.transform.Find("Item").GetComponent<Image>().sprite = rarityLegendary;
        }
        //Jos rarity common, common taustakuva
        else
        {
            shopItemObj.transform.Find("Item").GetComponent<Image>().sprite = rarityCommon;
        }

    }


    //If this panel is active, we are "selling"
    //Meaning a right-click on inventory will sell an item instead of equipping it
    public bool checkIfSelling()
    {
        if (transform.gameObject.activeSelf == true)
        {
            return true;
        }
        else return false;
    }



}
