using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject Bullet;
    public Transform FirePoint;
    public float FireForce;

    [SerializeField] private int m_maxAmmo;
    [SerializeField] private int m_bulletInZapas;
    public int m_currentAmmo;

    public PlayerController PlayerController;

    private void Start()
    {
        if (m_currentAmmo == -1)
        {
            m_currentAmmo = m_maxAmmo;
        }
    }

    public IEnumerator Reload()
    {
        PlayerController.m_isReloading = true;
        yield return new WaitForSeconds(1f);
        m_currentAmmo = m_maxAmmo;
        PlayerController.m_isReloading = false;
    }

    public void Fire()
    {
        GameObject projectile = Instantiate(Bullet, FirePoint.position, FirePoint.rotation);
        projectile.GetComponent<Rigidbody2D>().AddForce(FirePoint.up * FireForce, ForceMode2D.Impulse);
        m_currentAmmo--;
    }
}
