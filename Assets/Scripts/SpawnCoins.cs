using UnityEngine;

public class SpawnCoins : MonoBehaviour
{
    [SerializeField] private GameObject _coinPrefab;

    public void Spawn()
    {
        for (int i = 0; i< 3; i++) 
        {
            GameObject coin = Instantiate(_coinPrefab, transform.position, Quaternion.identity);

            float xForce = Random.Range(-110f, 110f);

            float yForce = Random.Range(100f, 250f);

            coin.GetComponent<Rigidbody2D>().AddForce(new Vector2(xForce,yForce));
        }
    }
}