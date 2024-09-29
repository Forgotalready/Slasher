using System;
using UnityEngine;

public interface IDamageable
{
  public event Action<GameObject> EnemyDamaged;
}