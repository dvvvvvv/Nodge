using UnityEngine;
using System.Collections;

public class NPCometMove : CometMove {

    protected override void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        if (!IsInSpace)
        {
            Respawn();
        }
    }

    
        
}
