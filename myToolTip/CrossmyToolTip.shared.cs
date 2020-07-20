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

        public static readonly BindableProperty IsVisibleProperty = BindableProperty.CreateAttached("IsVisible", typeof(bool), typeof(ToolTipEffect), false, propertyChanged: OnIsVisibleChanged);

        public static readonly BindableProperty TextColorProperty = BindableProperty.CreateAttached("TextColor", typeof(Color), typeof(ToolTipEffect), Color.White);

        public static readonly BindableProperty BackgroundColorProperty = BindableProperty.CreateAttached("BackgroundColor", typeof(Color), typeof(ToolTipEffect), Color.Black);

        /// <summary>
        /// Android Only
        /// </summary>
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.CreateAttached("CornerRadius", typeof(double), typeof(ToolTipEffect), default);

        /// <summary>
        /// Android Only
        /// </summary>
        public static readonly BindableProperty MarginProperty = BindableProperty.CreateAttached("Margin", typeof(double), typeof(ToolTipEffect), default);

        /// <summary>
        /// Android Only
        /// </summary>
        public static readonly BindableProperty PaddingProperty = BindableProperty.CreateAttached("Padding", typeof(double), typeof(ToolTipEffect), default);

        /// <summary>
        /// Android, IOS
        /// </summary>
        public static readonly BindableProperty ArrowHeightProperty = BindableProperty.CreateAttached("ArrowHeight", typeof(double), typeof(ToolTipEffect), default);

        /// <summary>
        /// Android, IOS
        /// </summary>
        public static readonly BindableProperty ArrowWidthProperty = BindableProperty.CreateAttached("ArrowWidth", typeof(double), typeof(ToolTipEffect), default);

        /// <summary>
        /// Overrides TextProperty when it is set. UWP only, IOS and Android not implemented yet
        /// </summary>
        public static readonly BindableProperty ContentProperty = BindableProperty.Create("Content", typeof(View), typeof(ToolTipEffect), (object)null, (BindingMode)0, (BindableProperty.ValidateValueDelegate)null, (BindableProperty.BindingPropertyChangedDelegate)null, (BindableProperty.BindingPropertyChangingDelegate)null, (BindableProperty.CoerceValueDelegate)null, (BindableProperty.CreateDefaultValueDelegate)null);

        public static double GetArrowHeight(BindableObject view)
        {
            return (double)view.GetValue(ArrowHeightProperty);
        }

        public static double GetArrowWidth(BindableObject view)
        {
            return (double)view.GetValue(ArrowWidthProperty);
        }


        //public static readonly BindableProperty ActionProperty = BindableProperty.CreateAttached("Action", typeof(Action), typeof(ToolTipEffect), Action.OnHover);

        /// <summary>
        /// Gets or sets the value of the Content. This property can be used to change the content in a tab header.
        /// </summary>

        public static View GetContent(BindableObject view)
        {
            return (View)view.GetValue(ContentProperty);
        }

        public static double GetCornerRadius(BindableObject view)
        {
            return (double)view.GetValue(CornerRadiusProperty);
        }

        public static double GetMargin(BindableObject view)
        {
            return (double)view.GetValue(MarginProperty);
        }

        public static double GetPadding(BindableObject view)
        {
            return (double)view.GetValue(PaddingProperty);

        }

        internal static float GetLineSpacing(Element element)
        {
            throw new NotImplementedException();
        }

        public static void SetCornerRadius(BindableObject view, float value)
        {
            view.SetValue(CornerRadiusProperty, value);
        }

        public static void SetContent(BindableObject view, View value)
        {
            view.SetValue(ContentProperty, value);
        }

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

        //public static Action GetAction(BindableObject view)
        //{
        //    return (Action)view.GetValue(ActionProperty);
        //}

        //public static void SetAction(BindableObject view, Action value)
        //{
        //    view.SetValue(ActionProperty, value);
        //}

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

    public enum Action
    {
        OnHover,

        OnClick,
    }

    class ControlTooltipEffect : RoutingEffect
    {
        public ControlTooltipEffect() : base($"Plugin.{nameof(ToolTipEffect)}")
        {

        }
    }
}
