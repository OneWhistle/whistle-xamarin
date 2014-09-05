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
            var a = context.ObtainStyledAttributes(attrs, Resource.Styleable.CustomFonts);
            var customFont = a.GetString(Resource.Styleable.CustomFonts_customFont);
            SetCustomFont(textview,customFont, context);
            a.Recycle();
        }

    }
}