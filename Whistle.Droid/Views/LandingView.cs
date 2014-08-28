// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the LandingView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Whistle.Droid.Views
{
    using System;
    using Android.App;
    using Android.Content;
    using Android.Graphics;
    using Android.Graphics.Drawables;
    using Android.OS;
    using Android.Views;
    using Android.Widget;
    using Cirrious.MvvmCross.Droid.Views;
    using Whistle.Core.Modal;

    /// <summary>
    /// Defines the LandingView type.
    /// </summary>
    [Activity(Label = "View for LandingView", NoHistory = true)]
    public class LandingView : MvxActivity
    {
        /// <summary>
        /// Called when [create].
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.Landing);

            var registrationButton = FindViewById<Button>(Resource.Id.btnRegister);
            var signInButton = FindViewById<Button>(Resource.Id.btnSignIn);
            signInButton.Click += _clickAction;
            registrationButton.Click += _clickAction;
            
        }

        void _clickAction(object sender, System.EventArgs e)
        {
            (this.ViewModel as Whistle.Core.ViewModels.LandingViewModel).Show();
          
        }
       
    }
}