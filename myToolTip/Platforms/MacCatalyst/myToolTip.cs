using System.ComponentModel;
using EastyToolTipMauiIos;
using UIKit;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Platform;

[assembly: ResolutionGroupName("Plugin")]

namespace Plugin.myToolTip
{
    /// <summary>
    /// Interface for myToolTip
    /// </summary>
    public class myToolTipImplementation :  PlatformEffect
    {
        EasyTipView tooltip;
        UITapGestureRecognizer tapGestureRecognizer;

        public myToolTipImplementation()
        {
            tooltip = new EasyTipView();
            tooltip.DidDismiss += OnDismiss;
        }

        void OnTap(object sender, EventArgs e)
        {
            var control = Control ?? Container;

            var text = ToolTipEffect.GetText(Element);

            if (string.IsNullOrEmpty(text)) return;

            tooltip.BubbleColor = ToolTipEffect.GetBackgroundColor(Element).ToPlatform();
            tooltip.ForegroundColor = ToolTipEffect.GetTextColor(Element).ToPlatform();
            tooltip.Text = new Foundation.NSString(text);
            var heightArrow = ToolTipEffect.GetArrowHeight(Element);
            if (heightArrow > 0.0)
                tooltip.ArrowHeight = Convert.ToSingle(heightArrow);
            var widthArrow = ToolTipEffect.GetArrowWidth(Element);
            if (widthArrow > 0.0)
                tooltip.ArrowWidth = Convert.ToSingle(widthArrow);
            UpdatePosition();

            var window = UIApplication.SharedApplication.KeyWindow;
            if (window == null) return;
            var vc = window.RootViewController;
            while (vc is { PresentedViewController: not null })
            {
                vc = vc.PresentedViewController;
            }


            if (vc != null) tooltip?.Show(control, vc.View, true);
        }

        private static void OnDismiss(object sender, EventArgs e)
        {
            // do something on dismiss
        }


        protected override void OnAttached()
        {
            var control = Control;
            if (control != null)
            {
                if (control is UIButton btn)
                {
                    btn.TouchUpInside += OnTap;
                }
                else
                {
                    var uiView = control as UIView;
                    tapGestureRecognizer = new UITapGestureRecognizer((UITapGestureRecognizer obj) => OnTap(obj, EventArgs.Empty));
                    if (uiView == null) return;
                    uiView.UserInteractionEnabled = true;
                    uiView.AddGestureRecognizer(tapGestureRecognizer);
                }
            }
            else
            {
                var container = Container;
                switch (container)
                {
                    case null:
                        return;
                    case UIButton btn:
                        btn.TouchUpInside += OnTap;
                        break;
                    default:
                    {
                        var uiView = container as UIView;
                        tapGestureRecognizer = new UITapGestureRecognizer((UITapGestureRecognizer obj) => OnTap(obj, EventArgs.Empty));
                        if (uiView == null) return;
                        uiView.UserInteractionEnabled = true;
                        uiView.AddGestureRecognizer(tapGestureRecognizer);
                        break;
                    }
                }
            }
        }

        protected override void OnDetached()
        {
            var control = Control;
            if (control != null)
            {
                if (control is UIButton btn)
                {
                    btn.TouchUpInside -= OnTap;
                }
                else
                {
                    if (tapGestureRecognizer != null)
                    {
                        var uiView = control as UIView;
                        uiView?.RemoveGestureRecognizer(tapGestureRecognizer);
                    }
                }
            }
            else
            {
                var container = Container;
                if (container is UIButton btn)
                {
                    btn.TouchUpInside -= OnTap;
                }
                else
                {
                    if (tapGestureRecognizer != null)
                    {
                        var uiView = container as UIView;
                        uiView?.RemoveGestureRecognizer(tapGestureRecognizer);
                    }
                }
            }

            tooltip?.Dismiss();
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            if (args.PropertyName == ToolTipEffect.BackgroundColorProperty.PropertyName)
            {
                tooltip.BubbleColor = ToolTipEffect.GetBackgroundColor(Element).ToPlatform();
            }
            else if (args.PropertyName == ToolTipEffect.TextColorProperty.PropertyName)
            {
                tooltip.ForegroundColor = ToolTipEffect.GetTextColor(Element).ToPlatform();
            }
            else if (args.PropertyName == ToolTipEffect.TextProperty.PropertyName)
            {
                tooltip.Text = new Foundation.NSString(ToolTipEffect.GetText(Element));
            }
            else if (args.PropertyName == ToolTipEffect.ArrowWidthProperty.PropertyName)
            {
                var widthArrow = ToolTipEffect.GetArrowWidth(Element);
                tooltip.ArrowWidth = Convert.ToSingle(widthArrow);
            }
            else if (args.PropertyName == ToolTipEffect.ArrowHeightProperty.PropertyName)
            {
                var heightArrow = ToolTipEffect.GetArrowHeight(Element);
                tooltip.ArrowWidth = Convert.ToSingle(heightArrow);
            }

            else if (args.PropertyName == ToolTipEffect.PositionProperty.PropertyName)
            {
                UpdatePosition();
            }
        }

        void UpdatePosition()
        {
            var position = ToolTipEffect.GetPosition(Element);
            tooltip.ArrowPosition = position switch
            {
                ToolTipPosition.Top => ArrowPosition.Bottom,
                ToolTipPosition.Left => ArrowPosition.Right,
                ToolTipPosition.Right => ArrowPosition.Left,
                _ => ArrowPosition.Top
            };
        }
    }
}