using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField]
    private float bgSpeed;
    [SerializeField]
    private Renderer bgRend;

    void Start()
    {
        
    }


    void Update()
    {
        bgRend.material.mainTextureOffset += new Vector2(0f,bgSpeed * Time.deltaTime);
    }
}
