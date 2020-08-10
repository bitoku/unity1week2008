using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorInputController : MonoBehaviour
{
    private Camera _camera;
    private bool _enable;
    
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _enable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsTouch()) return;
        EnableMove();
        Vector2 screenPos = TouchPos();
        Vector3 worldPos = _camera.ScreenToWorldPoint((Vector3) screenPos);
        worldPos.z = 0;
        transform.position = worldPos;
    }

    private static bool IsTouch()
    {
        if (Input.GetMouseButtonDown(0))
        {
            return true;
        }

        if (Input.touchCount > 0)
        {
            return true;
        }

        return false;
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
    }

    void DisableMove()
    {
        _enable = false;
    }

    public bool IsEnabled()
    {
        return _enable;
    }
}
