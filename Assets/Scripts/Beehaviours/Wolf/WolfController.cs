using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using MyBox;


public class WolfController : Animal
{
    [SerializeField] private float m_RadiusOfDetectEnemy;
    [SerializeField] private Wander m_Wander;
    [SerializeField] private Seek m_Seek;
    //[SerializeField] private GameObject[] m_ObjectsToFollow;

    /**private void Start()
    {
        var index = Random.Range(0, m_ObjectsToFollow.Length);
        m_Seek.objectToFollowNow = m_ObjectsToFollow[index];
    }**/

    private void FixedUpdate()
    {
        var results = new List<Collider2D>();
        Physics2D.OverlapCircle(transform.position, m_RadiusOfDetectEnemy, new ContactFilter2D().NoFilter(), results);
        results.RemoveAll(result => result.TryGetComponent<WolfController>(out _));
        
        if (results.Count > 0) {
            var objectToFollow = results.MinBy(obj => Vector3.Distance(obj.transform.position, transform.position));
            m_Seek.objectsToFollow = new List<Vector2> {objectToFollow.transform.position.ToVector2()};
            m_Wander.Weight = 0.5f;
            m_Seek.Weight = 1f;
        }
        else {
            m_Wander.Weight = 0.7f;
            m_Seek.Weight = 0f;
        }
        
        /**for (int i = 0; i < m_ObjectsToFollow.Length; i++)
        {
            if (m_Seek.objectToFollowNow == null)
            {
                m_Wander.Weight = 1f;
                m_Seek.Weight = 0f;
            }
            else if (Vector2.Distance(this.gameObject.transform.position, m_ObjectsToFollow[i].transform.position) <= m_RadiusOfDetectEnemy)
            {
                m_Wander.Weight = 0.5f;
                m_Seek.Weight = 1f;
                m_Seek.objectToFollowNow = m_ObjectsToFollow[i];
            }

        }**/

    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Bullet": Destroy(gameObject); break;
        }
    }
}
