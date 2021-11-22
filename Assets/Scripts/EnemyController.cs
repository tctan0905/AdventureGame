using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int healtEnemy;

    // Update is called once per frame
    void Update()
    {
        if(healtEnemy <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void takeDamage(int damage)
    {
         healtEnemy -= damage;
    }
}
