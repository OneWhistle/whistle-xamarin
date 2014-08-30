using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using System;

namespace Whistle.Droid.Views
{
    /// <summary>
    /// https://github.com/Cheesebaron/Cheesebaron.FontSample/
    /// </summary>
    public class WhistleTextView: TextView
    {        
        private const string Tag = "TextView";

        protected WhistleTextView(IntPtr javaReference, JniHandleOwnership transfer) 
            : base(javaReference, transfer)
        {
        }

        public WhistleTextView(Context context) 
            : this(context, null)
        {
        }

        public WhistleTextView(Context context, IAttributeSet attrs) 
            : this(context, attrs, 0)
        {
        }

        public WhistleTextView(Context context, IAttributeSet attrs, int defStyle) 
            : base(context, attrs, defStyle)
        {
            UIHelper.SetCustomFont(this, context, attrs);
        }

        public void SetCustomFont(string asset)
        {
            UIHelper.SetCustomFont(this, asset, Context);
        }

    }
}