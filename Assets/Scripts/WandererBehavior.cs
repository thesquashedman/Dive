using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandererBehavior : FishEnemyBehavior
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        mode = "wander";
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
