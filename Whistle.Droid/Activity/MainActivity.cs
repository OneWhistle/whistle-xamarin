using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Whistle.Droid.Fragments;
using Fragment = Android.Support.V4.App.Fragment;

namespace Whistle.Droid
{
    [Activity(MainLauncher = true, Icon = "@drawable/whistle_logo_green")]
    public class MainActivity : SlidingFragmentActivity
    {
        int baseFragment;
        private readonly int _titleRes;
        protected Fragment menuFragment;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            #region Menu

            SetBehindContentView(Resource.Layout.MenuHolder);

            #endregion

            #region First Screen

            if (FragmentManager.BackStackEntryCount == 0)
            {
                try
                {
                    var transaction = SupportFragmentManager.BeginTransaction();
                    menuFragment = new MenuFragments();
                    transaction.Replace(Resource.Id.MenuFrame, menuFragment);
                    transaction.Commit();

                    var landingFragment = new LandingFragments();
                    baseFragment = landingFragment.Id;
                    SwitchScreen(landingFragment, false, true);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception : " + ex.Message);
                }
            }
            else
                menuFragment =
                   (Fragment)
                   SupportFragmentManager.FindFragmentById(Resource.Id.MenuFrame);
            SlidingMenu.ShadowWidthRes = Resource.Dimension.shadow_width;
            SlidingMenu.BehindOffsetRes = Resource.Dimension.slidingmenu_offset;
            SlidingMenu.ShadowDrawableRes = Resource.Drawable.shadow;
            SlidingMenu.FadeDegree = 0.25f;
            //SlidingMenu.TouchModeAbove = TouchMode.Fullscreen;

            SetSlidingActionBarEnabled(false);

            #endregion
        }

        #region Switch Screen

        public int SwitchScreen( Fragment fragment, bool animated = true, bool isRoot = false)
        {
            var transaction = SupportFragmentManager.BeginTransaction();
            if (animated)
            {
                int animIn, animOut;
                GetAnimationForFragment(fragment, out animIn, out animOut);
                transaction.SetCustomAnimations(animIn, animOut);
            }
            transaction.Replace(Resource.Id.contentArea, fragment); // need to add first view
            if (!isRoot)
                transaction.AddToBackStack(null);
          //  SetUpActionBar(!isRoot);

            return transaction.Commit();
        }

        #endregion

        #region Instance Save and Restore

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutInt("baseFragment", baseFragment);
        }

        protected override void OnRestoreInstanceState(Bundle savedInstanceState)
        {
            base.OnRestoreInstanceState(savedInstanceState);
            baseFragment = savedInstanceState.GetInt("baseFragment");
        }

        #endregion

        #region Get Animation For Fragment

        void GetAnimationForFragment(Fragment fragment, out int animIn, out int animOut)
        {
            animIn = Resource.Animation.enter;
            animOut = Resource.Animation.exit;

            switch (fragment.GetType().Name)
            {
                case "SnapMailFragment":
                    animIn = Resource.Animation.product_detail_in;
                    animOut = Resource.Animation.product_detail_out;
                    break;
                case "ComposeMail":
                    animIn = Resource.Animation.basket_in;
                    break;
            }
        }

        #endregion

    }
}

