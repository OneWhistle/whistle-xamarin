

using Android.Widget;
using Cirrious.MvvmCross.Binding;
using Cirrious.MvvmCross.Binding.Droid.Target;
namespace Whistle.Droid
{
    public class MvxSelectorBinding : MvxAndroidTargetBinding
    {

        public MvxSelectorBinding(Button btn)
            : base(btn)
        {
        }

        public override Cirrious.MvvmCross.Binding.MvxBindingMode DefaultMode { get { return MvxBindingMode.OneTime; } }

        protected override void SetValueImpl(object target, object value)
        {
            var button = (Button)target;
            button.SetBackgroundResource((int)value);
        }

        public override System.Type TargetType { get { return typeof(int); } }
    }
}