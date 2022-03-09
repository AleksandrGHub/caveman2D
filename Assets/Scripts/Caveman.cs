using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class Caveman : MonoBehaviour
{
    [SerializeField] private float _startTimeBtwAttack;
    [SerializeField] private LayerMask _ground;

    private AttackPos _attackPos;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _sprite;
    private bool _isGrounded;
    private float _speed = 0.9f;
    private float _timeBtwAttack;
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
        _attackPos = GetComponentInChildren<AttackPos>();

        _sprite = GetComponent<SpriteRenderer>();

        _animator = GetComponent<Animator>();

        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        CheckGround();
    }
    private void Update()
    {
        if (_timeBtwAttack <= 0)
        {
            if (Input.GetButtonDown(Fire1))
            {
                _animator.SetTrigger(AnimatorController.State.PunchAttack);

                _timeBtwAttack = _startTimeBtwAttack;
            }
            if (Input.GetButtonDown(Fire2))
            {
                _animator.SetTrigger(AnimatorController.State.FistSlam);
                    
                _timeBtwAttack = _startTimeBtwAttack;
            }
        }
        else
        {
            _timeBtwAttack -= Time.deltaTime;
        }

        if (_isGrounded && Input.GetButtonDown(Jump))
        {
            JumpUp();
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

        FlipAtackPos();
    }
    private void JumpUp()
    {
        _rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);

        _animator.SetTrigger(AnimatorController.State.Jump);
    }
    private void FlipAtackPos()
    {
        int flipAttackPos = 1;
        float posX = _attackPos.transform.localPosition.x;
        float posY = _attackPos.transform.localPosition.y;

        if ((Input.GetAxis(Horizontal) < 0 && posX > 0) | (Input.GetAxis(Horizontal) > 0 && posX < 0))
        {
            flipAttackPos = -1;
        }
        _attackPos.transform.localPosition = new Vector3(posX * flipAttackPos, posY, 0);
    }
    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector3(transform.position.x+0.07f, transform.position.y + 0.15f, 0), 0.26f, _ground);

        _isGrounded = colliders.Length > 0;
    }
}