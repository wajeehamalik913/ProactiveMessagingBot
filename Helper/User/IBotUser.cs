using Microsoft.Bot.Schema;
using System;
using System.Threading.Tasks;

namespace Microsoft.BotBuilderSamples.Helper.User
{
    public interface IBotUser
    {
        Task AddUpdateConversationRefrence(IInstallationUpdateActivity activity, string AadObjectId, string upn);
    }
}
