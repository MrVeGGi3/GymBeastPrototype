using UnityEngine;

public class UpgradeItensUI : MonoBehaviour
{
    public Canvas upgradeItensCanvas;
    private Collider _upgradeCollider;
   
    private void Awake()
    {
        if(upgradeItensCanvas == null)
        {
            Debug.LogWarning("Upgrade Canvas n�o foi atribuido");
        }
        else
        {
            DeactivateCanvasVisibility();
        }
    }
    void Start()
    {
        _upgradeCollider = GetComponent<Collider>(); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(upgradeItensCanvas != null)
            {
                ActivateCanvasVisibility();
                Debug.Log("Canvas Acionado!");
            }
        }
        else
        {
            Debug.Log("Player n�o foi encontrado");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (upgradeItensCanvas != null)
            {
                DeactivateCanvasVisibility();
                Debug.Log("Canvas Acionado!");
            }
           ;
        }
    }

    private void DeactivateCanvasVisibility()
    {
        upgradeItensCanvas.gameObject.SetActive(false);
    }

    private void ActivateCanvasVisibility()
    {
        upgradeItensCanvas.gameObject.SetActive(true);
    }
}
