using Android.Views;
 
using System.ComponentModel;
using IO.Github.Douglasjunior.AndroidSimpleTooltip;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Platform;


[assembly: ResolutionGroupName("Plugin")]
namespace Plugin.myToolTip
{
    /// <summary>
    /// Interface for myToolTip
    /// </summary>
    public class myToolTipImplementation : PlatformEffect
    {
        private SimpleTooltip.Builder _builder;

        private void OnTap(object sender, EventArgs e)
        {
            GetToolTip();
        }

        private void GetToolTip()
        {
            var control = Control ?? Container;
            var currentActivity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
            var text = ToolTipEffect.GetText(Element);

            if (string.IsNullOrEmpty(text)) return;
            var position = ToolTipEffect.GetPosition(Element);
            _builder = new SimpleTooltip.Builder(currentActivity).AnchorView(control);
            if(_builder == null) return;
            switch (position)
            {
                case ToolTipPosition.Top:
                    _builder.Gravity((int)GravityFlags.Top);
                    break;
                case ToolTipPosition.Left:
                    _builder.Gravity((int)GravityFlags.Start);
                    break;
                case ToolTipPosition.Right:
                    _builder.Gravity((int)GravityFlags.End);
                    break;
                case ToolTipPosition.Bottom:
                    _builder.Gravity((int)GravityFlags.Bottom);
                    break;
                default:
                    _builder.Gravity((int)GravityFlags.Bottom);
                    break;
            }

            _builder.Text(text);
            _builder.CornerRadius(Convert.ToSingle(ToolTipEffect.GetCornerRadius(Element)));

            _builder.DismissOnInsideTouch(true);
            _builder.DismissOnOutsideTouch(true);
            _builder.BackgroundColor(ToolTipEffect.GetBackgroundColor(Element).ToAndroid());
            _builder.TextColor(ToolTipEffect.GetTextColor(Element).ToAndroid());
            var heightArrow = ToolTipEffect.GetArrowHeight(Element);
            if (heightArrow > 0.0)
                _builder.ArrowHeight(Convert.ToSingle(heightArrow));
            var widthArrow = ToolTipEffect.GetArrowWidth(Element);
            if (widthArrow > 0.0)
                _builder.ArrowWidth(Convert.ToSingle(widthArrow));

            _builder.ArrowColor(ToolTipEffect.GetBackgroundColor(Element).ToAndroid());

            // var textSize = ToolTipEffect.GetTextSize(Element);
            // if (textSize > 0)
            //     _builder.s(Convert.ToSingle(textSize));

            _builder.Margin(Convert.ToSingle(ToolTipEffect.GetMargin(Element)));
            // _builder.Padding(ToolTipEffect.GetPadding(Element));
            _builder.Animated(true);

            _builder.Build()?.Show();

            //  _toolTipsManager?.Show(toolTipView);
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
            _builder?.Dispose();
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);
            if (args.PropertyName == "Text")
            {
                GetToolTip();
            }
        }
    }
}