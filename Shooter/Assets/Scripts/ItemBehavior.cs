using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    private float MaxLifeTime = 5f;
    private SpriteRenderer spRender;

    private void Awake()
    {
        spRender = GetComponent<SpriteRenderer>();
        StartCoroutine(Flash());
    }

    private IEnumerator Flash()
    {
        for (int i = 0; i < 2; i++)
        {
            spRender.enabled = false;
            yield return new WaitForSeconds(0.2f);
            spRender.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }
    }

    void FixedUpdate()
    {
        Destroy(this.gameObject, MaxLifeTime);
    }
}
