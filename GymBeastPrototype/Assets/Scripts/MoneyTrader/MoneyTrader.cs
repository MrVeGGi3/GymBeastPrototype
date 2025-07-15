using NUnit.Framework;
using System.Collections;
using UnityEngine;

public class MoneyTrader : MonoBehaviour
{
    public MainUI mainUI;
    private int totalToSwitch;
    private Collider _collider;
    void Start()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if(player != null && player.enemyHolder.enemiesCollected > 0)
        {
            SwitchMoney(player);
        }
    }

    private void SwitchMoney(Player player)
    {
        var enemies = player.enemyHolder.enemies;

        if (enemies != null)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                totalToSwitch += enemies[i].GetValue();
                Destroy(enemies[i].gameObject);
            }
        }
        else return;
        player.enemyHolder.ResetEnemiesCount();
        GameManager.instance.moneyEarnt += totalToSwitch;

        if (mainUI != null)
            mainUI.UpdateMoneyScore();
        else
            Debug.LogWarning("Main UI não foi atribuido");

    }

}
