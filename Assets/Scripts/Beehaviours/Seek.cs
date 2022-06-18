using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MyBox;

public class Seek : DesiredVelocityProvider
{
    //[SerializeField] private Transform[] objectToFollow;
    [SerializeField, Range(0,10)] private float arriveRadius;
    //public GameObject objectToFollowNow;
    public List<Vector2> objectsToFollow;

    private void Start()
    {
        var rabbits = FindObjectsOfType<RabbitController>().Select(rabbit => rabbit.transform).ToList();
        //var does = FindObjectsOfType<DoeController>().Select(doe => doe.transform).ToList();
        Debug.Log(rabbits);
        var wolfs = FindObjectsOfType<WolfController>().Select(wolf => wolf.transform).ToList();
        objectsToFollow = rabbits.Union(wolfs).Select(_transform => _transform.position.ToVector2()).ToList();
    }

    public override Vector2 GetDesiredVelocity()
    {
        var result = Vector2.zero;
        foreach (var objectToFollow in objectsToFollow)
        {
            var distance = (objectToFollow - transform.position.ToVector2());
            float k = 1;
            if (distance.magnitude < arriveRadius)
            {
                k = distance.magnitude / arriveRadius;
            }

            result += distance.normalized * Animal.VelocityLimit * k;
        }

        return result;
    }
    
}
