using System;
using Xamarin.Forms;

namespace CustomSwitcher.Views
{
    public class RappSwitcher : ContentView
    {
        RelativeLayout _relativeLayout;
        Label textLabel;
        Label switchLabel;
        const int PaddingOffset = 6;

        bool isChecked;

        public RappSwitcher()
        {
            this.HeightRequest = 50;
            this.WidthRequest = 120;
            this.BackgroundColor = Color.FromHex("#FF6A00");
            _relativeLayout = new RelativeLayout();

            switchLabel = new Label(){ BackgroundColor = Color.White };
            textLabel = new Label(){ BackgroundColor = Color.Transparent, Text = "Yes", VerticalTextAlignment = TextAlignment.Center };

            var tap = new TapGestureRecognizer();
            tap.Tapped += Tap_Tapped;

            switchLabel.GestureRecognizers.Add(tap);
            textLabel.GestureRecognizers.Add(tap);

            switchLabel.Effects.Add(Effect.Resolve("TestEffect.CornerEffect"));

            _relativeLayout.Children.Add(switchLabel, 
                Constraint.RelativeToParent((parent) => parent.X + PaddingOffset / 2), 
                Constraint.RelativeToParent((parent) => parent.Y + PaddingOffset / 2), 
                Constraint.RelativeToParent((parent) => parent.Height - PaddingOffset), 
                Constraint.RelativeToParent((parent) => parent.Height - PaddingOffset));
            
            _relativeLayout.Children.Add(textLabel, 
                Constraint.RelativeToView(switchLabel, (rel, viewR) => viewR.X + viewR.Width + PaddingOffset),
                Constraint.RelativeToParent((parent) => parent.Y),
                Constraint.RelativeToView(switchLabel, (rel, viewR) => rel.Width - viewR.Width),
                Constraint.RelativeToParent((parent) => parent.Height));
            
            _relativeLayout.VerticalOptions = new LayoutOptions(LayoutAlignment.Center, false);
            _relativeLayout.HorizontalOptions = new LayoutOptions(LayoutAlignment.Start, false);

            Content = _relativeLayout;

        }

        async void Tap_Tapped(object sender, EventArgs e)
        {
            if (isChecked)
            {
                await textLabel.FadeTo(0, 20);
                var layoutAnimTask = switchLabel.LayoutTo(new Rectangle(_relativeLayout.X + PaddingOffset / 2, _relativeLayout.Y + PaddingOffset / 2, switchLabel.Width, switchLabel.Height), 150);
                await switchLabel.ScaleTo(0.7, 50);
                await switchLabel.ScaleTo(1, 50);
                await layoutAnimTask;
                textLabel.HorizontalTextAlignment = TextAlignment.Center;
                await textLabel.LayoutTo(new Rectangle(_relativeLayout.X + switchLabel.Width, _relativeLayout.Y, _relativeLayout.Width - switchLabel.Width, textLabel.Height), 0);
                await textLabel.FadeTo(1, 20);

                isChecked = false;
            }
            else
            {
                await textLabel.FadeTo(0, 20);
                var animationTask = switchLabel.LayoutTo(new Rectangle(_relativeLayout.Width - switchLabel.Width - PaddingOffset / 2, _relativeLayout.Y + PaddingOffset / 2, switchLabel.Width, switchLabel.Height), 150);
                await switchLabel.ScaleTo(0.7, 50);
                await switchLabel.ScaleTo(1, 50);
                await animationTask;

                textLabel.HorizontalTextAlignment = TextAlignment.Center;
                await textLabel.LayoutTo(new Rectangle(_relativeLayout.X, _relativeLayout.Y, _relativeLayout.Width - switchLabel.Width, textLabel.Height), 0);
                await textLabel.FadeTo(1, 20);
                isChecked = true;
            }
        }
    }
}

