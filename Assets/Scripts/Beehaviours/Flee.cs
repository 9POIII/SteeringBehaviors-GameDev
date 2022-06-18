using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class Flee : DesiredVelocityProvider
{
    public List<Vector2> fleeObjects;
    
    public override Vector2 GetDesiredVelocity() {
        var result = Vector2.zero;
        foreach (var objectToFlee in fleeObjects) {
            result += -(objectToFlee - transform.position.ToVector2()).normalized * Animal.VelocityLimit;
        }
        return result.normalized * Animal.VelocityLimit;
    }
}
