using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    [SerializeField] private float speed;

    private CursorInputController _cursor;
    // Start is called before the first frame update
    void Start()
    {
        _cursor = FindObjectOfType<CursorInputController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_cursor.IsEnabled()) return;
        Vector3 direction = (_cursor.transform.position - transform.position).normalized * speed;
        Debug.Log(direction);
        transform.Translate(direction);
    }
}
