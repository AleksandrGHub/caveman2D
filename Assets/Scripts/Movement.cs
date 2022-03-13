using UnityEngine;

public class Movement : MonoBehaviour
{
    private Vector3[] _points;
    private float _rangePatrol = 0.8f;
    private int _countPoints = 5;
    private int _number;
    private int _speed;
    private int _minSpeed = 1;
    private int _maxSpeed = 4;

    private void Start()
    {
        _speed = Random.Range(_minSpeed, _maxSpeed);
        _points = new Vector3[_countPoints];
        int angleStep = 360 / _countPoints;

        for (int i = 0; i < _countPoints; i++)
        {
            _points[i] = new Vector3(_rangePatrol * Mathf.Cos(angleStep * i * Mathf.Deg2Rad), _rangePatrol * Mathf.Sin(angleStep * i * Mathf.Deg2Rad)) + transform.position;
        }
    }

    private void Update()
    {
        Vector3 target = _points[_number];
        transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);

        if (transform.position == target)
        {
            _speed = Random.Range(_minSpeed, _maxSpeed);
            _number++;
            if (_number >= _points.Length)
            {
                _number = 0;
            }
        }
    }
}