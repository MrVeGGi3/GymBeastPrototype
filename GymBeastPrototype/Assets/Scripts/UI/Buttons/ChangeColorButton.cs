using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColorButton : MonoBehaviour
{
    public TextMeshProUGUI costText;

    private void Start()
    {
        UpdateText();
    }
    public void switchPlayerColor()
    {
        if (GameManager.instance.moneyEarnt >= GameManager.instance.changeColorCost)
        {
            GameManager.instance.AddPlayerMaterial();
            UpdateText();
        }
        else
        {
            Debug.Log("Dinheiro Insuficiente para mudar de cor");
        }
    }
    private void UpdateText()
    {
        costText.text = "-" + GameManager.instance.changeColorCost + "$";
    }
}
