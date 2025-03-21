using System.Windows;
using Engine.Models;
using Engine.ViewModels;

namespace WPFUI;

/// <summary>
/// Interaction logic for TradeScreen.xaml
/// </summary>
public partial class TradeScreen : Window
{
    public GameSession? Session => DataContext as GameSession;

    public TradeScreen()
    {
        InitializeComponent();
    }

    private void OnClick_Sell(object sender, RoutedEventArgs e)
    {
        if (Session == null) return;
        if (sender is FrameworkElement element && element.DataContext is GameItem item)
        {
            TradeService.SellItem(Session.CurrentPlayer, Session.CurrentTrader, item);
        }
    }

    private void OnClick_Buy(object sender, RoutedEventArgs e)
    {
        if (Session == null) return;
        if (sender is FrameworkElement element && element.DataContext is GameItem item)
        {
            if (!TradeService.BuyItem(Session.CurrentPlayer, Session.CurrentTrader, item))
            {
                MessageBox.Show("You do not have enough gold");
            }
        }
    }

    private void OnClick_Close(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
