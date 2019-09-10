using FFImageLoading.Forms.Platform;
using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

using Syncfusion.SfChart.XForms.iOS.Renderers;

using Syncfusion.SfSunburstChart.XForms.iOS;

using Syncfusion.SfPicker.XForms.iOS;

using Syncfusion.XForms.iOS.ProgressBar; 

using Syncfusion.SfNumericTextBox.XForms.iOS;

using Syncfusion.SfNumericUpDown.XForms.iOS;

using Syncfusion.SfRating.XForms.iOS;

using Syncfusion.SfPullToRefresh.XForms.iOS;

using Syncfusion.ListView.XForms.iOS;

using Syncfusion.SfBarcode.XForms.iOS;

using Syncfusion.XForms.iOS.MaskedEdit;

using Syncfusion.XForms.iOS.PopupLayout;

using Syncfusion.XForms.iOS.TabView;

using Syncfusion.XForms.iOS.Buttons;

using Syncfusion.XForms.iOS.TextInputLayout;

using Syncfusion.XForms.iOS.Border;

using Syncfusion.XForms.iOS.Cards;

namespace EBZ.Mobile.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            CachedImageRenderer.Init();
            SfGradientViewRenderer.Init();
            
			SfChartRenderer.Init();
			
			
			SfSunburstChartRenderer.Init();
			
			
			SfPickerRenderer.Init();
			
			
			SfLinearProgressBarRenderer.Init(); 
			
			
			SfCircularProgressBarRenderer.Init(); 
			
			
			SfNumericTextBoxRenderer.Init();
			
			
			SfNumericUpDownRenderer.Init();
			
			
			SfRatingRenderer.Init();
			
			
			SfPullToRefreshRenderer.Init();
			
			
			SfListViewRenderer.Init();
			
			
			SfBarcodeRenderer.Init();
			
			
			SfMaskedEditRenderer.Init();
			
			
			SfPopupLayoutRenderer.Init();
			
			
			SfTabViewRenderer.Init();
			
			
			SfCheckBoxRenderer.Init();
			
			
			SfTextInputLayoutRenderer.Init();
			
			
			SfButtonRenderer.Init();
			
			
			SfBorderRenderer.Init();
			
			
			SfCardViewRenderer.Init();
			
			LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
