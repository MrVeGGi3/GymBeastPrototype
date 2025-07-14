using UnityEngine;
using System.Collections;
using Unity.VisualScripting;


public abstract class Enemy : MonoBehaviour
{
    private bool playerCarrying = false;

    public bool GetPlayerCarrying()
    {
        return playerCarrying;
    }

    public void SetPlayerCarryingStatus()
    {
        playerCarrying = true;
    }


}
