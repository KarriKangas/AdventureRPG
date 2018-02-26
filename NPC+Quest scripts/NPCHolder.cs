using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class NPCHolder : MonoBehaviour {

    private NonPlayerCharacter currentNPC;
    List<NonPlayerCharacter> designedNPCs;
    private Sprite missingImage;

    [SerializeField]
    miscItemHandler miscItems;

    [SerializeField]
    gearItemHandler gearItems;

    [SerializeField]
    ShopManager shop;

    private float shopResetTimer;
    //Timer for how often shop resets, in seconds
    private float resetTime = 180;

	void Start () {
        designedNPCs = new List<NonPlayerCharacter>();
        missingImage = Resources.Load<Sprite>("Sprites/missItem");
        listNPCs();
        currentNPC = designedNPCs[0];
        shop.Initialize();

    }

    void Update()
    {
        dynamicShopReset();
    }

    
	
	NonPlayerCharacter findNPCid(int id)
    {
        for(int i = 0; i < designedNPCs.Count; i++)
        {
            if(designedNPCs[i].id == id)
            {
                return designedNPCs[i];
            }
        }
        return null;

    }

    //Creates a list of all existing NPCs
    void listNPCs()
    {
        NonPlayerCharacter tempNPC;

        //List innkeeper--Location 1!
        tempNPC = new NonPlayerCharacter("Innkeeper", loadSprite("innkeeper"), 3, 0);
        tempNPC.setButtonText(0, "Quests " + "(" + tempNPC.questAmount + ")");
        tempNPC.setButtonText(1, "Talk");
        tempNPC.setButtonText(2, "Shop");
        tempNPC.level = 1;
        designedNPCs.Add(tempNPC);

        //List innkeeper--Location 2!
        tempNPC = new NonPlayerCharacter("Child", loadSprite("child"), 2, 1);
        tempNPC.setButtonText(0, "Quests " + "(" + tempNPC.questAmount + ")");
        tempNPC.setButtonText(1, "Talk");
        tempNPC.level = 5;
        designedNPCs.Add(tempNPC);

        //List innkeeper--Location 3!
        tempNPC = new NonPlayerCharacter("Innkeeper", loadSprite("innkeeper2"), 2, 2);
        tempNPC.setButtonText(0, "Quests " + "(" + tempNPC.questAmount + ")");
        tempNPC.setButtonText(1, "Talk");
        tempNPC.setButtonText(2, "Shop");
        tempNPC.level = 10;
        designedNPCs.Add(tempNPC);



        //List lumberjack--Location 4!
        tempNPC = new NonPlayerCharacter("Lumberjack", loadSprite("lumberjack"), 2, 3);
        tempNPC.setButtonText(0, "Quests " + "(" + tempNPC.questAmount + ")");
        tempNPC.setButtonText(1, "Talk");
        tempNPC.level = 15;
        designedNPCs.Add(tempNPC);

        //List lumberjack--Location 5!
        tempNPC = new NonPlayerCharacter("Harbor master", loadSprite("Harbormaster"), 2, 4);
        tempNPC.setButtonText(0, "Quests " + "(" + tempNPC.questAmount + ")");
        tempNPC.setButtonText(1, "Talk");
        tempNPC.level = 20;
        designedNPCs.Add(tempNPC);

        //List lumberjack--Location 6!
        tempNPC = new NonPlayerCharacter("Mayor", loadSprite("Mayor"), 2, 5);
        tempNPC.setButtonText(0, "Quests " + "(" + tempNPC.questAmount + ")");
        tempNPC.setButtonText(1, "Talk");
        tempNPC.level = 25;
        designedNPCs.Add(tempNPC);

        //List lumberjack--Location 7!
        tempNPC = new NonPlayerCharacter("Wizard", loadSprite("Wizard"), 2, 6);
        tempNPC.setButtonText(0, "Quests " + "(" + tempNPC.questAmount + ")");
        tempNPC.setButtonText(1, "Talk");
        tempNPC.level = 30; 
        designedNPCs.Add(tempNPC);

        //List lumberjack--Location 8!
        tempNPC = new NonPlayerCharacter("Hero", loadSprite("Hero"), 2, 7);
        tempNPC.setButtonText(0, "Quests " + "(" + tempNPC.questAmount + ")");
        tempNPC.setButtonText(1, "Talk");
        tempNPC.level = 35;
        designedNPCs.Add(tempNPC);







    }

    //Loads sprite with NPC name, save sprite in lowercase in resources/sprites
    Sprite loadSprite(string name)
    {
        Sprite returnSprite = Resources.Load<Sprite>("Sprites/" + name);
        if (returnSprite != null) return returnSprite;
        else return missingImage;
    }

    //Methods for setting "current NPC", used in Inns, Shops etc.
    //Current NPC will be displayed in Interaction panel
    //Sets the current NPC, REFERENCE ONLY!!! Change displayed NPC in InteractionManager!!!
    public void setCurrentNPC(int id)
    {

        for (int i = 0; i < designedNPCs.Count; i++)
        {
            if (designedNPCs[i].id == id)
            {
                currentNPC = designedNPCs[i];
                createShop(currentNPC);
            }
        }
    }
    
    public void setCurrentNPC(string name)
    {
       
        for (int i = 0; i < designedNPCs.Count; i++)
        {
            if (designedNPCs[i].name == name)
            {
                currentNPC = designedNPCs[i];
                createShop(currentNPC);
            }
        }
    }

    public void setCurrentNPC(NonPlayerCharacter NPC)
    {
        currentNPC = NPC;
        createShop(currentNPC);
        shop.updateShopItems();
    }

    //And a method for getting current NPC
    public NonPlayerCharacter getCurrentNPC()
    {
        return currentNPC;
    }

    public void createShop(NonPlayerCharacter NPC)
    {
        clearShop(NPC);
        addShopPotions(NPC);
        addShopItems(NPC);
        shop.updateShopItems();
    }


    //Method to add potions to shop, always call this BEFORE adding other items
    private void addShopPotions(NonPlayerCharacter NPC)
    {
        
        for (int i = 0; i < 2; i++)
        {
            NPC.shopItems.Add(miscItems.generateUsable(NPC.level + 4 + i));
        }
    }

    //Method to add Items to shop
    private void addShopItems(NonPlayerCharacter NPC)
    {
        int itemAmount = Random.Range(3, 10);
        int gearOrwep;

        for (int i = 0; i < itemAmount; i++)
        {
            gearOrwep = Random.Range(0, 2);
            if (gearOrwep == 0)
            {
                NPC.shopItems.Add(gearItems.generateBaseWeapon(NPC.level, NPC.level, 1, 1));
                //Debug.Log("Item added to shop");
            }
            else if (gearOrwep == 1)
            {
                NPC.shopItems.Add(gearItems.generateBaseGear(NPC.level, NPC.level, 1, 1));
                //Debug.Log("Item added to shop");
            }
        }

    }

    //Remove all items from shop
    private void clearShop(NonPlayerCharacter NPC)
    {
        NPC.shopItems.RemoveRange(0, NPC.shopItems.Count);      
    }

    //Method to reset shop every shopResetTimer -seconds
    private void dynamicShopReset()
    {
        
        if (shopResetTimer > 0)
        {
            shopResetTimer -= Time.deltaTime;
        }
        else
        {
            for(int i = 0; i < designedNPCs.Count; i++)
            {

                createShop(designedNPCs[i]);
                
                shopResetTimer = resetTime;
            }
        }
    }

    public float getTimer()
    {
        return shopResetTimer;
    }

}
