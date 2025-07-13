using System.Collections;
using UnityEngine;

public class EnemyCollisionBehaviour : Enemy
{
    [SerializeField] public float forceMultiplier;
    [SerializeField] private float collisionCooldown;

    public Animator animator;
    private Rigidbody _rb;
    private Rigidbody[] _ragdollRigidbodies;
    private Collider[] _ragdollColliders;
    private Collider _mainCollider;


    private bool canCollide = true;
    private int playerCollisionCount = 0;


    void Start()
    {
        animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _mainCollider = GetComponent<Collider>();
        _ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        _ragdollColliders = GetComponentsInChildren<Collider>();
        DisableRagdoll();
    }

    private void DisableRagdoll()
    {
        Debug.Log("Disabling Ragdoll");
        foreach (var rb in _ragdollRigidbodies)
        {
            rb.isKinematic = true;
        }
        foreach (var col in _ragdollColliders)
        {
            if (col.gameObject != this.gameObject)
            {
                col.enabled = false;
            }
        }

        animator.enabled = true;
    }
    private void EnableRagdoll(Vector3 forceDirection)
    {
        Debug.Log("Enabling Ragdolls");
        animator.enabled = false;

        foreach (var rb in _ragdollRigidbodies)
        {
            rb.isKinematic = false;
            rb.AddForce(forceDirection * forceMultiplier, ForceMode.Impulse);
        }


        foreach (var col in _ragdollColliders)
        {
            col.enabled = true;
        }
        StartCoroutine(EnableMainCollider());
        StartCoroutine(DisableCollider());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (canCollide)
            {
                playerCollisionCount++;
                Debug.Log("Player colidiu com o Enemy:" + playerCollisionCount + "Vez");
                PlayerLocomotion player = collision.gameObject.GetComponent<PlayerLocomotion>();
                if (player != null)
                {
                    Vector3 forceDirection = player.GetPlayerDirection();

                    if (playerCollisionCount == 1)
                    {
                        player.TriggerPunch();
                        Debug.Log("Primeira Colisão");
                        EnableRagdoll(forceDirection);
                    }
                    else if (playerCollisionCount == 2)
                    {
                        Debug.Log("Segunda Colisão");
                    }

                    StartCoroutine(DisableCollider());
                }
            }

        }
    }
    IEnumerator DisableCollider()
    {
        canCollide = false;
        yield return new WaitForSeconds(collisionCooldown);
        canCollide = true;
    }

    IEnumerator EnableMainCollider()
    {
        _mainCollider.enabled = false;
        yield return new WaitForSeconds(collisionCooldown);
        _mainCollider.enabled = true;
    }
}
