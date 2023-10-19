namespace ShihBooks.Views.Controls;

public partial class EntitySearchBar : VerticalStackLayout
{
	public static readonly BindableProperty TextProperty = 
		BindableProperty.Create(nameof(Text), typeof(string), typeof(EntitySearchBar),
								defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnTextChanged);

	static void OnTextChanged(BindableObject bindableObject, object oldValue, object newValue)
	{
		var control = (EntitySearchBar)bindableObject;
		control.searchBar.Text = (string)newValue;
	}

	public string Text
	{
		get => (string)GetValue(TextProperty);
		set => SetValue(TextProperty, value);
	}

	public EntitySearchBar()
	{
		InitializeComponent();
	}

    private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
		Text = searchBar.Text;
    }
}