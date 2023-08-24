using LocationTracker.ViewModels;

namespace LocationTracker.Views;

public partial class TrackingView : ContentPage
{
	public TrackingView(TrackingViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}