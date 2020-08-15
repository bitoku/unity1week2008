using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSpriteController : MonoBehaviour
{
    private Material _material;
    private Vector3 _defaultScale;
    // Start is called before the first frame update
    void Start()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        _material = spriteRenderer.material;
        _defaultScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = _defaultScale * (1 + (float) Math.Sin(Time.time * 10) * 0.2f);
    }

    public void Transparency()
    {
        var c = _material.color;
        c.a = 0f;
        _material.color = c;
    }

    public void Intransparency()
    {
        var c = _material.color;
        c.a = 0.5f;
        _material.color = c;
    }
}
