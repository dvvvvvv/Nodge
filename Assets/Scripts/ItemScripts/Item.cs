using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    public PlayerCometMove player;
    public CometManager cometManager;
    public ItemManager itemManager;

    protected virtual void ItemEffect()
    {
        
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        string othersTag = other.tag;

        if (othersTag.Equals("Player"))
        {
            ItemEffect();
            itemManager.ItemUsed(this);
        }
    }


}
