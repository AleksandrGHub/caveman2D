using UnityEngine;

public class SpawnCoins : MonoBehaviour
{
    [SerializeField] private Coin _prefab;

    private int _quantityCoin = 3;

    public void Spawn()
    {
        for (int i = 0; i< _quantityCoin; i++)
        {
            Coin coin = Instantiate(_prefab, transform.position, Quaternion.identity);
            float xForce = Random.Range(-110f, 110f);
            float yForce = Random.Range(100f, 250f);
            coin.GetComponent<Rigidbody2D>().AddForce(new Vector2(xForce,yForce));
        }
    }
}