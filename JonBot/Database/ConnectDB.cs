using System;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;
using JonBot.Model;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;

namespace JonBot.Database
{
    public class ConnectDB
    {
        private DocumentClient client;
        string DatabaseName = ConfigurationManager.AppSettings["DatabaseName"];

        public ConnectDB()
        {
            this.client = new DocumentClient(new Uri(ConfigurationManager.AppSettings["EndPointUrl"]), ConfigurationManager.AppSettings["PrimaryKey"]);
            
        }

        public async Task CreateDocumentIfNotExists(string collectionName, Activity activity)
        {
            try
            {
                await this.client.ReadDocumentAsync(UriFactory.CreateDocumentUri(DatabaseName, collectionName, activity.Id));
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    await this.client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseName, collectionName), new MessageModel { Id = activity.Id, Message = activity.Text, Name = activity.Name, Status = "1" });
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task SaveMessage(string collectionName, Activity activity)
        {
            try
            {
                await this.client.ReadDocumentAsync(UriFactory.CreateDocumentUri(DatabaseName, collectionName, activity.Id));
                MessageModel message = new MessageModel();
                message.Id = activity.Id;
                message.Message = activity.Text;
                message.Name = activity.Name;
                message.Status = "1";
                await this.client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseName, collectionName, activity.Id), message);

            }
            catch (Exception ex)
            {

            }
            
        }
    }
}