﻿using PlantControlApp.Models;
using System;
using Xamarin.Forms;

namespace PlantControlApp.Views
{
    public partial class MainPage : FlyoutPage
    {
        public MainPage()
        {
            InitializeComponent();
            flyoutPage.listView.ItemSelected += OnItemSelected;
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as FlyoutPageItem;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance((Type)item.TargetType));
                flyoutPage.listView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}