using Microsoft.Bot.Schema;
using Microsoft.BotBuilderSampless.Data.Models;

namespace Microsoft.BotBuilderSamples.Helper
{
    public class AdaptiveCardHelper
    {
        public static Attachment GetNotificationAdaptiveCard(Message input)
        {


            HeroCard heroCard = new()
            {
                Title = "Message",
                Subtitle = input.Content,
            };
            Attachment Attachment = heroCard.ToAttachment();


            return Attachment;


        }
    }
}
