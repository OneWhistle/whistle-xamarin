// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MainView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Whistle.Droid.Views
{
    using Android.App;
    using Android.OS;

    /// <summary>
    /// Defines the MainView type.
    /// </summary>
    [Activity(Label = "View for MainView", NoHistory=true)]
    public class MainView : BaseView
    {
        /// <summary>
        /// Called when [create].
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.Landing);
        }
    }
}