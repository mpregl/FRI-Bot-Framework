using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace _4.ProperLuis
{
    [LuisModel("67ae8e7f-f82d-46e7-9102-f1d05626935b", "c10581d6707d4fc5aed63ab9d6e29f74")]

    [Serializable]
    public class Conference : LuisDialog<object>
    {
        public const string Entity_Session = "Session";

        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"Command not found, sorry: " + string.Join(", ", result.Intents.Select(i => i.Intent));
            await context.PostAsync(message);
            context.Wait(MessageReceived);

        }

        [LuisIntent("Hello")]
        public async Task Hello(IDialogContext context, LuisResult result)
        {
            string message = "Well hello to you too! use \"List commands\" to show you, what can I do";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }

        [LuisIntent("ListCommands")]
        public async Task ListCommands(IDialogContext context, LuisResult result)
        {
            string message = "I can list speakers, list top sessions and display sesson details";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }

        //private Alarm turnOff;

        [LuisIntent("SessionDetails")]
        public async Task SessionDetails(IDialogContext context, LuisResult result)
        {
            EntityRecommendation Session;
            if (!result.TryFindEntity(Entity_Session, out Session))
            {
                Session = new EntityRecommendation(type: Entity_Session) { Entity = string.Empty };
            }

            int x;
            string message;
            if (int.TryParse(Session.Entity, out x)) {
                message = "found session number: " + x;
            }
            else
            {
                message = "you need to provide session number";
                
            }

            await context.PostAsync(message);

            context.Wait(MessageReceived);
        }
        

        public Conference()
        {

        }
        public Conference(ILuisService service)
            : base(service)
        {
        }

        
    }
}