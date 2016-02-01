using UnityEngine;
using System.Collections;

public class CometItem : Item {

    protected override void ItemEffect()
    {
        base.ItemEffect();
        cometManager.AddComet();
    }
}
