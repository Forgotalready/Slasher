public interface ICell
{
  public void Render(Item item);

  public Item Item
  {
    get;
    set;
  }
}