public class NecklaceDecorator : IPlayerStat
{
  private IPlayerStat _wrapedObj;
  
  public NecklaceDecorator(IPlayerStat playerStat)
  {
    _wrapedObj = playerStat;
  }

  public int Damage { get => _wrapedObj.Damage; }
  public float Speed { get => _wrapedObj.Damage; }
  public int Health { get => _wrapedObj.Health + 10; }
  public int Resists { get => _wrapedObj.Resists; }
}
