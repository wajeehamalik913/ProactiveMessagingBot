using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.BotBuilderSamples.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.BotBuilderSampless.Data.Models;
using Microsoft.BotBuilderSamples.Data.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;

namespace Microsoft.BotBuilderSamples.Helper.NotificationHelper
{
    
        public class MessageHelper : IMessageHelper
    {
        private readonly IDbContextFactory<BotDbContext> _context;
        private readonly IBotFrameworkHttpAdapter _adapter;
        private readonly IConfiguration _configuration;
        public MessageHelper(IDbContextFactory<BotDbContext> context, IBotFrameworkHttpAdapter adapter, IConfiguration configuration)
        {
            _context = context;
            _adapter = adapter;
            _configuration = configuration;
        }



        public async Task<Message> PostMessage( string AadId, string message)
        {
            Message m = new Message();

            m.Content = message;
            BotInfo conRef = new BotInfo();

            using var context = await _context.CreateDbContextAsync();
            var user = context.BotInfo.FirstOrDefault(x => x.UserId == AadId);
            conRef.ConversationId = user.ConversationId;
            conRef.ServiceUrl = user.ServiceUrl;

            if (conRef != null)
            {
                ConversationReference reference = new()
                {
                    Conversation = new ConversationAccount()
                    {
                        Id = conRef.ConversationId
                    },
                    ServiceUrl = conRef.ServiceUrl,


                };



                await ((BotAdapter)_adapter).ContinueConversationAsync(
                       _configuration["MicrosoftAppId"],
                       reference,
                       async (context, token) =>
                       {

                           var attachment = MessageFactory.Attachment(AdaptiveCardHelper.GetNotificationAdaptiveCard(m));
                           // attachment.Summary = entity.ShortDescription;

                            await BotCallback(attachment, context, token);


                       },
                       default);



            }





            return m;



        }

        private static async Task<string> BotCallback(
           IMessageActivity message,
           ITurnContext turnContext,
           CancellationToken cancellationToken)
        {
            return (await turnContext.SendActivityAsync(message, cancellationToken)).Id;
        }




    }

}
