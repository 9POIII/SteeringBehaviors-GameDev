using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class Wander : DesiredVelocityProvider
{
    [SerializeField, Range(0.5f, 5)] private float circleDistance = 1;
    [SerializeField, Range(0.5f, 5)] private float circleRadius = 2;
    [SerializeField, Range(1, 80)] private int angleChangeStep = 15;
    private int m_Angle = 0;

    public override Vector2 GetDesiredVelocity() {
        var rnd = Random.value;
        if (rnd < 0.5) {
            m_Angle += angleChangeStep;
        }
        else if (rnd < 1) {
            m_Angle -= angleChangeStep;
        }

        var velocityNormalized = Animal.Velocity.normalized;
        var velocity = velocityNormalized.magnitude == 0 ? Animal.VelocityLimit * velocityNormalized : velocityNormalized;
        var futurePos = Animal.transform.position.ToVector2() + velocity * circleDistance;
        var vector = new Vector2(Mathf.Cos(m_Angle * Mathf.Deg2Rad), Mathf.Sin(m_Angle * Mathf.Deg2Rad)) * circleRadius;

        return (futurePos + vector - transform.position.ToVector2()).normalized * Animal.VelocityLimit;
    }
}
