using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class Caveman : MonoBehaviour
{
    [SerializeField] private float _startTimeBetweenAttack;
    [SerializeField] private LayerMask _ground;

    private AttackPosition _attackPosition;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _sprite;
    private bool _isAttack = true;
    private float _speed = 0.9f;
    private float _delay=0.7f;
    private float _jumpForce = 6.5f;
    private const string Fire1 = "Fire1";
    private const string Fire2 = "Fire2";
    private const string Jump = "Jump";
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

    private void FixedUpdate()
    {
        if (CheckGround() && Input.GetButtonDown(Jump))
        {
            Hop();
        }
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

        if (Input.GetButton(Horizontal))
        {
            if (Input.GetKey(left_shift))
            {
                Walk(2);
                _animator.SetBool(AnimatorController.State.Run, true);
            }
            else
            {
                _animator.SetBool(AnimatorController.State.Run, false);
            }
            Walk(1);
            _animator.SetBool(AnimatorController.State.Walk, true);
        }
        else
        {
            _animator.SetBool(AnimatorController.State.Run, false);
            _animator.SetBool(AnimatorController.State.Walk, false);
        }
    }

    private void Walk(int dynamics)
    {
        Vector3 direction = transform.right * Input.GetAxis(Horizontal);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, _speed * dynamics * Time.deltaTime);
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
        float posX = _attackPosition.transform.localPosition.x;
        float posY = _attackPosition.transform.localPosition.y;
        if ((Input.GetAxis(Horizontal) < 0 && posX > 0) | (Input.GetAxis(Horizontal) > 0 && posX < 0))
        {
            flipAttackPos = -1;
        }
        _attackPosition.transform.localPosition = new Vector3(posX * flipAttackPos, posY, 0);
    }

    private bool CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector3(transform.position.x+0.07f, transform.position.y + 0.15f, 0), 0.26f, _ground);
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