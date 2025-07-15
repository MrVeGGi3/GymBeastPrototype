using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{

    [Header("EnemyHolder Component")]
    [SerializeField] public EnemyHolder enemyHolder;
    public List<SkinnedMeshRenderer> renderers = new List<SkinnedMeshRenderer>();

    public void addEnemy(EnemyCollisionBehaviour enemy)
    {
        if (enemyHolder.checkCanAddEnemies())
        {
            enemy.EnablePlayerHolderState();
            enemyHolder.AddEnemyToList(enemy);
        }
    }

}
