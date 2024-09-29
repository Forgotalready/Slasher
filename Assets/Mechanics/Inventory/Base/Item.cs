using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject, IItem
{
  [SerializeField] private int _id;
  [SerializeField] private ItemData _itemData;
  
  public int Id
  {
    get => _id;
    set => _id = value;
  }
  public IItemData ItemData
  {
    get => _itemData;
  }

  private void OnEnable()
  {
    _itemData.OnEnable();
  }
}
