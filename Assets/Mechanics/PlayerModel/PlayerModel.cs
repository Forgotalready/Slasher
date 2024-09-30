using System;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
  private int _currentHealth;
  private IPlayerStat _playerStat;

  private IDamageable _damageableItem;

  public event Action PlayerDeath;
  
  public int Health 
    {
        get => _currentHealth;
    }  

  public void Init(IPlayerStat playerStat, IDamageable damageableItem)
  {
     _playerStat = playerStat;
     _damageableItem = damageableItem;
    _currentHealth = _playerStat.Health;
    _damageableItem.EnemyDamaged += OnEnemyDamaged;
  }

  private void OnEnemyDamaged(GameObject obj)
  {
    EnemyModel enemy = obj.GetComponent<EnemyModel>();
    enemy.GetDamage(_playerStat.Damage);
  }

  public void GetDamage(int damage)
  {
    _currentHealth -= damage;

    if (_currentHealth <= 0) PlayerDeath?.Invoke();
  }
}
