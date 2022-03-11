using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class Caveman : MonoBehaviour
{
    [SerializeField] private LayerMask _ground;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _sprite;
    private AttackPosition _attackPosition;
    private bool _isAttack = true;
    private float _speed;
    private float _delay=0.7f;
    private float _jumpForce = 6.5f;

    private const string Jump = "Jump";
    private const string Fire1 = "Fire1";
    private const string Fire2 = "Fire2";
    private const string Horizontal = "Horizontal";
    private const string left_shift = "left shift";

    public static class AnimatorController
    {
        public static class State
        {
            public const string Run = "Run";
            public const string Walk = "Walk";
            public const string Jump = "Jump";
            public const string FistSlam = "FistSlam";
            public const string PunchAttack = "PunchAttack";
        }
    }

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _attackPosition = GetComponentInChildren<AttackPosition>();
    }

    private void Update()
    {
        if (_isAttack)
        {
            if (Input.GetButtonDown(Fire1))
            {
                StartCoroutine(PunchAttack());
            }
            if (Input.GetButtonDown(Fire2))
            {
                StartCoroutine(FistSlam());
            }
        }

        if (CheckGround() && Input.GetButtonDown(Jump))
        {
            Hop();
        }

        if (Input.GetButton(Horizontal))
        {
            if (Input.GetKey(left_shift))
            {
                _speed = 2f;
                Walk();
                _animator.SetBool(AnimatorController.State.Run, true);
            }
            else
            {
                _animator.SetBool(AnimatorController.State.Run, false);
            }
            _speed = 1f;
            Walk();
            _animator.SetBool(AnimatorController.State.Walk, true);
        }
        else
        {
            _animator.SetBool(AnimatorController.State.Run, false);
            _animator.SetBool(AnimatorController.State.Walk, false);
        }
    }

    private void Walk()
    {
        Vector3 direction = transform.right * Input.GetAxis(Horizontal);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, _speed * Time.deltaTime);
        _sprite.flipX = direction.x > 0.0F;
        FlipAtackPosition();
    }

    private void Hop()
    {
        _rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
        _animator.SetTrigger(AnimatorController.State.Jump);
    }

    private void FlipAtackPosition()
    {
        int flipAttackPos = 1;
        float positionColliderX = _attackPosition.transform.localPosition.x;
        float positionColliderY = _attackPosition.transform.localPosition.y;
        if ((Input.GetAxis(Horizontal) < 0 && positionColliderX > 0) | (Input.GetAxis(Horizontal) > 0 && positionColliderX < 0))
        {
            flipAttackPos = -1;
        }
        _attackPosition.transform.localPosition = new Vector3(positionColliderX * flipAttackPos, positionColliderY);
    }

    private bool CheckGround()
    {
        float positionColliderX = transform.position.x + 0.07f;
        float positionColliderY = transform.position.y + 0.15f;
        float radiusCollider = 0.26f;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector3(positionColliderX, positionColliderY), radiusCollider, _ground);
        return colliders.Length > 0;
    }

    private IEnumerator PunchAttack()
    {
        _isAttack = false;
        _animator.SetTrigger(AnimatorController.State.PunchAttack);
        yield return new WaitForSeconds(_delay);
        _isAttack = true;
    }

    private IEnumerator FistSlam()
    {
        _isAttack = false;
        _animator.SetTrigger(AnimatorController.State.FistSlam);
        yield return new WaitForSeconds(_delay);
        _isAttack = true;
    }
}