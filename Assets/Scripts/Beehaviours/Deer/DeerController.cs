using System.Collections.Generic;
using System.Linq;
using MyBox;
using UnityEngine;


public class DeerController : Animal
{
    [SerializeField] private float m_RadiusOfDetectPlayer;
    [SerializeField] private float m_RadiusOfDetectWolf;
    [SerializeField] private List<DeerController> m_DeerControllers;
    [SerializeField] private Seek m_Seek;
    [SerializeField] private Wander m_Wander;
    [SerializeField] private Flee m_Flee;
    
    
    private void FixedUpdate()
    {
        /**var position = transform.position;
        var player = new List<Collider2D>();
        var wolfs = new List<Collider2D>();
        Physics2D.OverlapCircle(transform.position, m_RadiusOfDetectEnemy, new ContactFilter2D().NoFilter(), wolfs);
        wolfs = wolfs.Where(animal => animal.TryGetComponent<WolfController>(out _)).ToList();
        //Vector2 averageSpeed = m_DeerControllers.Select(deer => deer.Velocity).ToList().Average();
        Physics2D.OverlapCircle(position, m_RadiusOfDetectEnemy, new ContactFilter2D().NoFilter(), player);
        player = player.Where(hunter => hunter.TryGetComponent<PlayerController>(out _)).ToList();


        if (wolfs.Count > 0)
        {
            //m_Flee.fleeObjects = enemies.Select(obj => obj.transform.position.ToVector2()).ToList();
            m_Wander.Weight = 0.2f;
            m_Flee.Weight = 1f;
            m_Flee.fleeObjects.AddRange(wolfs.Select(wolf =>
                wolf.transform.position.ToVector2()));

        }

        if (player.Count > 0)
        {
            //m_Flee.fleeObjects = enemies.Select(obj => obj.transform.position.ToVector2()).ToList();
            m_Wander.Weight = 0.2f;
            m_Flee.Weight = 1f;
            m_Flee.fleeObjects.AddRange(player.Select(hunter => hunter.transform.position.ToVector2()));
        }   
        
        else {
            m_Wander.Weight = 0.7f;
            m_Flee.Weight = 0f;
            /**var centerOfStado = m_DeerControllers.Select(deer => deer.transform.position.ToVector2()).ToList().Average();
            m_Seek.objectsToFollow.Add(centerOfStado);
            //var averageSpeed = m_DeerControllers.Select(deer => deer.Velocity).ToList().Average();
            //m_Seek.objectsToFollow.Add(averageSpeed);
            m_Flee.fleeObjects.AddRange(m_DeerControllers.Except(m_DeerControllers).Select(deer =>
                deer.transform.position.ToVector2()));
            m_Flee.fleeObjects.Clear();}**/

        var position = this.transform.position;
        FindPlayers(position, out var players);
        FindWolfs(position, out var wolfs);
        
        m_Flee.fleeObjects.Clear();
        m_Seek.objectsToFollow.Clear();

        if (wolfs.Count > 0 || players.Count > 0)
        {
            FleeState(wolfs, players);
        }
        else
        {
            WanderState();
        }
    }

    private void WanderState()
    {
        m_Wander.Weight = 0.7f;
        m_Flee.Weight = 0f;
        m_Seek.Weight = 0.4f;
        
        var centerOfStado = m_DeerControllers.Select(doe => doe.transform.position.ToVector2()).ToList().Average();
        m_Seek.objectsToFollow.Add(centerOfStado);
        
        var averageSpeed = m_DeerControllers.Select(doe => doe.Velocity).ToList().Average();
        m_Seek.objectsToFollow.Add(averageSpeed);
        
        m_Flee.fleeObjects.AddRange(m_DeerControllers.Except(this).Select(deer => deer.transform.position.ToVector2()));
    }

    private void FleeState(List<Collider2D> wolfs, List<Collider2D> players)
    {
        m_Wander.Weight = 0f;
        m_Seek.Weight = 0f;
        m_Flee.Weight = 1f;

        if (wolfs.Count > 0)
        {
            m_Flee.fleeObjects.AddRange(wolfs.Select(wolf => wolf.transform.position.ToVector2()));
        }

        if (players.Count > 0)
        {
            m_Flee.fleeObjects.AddRange(players.Select(player => player.transform.position.ToVector2()));
        }
    }

    private void FindWolfs(Vector2 position, out List<Collider2D> wolfs)
    {
        wolfs = new List<Collider2D>();
        Physics2D.OverlapCircle(position, m_RadiusOfDetectWolf, new ContactFilter2D().NoFilter(), wolfs);
        wolfs = wolfs.Where(animal => animal.TryGetComponent<WolfController>(out _)).ToList();
    }

    private void FindPlayers(Vector2 position, out List<Collider2D> players)
    {
        players = new List<Collider2D>();
        // huntersFilter.SetLayerMask(LayerMask.GetMask("Player"));
        Physics2D.OverlapCircle(position, m_RadiusOfDetectPlayer, new ContactFilter2D().NoFilter(), players);
        players = players.Where(hunter => hunter.TryGetComponent<PlayerController>(out _)).ToList();
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

