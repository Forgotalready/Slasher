using UnityEngine;
[CreateAssetMenu(menuName = "PlayerStats")]
public class BasePlayerStats : ScriptableObject, IPlayerStat
{
  [SerializeField] private int _damage = 10;
  [SerializeField] private int _health = 100;
  [SerializeField] private float _speed = 0.1f;
  [SerializeField] private int _resists = 0;
  
  public int Damage
  {
    get => _damage;
  }

  public float Speed
  {
    get => _speed;
  }

  public int Health
  {
    get => _health;
  }

  public int Resists
  {
    get => _resists;
  }
}