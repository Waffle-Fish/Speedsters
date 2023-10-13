using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrezeItemEffect : ItemCore
{
   public override IEnumerator ActivateEffect()
    {
        enemyMovement.enabled = false;
        enemyRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(effectDuration);
        enemyRigidBody.constraints = RigidbodyConstraints2D.None;
        enemyRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        enemyMovement.enabled = true;
    }
}
