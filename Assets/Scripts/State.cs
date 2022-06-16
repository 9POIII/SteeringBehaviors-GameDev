using System;
using UnityEngine;

namespace Project.Scripts {

    
    [Serializable]
    public class State {
        [SerializeField] private DesiredVelocityProvider velocityProvider;
        [SerializeField] private float velocityLimit;
        public float VelocityLimit => velocityLimit;
        public DesiredVelocityProvider VelocityProvider => velocityProvider;
        
    }
}