using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Graphics;
using Android.Graphics.Drawables;
using CustomSwitcher.Droid.Effects;
using Android.Content.Res;

[assembly: ResolutionGroupName("TestEffect")]
[assembly: ExportEffect(typeof(CornerRadius), "CornerEffect")]
namespace CustomSwitcher.Droid.Effects
{
    public class CornerRadius : PlatformEffect
    {
        protected override void OnAttached()
        {

            var view = Element as View;
            if (view != null)
            {
                view.SizeChanged += OnElementSizeChanged;
            }

        }

        protected override void OnDetached()
        {
            var view = Element as View;
            if (view != null)
            {
                view.SizeChanged -= OnElementSizeChanged;
            }

        }

        void OnElementSizeChanged(object sender, EventArgs e)
        {
            var elem = sender as View;
            if (elem == null)
                return;

            var density = Resources.System.DisplayMetrics.Density;

            using (var imageBitmap = Bitmap.CreateBitmap((int)elem.Width, (int)elem.Height, Bitmap.Config.Argb8888))
            using (var canvas = new Canvas(imageBitmap))
            using (var paint = new Paint() { Dither = false, Color = elem.BackgroundColor.ToAndroid(), AntiAlias = true })
            {
                paint.AntiAlias = true;

                paint.Hinting = PaintHinting.On;
                paint.FilterBitmap = true;

                paint.Flags = PaintFlags.AntiAlias | PaintFlags.FilterBitmap;

                var height = (float)elem.Height;
//                var df = canvas.IsHardwareAccelerated;
                canvas.DrawRoundRect(new RectF(0, 0, (float)elem.Width, height), height / 2, height / 2, paint);

                Container.Background = new BitmapDrawable(imageBitmap);
            }
        }

        int ConvertPixelsToDp(float pixelValue)
        { 
            var dp = (int)((pixelValue) / Forms.Context.Resources.DisplayMetrics.Density);
            return dp;
        }
    }
}

