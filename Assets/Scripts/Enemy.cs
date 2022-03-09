using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CircleCollider2D))]
public class Enemy : MonoBehaviour
{
    private Animator _animator;
    private Movement _movement;
    private CircleCollider2D _collider;
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
        Destroy(gameObject,1.5f);
    }
}