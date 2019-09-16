using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EBZ.Mobile.Views.Sales
{
    /// <summary>
    /// Page to show the empty cart
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentSuccessfulPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyCartPage" /> class.
        /// </summary>
        public PaymentSuccessfulPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when view size is changed.
        /// </summary>
        /// <param name="width">The Width</param>
        /// <param name="height">The Height</param>
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width > height)
            {
                if (Device.Idiom == TargetIdiom.Phone)
                {
                    ErrorImage.IsVisible = false;
                }
            }
            else
            {
                ErrorImage.IsVisible = true;
            }
        }
    }
}