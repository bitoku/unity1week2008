using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorInputController : MonoBehaviour
{
    private Camera _camera;
    private bool _enable;
    private SpriteRenderer _spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        var circle = GameObject.Find("CursorCircle");
        _spriteRenderer = circle.GetComponent<SpriteRenderer>();
        DisableMove();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsTouch()) return;
        EnableMove();
        var screenPos = TouchPos();
        var worldPos = _camera.ScreenToWorldPoint((Vector3) screenPos);
        worldPos.z = 0;
        transform.position = worldPos;
    }

    private static bool IsTouch()
    {
        return Input.GetMouseButtonDown(0) || Input.touchCount > 0;
    }

    private static Vector2 TouchPos()
    {
        if (Input.GetMouseButtonDown(0))
        {
            return Input.mousePosition;
        }

        if (Input.touchCount > 0)
        {
            return Input.GetTouch(0).position;
        }
        
        throw new NotImplementedException();
    }

    void EnableMove()
    {
        _enable = true;
        Intransparency();
    }

    public void DisableMove()
    {
        _enable = false;
        Transparency();
    }

    public bool IsEnabled()
    {
        return _enable;
    }

    private void Transparency()
    {
        var material = _spriteRenderer.material;
        var c = material.color;
        c.a = 0f;
        material.color = c;
    }

    private void Intransparency()
    {
        var material = _spriteRenderer.material;
        var c = material.color;
        c.a = 1f;
        material.color = c;
    }
}
