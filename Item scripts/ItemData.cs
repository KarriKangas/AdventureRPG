using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{

    // ItemData class is a script which is attached to every item in the game, it makes items aware of themselves
    public Item item;
    public int amount;
    public int slotNum;

    private Tooltip tooltip;
    private Vector2 offset;
    private Inventory inventory;

    private GameObject invItemHome, eqpItemHome;

    public bool hovering, dragged;

    private bool trashing;

    private GameObject trash;




    void Start()
    {
        dragged = false;
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        tooltip = inventory.GetComponent<Tooltip>();
        invItemHome = GameObject.Find("Inventory Item");
        eqpItemHome = GameObject.Find("Equipment Item");
        trash = GameObject.Find("Trash");

    }

    void Update()
    {
        
        if(slotNum == -2 && hovering)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log(item.Title);
                
                lootItem();
            }
        }else if(slotNum == -3 && hovering)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("attempting to buy " +item.Title);

                buyItem();
            }
        }

        trashCheck();
    }

    //On mouse click (before drag begins)
    public void OnPointerDown(PointerEventData eventData)
    {
        //GetMouseButton(0) makes sure player is dragging with Mouse1, otherwise -> BUGS BUGS BGUS BUGS 
        //And make sure item ID is NOT equal to -2 (= loot item)
        if (item != null && Input.GetMouseButton(0) && !Input.GetMouseButton(1) && slotNum != -2 && slotNum != -3)
        {
            Debug.Log("fired");
            
                offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
                dragged = true;         
        }
    }

    //On begin drag
    public void OnBeginDrag(PointerEventData eventData)
    {
        
        if(item != null && dragged)
        {
            Debug.Log(dragged);
            //Set offset to correct mouse position
            offset = eventData.position - new Vector2( this.transform.position.x, this.transform.position.y);

            //Make this not blockraycast
            this.transform.GetComponent<CanvasGroup>().blocksRaycasts = false;

            //Set item position to mouse-offset
            this.transform.position = eventData.position - offset;

            //Set the "item home" so we know where the item came from
            if (transform.parent.parent.name == "Slot Panel")
            {
                this.transform.SetParent(invItemHome.transform);
            }else if(transform.parent.parent.name == "EquipmentSlotPanel")
            {
                this.transform.SetParent(eqpItemHome.transform);
            }
        }
    }

    //On drag simply move position
    public void OnDrag(PointerEventData eventData)
    {
        if (item != null && dragged)
        {
            this.transform.position = eventData.position - offset;
            
        }
    }

    //On end drag
    public void OnEndDrag(PointerEventData eventData)
    {
        
        if(trashing)
        {
            Destroy(transform.gameObject);
            inventory.items[slotNum] = new Item();
        }
        else if(slotNum == -2 ||slotNum == -3)
        {
            Debug.Log("This is a loot item");
        }
        //If the slotnum of the item is smaller than slotAmount (means it's dropped in inventory)
        else if (slotNum < inventory.slotAmount)
        {
            //Se correct parent, position and block raycasts again
            this.transform.SetParent(inventory.slots[slotNum].transform);
            this.transform.position = inventory.slots[slotNum].transform.position;
            this.transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        //If tje slotnum is greater than slotAmoun (means it's dropped in equipment panel)
        else 
        {
            //Se correct parent, position and block raycasts again
            this.transform.SetParent(inventory.equipmentSlots[slotNum-inventory.slotAmount].transform);
            this.transform.position = inventory.equipmentSlots[slotNum - inventory.slotAmount].transform.position;
            this.transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
       
    }

    //Tooltip activation method
    public void OnPointerEnter(PointerEventData eventData)
    {
        //slotNum -5 = TRASH! (item delete)
        if (slotNum != -5 && item.ID != "-1")
        {
            tooltip.Activate(item);
        }
        hovering = true;

    }

    //Tooltip deactivation method
    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Deactivate();
        hovering = false;
    }

    private void lootItem()
    {
        //On click, loot clicked item (add it to inventory)
        inventory.AddItem(item);

        //If it was the last item in loot panel, hide the panel
        if (transform.parent.parent.childCount == 1)
        {
            
            //parent(1) of item = loot instance, parent(2) of loot instance = loot content, parent(3) of loot content = scroll and parent(4) of scroll = panel!
            transform.parent.parent.parent.parent.gameObject.SetActive(false);
            
        }

        //And destroy the looted item
        Destroy(transform.parent.gameObject);
        tooltip.Deactivate();

        
    }

    private void buyItem()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        if (player.getGold() >= item.price*2)
        {
            //On click, buy clicked item (add it to inventory)
            inventory.AddItem(item);

            //And remove gold from player
            player.addGold(item.price * -(2));

            //And destroy the looted item
            if (!item.Usable)
            {
                Destroy(transform.parent.gameObject);
                tooltip.Deactivate();
            }
        }
        else
        {
            Debug.Log("Not enough gold");
        }
    


    }

    private void trashCheck()
    {

        //If item is hovered over trash bin
        if(trash.GetComponent<ItemData>().hovering == true)
        {
            
            trashing = true;
            trash.transform.localScale = new Vector3(1.4f, 1.4f, 1);
            
        }
        else
        {
            trashing = false;
            trash.transform.localScale = new Vector3(1, 1, 1);
        }
        
    }

    private void sellItem()
    {

    }

}
