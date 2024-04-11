using GAAPICommon.Messages;
using GACore.Architecture;

namespace GACore.DemoApp;

public class FooKingpin : IKingpinStateReporter
{
	private KingpinStateDto? _kingpinState = null;

	public KingpinStateDto? KingpinState
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