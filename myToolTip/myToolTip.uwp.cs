using System;
using System.Diagnostics.Tracing;

using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ResolutionGroupName("Plugin")]
[assembly: ExportEffect(typeof(Plugin.myToolTip.myToolTipImplementation), nameof(Plugin.myToolTip.ToolTipEffect))]
namespace Plugin.myToolTip
{
    /// <summary>
    /// Interface for myToolTip
    /// </summary> 
    public class myToolTipImplementation : PlatformEffect
    {
        //Action Action;

        protected override void OnAttached()
        {
            //Action = ToolTipEffect.GetAction(Element);
            //if (Action == Action.OnClick)
            //{
            //    var control = Control ?? Container;

            //    if (control is Windows.UI.Xaml.Controls.Button btn)
            //    {
            //        btn.Click += OnClick;
            //    }
            //    else
            //    {
            //        control.AddHandler(UIElement.TappedEvent, new TappedEventHandler(OnClick), true);
            //    }
            //}
            //else
            ShowToolTip();

        }


        //private void OnClick(object sender, RoutedEventArgs e)
        //{
        //    Popup codePopup = new Popup();
        //    TextBlock popupText = new TextBlock();
        //    popupText.Text = ToolTipEffect.GetText(Element);

        //    popupText.Foreground = XamarinColorToNative(ToolTipEffect.GetTextColor(Element));
        //    Border border = new Border() { Background = XamarinColorToNative(ToolTipEffect.GetBackgroundColor(Element)) };
        //    border.Child = popupText;
        //    codePopup.Child = border;
        //    codePopup.IsOpen = true;

        //}

        private void ShowToolTip()
        {
            var control = Control ?? Container;

            if (control is DependencyObject)
            {
                object toolTipContent;
                var content = ToolTipEffect.GetContent(Element);

                if (content != null)
                {                     
                    var viewToRendererConverter = new ViewToRendererConverter();
                    var frameworkElement = viewToRendererConverter.Convert(content, null, null, null);
                    toolTipContent = frameworkElement;
                }
                else
                {
                    toolTipContent = ToolTipEffect.GetText(Element);
                }
                ToolTip toolTip = new ToolTip
                {
                    Content = toolTipContent ?? "n/a",
                    Placement = GetPlacementMode()
                };

                ToolTipService.SetToolTip(control, toolTip);

                PlacementMode GetPlacementMode()
                {
                    switch (ToolTipEffect.GetPosition(Element))
                    {
                        case ToolTipPosition.Bottom:
                            return Windows.UI.Xaml.Controls.Primitives.PlacementMode.Bottom;
                        case ToolTipPosition.Top:
                            return Windows.UI.Xaml.Controls.Primitives.PlacementMode.Top;
                        case ToolTipPosition.Left:
                            return Windows.UI.Xaml.Controls.Primitives.PlacementMode.Left;
                        case ToolTipPosition.Right:
                            return Windows.UI.Xaml.Controls.Primitives.PlacementMode.Right;
                        default:
                            return Windows.UI.Xaml.Controls.Primitives.PlacementMode.Mouse;
                    }
                }

            }
        }

        protected override void OnDetached()
        {
            //if (Action == Action.OnClick)
            //{
            //    var control = Control ?? Container;
            //    control.RemoveHandler(UIElement.TappedEvent, new TappedEventHandler(OnClick));
            //}
        }

        private SolidColorBrush XamarinColorToNative(Xamarin.Forms.Color color)
        {
            var alpha = (byte)(color.A * 255);
            var red = (byte)(color.R * 255);
            var green = (byte)(color.G * 255);
            var blue = (byte)(color.B * 255);
            return new SolidColorBrush(Windows.UI.Color.FromArgb(alpha, red, green, blue));
        }
    }
}

