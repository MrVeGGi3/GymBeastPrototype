using UnityEngine;
using System.Collections;
using Unity.VisualScripting;


public abstract class Enemy : MonoBehaviour, IValuable
{
    [Header("Enemy Value")]
    [SerializeField] public int enemyValue;

    public int GetValue()
    {
        return enemyValue;
    }

    public void eliminateEnemy()
    {
        Destroy(gameObject);
    }

}
