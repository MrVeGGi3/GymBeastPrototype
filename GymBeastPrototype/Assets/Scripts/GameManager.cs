using System.Linq;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public MainUI mainUI;
    public Material newMaterial;


    public int moneyEarnt;
    public int maxEnemiesUpgradeCost = 200;
    public int changeColorCost = 1000;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPlayerMaxEnemies()
    {
        Debug.Log("Adicionando mais Inimigos que o Player pode Capturar");
        moneyEarnt -= maxEnemiesUpgradeCost;
        player.enemyHolder.maxEnemiesToCollect++;
        maxEnemiesUpgradeCost += 100;
        mainUI.UpdateMoneyScore();
    }

    public void AddPlayerMaterial()
    {
        Debug.Log("Mudando a cor do Material do Player");
        moneyEarnt -= changeColorCost;

        foreach(var render in player.renderers)
        {
            render.material = newMaterial;
        }
        mainUI.UpdateMoneyScore();
    }


}
