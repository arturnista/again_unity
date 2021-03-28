using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack_Shake : EnemyMeleeAttack
{

    protected override void Damage()
    {
        base.Damage();
        ScreenShake.Shake(.1f, .2f);
    }
    
}
