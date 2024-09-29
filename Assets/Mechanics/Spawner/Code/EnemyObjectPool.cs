using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : MonoBehaviour, IObjectPool
{
    private List<GameObject> _inUse = new();
    private List<GameObject> _free = new();

    public GameObject Create(Vector3 position, GameObject prefab)
    {
        GameObject created = null;
        
        if (_free.Count == 0)
        {
            created = Instantiate(prefab, position, Quaternion.identity);
        }
        else
        {
            created = _free[0];
            
            _free.RemoveAt(0);
            _inUse.Add(created);
            
            created.transform.position = position;
            created.GetComponent<EnemyModel>().Init();
            created.SetActive(true);
        }

        _inUse.Add(created);
        return created;
    }
    
    public void Delete(GameObject obj)
    {
        _free.Add(obj);
        _inUse.Remove(obj);
        obj.SetActive(false);
    }
}
