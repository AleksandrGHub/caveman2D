using UnityEngine;

public class SpawnCoins : MonoBehaviour
{
    [SerializeField] private Coin _prefab;

    private int _quantityCoin = 3;
    private float _minForceX = -110f;
    private float _maxForceX = 110f;
    private float _minForceY = 100f;
    private float _maxForceY = 250f;

    public void Spawn()
    {
        for (int i = 0; i< _quantityCoin; i++)
        {
            Coin coin = Instantiate(_prefab, transform.position, Quaternion.identity);
            float xForce = Random.Range(_minForceX, _maxForceX);
            float yForce = Random.Range(_minForceY, _maxForceY);
            coin.GetComponent<Rigidbody2D>().AddForce(new Vector2(xForce,yForce));
        }
    }
}