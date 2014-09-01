
namespace Whistle.Core.Services
{
    public interface IRegistrationService
    {
        void PartialRegister(/*missing params*/);
        void FinishRegister(/*missing params*/);
        void Done();
    }

    public class RegistrationService: IRegistrationService
    {
        public void PartialRegister()
        {
            /*here we'll save partial registration data*/
        }
        public void FinishRegister()
        {
            /*Here we'll complete the partial registration
            after what we'll call done.
            */
            Done();
        }

        public void Done()
        {
            // there we'll call the backend api..
        }        
    }
}
