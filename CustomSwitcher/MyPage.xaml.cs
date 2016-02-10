using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CustomSwitcher
{
    public partial class MyPage : ContentPage
    {
        public MyPage()
        {
            InitializeComponent();
            switcher.Effects.Add(Effect.Resolve("TestEffect.CornerEffect"));
        }
    }
}

