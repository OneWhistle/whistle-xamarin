

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Droid.Fragging.Fragments;
using System;
using Whistle.Core.ViewModels;

namespace Whistle.Droid.Fragments
{
    public class GenericFragment : MvxFragment, DatePickerDialog.IOnDateSetListener
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

        //test
        private TextView dobTextView;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(_layoutId, null);


            // Sorryyyyyyyyyyyy!! For Mean Time We'll improve this
            if (_layoutId == Resource.Layout.Registration)
            {

                //datepicker
                dobTextView = view.FindViewById<TextView>(Resource.Id.dobTextView);

                dobTextView.Focusable = false;
                //dobTextView.Click += delegate
                //{
                //    var dialog = new DatePickerDialogFragment(Activity, Convert.ToDateTime(dobTextView.Text), this);
                //    dialog.Show(SupportFragmentManager "date");
                //};

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

        public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
        {
            dobTextView.Text = new DateTime(year, monthOfYear + 1, dayOfMonth).ToString(); ;
        }
    }

    /// <summary>
    /// https://github.com/MvvmCross/MvvmCross-Tutorials/blob/master/Fragments/FragmentSample.UI.Droid/Views/Frags/Dialog/NameDialogFragment.cs
    /// </summary>
    public class GenericDialogFragment : MvxDialogFragment,DatePickerDialog.IOnDateSetListener
    {
        readonly int _layoutId;
        readonly int _backgroundResourceId;
        private readonly Context _context;
        private readonly bool _isDatePicker;

        public GenericDialogFragment(int layoutId)
            : this(layoutId, Resource.Color.app_gray_modal_color)
        {

        }

        //Date picker
        public GenericDialogFragment(Context context)
        {
            _context = context;
            _isDatePicker = true;
        }


        public GenericDialogFragment(int layoutId, int backgroundResourceId)
        {
            this._layoutId = layoutId;
            this._backgroundResourceId = backgroundResourceId;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (_isDatePicker)
                return base.OnCreateView(inflater, container, savedInstanceState);
            else
            {
                var view = this.BindingInflate(_layoutId, container);
                return view;
            }
        }

        public override Dialog OnCreateDialog(Bundle savedState)
        {
            base.EnsureBindingContextSet(savedState);
            DateTime _date = DateTime.Now;

            if (!_isDatePicker)
            {
              var  dialog = new Dialog(Activity, Android.Resource.Style.ThemeHoloNoActionBarFullscreen);
                dialog.Window.SetBackgroundDrawableResource(_backgroundResourceId);
                return dialog;
            }
            else
            {
               var DateDialog = new DatePickerDialog(_context, this, _date.Year, _date.Month - 1, _date.Day);
               return DateDialog;
            }
           
        }

        public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
        {
            var user = ((LandingViewModel)ViewModel).NewUser;
            if (user != null)
            {
                user.DOB = new DateTime(year, monthOfYear + 1, dayOfMonth).ToString("dd-MM-yyyy");
            }
        }
    }   
}