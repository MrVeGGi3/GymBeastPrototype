using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{

    [Header("Money")]
    public float moneyCatched;
    [Header("EnemyHolder Component")]
    [SerializeField] public EnemyHolder enemyHolder;
  

    public void addEnemy(EnemyCollisionBehaviour enemy)
    {
        if (enemyHolder.checkCanAddEnemies())
        {
            enemy.EnablePlayerHolderState();
            enemyHolder.AddEnemyToList(enemy);
        }
    }

}
