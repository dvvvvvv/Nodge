using UnityEngine;
using System.Collections;

public class SpeedUpItem : Item {

    public float speedIncrease = 2.0f;

    protected override void ItemEffect()
    {
        base.ItemEffect();
        player.moveSpeed += speedIncrease;
    }

}
