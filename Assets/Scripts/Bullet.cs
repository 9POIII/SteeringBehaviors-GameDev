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
        Destroy(gameObject);
    }

    public IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
