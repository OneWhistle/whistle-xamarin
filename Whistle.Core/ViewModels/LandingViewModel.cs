// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the LandingViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Whistle.Core.ViewModels
{
    using System.Windows.Input;
    using Cirrious.MvvmCross.ViewModels;

    /// <summary>
    /// Define the LandingViewModel type.
    /// </summary>
    public class LandingViewModel : BaseViewModel
    {

        /// <summary>
        ///  Backing field for my command.
        /// </summary>
        private MvxCommand mainViewCommand;

        /// <summary>
        /// Gets My Command.
        /// <para>
        /// An example of a command and how to navigate to another view model
        /// Note the ViewModel inside of ShowViewModel needs to change!
        /// </para>
        /// </summary>
        public ICommand MainViewCommand
        {
            get { return this.mainViewCommand ?? (this.mainViewCommand = new MvxCommand(this.Show)); }
        }

        /// <summary>
        /// Show the view model.
        /// </summary>
        public void Show()
        {
            this.ShowViewModel<MainViewModel>();
        }
    }
}
