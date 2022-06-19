using System;
using System.Collections.Generic;
using MyBox;
using UnityEngine;


public class Animal : MonoBehaviour
{
        [SerializeField] private Rigidbody2D rigidbody2D;

        private Vector2 velocity;
        private Vector2 acceleration;
        
        [SerializeField] private float mass = 1;
        [SerializeField, Range(1, 20)] private float velocityLimit = 3;
        [SerializeField, Range(1, 50)] private float steeringForceLimit = 5;           

        private const float Epsilon = 0.05f;
        public float VelocityLimit => velocityLimit;
        public Vector2 Velocity => velocity;

        [SerializeField]
        protected Collider2D Collider2D;

        public void ApplyForce(Vector2 force)
        {
            force /= mass;
            acceleration += force;
        }

        protected void Update()
        {
            ApplyFriction();
            ApplySteeringForce();
            ApplyForces();

            void ApplyFriction()
            {
                var friction = -velocity.normalized * 0.5f;
                ApplyForce(friction);
            }

            void ApplySteeringForce()
            {
                var providers = GetComponents<DesiredVelocityProvider>();
                var steering = Vector2.zero;
                foreach (var provider in providers)
                {
                    var desiredVelocity = provider.GetDesiredVelocity() * provider.Weight; //
                    steering += desiredVelocity - velocity;
                        
                }
                rigidbody2D.velocity = Vector2.ClampMagnitude(rigidbody2D.velocity + Vector2.ClampMagnitude(steering, steeringForceLimit), VelocityLimit);
            }

            void ApplyForces()
            {
                velocity += acceleration * Time.deltaTime;
                velocity = Vector2.ClampMagnitude(velocity, velocityLimit);
                if (velocity.magnitude < Epsilon)
                {
                    velocity = Vector2.zero;
                    return;
                }
                
                var transformPosition = (Vector2)transform.position;
                transformPosition += velocity * Time.deltaTime;
                //transform.position += velocity * Time.deltaTime;
                acceleration = Vector2.zero;
                //transform.rotation = Quaternion.LookRotation(velocity);
            }
        }
}

