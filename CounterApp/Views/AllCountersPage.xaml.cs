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
        //((Models.AllCounters)BindingContext).LoadCounters(); // Resetowanie wszystkich liczników LUB
        SetValueLabel(sender, counter.Value); // Resetowanie tylko label'a okreœlonego counter'a, którego przyciski s¹ klikane
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

        // Resetowanie sztywno label'a z wartoœci¹ licznika LUB
        //var valueLabel = (Label)parent.Children[2];
        //valueLabel.Text = value.ToString();

        // Resetowanie ze znalazieniem, które dziecko ma w³asnoœæ x:Name="ValueLabelName"
        foreach (var child in parent.Children)
        {
            if (child is Label label)
            {
                ((Label)label.FindByName("ValueLabelName")).Text = value.ToString();
            }
        }
    }
}