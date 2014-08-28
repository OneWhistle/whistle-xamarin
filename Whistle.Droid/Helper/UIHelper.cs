using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Droid.Views;

namespace Whistle.Droid
{
    public class UIHelper : MvxActivity
    {
        private static readonly UIHelper _helper = new UIHelper();

        public static UIHelper Helper
        {
            get { return _helper; }
        }

        /// <summary>
        /// Modal popup view wuth transparent background
        /// </summary>
        /// <param name="anchorView">View that will use to show popup from</param>
        /// <param name="layoutID">Layout view that will use as popup</param>
        public void ShowModalPopup(View anchorView, int layoutID)
        {
            View popupView = LayoutInflater.Inflate(layoutID, null);
            PopupWindow popupWindow = new PopupWindow(popupView, Android.Widget.RelativeLayout.LayoutParams.FillParent, Android.Widget.RelativeLayout.LayoutParams.FillParent);
            popupWindow.Focusable = true;
            popupWindow.AnimationStyle = Resource.Style.Animation;
            var backImage = Resources.GetDrawable(Resource.Drawable.transparent);
            popupWindow.SetBackgroundDrawable(backImage);
            int[] location = new int[2];
            anchorView.GetLocationOnScreen(location);
            popupWindow.ShowAtLocation(anchorView, GravityFlags.NoGravity, location[0], location[1] + anchorView.Height);
        }

        public static void ApplyFont(Context context, View root, string fontName)
        {
            try
            {
                if (root.GetType() == typeof(ViewGroup))
                {
                    ViewGroup viewGroup = (ViewGroup)root;
                    for (int i = 0; i < viewGroup.ChildCount; i++)
                        ApplyFont(context, viewGroup.GetChildAt(i), fontName);
                }
                else if (root.GetType() == typeof(TextView))
                    ((TextView)root).SetTypeface(Typeface.CreateFromAsset(context.Assets, fontName), TypefaceStyle.Normal);
            }
            catch (Exception e)
            {
#if Debug
            Console.WriteLine("Exception Occurred : {0} ",e.InnerException);
#endif
            }
        }
    }
}