using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
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
        }

        [LuisIntent("ListCommands")]
        public async Task ListCommands(IDialogContext context, LuisResult result)
        {
            string message = "I can list speakers, list top sessions and display sesson details";
            await context.PostAsync(message);
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
                message = "found session";
            }
            else
            {
                message = "you need to provide session number";
                
            }
            await context.PostAsync(message);

        }
        /*
        [LuisIntent("builtin.intent.alarm.set_alarm")]
        public async Task SetAlarm(IDialogContext context, LuisResult result)
        {
            EntityRecommendation title;
            if (!result.TryFindEntity(Entity_Alarm_Title, out title))
            {
                title = new EntityRecommendation(type: Entity_Alarm_Title) { Entity = DefaultAlarmWhat };
            }

            EntityRecommendation date;
            if (!result.TryFindEntity(Entity_Alarm_Start_Date, out date))
            {
                date = new EntityRecommendation(type: Entity_Alarm_Start_Date) { Entity = string.Empty };
            }

            EntityRecommendation time;
            if (!result.TryFindEntity(Entity_Alarm_Start_Time, out time))
            {
                time = new EntityRecommendation(type: Entity_Alarm_Start_Time) { Entity = string.Empty };
            }

            var parser = new Chronic.Parser();
            var span = parser.Parse(date.Entity + " " + time.Entity);

            if (span != null)
            {
                var when = span.Start ?? span.End;
                var alarm = new Alarm() { What = title.Entity, When = when.Value };
                this.alarmByWhat[alarm.What] = alarm;

                string reply = $"alarm {alarm} created";
                await context.PostAsync(reply);
            }
            else
            {
                await context.PostAsync("could not find time for alarm");
            }

            context.Wait(MessageReceived);
        }
        */




        /*
    [LuisIntent("builtin.intent.alarm.turn_off_alarm")]
    public async Task TurnOffAlarm(IDialogContext context, LuisResult result)
    {
        if (TryFindAlarm(result, out this.turnOff))
        {
            PromptDialog.Confirm(context, AfterConfirming_TurnOffAlarm, "Are you sure?", promptStyle: PromptStyle.None);
        }
        else
        {
            await context.PostAsync("did not find alarm");
            context.Wait(MessageReceived);
        }
    }


    public async Task AfterConfirming_TurnOffAlarm(IDialogContext context, IAwaitable<bool> confirmation)
    {
        if (await confirmation)
        {
            this.alarmByWhat.Remove(this.turnOff.What);
            await context.PostAsync($"Ok, alarm {this.turnOff} disabled.");
        }
        else
        {
            await context.PostAsync("Ok! We haven't modified your alarms!");
        }

        context.Wait(MessageReceived);
    }

*/

            /*

        [LuisIntent("builtin.intent.alarm.alarm_other")]
        public async Task AlarmOther(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("what ?");
            context.Wait(MessageReceived);
        }

    */

        public Conference()
        {

        }
        public Conference(ILuisService service)
            : base(service)
        {
        }

        
    }
}