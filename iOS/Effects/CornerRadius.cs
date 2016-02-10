using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using CustomSwitcher.iOS.Effects;

[assembly: ResolutionGroupName("TestEffect")]
[assembly: ExportEffect(typeof(CornerRadius), "CornerEffect")]
namespace CustomSwitcher.iOS.Effects
{
    public class CornerRadius : PlatformEffect
    {
        protected override void OnAttached()
        {
            var currentView = this.Element as View;
            if (currentView != null)
            {
                currentView.SizeChanged += Renderer_Element_SizeChanged;
            }
        }

        protected override void OnDetached()
        {
            var currentView = this.Element as View;
            if (currentView != null)
            {
                currentView.SizeChanged -= Renderer_Element_SizeChanged;
            }
        }

        void Renderer_Element_SizeChanged(object sender, EventArgs e)
        {
            var currentView = sender as View;
            if (currentView == null)
                return;
            
            var radius = new nfloat(currentView.Height / 2);
//            Container.Layer.EdgeAntialiasingMask = CoreAnimation.CAEdgeAntialiasingMask.All;
            Container.Layer.CornerRadius = radius;
        }
    }
}

