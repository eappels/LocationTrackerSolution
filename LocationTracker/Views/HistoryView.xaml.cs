using LocationTracker.ViewModels;

namespace LocationTracker.Views;

public partial class HistoryView : ContentPage
{
	public HistoryView(HistoryViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}