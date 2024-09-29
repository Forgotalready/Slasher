using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public event Action<GameObject> EnemyAttacked;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Attack");
            EnemyAttacked?.Invoke(other.gameObject);
        }
    }
}
