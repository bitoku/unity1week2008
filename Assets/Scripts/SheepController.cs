using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SheepController : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 _direction;
    private DogController _dog;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float changeRate;
    [SerializeField] private float restitution;
    
    void Start()
    {
        _direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * minSpeed;
        _dog = FindObjectOfType<DogController>();
    }

    // Update is called once per frame
    void Update()
    {
        DiedIfOutside();
        _direction *= 0.999f;
        if (_direction.magnitude < minSpeed)
        {
            _direction = _direction.normalized * minSpeed;
        }
        ChangeDirection();
        DistantFromDog();
        Vector3 direction3 = Vector3.ClampMagnitude(_direction, maxSpeed);
        transform.Translate(direction3);
    }

    private void ChangeDirection()
    {
        var randomVector = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        _direction += ((Vector2)transform.position + randomVector).normalized * changeRate;
        _direction = Vector2.ClampMagnitude(_direction * 0.5f, maxSpeed);
    }
    
    private void DistantFromDog()
    {
        var vecFromDog = transform.position - _dog.transform.position;
        var r = vecFromDog.magnitude + 0.01f;
        _direction = Vector2.ClampMagnitude(_direction + restitution * (Vector2) vecFromDog.normalized / (r * r), maxSpeed);
    }

    private void DiedIfOutside()
    {
        if (((Vector2) transform.position).magnitude < 4.9f) return;
        Destroy(gameObject);
    }
    
}
