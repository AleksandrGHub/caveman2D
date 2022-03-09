using UnityEngine;

[RequireComponent (typeof(Collider2D))]
public class Coin : MonoBehaviour
{
    private Collider2D _collider2D;
    private float _delay = 0.5f;

    private void Start()
    {
        _collider2D = GetComponent<Collider2D>();
        _collider2D.enabled = false;
    }

    private void Update()
    {
        if (_delay <= 0)
        {
            _collider2D.enabled = true;
        }
        else
        {
            _delay -= Time.deltaTime;
        }
    }
}