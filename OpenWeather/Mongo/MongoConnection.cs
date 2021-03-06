﻿/*


                                
                     _|      _|       _|_|_|_|_|    
                     _|_|  _|_|     _|          _|  
                     _|  _|  _|   _|    _|_|_|  _|  
                     _|      _|   _|  _|    _|  _|  
                     _|      _|   _|    _|_|_|_|    
                                    _|              
                                      _|_|_|_|_|_|  


*/
using MongoDB.Driver;
using System.Collections.Generic;

namespace csOpenWeather.Mongo
{
    internal class MongoConnection : MongoClient
    {                
        private IMongoClient MongoClient { get; set; }
        private string Username { get; set; }
        private string Password { get; set; }
        private string AuthenticationDatabase { get; set; }
        private List<string> Server { get; set; }
        private string Port { get; set; }
        private bool UseSsl { get; set; }
        private string ReplicaSet { get; set; }

        internal MongoConnection(string un, string pw, string authenticationDatabase, List<string> mongoServer, string mongoPort, bool useSsl, string replicaSet)
        {
            Username = un;
            Password = pw;
            AuthenticationDatabase = authenticationDatabase;
            Port = mongoPort;
            ReplicaSet = replicaSet;
            UseSsl = useSsl == true ? true : false;
            var useSslString = UseSsl == true ? "true" : "false";
            
            // set Mongo Hosts
            mongoServer.ForEach(mongoserver => { Server.Add(string.Format("{0}:{1}", mongoserver, mongoPort)); });
            var mongoHostsString = string.Join(",", Server);
            // end Set Mongo Hosts
            
            MongoClient = new MongoClient(
            string.Format(
                "mongodb://{0}:{1}@{2}/test?ssl={3}&replicaSet={4}&authSource={5}&retryWrites=true",
                un, pw, mongoHostsString, useSslString, replicaSet, authenticationDatabase
                )
            );
        }

        /* how to get docs from mongo
           var collection = mongo.GetDatabase("-").GetCollection<BsonDocument>("-");
           var documents = collection.Find(Builders<BsonDocument>.Filter.Empty).ToList();
           List<CurrentWeather> woList = new List<CurrentWeather>();
           foreach (var doc_ in documents) woList.Add(BsonSerializer.Deserialize<CurrentWeather>(doc_));
           return woList; */

        public MongoConnection()
        {
        }
    }
}
 