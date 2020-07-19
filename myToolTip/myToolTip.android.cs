using Android.Views;

using Com.Tomergoldst.Tooltips;

using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using static Com.Tomergoldst.Tooltips.ToolTipsManager;

[assembly: ResolutionGroupName("Plugin")]
[assembly: ExportEffect(typeof(Plugin.myToolTip.myToolTipImplementation), nameof(Plugin.myToolTip.ToolTipEffect))]
namespace Plugin.myToolTip
{
    /// <summary>
    /// Interface for myToolTip
    /// </summary>
    public class myToolTipImplementation : PlatformEffect
    {
        ToolTip toolTipView;
        ToolTipsManager _toolTipsManager;
        ITipListener listener;

        public myToolTipImplementation()
        {
            listener = new TipListener();
            _toolTipsManager = new ToolTipsManager(listener);
        }

        void OnTap(object sender, EventArgs e)
        {
            var control = Control ?? Container;

            var text = ToolTipEffect.GetText(Element);

            if (!string.IsNullOrEmpty(text))
            {
                ToolTip.Builder builder;
                var parentContent = control.RootView;

                var position = ToolTipEffect.GetPosition(Element);
                switch (position)
                {
                    case ToolTipPosition.Top:
                        builder = new ToolTip.Builder(control.Context, control, parentContent as ViewGroup, text.PadRight(80, ' '), ToolTip.PositionAbove);
                        break;
                    case ToolTipPosition.Left:
                        builder = new ToolTip.Builder(control.Context, control, parentContent as ViewGroup, text.PadRight(80, ' '), ToolTip.PositionLeftTo);
                        break;
                    case ToolTipPosition.Right:
                        builder = new ToolTip.Builder(control.Context, control, parentContent as ViewGroup, text.PadRight(80, ' '), ToolTip.PositionRightTo);
                        break;
                    default:
                        builder = new ToolTip.Builder(control.Context, control, parentContent as ViewGroup, text.PadRight(80, ' '), ToolTip.PositionBelow);
                        break;
                }

                builder.SetAlign(ToolTip.AlignLeft);
                builder.SetBackgroundColor(ToolTipEffect.GetBackgroundColor(Element).ToAndroid());
                builder.SetTextColor(ToolTipEffect.GetTextColor(Element).ToAndroid());

                toolTipView = builder.Build();

                _toolTipsManager?.Show(toolTipView);
            }

        }

        protected override void OnAttached()
        {
            var control = Control ?? Container;
            control.Click += OnTap;
        }


        protected override void OnDetached()
        {
            var control = Control ?? Container;
            control.Click -= OnTap;
            _toolTipsManager.FindAndDismiss(control);
        }

        class TipListener : Java.Lang.Object, ITipListener
        {
            public void OnTipDismissed(Android.Views.View p0, int p1, bool p2)
            {

            }
        }
    }

}
 
