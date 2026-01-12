using System;
using Microsoft.Maui.Controls;

namespace MauiPaintTristanAckermann.Views;

public partial class DrawPage : ContentPage
{
    public DrawPage()
    {
        InitializeComponent();
    }

    private void OnClearClicked(object sender, EventArgs e)
    {
        
        MainDrawingView.Lines.Clear();
    }
}