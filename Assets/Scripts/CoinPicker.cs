using UnityEngine;
using TMPro;

public class CoinPicker : MonoBehaviour
{
    [SerializeField] private TMP_Text _quantity—oins;

    private int _coins = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Coin>())
        {
            _coins++;
            _quantity—oins.text = _coins.ToString();
            Destroy(collision.gameObject);
        }
    }
}