
namespace Whistle.Core.Services
{
    public interface IPhoneService
    {
        string GetPhoneNumber();

        bool IsGlobalPhoneNumber(string input);
    }
}
