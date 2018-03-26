using System;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;
using JonBot.Model;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;

namespace JonBot.Database
{
    public class ConnectDB
    {
        private DocumentClient client;

        public ConnectDB()
        {
            this.client = new DocumentClient(new Uri(ConfigurationManager.AppSettings["EndPointUrl"]), ConfigurationManager.AppSettings["PrimaryKey"]);
            
        }

        public async Task CreateDocumentIfNotExists(string databaseName, string collectionName, MessageModel message)
        {
            try
            {
                await this.client.ReadDocumentAsync(UriFactory.CreateDocumentUri(databaseName, collectionName, message.Id));
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    await this.client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName), message);
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task SaveMessage(string databaseName, string collectionName, MessageModel message)
        {
            try
            {
                await this.client.ReadDocumentAsync(UriFactory.CreateDocumentUri(databaseName, collectionName, message.Id));
                await this.client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(databaseName, collectionName, message.Id), message);

            }
            catch (Exception ex)
            {

            }
            
        }
    }
}