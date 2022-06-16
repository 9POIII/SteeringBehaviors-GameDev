using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D Rigidbody;

    private void Start()
    {
        StartCoroutine(DestroyBullet());
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Wall": Destroy(gameObject); break;
            case "Enemy": Debug.Log("Enemy was killed"); Destroy(gameObject); break;
        }
    }

    public IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
