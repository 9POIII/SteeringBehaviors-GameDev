using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEngine;

public abstract class DesiredVelocityProvider : MonoBehaviour
{
    [SerializeField, Range(0,3)] private float weight = 1f;
    
    public float Weight
    {
        get => weight;
        set => weight = value;
    }
    
        
    protected Animal Animal;

    public void Awake()
    {
        Animal = GetComponent<Animal>();
    }

    public abstract Vector2 GetDesiredVelocity();
}

