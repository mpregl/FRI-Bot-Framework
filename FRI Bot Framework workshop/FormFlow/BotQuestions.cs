using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.FormFlow.Json;
using Microsoft.Bot.Builder;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace FormFlow
{
    [Serializable] 
    internal class BotQuestions
    {
        public static IForm<JObject> BuildForm ()
        {

            OnCompletionAsyncDelegate<JObject> processResult = async (context, state) =>
            {

                var responses = new Dictionary<string, string>();

                // Iterate the responses and do what you like here
                foreach (JProperty item in (JToken)state)
                {
                    responses.Add(item.Name, item.Value.ToString());


                    // process answers 
                    if (item.Name.Equals("Question3"))
                    {
                        // Display the repo url only if requested
                        if ((bool)item.Value == true)
                        {
                            //do something
                        }
                    }
                }

                var msg = context.MakeMessage();
                msg.Text = "Enjoy building your bot!";
                await context.PostAsync(msg);

            };
            

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("FormFlow.Questions.json"))
            {
                    var schema = JObject.Parse(new StreamReader(stream).ReadToEnd());

                    // The FormBuilder will manage where we are in the form flow and ask each subsequent question as they get answered
                    return new FormBuilderJson(schema)

                        .Message(new PromptAttribute("Start of a from flow"))

                        // Questions not yet answered
                        .AddRemainingFields()
                        
                        .Message("Thanks for the answers..")

                        // Callback once user has finished all the questions so we can process the result
                        .OnCompletion(processResult)
                        .Build();
                }
            }

    

    }
}