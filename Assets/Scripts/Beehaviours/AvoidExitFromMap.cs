using System;
using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;

public class AvoidExitFromMap : DesiredVelocityProvider
{
    [SerializeField] private Transform topLeftDot;
    [SerializeField] private Transform bottomRightDot;

    private float edge = 0.05f;

    public void Start()
    {
        topLeftDot = GameObject.FindGameObjectWithTag("TLD").transform;
        bottomRightDot = GameObject.FindGameObjectWithTag("BRD").transform;
    }

    public Vector2 GetPositionInRelativeFormat(Vector2 position) {
        var topLeftPos = topLeftDot.position;
        var bottomRightPos = bottomRightDot.position;
        var relativeX = (position.x - topLeftPos.x) / (bottomRightPos.x - topLeftPos.x);
        var relativeY= (position.y - bottomRightPos.y) / (topLeftPos.y - bottomRightPos.y);
        return new Vector2(relativeX, relativeY);
    }
    public override Vector2 GetDesiredVelocity()
    {
        var point = GetPositionInRelativeFormat(transform.position.ToVector2());
        var maxSpeed = Animal.VelocityLimit;
        var result = Vector2.zero;

        if (point.x > 1 - edge) {
            result += new Vector2(-maxSpeed, 0);
        }
        if (point.x < edge) {
            result += new Vector2(maxSpeed, 0);
        }

        if (point.y > 1 - edge) {
            result += new Vector2(0, -maxSpeed);
        }
        if (point.y < edge) {
            result += new Vector2(0, maxSpeed);
        }

        return result.magnitude == 0 ? Animal.Velocity : result.normalized * maxSpeed;
    }
}

