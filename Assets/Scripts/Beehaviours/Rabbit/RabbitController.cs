using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using MyBox;
using UnityEngine;

public class RabbitController : Animal
{
    [SerializeField] private float m_RadiusOfDetectEnemy;
    //[SerializeField] private CircleCollider2D m_CircleCollider2D;
    [SerializeField] private Flee m_Flee;
    [SerializeField] private Wander m_Wander;
    
    private void FixedUpdate()
    {
        var results = new List<Collider2D>();
        Physics2D.OverlapCircle(transform.position, m_RadiusOfDetectEnemy, new ContactFilter2D().NoFilter(), results);
        //results.RemoveAll(result => result.TryGetComponent<WolfController>(out _));
        //results.RemoveAll(result => result.TryGetComponent<RabbitController>(out _));
        //results.RemoveAll(result => result.TryGetComponent<DeerController>(out _));
        //results.RemoveAll(result => result.TryGetComponent<PlayerController>(out _));
        results.Remove(Collider2D);

        if (results.Count > 0) {
            m_Flee.fleeObjects = results.Select(obj => obj.transform.position.ToVector2()).ToList();
            m_Wander.Weight = 0.5f;
            m_Flee.Weight = 1f;
        }
        else {
            m_Wander.Weight = 0.7f;
            m_Flee.Weight = 0f;
            m_Flee.fleeObjects.Clear();
        }
        
        /**for (int i = 0; i < m_ObjectsToFlee.Length; i++)
        {
            if (Vector2.Distance(this.gameObject.transform.position, m_ObjectsToFlee[i].transform.position) <= m_RadiusOfDetectEnemy)
            {
                m_Wander.Weight = 0.5f;
                m_Flee.Weight = 1f;
            }
            else
            {
                m_Wander.Weight = 0.7f;
                m_Flee.Weight = 0f;
            }
        }**/

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

