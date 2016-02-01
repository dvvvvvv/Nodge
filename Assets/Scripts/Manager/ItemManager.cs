using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour {

    private List<Item> items;
    public GameObject[] itemPrefabs;

    public Transform space;
    public Transform outerSpace;

    public int initialItemCount;

    public PlayerCometMove player;
    public CometManager cometManager;

    void Awake()
    {
        items = new List<Item>();


        for(int i = 0;i<initialItemCount;i++)
        {
            CreateItem();
        }
        
    }

    public void CreateItem()
    {
        if (itemPrefabs.Length == 0) return;

        bool isntGoodPosition = true;
        int itemNumber = Random.Range(0, itemPrefabs.Length);
        Vector3 itemPosition = Vector3.zero;

        while (isntGoodPosition)
        {
            itemPosition = new Vector3(Random.Range((-outerSpace.lossyScale.x + 7) / 2, (outerSpace.lossyScale.x - 7) / 2), Random.Range((-outerSpace.lossyScale.y + 7) / 2, (outerSpace.lossyScale.y - 7) / 2), 0);

            if (Mathf.Abs(itemPosition.x) > (space.lossyScale.x + 7) / 2 ||
                Mathf.Abs(itemPosition.y) > (space.lossyScale.y + 7) / 2)
                isntGoodPosition = false;
        }

        Item item = GameObject.Instantiate(itemPrefabs[itemNumber]).GetComponent<Item>();
        item.transform.parent = transform;
        item.transform.position = itemPosition;
        item.itemManager = this;
        item.player = player;
        item.cometManager = cometManager;

        //items.Add(item);
    }

    public void ItemUsed(Item usedItem)
    {
        items.Remove(usedItem);
        GameObject.Destroy(usedItem.gameObject);
        CreateItem();
    }

}
