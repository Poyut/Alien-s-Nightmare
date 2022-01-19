using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopShipBehavior : MonoBehaviour
{
    private Rigidbody2D r2;
    private float Speed = 0.2f;
    private bool Direction;
    void Start()
    {
        r2 = GetComponent<Rigidbody2D>();

        InvokeRepeating("Movement",0.5f,0.6f);
    }

    void Update()
    {
    }

    private void Movement()
    {
        if (Direction)
        {
            r2.velocity = (new Vector2(0, 1) * Speed);
            Direction = false;
        }
        else if (!Direction)
        {
            r2.velocity = (new Vector2(0, 1) * -Speed);
            Direction = true;
        }
    }
}
