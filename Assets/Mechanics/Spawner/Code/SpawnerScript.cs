using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
  public List<GameObject> _enemys = new();
  [SerializeField] private GameObject _enemyTemplate;
  [SerializeField] private int _maxSpawnedMobs;
  [SerializeField] private float _cdTime;
  [SerializeField] private GameObject _player;
  [SerializeField] private EnemyObjectPool _objectPool;
  private float _timer;
  
  void Init()
  {
    _timer = 0.0f;
  }

  void Update()
  {
    _timer -= Time.deltaTime;
    if (_timer < 0.0f)
    {
      if (_enemys.Count < _maxSpawnedMobs)
      {
        GameObject enemy = _objectPool.Create(transform.position, _enemyTemplate);
        enemy.GetComponent<EnemyBrain>().Init(_player);

        SubscribeToDeath(enemy);
        
        _enemys.Add(enemy);
        _timer = _cdTime;
      }
      else
      {
        _timer = 0.0f;
      }
    }
  }

  private void SubscribeToDeath(GameObject enemy)
  {
    EnemyModel creatingEnemyModel = enemy.GetComponent<EnemyModel>();
    creatingEnemyModel.Init();
    creatingEnemyModel.EnemyDeath += OnEnemyDeath;
  }

  private void OnEnemyDeath(GameObject obj)
  {
    _enemys.Remove(obj);
    obj.GetComponent<EnemyModel>().EnemyDeath -= OnEnemyDeath;
    _objectPool.Delete(obj);
  }
}
