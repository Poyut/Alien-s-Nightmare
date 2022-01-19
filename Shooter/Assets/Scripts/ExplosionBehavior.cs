using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehavior : MonoBehaviour
{
    void FixedUpdate()
    {
        Destroy(this.gameObject,0.4f);
    }
}
