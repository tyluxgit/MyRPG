﻿using System.Windows;
using Engine.ViewModels;

namespace WpfUI;

public partial class MainWindow : Window
{
    private readonly GameSession _gameSession;
    public MainWindow()
    {
        InitializeComponent();
        _gameSession = new GameSession();
        DataContext = _gameSession;
    }
    private void OnClick_MoveNorth(object sender, RoutedEventArgs e)
    {
        _gameSession.MoveNorth();
    }
    private void OnClick_MoveWest(object sender, RoutedEventArgs e)
    {
        _gameSession.MoveWest();
    }
    private void OnClick_MoveEast(object sender, RoutedEventArgs e)
    {
        _gameSession.MoveEast();
    }
    private void OnClick_MoveSouth(object sender, RoutedEventArgs e)
    {
        _gameSession.MoveSouth();
    }

}