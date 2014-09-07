

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Droid.Fragging.Fragments;
using Whistle.Core.ViewModels;

namespace Whistle.Droid.Fragments
{
    public class GenericFragment : MvxFragment
    {
        readonly int _layoutId;
        readonly int _menuResId;

        public GenericFragment(int layoutId, int menuIconRes)
        {
            _layoutId = layoutId;
            _menuResId = menuIconRes;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            HasOptionsMenu = true;
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(_layoutId, null);


            // Sorryyyyyyyyyyyy!! For Mean Time We'll improve this
            if (_layoutId == Resource.Layout.Registration)
            {
                ((LandingViewModel)ViewModel).IsGenderChanged = (type, change) =>
                {
                    var maleImage = view.FindViewById<ImageButton>(Resource.Id.maleButton);
                    var femaleImage = view.FindViewById<ImageButton>(Resource.Id.femaleButton);
                    switch (type)
                    {
                        case 0:
                            if (change)
                            {
                                System.Console.WriteLine("Yes Male Change");
                                maleImage.SetBackgroundResource(Resource.Drawable.male_green_icon);
                            }
                            else
                            {
                                System.Console.WriteLine("No Male Change");
                                maleImage.SetBackgroundResource(Resource.Drawable.male_grey_icon);
                            }
                            break;
                        case 1:
                            if (change)
                            {
                                System.Console.WriteLine("Yes Female Change");
                                femaleImage.SetBackgroundResource(Resource.Drawable.female_green_icon);
                            }
                            else
                            {
                                System.Console.WriteLine("No Female Change");
                                femaleImage.SetBackgroundResource(Resource.Drawable.female_grey_icon);
                            }
                            break;
                        default:
                            break;
                    }
                };
            }

            return view;
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(_menuResId, menu);  
            base.OnCreateOptionsMenu(menu, inflater);
        }

        //public void RegisterControls(string _screenName)
        //{
        //    var currentViewModel=((LandingViewModel)ViewModel);
        //    switch (_screenName)
        //    {
        //        case "registration":
        //            if (currentViewModel.NewUser.IsMale)
        //                View.FindViewById<ImageButton>(Resource.Id.maleButton);
        //            break;
        //        default:
        //            break;
        //    }
        //}
    }

    /// <summary>
    /// https://github.com/MvvmCross/MvvmCross-Tutorials/blob/master/Fragments/FragmentSample.UI.Droid/Views/Frags/Dialog/NameDialogFragment.cs
    /// </summary>
    public class GenericDialogFragment : MvxDialogFragment
    {
        readonly int _layoutId;
        readonly int _backgroundResourceId;


        public GenericDialogFragment(int layoutId)
            : this(layoutId, Resource.Color.app_gray_modal_color)
        {

        }

        public GenericDialogFragment(int layoutId, int backgroundResourceId)
        {
            this._layoutId = layoutId;
            this._backgroundResourceId = backgroundResourceId;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = this.BindingInflate(_layoutId, container);
            return view;
        }

        public override Dialog OnCreateDialog(Bundle savedState)
        {
            base.EnsureBindingContextSet(savedState);
            var dialog = new Dialog(Activity, Android.Resource.Style.ThemeHoloNoActionBarFullscreen);
            //dialog.RequestWindowFeature((int)WindowFeatures.NoTitle);
            //dialog.Window.SetLayout(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            dialog.Window.SetBackgroundDrawableResource(_backgroundResourceId);
            return dialog;
        }
    }   
}