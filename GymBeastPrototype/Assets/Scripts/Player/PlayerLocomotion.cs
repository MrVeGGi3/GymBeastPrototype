using UnityEngine;
using UnityEngine.UIElements;

public class PlayerLocomotion : MonoBehaviour
{
    [SerializeField] public float walkSpeed;
    [SerializeField] public float runSpeed;
    [SerializeField] public FloatingJoystick joystick;

    private float moveSpeed;
    private Rigidbody _rb;
    Animator _animator;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        Vector3 direction = new Vector3(joystick.Horizontal, 0.0f, joystick.Vertical);
        
        if (direction.magnitude == 0.0f)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, 0.0f, walkSpeed);
        }
        else
        {
            moveSpeed += Mathf.Lerp(moveSpeed, runSpeed, walkSpeed);
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 500 * Time.fixedDeltaTime);
        }
         _rb.MovePosition(_rb.position + direction * moveSpeed * Time.fixedDeltaTime);

        _animator.SetFloat("Speed", moveSpeed);
    }


}
