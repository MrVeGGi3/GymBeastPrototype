using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Unity.VisualScripting;

public class EnemyHolder : MonoBehaviour
{
    [Header("Enemies Count")]
    public int enemiesCollected = 0;
    public int maxEnemiesToCollect;

    [Header("Player and Follow Parameters")]
    [SerializeField] public Player player;
    public List<Enemy> enemies = new List<Enemy>();

    public float enemiesSpeed = 500.0f;
    public Vector3 distanceFromPlayer = new Vector3(0, 2.0f, -0.05f);
    public Vector3 distanceFromEnemies = new Vector3(0, 0, -0.02f);

    [Header("Physics Parameters")]
    private Dictionary<Enemy, Vector3> enemyVelocities = new Dictionary<Enemy, Vector3>();
    public float inertiaFactor = 5.0f;

    public void AddEnemyToList(Enemy enemy)
    {
        Debug.Log("Enemy Holder: Estou adicionando o Enemy na Lista");
        enemiesCollected++;
        enemies.Add(enemy);
        enemyVelocities[enemy] = Vector3.zero;
    }

    public bool checkCanAddEnemies()
    {
        bool canAdd = enemiesCollected < maxEnemiesToCollect ? true : false;
        return canAdd;
    }

    private void Update()
    {
        if (enemies.Count > 0 && enemies != null)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                Enemy currentEnemy = enemies[i];
                Transform currentTransform = currentEnemy.transform;

                Vector3 targetPosition;
                Quaternion targetRotation;

                if (i == 0)
                {
                    targetPosition = player.transform.TransformPoint(distanceFromPlayer);
                    targetRotation = player.transform.rotation;
                }
                else
                {
                    Transform previousTransform = enemies[i - 1].transform;
                    targetPosition = previousTransform.TransformPoint(distanceFromEnemies);
                    targetRotation = previousTransform.rotation;
                }

               
                Vector3 currentVelocity = enemyVelocities[currentEnemy];
                Vector3 desiredVelocity = (targetPosition - currentTransform.position) * inertiaFactor;
                currentVelocity = Vector3.Lerp(currentVelocity, desiredVelocity, Time.deltaTime * inertiaFactor);

               
                currentTransform.position += currentVelocity * Time.deltaTime;
                enemyVelocities[currentEnemy] = currentVelocity;

                
                currentTransform.rotation = Quaternion.Lerp(currentTransform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }
        else return;
    }
    public void ResetEnemiesCount()
    {
        enemiesCollected = 0;
        enemyVelocities.Clear();
        enemies.Clear();
    }

}
