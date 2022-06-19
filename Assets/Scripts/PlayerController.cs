using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera MainCamera;  
    public float MoveSpeed;
    public Rigidbody2D Rigidbody;
    public Weapon _Weapon;

    private Vector2 m_moveDirection;
    private Vector2 m_mousePosition;

    public bool m_isReloading = false;
    
    void Update()
    {
        ProcessInputs();
    }
    private void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        float MoveX = Input.GetAxisRaw("Horizontal");
        float MoveY = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(0))
        {
            if (_Weapon.m_currentAmmo <= 0)
            {
                if (m_isReloading)
                {
                    return;
                }
                StartCoroutine(_Weapon.Reload());
                return;
            }
            _Weapon.Fire();
        }

        m_moveDirection = new Vector2(MoveX, MoveY).normalized;

        m_mousePosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void Move()
    {
        Rigidbody.velocity = new Vector2(m_moveDirection.x * MoveSpeed, m_moveDirection.y * MoveSpeed);

        Vector2 AimDirecrion = m_mousePosition - Rigidbody.position;
        float AimAngle = Mathf.Atan2(AimDirecrion.y, AimDirecrion.x) * Mathf.Rad2Deg - 90f;
        Rigidbody.rotation = AimAngle;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "DeathZone": Destroy(gameObject); break;
            case "Wolf" : Destroy(gameObject); break;
        }
    }
}
