using Microsoft.Bot.Schema;
using Microsoft.EntityFrameworkCore.Internal;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.BotBuilderSamples.Data;
using Microsoft.BotBuilderSamples.Data.Models;
using NPOI.SS.Formula.Functions;

namespace Microsoft.BotBuilderSamples.Helper.User
{
    public class BotUser : IBotUser
    {
        private readonly BotDbContext context;
        public async Task AddUpdateConversationRefrence(IInstallationUpdateActivity activity, string AadObjectId, string upn)
        {
            
            var user = await context.BotInfo.FirstOrDefaultAsync(x => x.UserId == AadObjectId);

            if (user == null)
            {
                Random rand = new Random();
                user = new BotInfo
                {

                   
                
                    ConversationId = activity.Conversation.Id,
                    ServiceUrl = activity.ServiceUrl,
                    UserId = AadObjectId,
                };

                await context.BotInfo.AddAsync(user);
            }
            else
            {
                //save conversation refrence 
                user.ConversationId = activity.Conversation.Id;
                user.ServiceUrl = activity.ServiceUrl;

                context.BotInfo.Update(user);
            }

            await context.SaveChangesAsync();
        }

    }
}
