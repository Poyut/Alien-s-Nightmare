﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float Speed;
    [SerializeField]
    private GameObject HitParticles;

    private Rigidbody2D r2;

    void Start()
    {
        r2 = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        r2.transform.Translate(0,Speed * Time.deltaTime,0);

        Destroy(this.gameObject, 2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss")
        {
            Instantiate(HitParticles, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
