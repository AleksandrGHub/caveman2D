using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform _attackPosition;
    [SerializeField] private LayerMask _enemy;
    [SerializeField] private float _attackRange;

    private void OnAttack()
    {
        Collider2D[] enemis = Physics2D.OverlapCircleAll(_attackPosition.position, _attackRange, _enemy);
        for (int i = 0; i < enemis.Length; i++)
        {
            enemis[i].GetComponent<Enemy>().TakeDamage();
            enemis[i].GetComponent<SpawnCoins>().Spawn();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPosition.position, _attackRange);
    }
}