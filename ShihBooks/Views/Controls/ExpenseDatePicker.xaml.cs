namespace ShihBooks.Views.Controls;

public partial class ExpenseDatePicker : ContentView
{
	private static bool _fromPropertyChaned = false;
	private static bool _fromDateSelected = false;

	public static readonly BindableProperty DateProperty =
		BindableProperty.Create(nameof(Date), typeof(DateTime), typeof(ExpenseDatePicker), 
								defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnDateChanged);

    static void OnDateChanged(BindableObject bindable, object oldValue, object newValue)
    {
		if (_fromDateSelected) return;

        var control = (ExpenseDatePicker)bindable;
		_fromPropertyChaned = true;
        control.datePicker.Date = (DateTime)newValue;
		_fromPropertyChaned = false;
    }

    public ExpenseDatePicker()
	{
		InitializeComponent();
	}		

	//public ExpenseDatePicker(ExpenseDatePickerViewModel expenseDatePickerViewModel)
	//{
	//	InitializeComponent();
	//	BindingContext = expenseDatePickerViewModel;
	//}

	public DateTime Date
	{
		get => (DateTime)GetValue(DateProperty);
		set => SetValue(DateProperty, value);
	}

    private void datePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
		if (_fromPropertyChaned ) return;

		_fromDateSelected = true;
		SetValue(DateProperty, datePicker.Date);
		_fromDateSelected = false;
    }
}