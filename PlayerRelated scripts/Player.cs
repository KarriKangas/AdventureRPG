using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class Player : MonoBehaviour {

    //Player luokka joka sisältää Inventoryn toiminnan ja pelaajan kaikki toiminnot... jne, päivittämiset 

    [SerializeField]
    private Stat health;
    [SerializeField]
    private Stat energy;

    [SerializeField]
    private Stat experience;

    [SerializeField]
    private Text damageText;

    [SerializeField]
    private Text LevelText;

    [SerializeField]
    private Text goldText;

    public int Level;

    [SerializeField]
    private ExperienceScript xpManager;

    [SerializeField]
    private FloatingNumbers floatNumbers;

    [SerializeField]
    private GameObject CharacterInfo;

    //Evertyhing level up related here:
    [SerializeField]
    private GameObject levelUpPanel;

    private GameObject levelX;

    private GameObject bonusContent;
    private GameObject unlockContent;

    private GameObject levelUpText;

    private List<string> levelUnlocks;

    //Info section 1 = damage, armor, health, energy, hp/energy regen, lifesteal
    //Info section 2 = exp, gold, loot quality/quantity
    private Text infoSection1, infoSection2;
    

    private float currentXP;
    private List<float> experienceRequired;

    private float maxTotalDamage;
    private float maxBaseDamage;
    private float damageBonus;

    private float minBaseDamage;
    private float minTotalDamage;

    private float totalHealth;
    private float totalEnergy;

    private float baseHealth;
    private float baseEnergy;

    private float healthBonus;
    private float energyBonus;

    public float armor { get; set; }
    public float lifesteal { get; set; }
    public float hpRegen { get; set; }
    public float energyRegen { get; set; }
    public float expBonus { get; set; }
    public float goldBonus { get; set; }
    public float lootQuality { get; set; }
    public float lootQuantity { get; set; }

    private gearItemHandler itemHandler;

    private int gold;
    public Location location { get; set; }
    private Inventory inventory;

    
    

	//Awakessa initialisoidaan kaikki ruudulla näkyvät "baarit"(palkit)
	private void Awake () {
        health.Initialize();
        energy.Initialize();
        experience.Initialize();
        itemHandler = GameObject.Find("ItemScriptHolder").GetComponent<gearItemHandler>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();

        infoSection1 = CharacterInfo.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        infoSection2 = CharacterInfo.transform.GetChild(1).GetChild(0).GetComponent<Text>();

        

    }

    //Startissa alustellaan perusjuttuja, kaikki numerot vaihdettavissa!(balance)
    void Start()
    {
        initLevelUp();

        addLevelUpText(("Health: " + 10), bonusContent);
        addLevelUpText(("Energy: " + 5), bonusContent);
        addLevelUpText(("Damage: " + 1 +"-"+3), bonusContent);
        addLevelUpText(("Skill points: " + 1), bonusContent);
        addLevelUpText("Perks", unlockContent);
        addLevelUpText("Arena", unlockContent);


        experienceRequired = xpManager.initExperienceCurve();

        Level = 1;
        minBaseDamage = 2;
        maxBaseDamage = 4;
        baseHealth = 30;
        baseEnergy = 12;

        damageBonus = 1;
        healthBonus = 5;
        energyBonus = 3;
        LevelText.text = "Level: " + Level;
        damageText.text = "Damage: " + maxBaseDamage;


        

        gold = 20;

        calculateStats();

       

    }



	// Updatessa tarkkaillaan Inputtia kuten aseiden käyttöönottoa, lasketaan Leveleitä... jne.
	void Update () {
        
        checkForLevelUp();
        updateGold();

}




   




   

    //Damagen uudestaan lasku metodi
    private void calculateStats()
    {
        float currentHealthMultiplier = health.CurrentVal / health.MaxVal;
        float currentEnergyMultiplier = energy.CurrentVal / energy.MaxVal;
        //damage = perusdamage 
        minTotalDamage = (int)minBaseDamage;
        maxTotalDamage = (int)maxBaseDamage;
        totalHealth = baseHealth;
        totalEnergy = baseEnergy;
        armor = 0;
        lifesteal = 0;
        hpRegen = 0;
        energyRegen = 0;
        expBonus = 0;
        goldBonus = 0;
        lootQuality = 0;
        lootQuantity = 0;
        //Jos ase damage = perusdamage + asedamage
        for(int i = 0; i < inventory.equippedItems.Count; i++)
        {
            if(inventory.equippedItems[i].ID != "-1")
            {
                minTotalDamage += inventory.equippedItems[i].minDamage;
                maxTotalDamage += inventory.equippedItems[i].maxDamage;
                totalHealth += inventory.equippedItems[i].Health;
                totalEnergy += inventory.equippedItems[i].Energy;
                armor += inventory.equippedItems[i].armor;
                lifesteal += inventory.equippedItems[i].lifeSteal;
                hpRegen += inventory.equippedItems[i].healthRegen;
                energyRegen += inventory.equippedItems[i].energyRegen;
                expBonus += inventory.equippedItems[i].experienceBonus;
                goldBonus += inventory.equippedItems[i].goldBonus;
                lootQuality += inventory.equippedItems[i].lootQuality;
                lootQuantity += inventory.equippedItems[i].lootQuantity;
                
            }
        }
        
        health.MaxVal = totalHealth;
        energy.MaxVal = totalEnergy;
        health.CurrentVal = health.MaxVal * currentHealthMultiplier;
        energy.CurrentVal = energy.MaxVal * currentEnergyMultiplier;


        

        damageText.text = "Damage: "+ minTotalDamage +"-" + maxTotalDamage;
        updateInfos();

    }

    void updateInfos()
    {
        infoSection1.text = @minTotalDamage + "-" + maxTotalDamage
            + "\n" + armor
            + "\n" + totalHealth
            + "\n" + totalEnergy
            + "\n" + hpRegen + "/t"
            + "\n" + energyRegen + "/t"
            + "\n" + (lifesteal * 100) + "%";

        infoSection2.text = @expBonus*100 + "%"
            + "\n" + (goldBonus*100) + "%"
            + "\n" + (lootQuality * 100) + "%"
            + "\n" + (lootQuantity * 100) + "%";
    }

    //Metodi jota kutsutaan kun saa Levelin
    private void LevelUp()
    {

        Level++;
        LevelText.text = "Level: " + Level;

        clearLevelTexts();
        levelUpPanel.SetActive(true);
        levelX.transform.GetChild(0).GetComponent<Text>().text = "Level " + Level + "!";
        addLevelUpText("Health: " + healthBonus, bonusContent);
        addLevelUpText("Energy: " + energyBonus, bonusContent);
        addLevelUpText("Damage: " + damageBonus + "-" + damageBonus, bonusContent);

        if (levelUnlocks[Level] != null)
        {
            addLevelUpText(levelUnlocks[Level], unlockContent);
        }
        if(Level >= 2)
        {
            addLevelUpText("Skill points: " + 1, unlockContent);
        }

        //Lisätään kaikki level bonukset->
        //"Parannetaan" pelaaja täyteen healthiin ja energiaan ja lasketaan damage uudestaan

        minBaseDamage += damageBonus;
        maxBaseDamage += damageBonus;
        baseHealth += healthBonus;
        baseEnergy += energyBonus;

        health.MaxVal = baseHealth;
        energy.MaxVal = baseEnergy;

        health.CurrentVal = health.MaxVal;
        energy.CurrentVal = energy.MaxVal;
        calculateStats();

        //Asetetaan uusi experienceReq ja experience levelin alkuun

        experience.CurrentVal = experience.CurrentVal - experience.MaxVal;

        experience.MaxVal = experienceRequired[Level];
        

        if(Level%2 == 0)
        {
            damageBonus+=2;
            healthBonus += 5;
            energyBonus+=3;
        }
        

    }

    //Metodi jolla tarkistellaan onko tullut Leveliä
    private void checkForLevelUp()
    {
        if(experience.CurrentVal >= experience.MaxVal && Level > 0){
            LevelUp();
            
        }
    }

    public void updateGearStats()
    {
        calculateStats();
    }

    //Kultamäärän päivitysmetodi
    private void updateGold()
    {        
        goldText.text = "Gold: " + gold;
        
    }


    public void addExperience(int amount)
    {
        experience.CurrentVal += amount;
        floatNumbers.createText(amount, "experience");
    }

    public void addGold(int amount)
    {
        gold += amount;
        floatNumbers.createText(amount, "gold");
    }
    public float getDamage()
    {
        float returnDamage = UnityEngine.Random.Range(minTotalDamage, maxTotalDamage+1);
        return returnDamage;
    }

    public void setCurrentHealth(int amount)
    {
        health.CurrentVal += amount;
    }

    public void setCurrentEnergy(int amount)
    {
        energy.CurrentVal += amount;
    }

    public int getCurrentHealth()
    {
        return (int)health.CurrentVal;
    }

    public int getCurrentEnergy()
    {
        return (int)energy.CurrentVal;
    }

    public int getMaxHealth()
    {
        return (int)health.MaxVal;
    }

    public int getMaxEnergy()
    {
        return (int)energy.MaxVal;
    }



    public int getGold()
    {
        return gold;
    }

    public int getExpReq()
    {
        return (int)experienceRequired[Level];
    }

    private void initLevelUp()
    {
        levelUpPanel.SetActive(true);
        levelX = levelUpPanel.transform.GetChild(1).gameObject;
        setUnlockList();
        bonusContent = levelUpPanel.transform.Find("LevelBonuses").Find("BonusesScroll").GetChild(0).gameObject;
        unlockContent = levelUpPanel.transform.Find("LevelUnlocks").Find("FeaturesScroll").GetChild(0).gameObject;
        levelUpText = Resources.Load<GameObject>("Prefabs/levelUpText");
        levelUpPanel.SetActive(false);
    }

    private void setUnlockList()
    {
        levelUnlocks = new List<string>();
        // replace 100 with MAXLEVEL when it is known
        for(int i = 0; i < 100; i++)
        {
            levelUnlocks.Add(null);
        }
        levelUnlocks[1] = "Classes";
        levelUnlocks[2] = "Skills";
        levelUnlocks[5] = "Perks";

    }

    public void continueLevelUp()
    {
        levelUpPanel.SetActive(false);
    }

    private void addLevelUpText(string text, GameObject panel)
    {
        GameObject temp = Instantiate(levelUpText);
        temp.transform.SetParent(panel.transform);
        temp.transform.localScale = new Vector3(1, 1, 1);
        temp.GetComponent<Text>().text = text;
    }

    public void clearLevelTexts()
    {
        for (int i = 0; i < bonusContent.transform.childCount; i++)
        {
            Destroy(bonusContent.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < unlockContent.transform.childCount; i++)
        {
            Destroy(unlockContent.transform.GetChild(i).gameObject);
        }
    }



}
