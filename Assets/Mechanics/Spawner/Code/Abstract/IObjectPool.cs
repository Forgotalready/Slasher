using UnityEngine;

public interface IObjectPool
{
  public GameObject Create(Vector3 position, GameObject prefab);
  public void Delete(GameObject obj);
}
