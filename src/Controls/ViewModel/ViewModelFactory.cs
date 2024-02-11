using GACore.Architecture;

namespace GACore.UI.Controls.ViewModel;

public static class ViewModelFactory
{
    public static KingpinStateReporterViewModel GetKingpinStateReporterViewModel(IKingpinStateReporter kingpinStateReporter)
    {
        ArgumentNullException.ThrowIfNull(kingpinStateReporter);

        return new KingpinStateReporterViewModel()
        {
            Model = kingpinStateReporter
        };
    }
}
