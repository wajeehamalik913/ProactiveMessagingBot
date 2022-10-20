// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Teams;
using Microsoft.Bot.Schema;
using Microsoft.Bot.Schema.Teams;
using Microsoft.BotBuilderSamples.Data;
using Microsoft.BotBuilderSamples.Helper.NotificationHelper;
using Microsoft.BotBuilderSamples.Helper.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Microsoft.BotBuilderSamples.Bots
{
    public class EchoBot : ActivityHandler
    {
       
        private readonly ILogger _logger;
        private readonly IDbContextFactory<BotDbContext> _contextFactory;
        private readonly IBotUser _userHelper;
        private readonly IMessageHelper _messageHelper;


        public EchoBot(
          
           ILogger<EchoBot> logger,
           IDbContextFactory<BotDbContext> contextFactory,
           IBotUser userHelper,
            IMessageHelper messageHelper
           )
        {
            _messageHelper = messageHelper;
            _logger = logger;
            _contextFactory = contextFactory;
            _userHelper = userHelper;
          

        }
        protected override async Task OnInstallationUpdateActivityAsync(ITurnContext<IInstallationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            Guid transactionId = Guid.NewGuid();
            try
            {
                var activity = turnContext.Activity;
                var currentMember = await TeamsInfo.GetMemberAsync(turnContext, activity.From.Id, cancellationToken);
                if (activity.Action.Equals("add", StringComparison.Ordinal))
                {

                    await _userHelper.AddUpdateConversationRefrence(activity, currentMember.AadObjectId, currentMember.UserPrincipalName);
                    await _messageHelper.PostMessage(currentMember.AadObjectId,"Welcome");
                }
                else if (activity.Action.Equals("remove", StringComparison.Ordinal))
                {
                    // do nothing

                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, transactionId.ToString());
            }
        }
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var teamConversationData = turnContext.Activity.GetChannelData<TeamsChannelData>();
            await turnContext.SendActivityAsync(MessageFactory.Text("Your ID is " + turnContext.Activity.From.Id), cancellationToken);
            await turnContext.SendActivityAsync(MessageFactory.Text("My ID is " + turnContext.Activity.Recipient.Id), cancellationToken);
            await turnContext.SendActivityAsync(MessageFactory.Text("Tenant ID is " + teamConversationData.Tenant.Id));
            await turnContext.SendActivityAsync(MessageFactory.Text("Service URL is " + turnContext.Activity.ServiceUrl));
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var welcomeText = "Hello and welcome!";
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
                }
            }
        }
    }
}
