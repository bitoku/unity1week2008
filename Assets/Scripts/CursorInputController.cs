using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorInputController : MonoBehaviour
{
    private Camera _camera;
    private bool _enable;
    private CursorSpriteController _cursorSpriteController;
    private float _fieldRadius;
    
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _cursorSpriteController = FindObjectOfType<CursorSpriteController>();
        var fieldCircle = GameObject.Find("FieldCircle");
        _fieldRadius = fieldCircle.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        DisableMove();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsTouch()) return;
        var screenPos = TouchPos();
        var worldPos = _camera.ScreenToWorldPoint(screenPos);
        if (((Vector2) worldPos).magnitude > _fieldRadius) return;
        worldPos.z = 10;
        EnableMove();
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
        _cursorSpriteController.Intransparency();
    }

    public void DisableMove()
    {
        _enable = false;
        _cursorSpriteController.Transparency();
    }

    public bool IsEnabled()
    {
        return _enable;
    }
}
