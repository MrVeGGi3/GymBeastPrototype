using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCollisionBehaviour : Enemy
{
    [Header("Collision Attributes")]
    [SerializeField] public float forceMultiplier;
    [SerializeField] private float collisionCooldown;

    [Header("Collision Elements")]
    public Animator animator;
    private Rigidbody _rb;
    private Rigidbody[] _ragdollRigidbodies;
    private Collider[] _ragdollColliders;
    private Collider _mainCollider;

    private bool canCollide = true;

    private void Awake()
    {
        _ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        _ragdollColliders = GetComponentsInChildren<Collider>();
        _mainCollider = GetComponent<Collider>();
        animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        DisableRagdoll();
    }

    private void DisableRagdoll()
    {
        animator.enabled = true;
        Debug.Log("Disabling Ragdoll");
        foreach (var rb in _ragdollRigidbodies)
        {
            rb.isKinematic = true;
        }
        foreach (var col in _ragdollColliders)
        {
            col.enabled = false;
        }
        _mainCollider.enabled = true;
        _rb.isKinematic = true;
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

        _mainCollider.enabled = true;
        _rb.isKinematic = false;

        canCollide = false;
    }

    public void EnablePlayerHolderState()
    {

        DisableRagdoll();
        _mainCollider.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (canCollide)
            {
                PlayerLocomotion player = collision.gameObject.GetComponent<PlayerLocomotion>();
                Debug.Log("Enemy: Colidi com o Player");

                if (player != null && player.enemyHolder.checkCanAddEnemies())
                {
                    Debug.Log("Enemy: Posso ser adicionado ao Player");
                    Vector3 forceDirection = player.GetPlayerDirection();
                    player.TriggerPunch();
                    EnableRagdoll(forceDirection);
                    StartCoroutine(AddEnemyTimer(player));
                }
            }
        }
    }

    private IEnumerator AddEnemyTimer(Player player)
    {
        yield return new WaitForSeconds(1.5f);
        player.addEnemy(this);
    }
}
