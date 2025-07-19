using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{

    //=====ITEM DATA========//\

    public string itemName;
    public string itemDescription;
    public int qty;
    public Sprite sprite;
    public Sprite emptySprite;
    public bool isFull;

    //create a "unuseable" tag?

    [SerializeField]
    private int maxNumberInStack;
    


    //=====ITEM SLOT=======//
    [SerializeField]
    private TMP_Text quantity;

    [SerializeField]
    private Image itemImage;



    //=====Item Description=====//
    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;



    //=====Item Description=====/
    public GameObject selectedShader;
    public bool thisItemSelected;

    private InventoryManager inventoryManager;

    private void Awake()
    {
        inventoryManager = GameObject.Find("Canvas").GetComponent<InventoryManager>();
    }
    public int AddItem(string itemName, int qty, Sprite itemSprite, string itemDescription, bool unstackable)
    {
        //checks if slot is already full
        if (isFull)
            return qty;

        //update name
        this.itemName = itemName;

        //update image
        this.sprite = itemSprite;
        itemImage.sprite = itemSprite;

        //update Description
        this.itemDescription = itemDescription;

        if (unstackable)
        {
            isFull = true;
        }

        //update qty
        this.qty += qty;
        if (this.qty >= maxNumberInStack)
        {
            quantity.text = maxNumberInStack.ToString();
            quantity.enabled = true;
            isFull = true;

            //return the leftOvers
            int extraItems = this.qty - maxNumberInStack;
            this.qty = maxNumberInStack;
            return extraItems;
        }

        //update quantity text
        quantity.text = this.qty.ToString();
        quantity.enabled = true;

        return 0;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public void OnLeftClick()
    {
        
        if (thisItemSelected)
        {
            inventoryManager.UseItem(itemName);
            this.qty -= 1;
            quantity.text = this.qty.ToString();
            isFull = false;
            if (this.qty <= 0)
            {
                qty = 0;
            emptySlot();
                Debug.Log("Item Used");
            }
        }
        else
        {//deselects then reselects inventory item
            inventoryManager.DeselectAllSlots();
            selectedShader.SetActive(true);
            thisItemSelected = true;
            itemDescriptionNameText.text = itemName;
            itemDescriptionText.text = itemDescription;
            itemDescriptionImage.sprite = sprite;
            if (itemDescriptionImage.sprite == null)
            {
                itemDescriptionImage.sprite = emptySprite;
            }
        }
        
    }

    //If slot is empty, changes the slot to show it is empty
    private void emptySlot()
    {
        itemImage.sprite = emptySprite;
        itemDescription = "";
        itemName = "";
        isFull = false;
        quantity.enabled = false;

        itemDescriptionNameText.text = itemName;
        itemDescriptionText.text = itemDescription;
        itemDescriptionImage.sprite = emptySprite;
        sprite = emptySprite;
    }

    //drops selected item
    public void OnRightClick()
    {
        if (thisItemSelected)
        {
            
            inventoryManager.DropItem(itemName);
            this.qty -= 1;
            quantity.text = this.qty.ToString();
            isFull = false;
            Debug.Log("dropped");
            if (this.qty <= 0)
            {
                qty = 0;
                emptySlot();
                
            }
        }
    }

}

