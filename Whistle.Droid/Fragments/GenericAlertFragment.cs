using Android.OS;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace Whistle.Droid.Fragments
{
    /// <summary>
    /// Use to display simple alerts. For more advanced scenario
    /// pls create a custom dialog fragment.
    /// </summary>
    public class GenericAlertFragment : GenericDialogFragment
    {
        int _iconResId;
        int _titleResId;
        Func<string> _descriptionResolve;
        readonly Dictionary<int, Action> _buttonActionReference = new Dictionary<int, Action>();

        public bool HasIcon { get; private set; }

        public bool HasTitle { get; private set; }

        public bool HasDescription { get; private set; }

        public GenericAlertFragment(int backgroundResId) : base(Resource.Layout.AlertDialog, backgroundResId) { }

        /// <summary>
        /// When set, icon will appear on the top
        /// of the dialog
        /// </summary>
        /// <param name="iconResId">Icon resource Id</param>
        /// <returns>Alert Fragment instance</returns>
        public GenericAlertFragment WithIcon(int iconResId)
        {
            HasIcon = true;
            _iconResId = iconResId;
            return this;
        }
        /// <summary>
        /// Adds a title to the alert.
        /// The Alert will be below the icon
        /// </summary>
        /// <param name="textResId">Text Resource Id</param>
        /// <returns>Alert fragment instance</returns>
        public GenericAlertFragment WithTitle(int textResId)
        {
            HasTitle = true;
            _titleResId = textResId;
            return this;
        }
        /// <summary>
        /// Addds a description to the alert.
        /// It will be below the title which is below the icon.
        /// </summary>
        /// <param name="msgResId"></param>
        /// <returns></returns>
        public GenericAlertFragment WithDescription(int msgResId)
        {
            HasDescription = true;
            _descriptionResolve = () => GetString(msgResId); // I use a delegate (function) because calling GetString Here will throw IllegalStateException
            return this;
        }
        public GenericAlertFragment WithDescription(int msgResId, params string [] obj)
        {
            HasDescription = true;
            _descriptionResolve = () => string.Format(GetString(msgResId), obj); // I use a delegate (function) because calling GetString Here will throw IllegalStateException
            return this;
        }
        /// <summary>
        /// Alternatively add a description directly with text.
        /// Prefer to use <see cref="WithDescription"/>
        /// </summary>
        /// <param name="description">description string content</param>
        /// <returns>Fragment instance</returns>
        public GenericAlertFragment WithDescription(string description)
        {
            HasDescription = true;
            _descriptionResolve = () => description;
            return this;
        }
        /// <summary>
        /// Simply adds a button to the dialog.
        /// </summary>
        /// <param name="buttonTextResId">Button text resource id</param>
        /// <param name="actionOnClick">Action to perform on click</param>
        /// <returns>fragment instance</returns>
        public GenericAlertFragment AddButton(int buttonTextResId, Action actionOnClick)
        {
            if (null == actionOnClick)
                throw new ArgumentNullException("actionOnClick");
            _buttonActionReference.Add(buttonTextResId, actionOnClick);
            return this;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            var titleView = view.FindViewById<TextView>(Resource.Id.alertTitle);
            var descView = view.FindViewById<TextView>(Resource.Id.alertMsg);
            var iconView = view.FindViewById<ImageView>(Resource.Id.alertIcon);
            var buttonPlaceHolder = view.FindViewById<LinearLayout>(Resource.Id.bottomLayout);

            if (HasIcon)
                iconView.SetImageResource(_iconResId);
            else
                iconView.Visibility = ViewStates.Gone;
            if (HasTitle)
                titleView.Text = GetString(_titleResId);
            else
                titleView.Visibility = ViewStates.Gone;
            if (HasDescription)
            {
                descView.Text = _descriptionResolve(); // The description is resolved when needed.
            }
            else
                descView.Visibility = ViewStates.Gone;

            foreach (var btn in _buttonActionReference)
            {      
                var layoutParam = new LinearLayout.LayoutParams(0, LinearLayout.LayoutParams.MatchParent, 1);
                var btnView = new Button(Activity);

               // var btnView = btnInflate.FindV as Button;
                btnView.Text = GetString(btn.Key);
                
                btnView.Click += (a, b) =>
                    {
                        this.Dismiss();
                        btn.Value(); // run the button action.
                        
                    };
                buttonPlaceHolder.AddView(btnView,layoutParam);
            }

            return view;
        }
    }
}