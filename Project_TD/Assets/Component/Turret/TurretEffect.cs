using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  TurretEffect : ScriptableObject
{
    //this dicates what happens once a bullet intereacts.
    //deal damage, apply fire, apply cold

    public abstract void ApplyEffect(GameObject target);

}
//there is also bullet behavior.