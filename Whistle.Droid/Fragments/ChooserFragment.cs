using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Whistle.Droid.Fragments
{
    public class ChooserFragment : GenericDialogFragment
    {
        public ChooserFragment() : base(Resource.Layout.MediaChooser)
        {

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
            var cameraButton = view.FindViewById<Button>(Resource.Id.cameraButton);
            var galleryButton = view.FindViewById<Button>(Resource.Id.galleryButton);

            cameraButton.Click += MediaChooserClicked;
            cameraButton.Click += MediaChooserClicked;
            return view;
        }

        private void MediaChooserClicked(object sender, EventArgs e)
        {
            switch (((Button)sender).Text)
            {
                case "CAMERA":

                    Dismiss();
                    break;
                case "GALLERY":

                    Dismiss();
                    break;
                default:
                    break;
            }
        }
    }
}