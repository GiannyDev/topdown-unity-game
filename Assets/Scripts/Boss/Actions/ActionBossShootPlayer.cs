using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Boss/Actions/Shoot Player", fileName = "ActionBossShootPlayer")]
public class ActionBossShootPlayer : AIAction
{
    public float shotDelay = 3f;
    private float nextShotTime;
    
    public override void Act(StateController controller)
    {
        Shoot(controller);   
    }

    private void Shoot(StateController controller)
    {
        if (Time.time > nextShotTime)
        {
            if (controller.PlayerHealth.CurrentHealth >= 7)
            {
                controller.BossSpiralPattern.EnableProjectile();
            }

            if (controller.PlayerHealth.CurrentHealth >= 4 && controller.PlayerHealth.CurrentHealth < 7)
            {
                controller.BossRandomPattern.EnableProjectile();
            }

            if (controller.PlayerHealth.CurrentHealth < 4)
            {
                controller.BossCirclePattern.EnableProjectile();
            }
            
            nextShotTime = Time.time + shotDelay;
        }
    }

    private void OnEnable()
    {
        nextShotTime = 0f;
    }
}
