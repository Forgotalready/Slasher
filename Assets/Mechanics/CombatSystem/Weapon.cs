using System;
using UnityEngine;

public class Weapon : MonoBehaviour, IDamageable
{

    public event Action<GameObject> EnemyDamaged;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemyDamaged?.Invoke(other.gameObject);
        }
    }
}
