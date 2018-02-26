using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LootHandler : MonoBehaviour {


    private Sprite missingImage;

    [SerializeField]
    private GameObject lootPrefab;

    [SerializeField]
    private GameObject lootContent;

    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    private GameObject battleLootLocation;

    [SerializeField]
    private GameObject adventureRewardLocation;

    private GameObject Title;
    private Sprite rarityCommon;
    private Sprite rarityUncommon;
    private Sprite rarityRare;
    private Sprite rarityEpic;
    private Sprite rarityLegendary;
    private Sprite noImage;

    public void Initialize()
    {
        missingImage = Resources.Load<Sprite>("Sprites/missItem");
        noImage = Resources.Load<Sprite>("Sprites/noImage");
        rarityCommon = Resources.Load<Sprite>("Sprites/Items/SlotColor/Common");
        rarityUncommon = Resources.Load<Sprite>("Sprites/Items/SlotColor/Uncommon");
        rarityRare = Resources.Load<Sprite>("Sprites/Items/SlotColor/Rare");
        rarityEpic = Resources.Load<Sprite>("Sprites/Items/SlotColor/Epic");
        rarityLegendary = Resources.Load<Sprite>("Sprites/Items/SlotColor/Legendary");
        //Find the third child which is title and it's only child
        Title = transform.GetChild(3).GetChild(0).gameObject;
    }
	

    public void AddItemToLoot(Item item)
    {

        //Instantioidaan uusi item prefab
        GameObject lootObj = Instantiate(lootPrefab);


        //Ja asetetaan parent oikein
        lootObj.transform.SetParent(lootContent.transform);

        //Asetetaan sen position vastaamaan parentin keskusta
        lootObj.transform.position = lootObj.transform.parent.position;

        //Ja scale kuntoon
        lootObj.transform.localScale = new Vector3(1, 1, 1);

        //Otetaan itemData
        ItemData Data = lootObj.transform.Find("Item").GetComponent<ItemData>();
        
        //Ja datan.item oikein
        Data.item = item;


        //Itemin slot num "-2" kun itemin on loot windowissa
        Data.slotNum = -2;
        

        //Ja objectin nimi oikein editorissa
        lootObj.name = item.Title;


        //Ja itemin image oikeaksi, jos itemillä ei ole sprite, laita missingImage
        if(Data.item.Sprite != null) lootObj.transform.Find("Item").Find("Image").GetComponent<Image>().sprite = Data.item.Sprite;
        else lootObj.transform.Find("Item").Find("Image").GetComponent<Image>().sprite = missingImage;

        //Ja set description ja rarity
        setLootDescription(lootObj);
        setLootRarity(lootObj);
    }

    //Clear loot metodi tyhjentää loot windowin
    public void clearLoot()
    {
        for(int i = 0; i < lootContent.transform.childCount; i++)
        {
            Destroy(lootContent.transform.GetChild(i).gameObject);
        }
    }

    //Set loot description asettaa loot descriptionin oikeaksi
    private void setLootDescription(GameObject lootObj)
    {
        string itemName = lootObj.transform.Find("Item").GetComponent<ItemData>().item.Title;
        lootObj.transform.Find("Description").GetComponent<Text>().text = itemName;
    }

    //setLootRarity määrää rarity-kuvan joka näkyy uncommon - legendary itemeissä
    private void setLootRarity(GameObject lootObj)
    {
       // Debug.Log("setting loot rarity");
            
            if (lootObj.transform.Find("Item").GetComponent<ItemData>().item.Rarity == "Uncommon")
            {
                lootObj.transform.Find("Item").GetComponent<Image>().sprite = rarityUncommon;
            }
            else if (lootObj.transform.Find("Item").GetComponent<ItemData>().item.Rarity == "Rare")
            {
                lootObj.transform.Find("Item").GetComponent<Image>().sprite = rarityRare;
            }
            else if (lootObj.transform.Find("Item").GetComponent<ItemData>().item.Rarity == "Epic")
            {
                lootObj.transform.Find("Item").GetComponent<Image>().sprite = rarityEpic;
            }
            else if (lootObj.transform.Find("Item").GetComponent<ItemData>().item.Rarity == "Legendary")
            {
                lootObj.transform.Find("Item").GetComponent<Image>().sprite = rarityLegendary;
            }
            //Jos rarity common, common taustakuva
            else
            {
                lootObj.transform.Find("Item").GetComponent<Image>().sprite = rarityCommon;
            }

    }

    //showLoot näyttää loot paneelin
    public void showLoot(bool show)
    {
        if (show)
            transform.gameObject.SetActive(true);
        else
            transform.gameObject.SetActive(false);
    }

    public void lootAll()
    {
        int lootAmount = lootContent.transform.childCount;
        for(int i = 0; i < lootContent.transform.childCount; i++)
        {
            int freeSlot = inventory.hasFreeSlot();
            Item itemToLoot = lootContent.transform.GetChild(i).Find("Item").GetComponent<ItemData>().item;

            //Debug.Log(freeSlot);
            if (freeSlot != -1)
            {
               // Debug.Log("adding item " + itemToLoot.Title);
                inventory.AddItem(itemToLoot);
                
                Destroy(lootContent.transform.GetChild(i).gameObject);
                lootAmount--;
            }
            
        }
        
        if (lootAmount == 0)
        {
            
            showLoot(false);
        }
        
    }


    public void setLocation(string location)
    {
        switch (location)
        {
            case ("Battle"):
                transform.SetParent(battleLootLocation.transform);
                transform.position = battleLootLocation.transform.position;
                Title.GetComponent<Text>().text = "Dropped items";
                break;

            case ("Adventure"):
                transform.SetParent(adventureRewardLocation.transform);
                transform.position = adventureRewardLocation.transform.position;
                Title.GetComponent<Text>().text = "Reward";
                break;
        }

    }
       
}
