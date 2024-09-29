using System;

public interface IItem
{
  public int Id { get; set; }
  public IItemData ItemData { get; }
}
