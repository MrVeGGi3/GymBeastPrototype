using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerLocomotion : Player
{

    [Header("Player Movement")]
    [SerializeField] public float walkSpeed;
    [SerializeField] public float runSpeed;
    private float moveSpeed;
    [SerializeField] public FloatingJoystick joystick;

    [Header("Attack Control")]
    private bool isAttacking = false;

    [Header("Components")]
    private Rigidbody _rb;
    Animator _animator;

    private Vector3 _direction;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        SetRigidBodyInitialStatus();
    }
    private void FixedUpdate()
    {
        _direction = new Vector3(joystick.Horizontal, 0.0f, joystick.Vertical);
        
        if (_direction.magnitude == 0.0f)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, 0.0f, walkSpeed);
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, runSpeed, walkSpeed);
            Quaternion rotation = Quaternion.LookRotation(_direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 500 * Time.fixedDeltaTime);
        }
         _rb.MovePosition(_rb.position + _direction * moveSpeed * Time.fixedDeltaTime);

        _animator.SetFloat("Speed", moveSpeed);
    }

    public Vector3 GetPlayerDirection()
    {
        return _direction;
    }

    public void TriggerPunch()
    {
        if (isAttacking) return;

        Debug.Log("Player: Estou executando a Animação de Ataque");
        isAttacking = true;
        _animator.SetTrigger("AttackTrigger");
        StartCoroutine(ResetAttack());
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(1.20f);
        isAttacking = false;

        if (_direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(_direction, Vector3.up);
            transform.rotation = rotation;
        }
    }

    private void SetRigidBodyInitialStatus()
    {
        _rb.isKinematic = false;
        _rb.freezeRotation = true;
    }
}
