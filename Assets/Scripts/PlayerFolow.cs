using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFolow : MonoBehaviour
{
    private GameObject Player;
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, this.transform.position.z);
    }
}
