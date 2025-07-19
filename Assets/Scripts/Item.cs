using UnityEngine;

public class Item : MonoBehaviour
{
    //serialized field allows naming and editing of item name
    [SerializeField]
    private string itemName;

    //flavortext and stats
    [TextArea]
    [SerializeField]
    private string itemDescription;

    [SerializeField]
    private int qty;
    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private bool unstackable = false;

    private InventoryManager InventoryManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InventoryManager = GameObject.Find("Canvas").GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //checks player tag
        
        if(other.gameObject.tag == "Player")
        {
            //addItem method to communicate with inventoryManagement
            //create the item in inventory
            //then destroy in the world to "pick up"
            int leftOverItems = InventoryManager.AddItem(itemName, qty, sprite, itemDescription, unstackable);
            if (leftOverItems <= 0)
            {
                Destroy(gameObject);
            }
            else qty = leftOverItems;
        }
    }
}
