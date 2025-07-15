using UnityEngine;

public class UpgradeItensUI : MonoBehaviour
{
    public Canvas upgradeItensCanvas;
    private Collider _upgradeCollider;
   
    void Start()
    {
        _upgradeCollider = GetComponent<Collider>(); 
        DeactivateCanvasVisibility();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            upgradeItensCanvas.gameObject.SetActive(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player)
        {
            DeactivateCanvasVisibility();
        }
    }

    private void DeactivateCanvasVisibility()
    {
        upgradeItensCanvas.gameObject.SetActive(false);
    }
}
