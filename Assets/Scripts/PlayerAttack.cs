using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform _attackPosition;
    [SerializeField] private float _attackRange;

    private void OnAttack()
    {
        Collider2D[] enemis = Physics2D.OverlapCircleAll(_attackPosition.position, _attackRange);
        for (int i = 0; i < enemis.Length; i++)
        {
            if (enemis[i].GetComponent<Enemy>())
            {
                enemis[i].GetComponent<Enemy>().TakeDamage();
                enemis[i].GetComponent<SpawnCoins>().Spawn();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPosition.position, _attackRange);
    }
}