using GAAPICommon.Interfaces;
using GACore.Architecture;

namespace GACore.DemoApp;

public class FooKingpin : IKingpinStateReporter
{
	private IKingpinState? _kingpinState = null;

	public IKingpinState? KingpinState
	{
		get { return _kingpinState; }
		set 
		{ 
			_kingpinState = value; 
		}
	}

    public void Randomize()
	{
		KingpinState = FooKingpinState.GetKingpinState();
	}

    public void SetGood()
	{
		KingpinState = FooKingpinState.FromGood();
	}
}