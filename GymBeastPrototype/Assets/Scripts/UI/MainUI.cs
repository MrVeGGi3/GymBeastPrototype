using TMPro;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    public TextMeshProUGUI moneyText;

    void Start()
    {
        moneyText.text = "Dinheiro: $" + 0;
    }
    public void UpdateMoneyScore()
    {
        moneyText.text = "Dinheiro:$" + GameManager.instance.moneyEarnt;    
    }
}
