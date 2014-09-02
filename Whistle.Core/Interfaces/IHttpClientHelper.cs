
using System.Net.Http;

namespace Whistle.Core.Interfaces
{
    /// <summary>
    /// https://github.com/jamesmontemagno/MeetupManager/blob/master/MeetupManager.Portable/Interfaces/IHttpClientHelper.cs
    /// </summary>
    public interface IHttpClientHelper
    {
        HttpMessageHandler MessageHandler { get; }
    }
}
