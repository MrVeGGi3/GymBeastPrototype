using TMPro;
using UnityEngine;

public class MaximumEnemiesButton : MonoBehaviour
{
    public TextMeshProUGUI costText;

    private void Start()
    {
        UpdateText();
    }
    public void buyMaxEnemies()
    {
        if (GameManager.instance.moneyEarnt >= GameManager.instance.maxEnemiesUpgradeCost)
        {
            GameManager.instance.AddPlayerMaxEnemies();
            UpdateText();
        }
        else
        {
            Debug.Log("Dinheiro insuficiente para comprar novos inimigos");
        }
    }

    private void UpdateText()
    {
        costText.text = "-" + GameManager.instance.maxEnemiesUpgradeCost + "$";
    }
}
