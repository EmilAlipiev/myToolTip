using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
        protected override void OnAttached()
        {
            var control = Control ?? Container;
      
            if (control is DependencyObject)
            {
                ToolTip toolTip = new ToolTip();
                toolTip.Content = ToolTipEffect.GetText(Element);
                switch (ToolTipEffect.GetPosition(Element))
                {
                    case ToolTipPosition.Bottom:
                        toolTip.Placement = Windows.UI.Xaml.Controls.Primitives.PlacementMode.Bottom;
                        break;
                    case ToolTipPosition.Top:
                        toolTip.Placement = Windows.UI.Xaml.Controls.Primitives.PlacementMode.Top;
                        break;
                    case ToolTipPosition.Left:
                        toolTip.Placement = Windows.UI.Xaml.Controls.Primitives.PlacementMode.Left;
                        break;
                    case ToolTipPosition.Right:
                        toolTip.Placement = Windows.UI.Xaml.Controls.Primitives.PlacementMode.Right;
                        break;
                    default:
                        return;
                }
                ToolTipService.SetToolTip(control, toolTip);
            }

        }

        protected override void OnDetached()
        {

        }
    }
}
 
