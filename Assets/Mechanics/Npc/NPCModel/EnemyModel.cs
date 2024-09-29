using System;
using UnityEngine;

public class EnemyModel : MonoBehaviour
{
  public event Action<GameObject> EnemyDeath;

  [SerializeField] private int _maxHealthPoint;
  [SerializeField] private int _damage;
  [SerializeField] private float _cdTime;
   private float cd;

  private int _healthPoint;

  public void Init()
  {
    _healthPoint = _maxHealthPoint;
     cd = 0f;
  }

  public void GetDamage(int damage)
  {
    _healthPoint -= damage;
    if (_healthPoint <= 0)
    {
      EnemyDeath?.Invoke(transform.gameObject);
    }
  }

    private void Update()
    {
        cd -= Time.deltaTime;
        Collider[] objects = Physics.OverlapSphere(transform.position, 1.0f);
        foreach (var collider in objects) 
        {
            if (collider.CompareTag("Player") && cd <= 0) 
            {
                PlayerModel player = collider.gameObject.GetComponent<PlayerModel>();
                Debug.Log("Player damaged");
                player.GetDamage(_damage);
                cd = _cdTime;
            }
        }
    }
}
