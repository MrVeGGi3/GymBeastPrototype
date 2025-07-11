using UnityEngine;

public class EnemyCollisionBehaviour : Enemy
{
    [SerializeField] public float forceMultiplier;
    public Animator animator;
    private Rigidbody _rb;
    private Rigidbody[] _ragdollRigidbodies;
    private Collider[] _ragdollColliders;
    void Start()
    {
        animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        _ragdollColliders = GetComponentsInChildren<Collider>();
        DisableRagdoll();
    }

    private void DisableRagdoll()
    {
        Debug.Log("Disabling Ragdoll");
        foreach(var rb in _ragdollRigidbodies)
        {
            rb.isKinematic = true;
        }
        foreach(var col in _ragdollColliders)
        {
            if(col.gameObject != this.gameObject)
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
        foreach(var col in _ragdollColliders)
        {
            col.enabled = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(!isPlayerHit)
            {
                Debug.Log("Player colidiu com o Enemy");
                PlayerLocomotion player = collision.gameObject.GetComponent<PlayerLocomotion>();
                if (player != null)
                {
                    player.TriggerPunch();
                    Vector3 forceDirection = player.GetPlayerDirection();
                    EnableRagdoll(forceDirection);
                    isPlayerHit = true;
                }
            }
      
        }
    }
}
