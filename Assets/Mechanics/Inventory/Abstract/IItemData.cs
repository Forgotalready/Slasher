using UnityEngine;
public interface IItemData
{
  public string Name { get; set; }
  public Sprite Icon { get; set; }

  public int Amount { get; set; }
  public IItemData Clone();
}
