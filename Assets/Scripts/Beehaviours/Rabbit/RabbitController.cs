using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using MyBox;
using UnityEngine;
using Project.Scripts;

public class RabbitController : Animal
{
    [SerializeField] private float m_RadiusOfDetectEnemy;
    [SerializeField] private CircleCollider2D m_CircleCollider2D;
    [SerializeField] private Flee m_Flee;
    [SerializeField] private Wander m_Wander;
    //[SerializeField] private List<Vector2> m_ObjectsToFlee;
    [SerializeField] private Seek m_Seek;

    private void FixedUpdate()
    {
        var results = new List<Collider2D>();
        Physics2D.OverlapCircle(transform.position, m_RadiusOfDetectEnemy, new ContactFilter2D().NoFilter(), results);
        //results.Remove(collider);
        results.RemoveAll(result => result.TryGetComponent<WolfController>(out _)); 
        if (results.Count > 1) {
            m_Flee.fleeObjects = results.Select(obj => obj.transform.position.ToVector2()).ToList();
            m_Wander.Weight = 0.5f;
            m_Flee.Weight = 1f;
        }
        else {
            m_Wander.Weight = 0.7f;
            m_Flee.Weight = 0f;
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
            case "Bullet": gameObject.SetActive(false); break;
            case "Wolf" : Destroy(gameObject); break;
        }
    }
}

