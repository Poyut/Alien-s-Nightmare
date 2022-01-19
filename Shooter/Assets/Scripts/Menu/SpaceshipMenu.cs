using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipMenu : MonoBehaviour
{
    private Rigidbody2D r2;
    private float Speed = 5f;
    private Transform ObjectTransform;

    void Awake()
    {
        r2 = GetComponent<Rigidbody2D>();
        ObjectTransform = GetComponent<Transform>();
    }

    void Update()
    {
        r2.velocity = (new Vector2(1, 0.5f) * Speed);
        //ObjectTransform.localScale = Vector3.);

        Destroy(this.gameObject,10f);
    }
}
