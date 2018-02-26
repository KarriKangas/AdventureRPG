using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class AdventureScript : MonoBehaviour {

    [SerializeField]
    private GameObject adventurePanel;

    [SerializeField]
    private List<Button> CombatActions; // Combat actions [0] = attack, [1] = skills, [2] = flee

    [SerializeField]
    private Image enemyImage;

    [SerializeField]
    private Text enemyName;

    [SerializeField]
    private GameObject enemyInfoPanel;  

    [SerializeField]
    private Stat HealthBar;

    [SerializeField]
    private AnimationHandler animationHandler;

    [SerializeField]
    private FloatingNumbers damageText;

    [SerializeField]
    private GameObject rewardPanel;

    [SerializeField]
    private QuestMasterScript QuestMaster;

    [SerializeField]
    private BlockerScript Block;

    [SerializeField]
    private LocationManager LocationManager;

    [SerializeField]
    private NPCHolder NPCManager;

    [SerializeField]
    private InteractionManager InteractionManager;

    [SerializeField]
    private LootHandler LootHandler;

    [SerializeField]
    private AdventureBuilder AdventureBuilder;

    [SerializeField]
    private GameObject AdventureDone;

    [SerializeField]
    private EncounterManager EncounterManager;
    private bool encounterHappens;
    private float encounterChance = 0.1f;

    [SerializeField]
    private Text doneStats;

    private Inventory inventory;

    public Player player;

    private Enemy currentEnemy;

    private EnemyHandler enemyHandler;

    private gearItemHandler gearHandler;

    private miscItemHandler miscItemHandler;

    private Sprite missingImage;

    private bool infoToggle;

    private Text rewardText;

    private int expGain, goldGain;

    private bool victory, loss;

    private int damageDealt;
    private int enemyDmgDealt;
    private float lifeStolen;

    List<Item> lootPool;
    private int lootPoolSize = 30;
    private int itemsDropped;

    private int battleAmount;
    private int battleDifficulty;
    private bool firstBattle;
    private bool isBossBattle;

    private int totalGold, totalBonusGold, totalExperience,totalBonusExp, battlesWon;
    private float rarityBonus, rarityStageBonus;


    void Start () {
        AdventureBuilder.Initialize();
        LootHandler.Initialize();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        player = GameObject.Find("Player").GetComponent<Player>();

        enemyHandler = GameObject.Find("EnemyScriptHolder").GetComponent<EnemyHandler>();
        gearHandler = GameObject.Find("ItemScriptHolder").GetComponent<gearItemHandler>();
        miscItemHandler = GameObject.Find("ItemScriptHolder").GetComponent<miscItemHandler>();

        missingImage = Resources.Load<Sprite>("Sprites/missItem");
        rewardText = rewardPanel.transform.GetChild(1).GetComponent<Text>();

        
        rewardPanel.SetActive(false);
        HealthBar.Initialize();

        firstBattle = true;
    }

    public void startBattle()
    {
        //If the battle is the first of the adventure, set battleAmount and Difficulty
        if (firstBattle)
        {
            Debug.Log("first battle");
            battleAmount = AdventureBuilder.getLength();
            battleDifficulty = AdventureBuilder.getDifficulty();
            firstBattle = false;

            totalGold = 0;
            totalExperience = 0;
            battlesWon = 0;
            totalBonusGold = 0;
            totalExperience = 0;
            setLootQuality();
        }

        AdventureBuilder.transform.gameObject.SetActive(false);
        Block.activeBlock("Adventuring", true);
        
        //Debug.Log("Battle amount is " + battleAmount + " and difficulty is " + battleDifficulty);


        //Difficulty = location.Level * location.id + 6
        // 5 == minimum, must be over that

        //currentLevel is used in enemyType creation, whereas difficulty is used in prefix generation
        float minDifficulty = player.location.getLevel();
        float maxDifficulty = LocationManager.LocationList[player.location.id + 1].GetComponent<Location>().getLevel()+1;
        int currentLevel = (int) Random.Range(minDifficulty, maxDifficulty);
        float difficulty = Random.Range(minDifficulty*battleDifficulty, maxDifficulty*battleDifficulty);


        if (isBossBattle)
        {
            currentEnemy = enemyHandler.returnBossEnemy(currentLevel, difficulty);
        }
        else {
            currentEnemy = enemyHandler.returnEnemy(currentLevel, difficulty);
        }

        //Debug.Log("Your enemy will be: " + currentEnemy.Title);

        initAdventurePanel();
        

        
        adventurePanel.SetActive(true);
        //Call this so enemy health bar is INSTANTLY(usually lerps slowly) set to 100%, only used here: could be a method, could define EnemyHPBar earlier, 
        GameObject.Find("EnemyHPBar").transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = 1;
        Block.activeBlock("Adventure", true);
        rewardPanel.SetActive(false);

        dynamicQuestAdd();


    }

    //Initialize adventure panel values
    //Image, text, info, health... 
    private void initAdventurePanel()
    {
        //If sprite is found, cool
        if(currentEnemy.Sprite != null) 
            enemyImage.sprite = currentEnemy.Sprite;
        else //if not, display missingImage   
            enemyImage.sprite = missingImage;
        

        //Set enemy name
        enemyName.text = currentEnemy.Title;

        //Set Health

        HealthBar.MaxVal = currentEnemy.Health;
        HealthBar.CurrentVal = currentEnemy.Health;
        
        //Set info
        //enemyInfo.text = "Type: " + currentEnemy.Type + "\nDifficulty: " + currentEnemy.Difficulty + " \nDamage: " + currentEnemy.Damage + "\nHealth: " + currentEnemy.Health;
        enemyInfoPanel.GetComponentInChildren<Text>().text = currentEnemy.Description;


        


    }

    //method for displaying enemy information
    public void showInfo()
    {
        if (infoToggle != true)
        {
            enemyInfoPanel.SetActive(true);
            infoToggle = true;

            
        }
        else
        {
            enemyInfoPanel.SetActive(false);
            infoToggle = false;
        }

    }

   
    //Player attack mehtod
    IEnumerator attack()
    {
        //Adventure block unables player to click buttons while attack in progress
        Block.activeBlock("Attack", true);

        //Play attack animation
        animationHandler.playAnimation("hitAnimation");

        //Small wait makes it feel more interactive
        yield return new WaitForSeconds(0.1f);

        damageDealt = (int) player.getDamage();

        //Display damage
        damageText.createText(damageDealt, "player");

        //Add lifesteal
        if (player.lifesteal > 0 && player.getCurrentHealth() < player.getMaxHealth())
        {
            //Calculate lifesteal, if lifestolen is zero, set it to minimum 1, then add it to health and create floating text
            lifeStolen = damageDealt * player.lifesteal;
            if (lifeStolen < 1) lifeStolen = 1;
            player.setCurrentHealth((int)lifeStolen);
            damageText.createText((int)lifeStolen, "lifesteal");
        }

        //And set enemy health -= damage
        HealthBar.CurrentVal -= damageDealt;

        //Wait for the damage float upwards...
        //This wait can be split to parts for more animation plays! :)
        yield return new WaitForSeconds(1.5f);
        
        //If enemy health == 0, show reward, else enemyAction
        if (HealthBar.CurrentVal == 0)
        {
            showReward();
        }
        else
        {
            enemyAction();
        }
    }

    //Method for showing battle reward
    private void showReward()
    {
        //If showing reward, enemy is dead, progress quests->
        QuestMaster.progressKillQuest(currentEnemy.Type);

        //If this was not the last battle, show normal reward
        if (battleAmount == 1)
        {
            //Set rewards
            Debug.Log("Showing adventure reward + battleAmount is " + battleAmount);
            setAdventureReward();

        }
        else if(battleAmount > 1)
        {
            //Set rewards
            //Debug.Log("Showing battle reward + battleAmount is " + battleAmount);
            setBattleReward();

        }
    }

    //Method for setting rewardpanel texts
    private void setBattleReward()
    {
        //Battle complete -> battleAmount--
        battleAmount--;
        battlesWon++;

        //Set exp and gold gains... balance!
        expGain = currentEnemy.Difficulty * 4;

        //Calculate total BONUS gained for end screen and achievements
        int bonusExp = (int)(expGain * player.expBonus);
        if (bonusExp == 0) bonusExp = 1;
        totalBonusExp += bonusExp;
        
        //And add the bonus to expGain
        expGain += bonusExp;

        //Calculate total gained for end screen and achievements
        totalExperience += expGain;


        //Do all the same for gold as exp^
        goldGain = currentEnemy.Difficulty * 2;

        int bonusGold = (int)(goldGain * player.goldBonus);
        if (bonusGold == 0) bonusGold = 1;
        totalBonusGold += bonusGold;
        totalGold += goldGain;
        goldGain += bonusGold;

        //And item gains...
        setLootAmount(false);
        //Loot pool, prefix/suffix level is added, but rarityStageBonus is not (stage = common/uncommon/rare... etc)
        lootPool = createLootPool(currentEnemy.Type.Difficulty, currentEnemy.Difficulty, rarityBonus, 1);
        LootHandler.setLocation("Battle");

        //Add exp and gold rewards to player
        player.addExperience(expGain);
        player.addGold(goldGain);

        //Set victory!
        victory = true;
        string rewardItems = "";
        //Set the title
        rewardPanel.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Victory!";
        rewardText.text = "Experience gained: " + expGain + "\nGold earned: " + goldGain;

        //Show loot panel if items dropped > 0
        if (itemsDropped > 0)
        {
            LootHandler.showLoot(true);
            
            rewardItems = "\nFound " + itemsDropped + " item";

            if (itemsDropped > 1) rewardItems = rewardItems + "s";

            for (int i = 0; i < itemsDropped; i++)
            {
                Item itemToLoot = lootPool[Random.Range(0, lootPool.Count)];
                LootHandler.AddItemToLoot(itemToLoot);
            }
            rewardText.text = rewardText.text + rewardItems;
        }
        //If no loot, dont show loot panel
        else
        {
            LootHandler.showLoot(false);
        }

        rewardText.text += "\nBattles left: " + battleAmount;
        setRewardBlocks();
    }
    
    private void setAdventureReward()
    {
        AdventureDone.SetActive(true);
        LootHandler.clearLoot();
        battleAmount--;
        battlesWon++;
        //Set exp and gold gains... balance!
        expGain = currentEnemy.Difficulty * 4;
        totalExperience += expGain;
        goldGain = currentEnemy.Difficulty * 2;
        totalGold += goldGain;

        //And item gains...
        setLootAmount(true);
        //Loot pool, prefix/suffix level is added and stageBonus is added as well
        lootPool = createLootPool(currentEnemy.Type.Difficulty, currentEnemy.Difficulty, rarityBonus, rarityStageBonus);
        LootHandler.setLocation("Adventure");
        
        //Add exp and gold rewards to player
        player.addExperience(expGain);
        player.addGold(goldGain);

        if (itemsDropped > 0)
        {
            LootHandler.showLoot(true);                      

            for (int i = 0; i < itemsDropped; i++)
            {
                //Debug.Log(i);
                Item itemToLoot = lootPool[Random.Range(0, lootPool.Count)];
                LootHandler.AddItemToLoot(itemToLoot);
            }
            
        }
        //If no loot, dont show loot panel
        else
        {
            LootHandler.showLoot(false);
        }

        setRewardBlocks();
        setDoneTexts();
        AdventureDone.SetActive(true);
        AdventureDone.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Adventure Completed!";
    }

    private void setAdventureLoss()
    {
        setRewardBlocks();
        setDoneTexts();
        AdventureDone.SetActive(true);
        AdventureDone.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Adventure Failed!";
    }

    //Method to show loss after panel
    private void showLoss()
    {
        
        setLoss();
        
    }

    private void setLoss()
    {
        setRewardBlocks();
        //Set victory!
        victory = false;
        loss = true;
        //Set the title
        rewardPanel.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Loss!";
        int goldLoss = (player.getGold() / 10);
        rewardText.text = "You are defeated. You manage to crawl out of the battle losing a few (" + goldLoss + ") gold pieces.";
        player.setCurrentHealth(1);
        player.addGold(goldLoss*-1);

        battleAmount = 0;

        LootHandler.showLoot(false);

    }


    //Enemy attack call method
    private void enemyAction()
    {
        StartCoroutine("enemyAttack");
    }

    //Actualy enemyAttack method
    IEnumerator enemyAttack()
    {
        //Attack
        //Play enemy hit animation here!
        yield return new WaitForSeconds(0.1f);

        //Calculate damage, (min<->max)-armor, if negative damage, set it to 0
        enemyDmgDealt = Random.Range(currentEnemy.minDamage, currentEnemy.maxDamage + 1) - (int)(player.armor/10);
        if (enemyDmgDealt < 0) enemyDmgDealt = 0;
        Debug.Log("Player armor is " + player.armor + " and it blocked " + (player.armor / 10) + " damage!");

        //Show damage
        damageText.createText(enemyDmgDealt, "enemy");
        //set player health
        player.setCurrentHealth(enemyDmgDealt * -1);

        if (player.getCurrentHealth() <= 0)
        {
            showLoss();
        }
        else {
            StartCoroutine("startPlayerTurn");
        }
    }

    //Method which is called when player turn starts
    IEnumerator startPlayerTurn()
    {
        yield return new WaitForSeconds(0.5f);
        Block.activeBlock("Attack", false);
        //Add health regen
        if (player.hpRegen > 0 && player.getCurrentHealth() != player.getMaxHealth())
        {
            player.setCurrentHealth((int)player.hpRegen);
            damageText.createText((int)player.hpRegen, "HPregen");
        }
        //Add energy regen
        if (player.energyRegen > 0 && player.getCurrentEnergy() != player.getMaxEnergy())
        {
            player.setCurrentEnergy((int)player.energyRegen);
            damageText.createText((int)player.energyRegen, "Energyregen");
        }
    }

    //OnClick methods:
    public void attackClick()
    {
        StartCoroutine("attack");
    }

    //Method for "continue"  button after battle
    public void endBattle()
    {
        rewardPanel.SetActive(false);
        Block.activeBlock("Adventuring", false);
        Block.activeBlock("Attack", false);
        Block.activeBlock("Actions", false);
        Block.activeBlock("Behind", false);
        AdventureDone.SetActive(false);

        if (victory)
        {
            QuestMaster.progressKillQuest(currentEnemy.Type);

            LootHandler.clearLoot();
            checkForEncounter();
            if (battleAmount > 0 && encounterHappens)
            {
                EncounterManager.showEncounter();
                encounterHappens = false;
            }
            else if (battleAmount > 0)
            {
                if (battleAmount == 1)
                {
                    isBossBattle = true;
                }
                startBattle();
            }
            else
            {
                //Adventure is over, firstBattle true again and next battle is not bossBattle anymore
                firstBattle = true;
                isBossBattle = false;

            }
        }
        else if (loss)
        {
            setAdventureLoss();
            //Set loss to false so this block doesn't run a 2nd time
            loss = false;
            firstBattle = true;
            isBossBattle = false;


        }
        else
        {
            Debug.Log("Adventure over by loss");
        }
    }

    private void checkForEncounter()
    {
        Debug.Log("Checking for encounter with a " + (100*encounterChance) + "% chance...");
        float random = Random.Range(0f, 1f);
        if(random < encounterChance)
        {
            encounterHappens = true;
        }
        Debug.Log("There is going to be an encounter: " + encounterHappens);
    }

    private void setRewardBlocks()
    {
        //hide adventure panel
        adventurePanel.SetActive(false);

        //And show reward panel
        rewardPanel.SetActive(true);

        //And disable info panel
        enemyInfoPanel.SetActive(false);

        //And hide block right behind adventure panel
        Block.activeBlock("Adventuring", false);

        //And activate block far behind adventure panel
        Block.activeBlock("Behind", true);

        //And only allow the clicking of "character" in actions panel
        Block.activeBlock("Actions", true);
    }


    private List<Item> createLootPool(int level, int rarity, float rarityBonus, float rarityStageBonus)
    {
        List<Item> lootHolder = new List<Item>();

        for(int i = 0; i < lootPoolSize-15; i++)
        {
            lootHolder.Add(gearHandler.generateBaseWeapon((level), rarity, rarityBonus, rarityStageBonus));
            
        }
        for(int i = 0; i < lootPoolSize -5; i++)
        {
            lootHolder.Add(gearHandler.generateBaseGear((level), rarity, rarityBonus, rarityStageBonus));
        }
        
        for(int i = 0; i < lootPoolSize - lootHolder.Count; i++)
        {
            lootHolder.Add(miscItemHandler.generateUsable(level));
        }

        return lootHolder;

    }

    private void setLootAmount(bool adventureDone)
    {
        float random = Random.Range(0, 100);
        Debug.Log("Loot quantity before bonus of (" + (1+player.lootQuantity) + ")" + random);
        random *= (1+player.lootQuantity);
        Debug.Log("Loot quantity after bonus" + random);
        if (random < 40)
        {
            itemsDropped = 0;
        }else if(random < 80)
        {
            itemsDropped = 1;
        }
        else if (random < 95)
        {
            itemsDropped = 2;
        }
        else if (random < 99)
        {
            itemsDropped = 3;
        }
        else
        {
            itemsDropped = 4;
        }

        if (adventureDone) itemsDropped += (AdventureBuilder.getLength()/2);

        //Debug.Log("Items dropped: " + itemsDropped);
    }

    //Method to set bonus loot qualities depending on difficulty
    private void setLootQuality()
    {
        if(AdventureBuilder.getDifficulty() == 3)
        {
            rarityBonus = 1.3f;
            rarityStageBonus = 1.3f;
        }
        else if(AdventureBuilder.getDifficulty() == 5)
        {
            rarityBonus = 1.5f;
            rarityStageBonus = 1.5f;
        }
        else
        {
            rarityBonus = 1f;
            rarityStageBonus = 1f;
        }
    }

    //Method to set stats shown in "Adventure complete!"
    private void setDoneTexts()
    {
        string n = @AdventureBuilder.currentDifficulty
            + "\n" + AdventureBuilder.currentLength
            + "\n" + battlesWon
            + "\n" + "?"
            + "\n" + totalGold + "(" + totalBonusGold+ ")"
            + "\n" + "?"
            + "\n" + totalExperience + "(" + totalBonusExp +")";

        doneStats.text = n;
    }

    //dynamicQuestAdd method is ran every time the player goes into an adventure
    //it determines the chance a new quest is added to the currentNPC
    //Base chance = 20%
    //Each quest the NPC already has sets the chance -8%
    //Each quest the player has sets the chance -3%
    private void dynamicQuestAdd()
    {
        float newQuestChance = 0.2f;
        for(int i = 0; i < NPCManager.getCurrentNPC().questAmount; i++)
        {
            newQuestChance -= 0.08f;
        }

        for(int i = 0; i < QuestMaster.QuestList.Count; i++)
        {
            newQuestChance -= 0.03f;
        }

        if(Random.Range(0f,1f) < newQuestChance)
        {
            InteractionManager.addQuest(NPCManager.getCurrentNPC());
            Debug.Log("new quest added to npc!");
        }
        else
        {
            Debug.Log("No new quest :(");
        }
        
    }
}
