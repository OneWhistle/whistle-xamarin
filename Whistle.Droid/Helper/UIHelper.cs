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
using Android.Util;

namespace Whistle.Droid
{
    public class FontCache
    {
        const string tag = "FontCache";
        private static Dictionary<string, Typeface> fontCache = new Dictionary<String, Typeface>();

        public static Typeface Get(string name, Context context)
        {
            if (!fontCache.ContainsKey(name))
            {
                Typeface tf = null;
                try
                {
                    tf = Typeface.CreateFromAsset(context.Assets, name);
                }
                catch (Exception e)
                {
                    Log.Error(tag, e.Message);
                    return null;
                }
                fontCache.Add(name, tf);
            }
            return fontCache[name];
        }
    }

    public class UIHelper
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
 /*           View popupView = LayoutInflater.Inflate(layoutID, null);
            PopupWindow popupWindow = new PopupWindow(popupView, Android.Widget.RelativeLayout.LayoutParams.FillParent, Android.Widget.RelativeLayout.LayoutParams.FillParent);
            popupWindow.Focusable = true;
            popupWindow.AnimationStyle = Resource.Style.Animation;
            var backImage = Resources.GetDrawable(Resource.Drawable.transparent);
            popupWindow.SetBackgroundDrawable(backImage);
            int[] location = new int[2];
            anchorView.GetLocationOnScreen(location);
            popupWindow.ShowAtLocation(anchorView, GravityFlags.NoGravity, location[0], location[1] + anchorView.Height);*/
        }

        /**
          * Sets a font on a textview
          * @param textview
          * @param font
          * @param context
          */
        public static void SetCustomFont(TextView textview, string font, Context context)
        {
            if (string.IsNullOrEmpty(font))            
                return;
            Typeface tf = FontCache.Get(font, context);
            if (tf != null)
            {
                textview.SetTypeface(tf, TypefaceStyle.Normal);
            }
        }

        /**
     * Sets a font on a textview based on the custom com.my.package:font attribute
     * If the custom font attribute isn't found in the attributes nothing happens
     * @param textview
     * @param context
     * @param attrs
     */
        public static void SetCustomFont(TextView textview, Context context, IAttributeSet attrs)
        {
            TypedArray a = context.ObtainStyledAttributes(attrs, Resource.Styleable.CustomFonts);
            var customFont = a.GetString(Resource.Styleable.CustomFonts_customFont);
            SetCustomFont(textview,customFont, context);
            a.Recycle();
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