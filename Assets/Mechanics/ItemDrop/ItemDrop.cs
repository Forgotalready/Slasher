using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private List<GameObject> _dropableObjects;
    EnemyModel _enemyModel;
    public void Init()
    {
        _enemyModel = GetComponent<EnemyModel>();
        _enemyModel.EnemyDeath += OnEnemyDeath;
    }

    private void OnEnemyDeath(GameObject enemy) 
    {
        Debug.Log("ItemDrop");
        Instantiate(_dropableObjects[0], enemy.transform.position, Quaternion.identity, null);
    }
    private void OnDisable()
    {
        _enemyModel.EnemyDeath -= OnEnemyDeath;
    }
}
