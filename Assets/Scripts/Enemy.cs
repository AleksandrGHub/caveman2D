using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(SpawnCoins))]
[RequireComponent(typeof(CircleCollider2D))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private SpawnCoins _spawnCoins;

    private Animator _animator;
    private Movement _movement;
    private CircleCollider2D _collider;
    private float _delayDestoy = 1.5f;

    public SpawnCoins SpawnCoins => _spawnCoins;

    public static class AnimatorController
    {
        public static class State
        {
            public const string EnemyGetHit = "EnemyGetHit";
        }
    }

    private void Start()
    {
        _movement = GetComponent<Movement>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<CircleCollider2D>();
    }

    public void TakeDamage()
    {
        _movement.enabled = false;
        _animator.SetBool(AnimatorController.State.EnemyGetHit, true);
        _collider.enabled = false;
        Destroy(gameObject, _delayDestoy);
    }
}