using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttact : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit Enemy Trigger");
            other.GetComponent<EnemyController>().takeDamage(20);
        }
    }
}
