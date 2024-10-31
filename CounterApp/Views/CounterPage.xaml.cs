using Color = Microsoft.Maui.Graphics.Color;

namespace CounterApp.Views;

public partial class CounterPage : ContentPage
{
    private Models.AllCounters AllCounters { get; set; }
    private Color CounterColor { get; set; } = Color.FromRgb(0, 0, 0);

    public CounterPage(Models.AllCounters allCounters)
    {
        InitializeComponent();
        AllCounters = allCounters;
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        string name = NameEntry.Text;
        if (string.IsNullOrEmpty(name))
        {
            name = "Counter";
            await DisplayAlert("WARNING", "Counter's name is empty! It will be replaced with 'Counter'", "OK");
        }

        if (!int.TryParse(InitialValueEntry.Text, out int initialValue))
        {
            initialValue = 0;
            await DisplayAlert("WARNING", "Counter's value is empty! It will be replaced with '0'", "OK");
        }

        AllCounters.Counters.Add(new Models.Counter(name, initialValue, CounterColor));
        AllCounters.SaveCounters();

        await Shell.Current.GoToAsync("..");
    }

    private void Slider_Changed(object sender, ValueChangedEventArgs e)
    {
        int r = (int)SliderRed.Value;
        int g = (int)SliderGreen.Value;
        int b = (int)SliderBlue.Value;

        CounterColor = Color.FromRgb(r, g, b);
        ColorPicker.BackgroundColor = CounterColor;
    }
}