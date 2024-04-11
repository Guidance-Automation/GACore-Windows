using GACore.Architecture;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;

namespace GACore.UI.ViewModel;

public abstract class AbstractCollectionViewModel<T, U, V> : AbstractViewModel<T>, ICollectionViewModel<T, U, V>
        where T : class, IModelCollection<V>
        where U : AbstractViewModel<V>, new()
        where V : class
{
    protected ObservableCollection<U> viewModels = [];

    public void Dispose() => Dispose(true);

    public AbstractCollectionViewModel()
    {
        ViewModels = new ReadOnlyObservableCollection<U>(viewModels);
    }

    ~AbstractCollectionViewModel()
    {
        Dispose(false);
    }

    public ReadOnlyObservableCollection<U> ViewModels { get; }

    private bool _isDisposed = false;

    private readonly SemaphoreSlim _semaphoreSlim = new(1, 1);

    private void HandleAddCollectionItemModel(V collectionItemModel)
    {
        if (collectionItemModel == null)
        {
            Logger?.Warn("[{0}] HandleAddCollectionItemModel() collectionItemModel was null", GetType().Name);
            return;
        }

        _semaphoreSlim.Wait();

        try
        {
            // To solve race condition when getting an item added while the model is changed. 
            if (viewModels.Select(e => e.Model).Any(e => e != null && e.Equals(collectionItemModel)))
            {
                Logger?.Warn("[{0}] HandleAddCollectionItemModel() viewModels already contains a collectionItemViewModel for this collectionItemModel", GetType().Name);
                return;
            }

            U collectionItemViewModel = new() { Model = collectionItemModel };
            Application.Current.Dispatcher.BeginInvoke(() =>
            {
                viewModels.Add(collectionItemViewModel);
            }, DispatcherPriority.DataBind);

            Logger?.Trace("[{0}] HandleAddCollectionItemModel() added: {1}", GetType().Name, collectionItemModel);
        }
        catch (Exception ex)
        {
            Logger?.Error(ex);
        }
        finally
        {
            _semaphoreSlim.Release();
        }
    }

    protected void HandleCollectionRefresh()
    {
        Logger?.Trace("[{0}] HandleCollectionRefresh()", GetType().Name);

        Application.Current.Dispatcher.BeginInvoke(() =>
        {
            viewModels.Clear();
        }, DispatcherPriority.DataBind);

        if (Model == null) return;

        IEnumerable<V> existingModels = Model.GetModels();

        Logger?.Trace("[{0}] HandleCollectionRefresh() adding {1} existing model(s)", GetType().Name, existingModels.Count());

        foreach (V model in existingModels)
        {
            HandleAddCollectionItemModel(model);
        }
    }

    protected override void HandleModelUpdate(T? oldValue, T? newValue)
    {
        if (oldValue != null) oldValue.Added -= Model_Added;
        if (oldValue != null) oldValue.Removed -= Model_Removed;

        if (newValue != null) newValue.Added += Model_Added;
        if (newValue != null) newValue.Removed += Model_Removed;

        base.HandleModelUpdate(oldValue, newValue);
        HandleCollectionRefresh();
    }

    public abstract U GetViewModelForModel(V model);

    private void Model_Removed(V obj)
    {
        if (obj == null)
        {
            Logger?.Warn("[{0}] Model_Removed() obj was null", GetType().Name);
            return;
        }

        _semaphoreSlim.Wait();

        try
        {
            U viewModel = GetViewModelForModel(obj);

            if (viewModel == null)
            {
                Logger?.Warn("[{0}] Model_Removed() GetViewModelForModel() returned null", GetType().Name);
                return;
            }

            Application.Current.Dispatcher.BeginInvoke(() =>
            {
                viewModels.Remove(viewModel);
            }, DispatcherPriority.DataBind);
        }
        catch (Exception ex)
        {
            Logger?.Error(ex);
        }

        finally
        {
            _semaphoreSlim.Release();
        }
    }

    private void Model_Added(V obj)
    {
        Logger?.Trace("[{0}] Model_Added()", GetType().Name);
        HandleAddCollectionItemModel(obj);
    }

    protected virtual void Dispose(bool isDisposing)
    {
        if (_isDisposed) return;

        if (isDisposing)
        {
            if (Model != null) Model.Added -= Model_Added;
            if (Model != null) Model.Removed -= Model_Removed;
        }

        _isDisposed = true;
    }
}
