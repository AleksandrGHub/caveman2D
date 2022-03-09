using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform attackPos;
    [SerializeField] private LayerMask enemy;
    [SerializeField] float attackRange;
    private void OnAttack()
    {
        Collider2D[] enemis = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);

        for (int i = 0; i < enemis.Length; i++)
        {
            enemis[i].GetComponent<Enemy>().TakeDamage();

            enemis[i].GetComponent<SpawnCoins>().Spawn();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}