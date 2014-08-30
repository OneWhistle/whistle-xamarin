using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using System;

namespace Whistle.Droid.Views
{
    public class WhistleButton : Button
    {
        private const string Tag = "Button";

        protected WhistleButton(IntPtr javaReference, JniHandleOwnership transfer) 
            : base(javaReference, transfer)
        {
        }

        public WhistleButton(Context context) 
            : this(context, null)
        {
        }

        public WhistleButton(Context context, IAttributeSet attrs) 
            : this(context, attrs, 0)
        {
        }

        public WhistleButton(Context context, IAttributeSet attrs, int defStyle) 
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