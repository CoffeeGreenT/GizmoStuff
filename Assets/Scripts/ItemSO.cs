using UnityEngine;
[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public StatToChange statToChange = new StatToChange();
    public int amount;
    public enum StatToChange
    {
        None,
        Health
    };
    //method called to change values, i.e. hp
    public void UseItem()
    {
        //hp changes ex) heal/dmg
        if(statToChange ==StatToChange.Health)
        {
            PlayerHealth hp = GameObject.Find("Player").GetComponent<PlayerHealth>();
            GameObject.Find("Player").GetComponent<PlayerHealth>().Damage(amount);
        }
    }
    //look for more efficient way to do this
    public void DropItem()
    {
        GameObject itemToDrop = Resources.Load<GameObject>(itemName);
        Instantiate(itemToDrop, GameObject.FindWithTag("Player").transform.position + new Vector3(0, 0, 2), Quaternion.identity);
    }
}
