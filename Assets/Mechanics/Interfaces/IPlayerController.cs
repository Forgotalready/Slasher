using System;

public interface IPlayerController
{
  public event Action<Item> itemCollected;
}
