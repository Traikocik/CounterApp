using System.Collections.ObjectModel;

namespace CounterApp.Views;

public partial class AllCountersPage : ContentPage
{
    public AllCountersPage()
    {
        InitializeComponent();
        BindingContext = new Models.AllCounters();
    }

    protected override void OnAppearing()
    {
        ((Models.AllCounters)BindingContext).LoadCounters();
    }

    private async void OnAddCounterClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CounterPage((Models.AllCounters)BindingContext));
    }

    private void IncrementButton_Clicked(object sender, EventArgs e)
    {
        var counter = (Models.Counter)((ImageButton)sender).BindingContext;
        counter.Value++;
        ((Models.AllCounters)BindingContext).SaveCounters();
        ((Models.AllCounters)BindingContext).LoadCounters();
    }

    private void DecrementButton_Clicked(object sender, EventArgs e)
    {
        var counter = (Models.Counter)((ImageButton)sender).BindingContext;
        counter.Value--;
        ((Models.AllCounters)BindingContext).SaveCounters();
        ((Models.AllCounters)BindingContext).LoadCounters();
    }

    private void ResetButton_Clicked(object sender, EventArgs e)
    {
        var counter = (Models.Counter)((ImageButton)sender).BindingContext;
        counter.Value = counter.InitialValue;
        ((Models.AllCounters)BindingContext).SaveCounters();
        ((Models.AllCounters)BindingContext).LoadCounters();
    }

    private void RemoveButton_Clicked(object sender, EventArgs e)
    {
        var counter = (Models.Counter)((ImageButton)sender).BindingContext;
        ((Models.AllCounters)BindingContext).Counters.Remove(counter);
        ((Models.AllCounters)BindingContext).SaveCounters();
    }
}