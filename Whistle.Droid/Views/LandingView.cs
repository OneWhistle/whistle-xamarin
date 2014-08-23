// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the LandingView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Whistle.Droid.Views
{
    using System;
    using Android.App;
    using Android.OS;
    using Android.Widget;
    using Cirrious.MvvmCross.Droid.Views;
    using Whistle.Core.ViewModels;

    /// <summary>
    /// Defines the LandingView type.
    /// </summary>
    [Activity(Label = "View for LandingView")]
    public class LandingView : MvxActivity
    {
        /// <summary>
        /// Called when [create].
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.SplashScreen);
            ActionBar.Hide();
            
        }

       
    }
}