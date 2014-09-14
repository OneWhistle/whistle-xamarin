// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BaseViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Whistle.Core.ViewModels
{
    using System;
    using System.Linq.Expressions;
    using System.Threading;
    using Cirrious.CrossCore;
    using Cirrious.MvvmCross.ViewModels;
    using Whistle.Core.Services;
    using Whistle.Core.Helper;
    using Whistle.Core.Modal;
    using Cirrious.CrossCore.Platform;
    using Cirrious.MvvmCross.Plugins.Messenger;
    using Whistle.Core.Helpers;
    using Newtonsoft.Json;

    /// <summary>
    ///    Defines the BaseViewModel type.
    /// </summary>
    public abstract class BaseViewModel : MvxViewModel
    {

        #region Private fields
        public IMvxMessenger _messenger;
        //readonly IMvxLocationWatcher _locationWatcher;
        #endregion

        private bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value; RaisePropertyChanged(() => IsBusy);
                if (IsBusyChanged != null)
                    IsBusyChanged(isBusy);
            }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; RaisePropertyChanged(() => Title); }
        }

        #region Properties

        //TEMP testing
        private User newUser;
        public User NewUser
        {
            get { return newUser; }
            set
            {
                newUser = value; RaisePropertyChanged("NewUser");
            }
        }


        #endregion

        //Add Update
        #region User CREATION / UPDATE

        protected async void onUserUpdate(string _method = "POST")
        {
            IsBusy = true;
            var result = await ServiceHandler.PostAction<RegistrationRequest, RegistrationResponse>(new RegistrationRequest { User = newUser }, ApiAction.REGISTRATION, _method);
            IsBusy = false;

            if (result.HasError)
            {
                _messenger.Publish(new MessageHandler(this, LandingConstants.RESULT_BACKEND_ERROR).WithPayload(result.Error.GetErrorMessage()));
                onUserUpdateFail();
            }
            else
            {
                Mvx.Trace(MvxTraceLevel.Diagnostic, "onRegister Success");
                _messenger.Publish(new MessageHandler(this, LandingConstants.RESULT_REGISTER_SUCCESS));
                this.afterUserUpdate();
            }
        }

        protected virtual void onUserUpdateFail()
        {

        }

        protected abstract void afterUserUpdate();

        #endregion

        public Action<bool> IsBusyChanged { get; set; }

        public virtual bool IsUserCreationMode { get { return false; } }

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <returns>An instance of the service.</returns>
        public TService GetService<TService>() where TService : class
        {
            return Mvx.Resolve<TService>();
        }

        /// <summary>
        /// Checks if a property already matches a desired value.  Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="backingStore">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="property">The property.</param>
        protected void SetProperty<T>(
            ref T backingStore,
            T value,
            Expression<Func<T>> property)
        {
            if (Equals(backingStore, value))
            {
                return;
            }

            backingStore = value;

            this.RaisePropertyChanged(property);
        }

        protected override void InitFromBundle(IMvxBundle parameters)
        {
            base.InitFromBundle(parameters);
            if (_messenger != null)
                return;
            _messenger = Mvx.Resolve<IMvxMessenger>();
        }
    }
}
