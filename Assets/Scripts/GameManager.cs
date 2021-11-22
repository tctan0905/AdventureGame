using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public EnemyController enemyHealth = new EnemyController();
    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int dam)
    {
        enemyHealth.takeDamage(dam);
    }
}
