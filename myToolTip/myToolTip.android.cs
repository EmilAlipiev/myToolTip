using Android.Views;

using Com.Tooltip;

using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ResolutionGroupName("Plugin")]
[assembly: ExportEffect(typeof(Plugin.myToolTip.myToolTipImplementation), nameof(Plugin.myToolTip.ToolTipEffect))]
namespace Plugin.myToolTip
{
    /// <summary>
    /// Interface for myToolTip
    /// </summary>
    public class myToolTipImplementation : PlatformEffect
    {

        //ToolTipsManager _toolTipsManager;
        //ITipListener listener;
        Tooltip.Builder builder;
        public myToolTipImplementation()
        {
            //listener = new TipListener();
            //_toolTipsManager = new ToolTipsManager(listener);
        }

        void OnTap(object sender, EventArgs e)
        {
            var control = Control ?? Container;

            var text = ToolTipEffect.GetText(Element);

            if (!string.IsNullOrEmpty(text))
            {

                var parentContent = control.RootView;

                var position = ToolTipEffect.GetPosition(Element);
                switch (position)
                {
                    case ToolTipPosition.Top:
                        builder = new Tooltip.Builder(control, (int)GravityFlags.Top);
                        break;
                    case ToolTipPosition.Left:
                        builder = new Tooltip.Builder(control, (int)GravityFlags.Left);
                        break;
                    case ToolTipPosition.Right:
                        builder = new Tooltip.Builder(control, (int)GravityFlags.Right);
                        break;
                    default:
                        builder = new Tooltip.Builder(control, (int)GravityFlags.Bottom);
                        break;
                }

                builder.SetText(text);
                builder.SetCornerRadius(Convert.ToSingle(ToolTipEffect.GetCornerRadius(Element)));
                builder.SetDismissOnClick(true);
                builder.SetBackgroundColor(ToolTipEffect.GetBackgroundColor(Element).ToAndroid());
                builder.SetTextColor(ToolTipEffect.GetTextColor(Element).ToAndroid());
                var heightArrow = ToolTipEffect.GetArrowHeight(Element);
                if (heightArrow > 0.0)
                    builder.SetArrowHeight(Convert.ToSingle(heightArrow));
                var widthArrow = ToolTipEffect.GetArrowWidth(Element);
                if (widthArrow > 0.0)
                    builder.SetArrowWidth(Convert.ToSingle(widthArrow));
 
                builder.SetMargin(Convert.ToSingle(ToolTipEffect.GetMargin(Element)));
                builder.SetPadding(Convert.ToSingle(ToolTipEffect.GetPadding(Element)));
                builder.SetCancelable(true);
                builder.Build().Show();

                //  _toolTipsManager?.Show(toolTipView);
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
            builder.Dispose();
        }


    }

}

