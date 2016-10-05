using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Microsoft.Cognitive.LUIS;

namespace BasicLuis
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                try
                {
                    LuisClient Client = new LuisClient("67ae8e7f-f82d-46e7-9102-f1d05626935b", "c10581d6707d4fc5aed63ab9d6e29f74", true);
                    var Luisresult = await Client.Predict(activity.Text);
                    var topResult = Luisresult.TopScoringIntent;

                    if (topResult!=null)
                    {
                        switch (topResult.Name.ToLower())
                        {
                            case "listspeakers":
                                await SendMessageBack("So, you want top speakers? better luck next time :)", activity);

                                break;

                            case "listtopsessions":
                                await SendMessageBack("All sessions sucked, unfortunately", activity);

                                break;
                            case "hello":
                                await SendMessageBack("Hello to you too!", activity);

                                break;
                            case "sessiondetails":
                                if (Luisresult.Entities == null)
                                {
                                    await SendMessageBack("You have to write session number", activity);

                                }
                                else
                                {
                                    if (Luisresult.Entities.Count>0 && Luisresult.Entities["Session"]!=null)
                                        await SendMessageBack("you want details about session "+ Luisresult.Entities["Session"].FirstOrDefault().Value.ToString(), activity);

                                    else
                                        await SendMessageBack("you need to specify session number ", activity);
                                }
                                
                                break;
                        }
                    
                    }


                    //foreach (Intent int1 in result.Intents)
                    //{
                    //    await SendMessageBack("score: " + int1.Score.ToString() + " Intent " + int1.Name, activity);
                    //
                    //}


                }
                catch (Exception e)
                {

                    await SendMessageBack(e.ToString(), activity);
                }
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        public async Task SendMessageBack(String message, Activity incomingMessage)
        {
            ConnectorClient connector = new ConnectorClient(new Uri(incomingMessage.ServiceUrl));
            var responseMessage = incomingMessage.CreateReply(message);

            await connector.Conversations.ReplyToActivityAsync(responseMessage);


        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}