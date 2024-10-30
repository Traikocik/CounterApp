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
        //((Models.AllCounters)BindingContext).LoadCounters(); // Resetowanie wszystkich licznik�w LUB
        SetValueLabel(sender, counter.Value); // Resetowanie tylko label'a okre�lonego counter'a, kt�rego przyciski s� klikane
    }

    private void DecrementButton_Clicked(object sender, EventArgs e)
    {
        var counter = (Models.Counter)((ImageButton)sender).BindingContext;
        counter.Value--;
        ((Models.AllCounters)BindingContext).SaveCounters();
        //((Models.AllCounters)BindingContext).LoadCounters();
        SetValueLabel(sender, counter.Value);
    }

    private void ResetButton_Clicked(object sender, EventArgs e)
    {
        var counter = (Models.Counter)((ImageButton)sender).BindingContext;
        counter.Value = counter.InitialValue;
        ((Models.AllCounters)BindingContext).SaveCounters();
        //((Models.AllCounters)BindingContext).LoadCounters();
        SetValueLabel(sender, counter.Value);
    }

    private void RemoveButton_Clicked(object sender, EventArgs e)
    {
        var counter = (Models.Counter)((ImageButton)sender).BindingContext;
        ((Models.AllCounters)BindingContext).Counters.Remove(counter);
        ((Models.AllCounters)BindingContext).SaveCounters();
    }

    private void SetValueLabel(object sender, int value)
    {
        var parent = (StackLayout)((ImageButton)sender).Parent.Parent;

        // Resetowanie sztywno label'a z warto�ci� licznika LUB
        //var valueLabel = (Label)parent.Children[2];
        //valueLabel.Text = value.ToString();

        // Resetowanie ze znalazieniem, kt�re dziecko ma w�asno�� x:Name="ValueLabelName"
        foreach (var child in parent.Children)
        {
            if (child is Label label)
            {
                ((Label)label.FindByName("ValueLabelName")).Text = value.ToString();
            }
        }
    }
}