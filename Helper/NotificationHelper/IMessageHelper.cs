using System.Threading.Tasks;
using Microsoft.BotBuilderSampless.Data.Models;

namespace Microsoft.BotBuilderSamples.Helper.NotificationHelper
{
    public interface IMessageHelper
    {
        public Task<Message> PostMessage( string id, string message);
    }
}
