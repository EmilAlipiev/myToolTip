using System;
using System.Linq;

using Xamarin.Forms;

namespace Plugin.myToolTip
{
    /// <summary>
    /// Cross myToolTip
    /// </summary>
    public static class ToolTipEffect
    {

        public static readonly BindableProperty TextProperty = BindableProperty.CreateAttached("Text", typeof(string), typeof(ToolTipEffect), string.Empty);

        public static readonly BindableProperty PositionProperty = BindableProperty.CreateAttached("Position", typeof(ToolTipPosition), typeof(ToolTipEffect), ToolTipPosition.Bottom);

        public static readonly BindableProperty IsVisibleProperty = BindableProperty.CreateAttached("HasTooltip", typeof(bool), typeof(ToolTipEffect), false, propertyChanged: OnIsVisibleChanged);

        public static readonly BindableProperty TextColorProperty = BindableProperty.CreateAttached("TextColor", typeof(Color), typeof(ToolTipEffect), Color.White);

        public static readonly BindableProperty BackgroundColorProperty = BindableProperty.CreateAttached("BackgroundColor", typeof(Color), typeof(ToolTipEffect), Color.Black);

        public static bool GetIsVisible(BindableObject view)
        {
            return (bool)view.GetValue(IsVisibleProperty);
        }

        public static void SetIsVisible(BindableObject view, bool value)
        {
            view.SetValue(IsVisibleProperty, value);
        }

        public static string GetText(BindableObject view)
        {
            return (string)view.GetValue(TextProperty);
        }

        public static Color GetTextColor(BindableObject view)
        {
            return (Color)view.GetValue(TextColorProperty);
        }

        public static void SetTextColor(BindableObject view, Color value)
        {
            view.SetValue(TextColorProperty, value);
        }

        public static Color GetBackgroundColor(BindableObject view)
        {
            return (Color)view.GetValue(BackgroundColorProperty);
        }

        public static void SetBackgroundColor(BindableObject view, Color value)
        {
            view.SetValue(BackgroundColorProperty, value);
        }


        public static void SetText(BindableObject view, string value)
        {
            view.SetValue(TextProperty, value);
        }

        public static ToolTipPosition GetPosition(BindableObject view)
        {
            return (ToolTipPosition)view.GetValue(PositionProperty);
        }

        public static void SetPosition(BindableObject view, ToolTipPosition value)
        {
            view.SetValue(PositionProperty, value);
        }
               
        static void OnIsVisibleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as View;
            if (view == null)
            {
                return;
            }

            bool isVisible = (bool)newValue;
            if (isVisible)
            {
                view.Effects.Add(new ControlTooltipEffect());
            }
            else
            {
                var toRemove = view.Effects.FirstOrDefault(e => e is ControlTooltipEffect);
                if (toRemove != null)
                {
                    view.Effects.Remove(toRemove);
                }
            }
        }
    }

    public enum ToolTipPosition
    {
        Bottom,

        Right,

        Left,

        Top
    }

    class ControlTooltipEffect : RoutingEffect
    {
        public ControlTooltipEffect() : base($"Plugin.{nameof(ToolTipEffect)}")
        {

        }
    }
}
