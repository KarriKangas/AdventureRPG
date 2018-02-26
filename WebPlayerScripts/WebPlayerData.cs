using UnityEngine;
using System.Collections;

public class WebPlayerData : MonoBehaviour {

    Inventory inventory;
    itemInformationDatabase itemInfoDB;
    EnemyInformationDatabase enemyInfoDB;

	// Use this for initialization

    public void initWebData()
    {
        itemInfoDB = GameObject.Find("Inventory").GetComponent<itemInformationDatabase>();
        enemyInfoDB = GameObject.Find("EnemyScriptHolder").GetComponent<EnemyInformationDatabase>();
    }

    public void buildItemDatas()
    {

        buildWeaponPrefixes();
        buildWeaponSuffixes();
        buildWeaponTypes();
        buildUsables();
        buildGearTypes();

        //Debug.Log("Item datas successfully built!");
    }

    public void buildEnemyDatas()
    {
        
        buildEnemyPrefixes();
        buildEnemyTypes();

        //Debug.Log("Enemy datas successfully built!");
    }

    private void buildWeaponPrefixes()
    {
        itemInfoDB.weaponPrefixNames.Add(("Broken"));
        itemInfoDB.weaponPrefixNames.Add(("Moldy"));
        itemInfoDB.weaponPrefixNames.Add(("Awful"));
        itemInfoDB.weaponPrefixNames.Add(("Weak"));
        itemInfoDB.weaponPrefixNames.Add(("Faulty"));
        itemInfoDB.weaponPrefixNames.Add(("Terrible"));
        itemInfoDB.weaponPrefixNames.Add(("Bad"));
        itemInfoDB.weaponPrefixNames.Add(("Rusty"));
        itemInfoDB.weaponPrefixNames.Add(("Dull"));
        itemInfoDB.weaponPrefixNames.Add(("Lame"));
        itemInfoDB.weaponPrefixNames.Add(("Clumsy"));
        itemInfoDB.weaponPrefixNames.Add(("Cracked"));
        itemInfoDB.weaponPrefixNames.Add(("Old"));
        itemInfoDB.weaponPrefixNames.Add(("Flimsy"));
        itemInfoDB.weaponPrefixNames.Add(("Damaged"));
        itemInfoDB.weaponPrefixNames.Add(("Hollow"));
        itemInfoDB.weaponPrefixNames.Add(("Crooked"));
        itemInfoDB.weaponPrefixNames.Add(("Rough"));
        itemInfoDB.weaponPrefixNames.Add(("Odd"));
        itemInfoDB.weaponPrefixNames.Add(("Rigid"));
        itemInfoDB.weaponPrefixNames.Add(("Slow"));
        itemInfoDB.weaponPrefixNames.Add(("Dirty"));
        itemInfoDB.weaponPrefixNames.Add(("Dusty"));
        itemInfoDB.weaponPrefixNames.Add(("Faded"));
        itemInfoDB.weaponPrefixNames.Add(("Plain"));
        itemInfoDB.weaponPrefixNames.Add(("Curvy"));
        itemInfoDB.weaponPrefixNames.Add(("Pretty"));
        itemInfoDB.weaponPrefixNames.Add(("Simple"));
        itemInfoDB.weaponPrefixNames.Add(("Steady"));
        itemInfoDB.weaponPrefixNames.Add(("New"));
        itemInfoDB.weaponPrefixNames.Add(("Handy"));
        itemInfoDB.weaponPrefixNames.Add(("Good"));
        itemInfoDB.weaponPrefixNames.Add(("Unusual"));
        itemInfoDB.weaponPrefixNames.Add(("Cool"));
        itemInfoDB.weaponPrefixNames.Add(("Sturdy"));
        itemInfoDB.weaponPrefixNames.Add(("Unused"));
        itemInfoDB.weaponPrefixNames.Add(("Warm"));
        itemInfoDB.weaponPrefixNames.Add(("Stingy"));
        itemInfoDB.weaponPrefixNames.Add(("Improved"));
        itemInfoDB.weaponPrefixNames.Add(("Quick"));
        itemInfoDB.weaponPrefixNames.Add(("Marked"));
        itemInfoDB.weaponPrefixNames.Add(("Military"));
        itemInfoDB.weaponPrefixNames.Add(("Great"));
        itemInfoDB.weaponPrefixNames.Add(("Enchanted"));
        itemInfoDB.weaponPrefixNames.Add(("Volatile"));
        itemInfoDB.weaponPrefixNames.Add(("Endurable"));
        itemInfoDB.weaponPrefixNames.Add(("Magical"));
        itemInfoDB.weaponPrefixNames.Add(("Majestic"));
        itemInfoDB.weaponPrefixNames.Add(("Precious"));
        itemInfoDB.weaponPrefixNames.Add(("Tranquil"));
        itemInfoDB.weaponPrefixNames.Add(("Malicious"));
        itemInfoDB.weaponPrefixNames.Add(("Superb"));
        itemInfoDB.weaponPrefixNames.Add(("Dazzling"));
        itemInfoDB.weaponPrefixNames.Add(("Sparkling"));
        itemInfoDB.weaponPrefixNames.Add(("Magnificent"));
        itemInfoDB.weaponPrefixNames.Add(("Powerful"));
        itemInfoDB.weaponPrefixNames.Add(("Exotic"));
        itemInfoDB.weaponPrefixNames.Add(("Dark"));
        itemInfoDB.weaponPrefixNames.Add(("Ancient"));




    }

    private void buildWeaponSuffixes()
    {
        itemInfoDB.weaponSuffixNames.Add(("Mouse"));
        itemInfoDB.weaponSuffixNames.Add(("Rat"));
        itemInfoDB.weaponSuffixNames.Add(("Rabbit"));
        itemInfoDB.weaponSuffixNames.Add(("Mole"));
        itemInfoDB.weaponSuffixNames.Add(("Chipmunk"));
        itemInfoDB.weaponSuffixNames.Add(("Chinchilla"));
        itemInfoDB.weaponSuffixNames.Add(("Infant"));
        itemInfoDB.weaponSuffixNames.Add(("Seagull"));
        itemInfoDB.weaponSuffixNames.Add(("Sheep"));
        itemInfoDB.weaponSuffixNames.Add(("Pig"));
        itemInfoDB.weaponSuffixNames.Add(("Novice"));
        itemInfoDB.weaponSuffixNames.Add(("Parrot"));
        itemInfoDB.weaponSuffixNames.Add(("Squid"));
        itemInfoDB.weaponSuffixNames.Add(("Cow"));
        itemInfoDB.weaponSuffixNames.Add(("Turtle"));
        itemInfoDB.weaponSuffixNames.Add(("Fox"));
        itemInfoDB.weaponSuffixNames.Add(("Bandit"));
        itemInfoDB.weaponSuffixNames.Add(("Goat"));
        itemInfoDB.weaponSuffixNames.Add(("Bard"));
        itemInfoDB.weaponSuffixNames.Add(("Cat"));
        itemInfoDB.weaponSuffixNames.Add(("Dog"));
        itemInfoDB.weaponSuffixNames.Add(("Chameleon"));
        itemInfoDB.weaponSuffixNames.Add(("Apprentice"));
        itemInfoDB.weaponSuffixNames.Add(("Owl"));
        itemInfoDB.weaponSuffixNames.Add(("Lizard"));
        itemInfoDB.weaponSuffixNames.Add(("Thief"));
        itemInfoDB.weaponSuffixNames.Add(("Soldier"));
        itemInfoDB.weaponSuffixNames.Add(("Crow"));
        itemInfoDB.weaponSuffixNames.Add(("Archer"));
        itemInfoDB.weaponSuffixNames.Add(("Elk"));
        itemInfoDB.weaponSuffixNames.Add(("Smith"));
        itemInfoDB.weaponSuffixNames.Add(("Pirate"));
        itemInfoDB.weaponSuffixNames.Add(("Antelope"));
        itemInfoDB.weaponSuffixNames.Add(("Armadillo"));
        itemInfoDB.weaponSuffixNames.Add(("Rogue"));
        itemInfoDB.weaponSuffixNames.Add(("Hawk"));
        itemInfoDB.weaponSuffixNames.Add(("Slaver"));
        itemInfoDB.weaponSuffixNames.Add(("Coyote"));
        itemInfoDB.weaponSuffixNames.Add(("Priest"));
        itemInfoDB.weaponSuffixNames.Add(("Eagle"));
        itemInfoDB.weaponSuffixNames.Add(("Warrior"));
        itemInfoDB.weaponSuffixNames.Add(("Jackal"));
        itemInfoDB.weaponSuffixNames.Add(("Samurai"));
        itemInfoDB.weaponSuffixNames.Add(("Elephant"));
        itemInfoDB.weaponSuffixNames.Add(("Shaman"));
        itemInfoDB.weaponSuffixNames.Add(("Dolphin"));
        itemInfoDB.weaponSuffixNames.Add(("Hippopotamus"));
        itemInfoDB.weaponSuffixNames.Add(("Ninja"));
        itemInfoDB.weaponSuffixNames.Add(("Monkey"));
        itemInfoDB.weaponSuffixNames.Add(("Crusader"));
        itemInfoDB.weaponSuffixNames.Add(("Jaguar"));
        itemInfoDB.weaponSuffixNames.Add(("Gladiator"));
        itemInfoDB.weaponSuffixNames.Add(("Ox"));
        itemInfoDB.weaponSuffixNames.Add(("Tiger"));
        itemInfoDB.weaponSuffixNames.Add(("Executioner"));
        itemInfoDB.weaponSuffixNames.Add(("Gorilla"));
        itemInfoDB.weaponSuffixNames.Add(("Wizard"));
        itemInfoDB.weaponSuffixNames.Add(("Knight"));
        itemInfoDB.weaponSuffixNames.Add(("Lion"));
        itemInfoDB.weaponSuffixNames.Add(("Bear"));
        itemInfoDB.weaponSuffixNames.Add(("King"));
        itemInfoDB.weaponSuffixNames.Add(("Dragon"));
        itemInfoDB.weaponSuffixNames.Add(("Champion"));



    }

    private void buildWeaponTypes()
    {
        itemInfoDB.weaponTypeNames.Add(new gearType("Wooden Club", "1h"));
        itemInfoDB.weaponTypeNames.Add(new gearType("Wooden Dagger", "1h"));
        itemInfoDB.weaponTypeNames.Add(new gearType("Dagger", "1h"));
        itemInfoDB.weaponTypeNames.Add(new gearType("Wooden Short sword", "1h"));
        itemInfoDB.weaponTypeNames.Add(new gearType("Wooden Gauntlet", "1h"));
        itemInfoDB.weaponTypeNames.Add(new gearType("Short sword", "1h"));
        itemInfoDB.weaponTypeNames.Add(new gearType("Gauntlet", "1h"));
        itemInfoDB.weaponTypeNames.Add(new gearType("Mace", "1h"));
        itemInfoDB.weaponTypeNames.Add(new gearType("Cutlass", "1h"));
        itemInfoDB.weaponTypeNames.Add(new gearType("Sword", "1h"));
        itemInfoDB.weaponTypeNames.Add(new gearType("Flail", "1h"));
        itemInfoDB.weaponTypeNames.Add(new gearType("Rapier", "1h"));
        itemInfoDB.weaponTypeNames.Add(new gearType("Scimitar", "1h"));
        itemInfoDB.weaponTypeNames.Add(new gearType("Tanto", "1h"));
        itemInfoDB.weaponTypeNames.Add(new gearType("Saber", "1h"));
        itemInfoDB.weaponTypeNames.Add(new gearType("Axe", "1h"));
        itemInfoDB.weaponTypeNames.Add(new gearType("Spear", "1h"));
        itemInfoDB.weaponTypeNames.Add(new gearType("Whip", "1h"));
        itemInfoDB.weaponTypeNames.Add(new gearType("Lance", "1h"));
        itemInfoDB.weaponTypeNames.Add(new gearType("Javelin", "1h"));
        itemInfoDB.weaponTypeNames.Add(new gearType("Pike", "1h"));
        itemInfoDB.weaponTypeNames.Add(new gearType("Claw", "1h"));
        itemInfoDB.weaponTypeNames.Add(new gearType("Longsword", "1h"));
        itemInfoDB.weaponTypeNames.Add(new gearType("Wakizashi", "1h"));
        itemInfoDB.weaponTypeNames.Add(new gearType("Battleaxe", "1h"));
        itemInfoDB.weaponTypeNames.Add(new gearType("Broadsword", "1h"));
        itemInfoDB.weaponTypeNames.Add(new gearType("Trident", "1h"));
        itemInfoDB.weaponTypeNames.Add(new gearType("Warhammer", "1h"));
        itemInfoDB.weaponTypeNames.Add(new gearType("Claymore", "1h"));
        itemInfoDB.weaponTypeNames.Add(new gearType("Scythe", "1h"));
        itemInfoDB.weaponTypeNames.Add(new gearType("Katana", "1h"));
        itemInfoDB.weaponTypeNames.Add(new gearType("Halberd", "1h"));
        itemInfoDB.weaponTypeNames.Add(new gearType("Glaive", "1h"));
        itemInfoDB.weaponTypeNames.Add(new gearType("Greatsword", "1h"));
        itemInfoDB.weaponTypeNames.Add(new gearType("DaDao", "1h"));





    }

    private void buildGearTypes()
    {
        itemInfoDB.gearTypeNames.Add(new gearType("Cloth Shirt", "Torso"));
        itemInfoDB.gearTypeNames.Add(new gearType("Cloth Cap", "Head"));
        itemInfoDB.gearTypeNames.Add(new gearType("Cloth Hand Wraps", "Hands"));
        itemInfoDB.gearTypeNames.Add(new gearType("Loincloth", "Legs"));
        itemInfoDB.gearTypeNames.Add(new gearType("Cloth Sandals", "Feet"));
        itemInfoDB.gearTypeNames.Add(new gearType("Cloth Tunic", "Torso"));
        itemInfoDB.gearTypeNames.Add(new gearType("Cloth Hood", "Head"));
        itemInfoDB.gearTypeNames.Add(new gearType("Cloth Gloves", "Hands"));
        itemInfoDB.gearTypeNames.Add(new gearType("Pants", "Legs"));
        itemInfoDB.gearTypeNames.Add(new gearType("Cloth Slippers", "Feet"));
        itemInfoDB.gearTypeNames.Add(new gearType("Leather Shirt", "Torso"));
        itemInfoDB.gearTypeNames.Add(new gearType("Leather Cap", "Head"));
        itemInfoDB.gearTypeNames.Add(new gearType("Leather Hand Wraps", "Hands"));
        itemInfoDB.gearTypeNames.Add(new gearType("Leather Loincloth", "Legs"));
        itemInfoDB.gearTypeNames.Add(new gearType("Leather Sandals", "Feet"));
        itemInfoDB.gearTypeNames.Add(new gearType("Leather Tunic", "Torso"));
        itemInfoDB.gearTypeNames.Add(new gearType("Leather Hood", "Head"));
        itemInfoDB.gearTypeNames.Add(new gearType("Leather Gloves", "Hands"));
        itemInfoDB.gearTypeNames.Add(new gearType("Leather Pants", "Legs"));
        itemInfoDB.gearTypeNames.Add(new gearType("Leather Slippers", "Feet"));
        itemInfoDB.gearTypeNames.Add(new gearType("Cloth Coat", "Torso"));
        itemInfoDB.gearTypeNames.Add(new gearType("Cloth Headdress", "Head"));
        itemInfoDB.gearTypeNames.Add(new gearType("Cloth Vambraces", "Hands"));
        itemInfoDB.gearTypeNames.Add(new gearType("Cloth Skirt", "Legs"));
        itemInfoDB.gearTypeNames.Add(new gearType("Cloth Boots", "Feet"));
        itemInfoDB.gearTypeNames.Add(new gearType("Iron Shirt", "Torso"));
        itemInfoDB.gearTypeNames.Add(new gearType("Iron Cap", "Head"));
        itemInfoDB.gearTypeNames.Add(new gearType("Iron Hand Wraps", "Hands"));
        itemInfoDB.gearTypeNames.Add(new gearType("Iron Loincloth", "Legs"));
        itemInfoDB.gearTypeNames.Add(new gearType("Iron Sandals", "Feet"));
        itemInfoDB.gearTypeNames.Add(new gearType("Iron Tunic", "Torso"));
        itemInfoDB.gearTypeNames.Add(new gearType("Iron Hood", "Head"));
        itemInfoDB.gearTypeNames.Add(new gearType("Iron Gloves", "Hands"));
        itemInfoDB.gearTypeNames.Add(new gearType("Iron Pants", "Legs"));
        itemInfoDB.gearTypeNames.Add(new gearType("Iron Slippers", "Feet"));
        itemInfoDB.gearTypeNames.Add(new gearType("Leather Coat", "Torso"));
        itemInfoDB.gearTypeNames.Add(new gearType("Leather Headdress", "Head"));
        itemInfoDB.gearTypeNames.Add(new gearType("Leather Vambraces", "Hands"));
        itemInfoDB.gearTypeNames.Add(new gearType("Leather Skirt", "Legs"));
        itemInfoDB.gearTypeNames.Add(new gearType("Leather Boots", "Feet"));
        itemInfoDB.gearTypeNames.Add(new gearType("Cloth Armor", "Torso"));
        itemInfoDB.gearTypeNames.Add(new gearType("Cloth Helmet", "Head"));
        itemInfoDB.gearTypeNames.Add(new gearType("Cloth Gauntlets", "Hands"));
        itemInfoDB.gearTypeNames.Add(new gearType("Cloth Breeches", "Legs"));
        itemInfoDB.gearTypeNames.Add(new gearType("Cloth Greaves", "Feet"));


    }

    //(string title, int value, int buffAmount, bool usable, bool stackable, string id)
    private void buildUsables()
    {
itemInfoDB.usableDatabase.Add(new Item("Tiny health potion",5,20,true, true, 0));
itemInfoDB.usableDatabase.Add(new Item("Tiny energy potion",6,10, true, true, 1));
itemInfoDB.usableDatabase.Add(new Item("Small health potion",20,40, true, true, 2));
itemInfoDB.usableDatabase.Add(new Item("Small energy potion",22,20, true, true, 3));
itemInfoDB.usableDatabase.Add(new Item("Health potion",30,60, true, true, 4));
itemInfoDB.usableDatabase.Add(new Item("Energy potion",34,30, true, true, 5));

    }

    private void buildEnemyPrefixes()
    {

        enemyInfoDB.enemyPrefixNames.Add(("Tiny"));
        enemyInfoDB.enemyPrefixNames.Add(("Cute"));
        enemyInfoDB.enemyPrefixNames.Add(("Malnourished"));
        enemyInfoDB.enemyPrefixNames.Add(("Blue-eyed"));
        enemyInfoDB.enemyPrefixNames.Add(("Petite"));
        enemyInfoDB.enemyPrefixNames.Add(("Goofy"));
        enemyInfoDB.enemyPrefixNames.Add(("Fuzzy"));
        enemyInfoDB.enemyPrefixNames.Add(("Ill"));
        enemyInfoDB.enemyPrefixNames.Add(("Cowardly"));
        enemyInfoDB.enemyPrefixNames.Add(("Helpless"));
        enemyInfoDB.enemyPrefixNames.Add(("Innocent"));
        enemyInfoDB.enemyPrefixNames.Add(("Small"));
        enemyInfoDB.enemyPrefixNames.Add(("Smelly"));
        enemyInfoDB.enemyPrefixNames.Add(("Thirsty"));
        enemyInfoDB.enemyPrefixNames.Add(("Hungry"));
        enemyInfoDB.enemyPrefixNames.Add(("Lazy"));
        enemyInfoDB.enemyPrefixNames.Add(("Lowly"));
        enemyInfoDB.enemyPrefixNames.Add(("Tired"));
        enemyInfoDB.enemyPrefixNames.Add(("Foolish"));
        enemyInfoDB.enemyPrefixNames.Add(("Disgusting"));
        enemyInfoDB.enemyPrefixNames.Add(("Noisy"));
        enemyInfoDB.enemyPrefixNames.Add(("Skinny"));
        enemyInfoDB.enemyPrefixNames.Add(("Old"));
        enemyInfoDB.enemyPrefixNames.Add(("Young"));
        enemyInfoDB.enemyPrefixNames.Add(("Nutty"));
        enemyInfoDB.enemyPrefixNames.Add(("Obese"));
        enemyInfoDB.enemyPrefixNames.Add(("Scared"));
        enemyInfoDB.enemyPrefixNames.Add(("Arrogant"));
        enemyInfoDB.enemyPrefixNames.Add(("Bored"));
        enemyInfoDB.enemyPrefixNames.Add(("Passive"));
        enemyInfoDB.enemyPrefixNames.Add(("Hulking"));
        enemyInfoDB.enemyPrefixNames.Add(("Aware"));
        enemyInfoDB.enemyPrefixNames.Add(("Healthy"));
        enemyInfoDB.enemyPrefixNames.Add(("Ready"));
        enemyInfoDB.enemyPrefixNames.Add(("Hissing"));
        enemyInfoDB.enemyPrefixNames.Add(("Hideous"));
        enemyInfoDB.enemyPrefixNames.Add(("Accurate"));
        enemyInfoDB.enemyPrefixNames.Add(("Careful"));
        enemyInfoDB.enemyPrefixNames.Add(("Disturbed"));
        enemyInfoDB.enemyPrefixNames.Add(("Free"));
        enemyInfoDB.enemyPrefixNames.Add(("Infamous"));
        enemyInfoDB.enemyPrefixNames.Add(("Wild"));
        enemyInfoDB.enemyPrefixNames.Add(("Nimble"));
        enemyInfoDB.enemyPrefixNames.Add(("Aggressive"));
        enemyInfoDB.enemyPrefixNames.Add(("Dashing"));
        enemyInfoDB.enemyPrefixNames.Add(("Intelligent"));
        enemyInfoDB.enemyPrefixNames.Add(("Resolute"));
        enemyInfoDB.enemyPrefixNames.Add(("Brawny"));
        enemyInfoDB.enemyPrefixNames.Add(("Sneaky"));
        enemyInfoDB.enemyPrefixNames.Add(("Efficient"));
        enemyInfoDB.enemyPrefixNames.Add(("Vigorous"));
        enemyInfoDB.enemyPrefixNames.Add(("Dangerous"));
        enemyInfoDB.enemyPrefixNames.Add(("Venomous"));
        enemyInfoDB.enemyPrefixNames.Add(("Frightening"));
        enemyInfoDB.enemyPrefixNames.Add(("Violent"));
        enemyInfoDB.enemyPrefixNames.Add(("Honorable"));
        enemyInfoDB.enemyPrefixNames.Add(("Enormous"));
        enemyInfoDB.enemyPrefixNames.Add(("Endurable"));
        enemyInfoDB.enemyPrefixNames.Add(("Fierce"));
        enemyInfoDB.enemyPrefixNames.Add(("Giant"));
        enemyInfoDB.enemyPrefixNames.Add(("Ruthless"));
        enemyInfoDB.enemyPrefixNames.Add(("Gruesome"));
        enemyInfoDB.enemyPrefixNames.Add(("Vengeful"));
        enemyInfoDB.enemyPrefixNames.Add(("Hellish"));
        enemyInfoDB.enemyPrefixNames.Add(("Condemned"));
        enemyInfoDB.enemyPrefixNames.Add(("Possessed"));
        enemyInfoDB.enemyPrefixNames.Add(("Elite"));
        enemyInfoDB.enemyPrefixNames.Add(("Wrathful"));
        enemyInfoDB.enemyPrefixNames.Add(("Godly"));




        Debug.Log("" + enemyInfoDB.enemyPrefixDatabase.Count);
    }

    private void buildEnemyTypes()
    {
        enemyInfoDB.enemyTypeNames.Add(("Spider"));
        enemyInfoDB.enemyTypeNames.Add(("Rat"));
        enemyInfoDB.enemyTypeNames.Add(("Bat"));
        enemyInfoDB.enemyTypeNames.Add(("Green Slime"));
        enemyInfoDB.enemyTypeNames.Add(("Lizard"));
        enemyInfoDB.enemyTypeNames.Add(("Snake"));
        enemyInfoDB.enemyTypeNames.Add(("Blue Slime"));
        enemyInfoDB.enemyTypeNames.Add(("Dragon"));




    }
}
