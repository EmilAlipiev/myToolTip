using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace myTooltipSample;

public partial class MainPage : ContentPage
{
	int count = 0;
	public string Tooltip { get; private set; }
	
	public MainPage()
	{
		InitializeComponent();
		BindingContext=this;
	}
	private void Handle_Tapped(object sender, EventArgs e)
	{

	}
	
	protected override void OnAppearing()
	{
		base.OnAppearing(); 
		Tooltip = @"#This is the bindbale tooltip text";

		RaisePropertyChanged(nameof(Tooltip));
	}

	public event PropertyChangedEventHandler PropertyChanged;

	private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
	{
		var handler = PropertyChanged;
		handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	protected bool SetProperty<T>(ref T backingStore, T value,
		[CallerMemberName] string propertyName = "",
		System.Action onChanged = null)
	{
		if (EqualityComparer<T>.Default.Equals(backingStore, value))
			return false;

		backingStore = value;
		onChanged?.Invoke();
		RaisePropertyChanged(propertyName);
		return true;
	}
}


