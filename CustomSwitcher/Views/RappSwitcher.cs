using System;
using Xamarin.Forms;

namespace CustomSwitcher.Views
{
    public class RappSwitcher : ContentView
    {
        RelativeLayout _relativeLayout;
        Label _textLabel;
        Label _switchLabel;
        const int PaddingOffset = 6;

        public RappSwitcher()
        {
            this.HeightRequest = 50;
            this.WidthRequest = 120;
            this.BackgroundColor = Color.FromHex("#FF6A00");
            _relativeLayout = new RelativeLayout();

            _switchLabel = new Label(){ BackgroundColor = Color.White };
            _textLabel = new Label(){ BackgroundColor = Color.Transparent, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center };

            var tap = new TapGestureRecognizer();
            tap.Tapped += Tap_Tapped;

            _switchLabel.GestureRecognizers.Add(tap);
            _textLabel.GestureRecognizers.Add(tap);

            _switchLabel.Effects.Add(Effect.Resolve("TestEffect.CornerEffect"));

            _relativeLayout.Children.Add(_switchLabel, 
                Constraint.RelativeToParent((parent) => parent.X + PaddingOffset / 2), 
                Constraint.RelativeToParent((parent) => parent.Y + PaddingOffset / 2), 
                Constraint.RelativeToParent((parent) => parent.Height - PaddingOffset), 
                Constraint.RelativeToParent((parent) => parent.Height - PaddingOffset));
            
            _relativeLayout.Children.Add(_textLabel, 
                Constraint.RelativeToView(_switchLabel, (rel, viewR) => viewR.X + viewR.Width - PaddingOffset / 2),
                Constraint.RelativeToParent((parent) => parent.Y),
                Constraint.RelativeToView(_switchLabel, (rel, viewR) => rel.Width - viewR.Width),
                Constraint.RelativeToParent((parent) => parent.Height));
            
            _relativeLayout.VerticalOptions = new LayoutOptions(LayoutAlignment.Center, false);
            _relativeLayout.HorizontalOptions = new LayoutOptions(LayoutAlignment.Start, false);

            Content = _relativeLayout;

        }

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == TextCheckedProperty.PropertyName || propertyName == TextUnCheckedProperty.PropertyName || propertyName == CheckedProperty.PropertyName)
            {
                if (Checked)
                {
                    _textLabel.Text = TextUnChecked;
                }
                else
                {
                    _textLabel.Text = TextChecked;
                }
            }
        }

        public Color TextColor
        {
            get
            {
                return _textLabel.TextColor;
            }
            set
            {
                _textLabel.TextColor = value;
            }
        }

        public Color SwitchButtonColor
        {
            get
            {
                return _switchLabel.BackgroundColor;
            }
            set
            {
                _switchLabel.BackgroundColor = value;
            }
        }

        public static readonly BindableProperty TextCheckedProperty =
            BindableProperty.Create("TextChecked", typeof(string), typeof(RappSwitcher), "");

        public string TextChecked
        {
            get { return (string)GetValue(TextCheckedProperty); }
            set { SetValue(TextCheckedProperty, value); }
        }

        public static readonly BindableProperty TextUnCheckedProperty =
            BindableProperty.Create("TextUnChecked", typeof(string), typeof(RappSwitcher), "");

        public string TextUnChecked
        {
            get { return (string)GetValue(TextUnCheckedProperty); }
            set { SetValue(TextUnCheckedProperty, value); }
        }

        public static readonly BindableProperty CheckedProperty = 
            BindableProperty.Create("Checked", typeof(bool), typeof(RappSwitcher), false);

        public bool Checked
        {
            get { return (bool)GetValue(CheckedProperty); }
            set { SetValue(CheckedProperty, value); }
        }

        async void Tap_Tapped(object sender, EventArgs e)
        {
            if (Checked)
            {
                await _textLabel.FadeTo(0, 20);
                var layoutAnimTask = _switchLabel.LayoutTo(new Rectangle(_relativeLayout.X + PaddingOffset / 2, _relativeLayout.Y + PaddingOffset / 2, _switchLabel.Width, _switchLabel.Height), 150);
                await _switchLabel.ScaleTo(0.7, 50);
                await _switchLabel.ScaleTo(1, 50);
                await layoutAnimTask;

                Checked = false;

                _textLabel.HorizontalTextAlignment = TextAlignment.Center;

                await _textLabel.LayoutTo(new Rectangle(_relativeLayout.X + _switchLabel.Width, _relativeLayout.Y, _relativeLayout.Width - _switchLabel.Width, _textLabel.Height), 0);
                await _textLabel.FadeTo(1, 20);
            }
            else
            {
                await _textLabel.FadeTo(0, 20);

                Checked = true;

                var animationTask = _switchLabel.LayoutTo(new Rectangle(_relativeLayout.Width - _switchLabel.Width - PaddingOffset / 2, _relativeLayout.Y + PaddingOffset / 2, _switchLabel.Width, _switchLabel.Height), 150);
                await _switchLabel.ScaleTo(0.7, 50);
                await _switchLabel.ScaleTo(1, 50);
                await animationTask;

                _textLabel.HorizontalTextAlignment = TextAlignment.Center;

                await _textLabel.LayoutTo(new Rectangle(_relativeLayout.X, _relativeLayout.Y, _relativeLayout.Width - _switchLabel.Width, _textLabel.Height), 0);
                await _textLabel.FadeTo(1, 20);
            }
        }
    }
}

