using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MyBox;


public class DeerController : Animal
{
    [SerializeField] private float m_RadiusOfDetectEnemy;
    [SerializeField] private List<DeerController> m_DeerControllers;
    [SerializeField] private Seek m_Seek;
    [SerializeField] private Wander m_Wander;
    [SerializeField] private Flee m_Flee;
    private void FixedUpdate()
    {
        var position = transform.position;
        var enemies = new List<Collider2D>();
        var filter = new ContactFilter2D().NoFilter();
        //Vector2 averageSpeed = m_DeerControllers.Select(deer => deer.Velocity).ToList().Average();

        Physics2D.OverlapCircle(position, m_RadiusOfDetectEnemy, filter, enemies);

        if (enemies.Count > 0)
        {
            m_Flee.fleeObjects = enemies.Select(obj => obj.transform.position.ToVector2()).ToList();
            m_Wander.Weight = 0.2f;
            m_Flee.Weight = 1f;
        }
        else {
            m_Wander.Weight = 0.7f;
            m_Flee.Weight = 0f;
            //Vector2 centerOfStado = m_DeerControllers.Select(deer => deer.transform.position.ToVector2()).ToList().Average();
            //m_Seek.objectsToFollow.Add(centerOfStado);
            //m_Seek.objectsToFollow.Add(averageSpeed);
            m_Flee.fleeObjects.AddRange(m_DeerControllers.Except(m_DeerControllers).Select(deer =>
                deer.transform.position.ToVector2()));
        }
    }
    
    
    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Bullet": Destroy(gameObject); break;
            case "Wolf" : Destroy(gameObject); break;
        }
    }
}

