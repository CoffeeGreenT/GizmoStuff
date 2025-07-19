using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;

    private bool menuActive;

    //Allows HPBar to move when opening/closing inventory
    public RectTransform HPBarMove;


    //creates an array of item slots
    public ItemSlot[] itemSlot;

    public ItemSO[] itemSOs;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //disables inventory on start
        InventoryMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //opens/close inventory
        if (Input.GetButtonDown("Inventory") && menuActive)
        {
            InventoryMenu.SetActive(false);
            menuActive = false;
            //move hpbar back to 0,0,0
            HPBarMove.anchoredPosition = Vector2.zero;
        }
        else if(Input.GetButtonDown("Inventory")&& !menuActive)
        {
            InventoryMenu.SetActive(true);
            menuActive=true;
            //move hpbar move to 660,450,0
            HPBarMove.anchoredPosition= new Vector2(660, 450);
        }

    }
    //checks for the item in the array on use
    public void UseItem(string itemName)
    {
        for (int i = 0; i < itemSOs.Length; i++)
        {
            if (itemSOs[i].itemName == itemName)
            {
                itemSOs[i].UseItem();
            }
        }
    }

    //works like use item, but removes it, then creates one in the world as a "Drop"
    public void DropItem(string itemName)
    {
        for (int i = 0; i < itemSOs.Length; i++)
        {
            if (itemSOs[i].itemName == itemName)
            {
                itemSOs[i].DropItem();
            }
        }
    }

    //adds item to the inventory
    public int AddItem(string itemName, int qty, Sprite itemSprite, string itemDescription, bool unstackable)
    {
        for (int i = 0; i<itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false && itemSlot[i].itemName == itemName || itemSlot[i].qty <= 0 || itemName == "")
            {
                int leftOverItems = itemSlot[i].AddItem(itemName, qty, itemSprite, itemDescription, unstackable);
                if (leftOverItems > 0)
                {
                    leftOverItems = AddItem(itemName, leftOverItems, itemSprite, itemDescription, unstackable);
                }
                return leftOverItems;
            }
        }
        return qty;
    }

    //called to delselect slots
    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }

}
