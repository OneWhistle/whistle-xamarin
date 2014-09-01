using System;
using System.Threading.Tasks;

namespace Whistle.Core.Services
{
    /// <summary>
    /// Caution: This class will be moved soon, to a better place..
    /// </summary>
    public class AuthResult
    {
        // need some property there..
    }

    public interface IAuthenticationService
    {
        Task<AuthResult> Authenticate(string email, string password);
        Task<AuthResult> Authenticate(string socialNetwork);
    }
    
    public class AuthenticationService: IAuthenticationService
    {
        internal const string AUTH_FB = "{3D4E2F26-F06F-462C-A6E5-E66B0494BDEE}"; // random..
        internal const string AUTH_GPLUS = "{23C69149-2576-46FB-9BF8-F03B67F0B615}"; // same...
        /// <summary>
        /// Classic authentication
        /// </summary>
        /// <param name="email">user email</param>
        /// <param name="password">user password</param>
        /// <returns></returns>
        public async Task<AuthResult> Authenticate(string email, string password)
        {
            await Task.Delay(50); //need to have a progress bar or something..
            return new AuthResult();
        }
        /// <summary>
        /// Lazy authentication..
        /// </summary>
        /// <param name="socialNetwork"></param>
        /// <returns></returns>
        public async Task<AuthResult> Authenticate(string socialNetwork)
        {
            switch (socialNetwork)
            {
                case AUTH_FB:
                    break;
                case AUTH_GPLUS:
                    break;
                default:
                    throw new InvalidOperationException();
            }
            await Task.Delay(50);
            return new AuthResult();
        }
    }
}
