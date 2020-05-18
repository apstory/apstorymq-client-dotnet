using ApStory.PubSub.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Apstory.ApstoryMQClient.Extention;

namespace Apstory.ApstoryMQClient
{
    public class ApstoryMQClient
    {
        private string _apiUrl { get; set; }
        private string _key { get; set; }
        private string _client { get; set; }
        private string _topic { get; set; }
        private string _cipherKey { get; set; }

        public ApstoryMQClient(string apiUrl, string key, string client, string topic = "", string cipherKey = "")
        {
            _apiUrl = apiUrl;
            _key = key;
            _client = client;
            _topic = topic;
            _cipherKey = cipherKey;
        }

        public List<Message> Publish(List<Message> message, string topic = "")
        {
            _topic = topic == "" ? _topic : topic;
            var restClient = new RestClient(_apiUrl);
            var restRequest = new RestRequest("message?key=" + _key + "&client=" + _client + "&topic=" + _topic, Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            if (_cipherKey != string.Empty)
            {
                restRequest.AddJsonBody(EncryptedMessageList(message));
            }
            else
            {
                restRequest.AddJsonBody(message);
            }
            var response = restClient.Execute<List<Message>>(restRequest);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.Content);
            }
            return response.Data;
        }

        public async Task<List<Message>> PublishAsync(List<Message> message, string topic = "")
        {
            _topic = topic == "" ? _topic : topic;
            var restClient = new RestClient(_apiUrl);
            var restRequest = new RestRequest("message?key=" + _key + "&client=" + _client + "&topic=" + _topic, Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            if (_cipherKey != string.Empty)
            {
                restRequest.AddJsonBody(EncryptedMessageList(message));
            }
            else
            {
                restRequest.AddJsonBody(message);
            }
            var response = await restClient.ExecuteAsync<List<Message>>(restRequest);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.Content);
            }
            return response.Data;
        }

        public Messages Subscribe(string topic = "")
        {
            _topic = topic == "" ? _topic : topic;
            var restClient = new RestClient(_apiUrl);
            var restRequest = new RestRequest("message?key=" + _key + "&client=" + _client + "&topic=" + _topic, Method.GET)
            {
                RequestFormat = DataFormat.Json,
            };
            var response = restClient.Execute<Messages>(restRequest);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.Content);
            }
            if (_cipherKey != string.Empty)
            {
                return DecryptedMessageList(response.Data);
            }
            else
            {
                return response.Data;
            }
        }

        public async Task<Messages> SubscribeAsync(string topic = "")
        {
            _topic = topic == "" ? _topic : topic;
            var restClient = new RestClient(_apiUrl);
            var restRequest = new RestRequest("message?key=" + _key + "&client=" + _client + "&topic=" + _topic, Method.GET)
            {
                RequestFormat = DataFormat.Json,
            };
            var response = await restClient.ExecuteAsync<Messages>(restRequest);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.Content);
            }
            if (_cipherKey != string.Empty)
            {
                return DecryptedMessageList(response.Data);
            }
            else
            {
                return response.Data;
            }
        }

        public bool Commit(int messageId, string topic = "")
        {
            _topic = topic == "" ? _topic : topic;
            var restClient = new RestClient(_apiUrl);
            var restRequest = new RestRequest("message/" + messageId + "?key=" + _key + "&client=" + _client + "&topic=" + _topic, Method.DELETE)
            {
                RequestFormat = DataFormat.Json
            };
            var response = restClient.Execute<bool>(restRequest);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.Content);
            }

            return response.Data;
        }

        public async Task<bool> CommitAsync(int messageId, string topic = "")
        {
            _topic = topic == "" ? _topic : topic;
            var restClient = new RestClient(_apiUrl);
            var restRequest = new RestRequest("message/" + messageId + "?key=" + _key + "&client=" + _client + "&topic=" + _topic, Method.DELETE)
            {
                RequestFormat = DataFormat.Json
            };
            var response = await restClient.ExecuteAsync<bool>(restRequest);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.Content);
            }
            return response.Data;
        }

        public List<Message> Commit(List<Message> message, string topic = "")
        {
            _topic = topic == "" ? _topic : topic;
            var restClient = new RestClient(_apiUrl);
            var restRequest = new RestRequest("message?key=" + _key + "&client=" + _client + "&topic=" + _topic, Method.DELETE)
            {
                RequestFormat = DataFormat.Json
            };
            restRequest.AddJsonBody(message);
            var response = restClient.Execute<List<Message>>(restRequest);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.Content);
            }
            return response.Data;
        }

        public async Task<List<Message>> CommitAsync(List<Message> message, string topic = "")
        {
            _topic = topic == "" ? _topic : topic;
            var restClient = new RestClient(_apiUrl);
            var restRequest = new RestRequest("message?key=" + _key + "&client=" + _client + "&topic=" + _topic, Method.DELETE)
            {
                RequestFormat = DataFormat.Json
            };
            restRequest.AddJsonBody(message);
            var response = await restClient.ExecuteAsync<List<Message>>(restRequest);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.Content);
            }
            return response.Data;
        }

        public bool CreateSubscription(string topic = "")
        {
            _topic = topic == "" ? _topic : topic;
            var restClient = new RestClient(_apiUrl);
            var restRequest = new RestRequest("subscriptions?key=" + _key + "&client=" + _client + "&topic=" + _topic, Method.GET)
            {
                RequestFormat = DataFormat.Json,
            };
            var response = restClient.Execute<bool>(restRequest);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.Content);
            }
            return response.Data;
        }

        public async Task<bool> CreateSubscriptionAsync(string topic = "")
        {
            _topic = topic == "" ? _topic : topic;
            var restClient = new RestClient(_apiUrl);
            var restRequest = new RestRequest("subscriptions?key=" + _key + "&client=" + _client + "&topic=" + _topic, Method.GET)
            {
                RequestFormat = DataFormat.Json,
            };
            var response = await restClient.ExecuteAsync<bool>(restRequest);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.Content);
            }
            return response.Data;
        }

        public bool DeleteSubscription(string topic = "")
        {
            _topic = topic == "" ? _topic : topic;
            var restClient = new RestClient(_apiUrl);
            var restRequest = new RestRequest("subscriptions?key=" + _key + "&client=" + _client + "&topic=" + _topic, Method.DELETE)
            {
                RequestFormat = DataFormat.Json,
            };
            var response = restClient.Execute<bool>(restRequest);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.Content);
            }
            return response.Data;
        }

        public async Task<bool> DeleteSubscriptionAsync(string topic = "")
        {
            _topic = topic == "" ? _topic : topic;
            var restClient = new RestClient(_apiUrl);
            var restRequest = new RestRequest("subscriptions?key=" + _key + "&client=" + _client + "&topic=" + _topic, Method.DELETE)
            {
                RequestFormat = DataFormat.Json,
            };
            var response = await restClient.ExecuteAsync<bool>(restRequest);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.Content);
            }
            return response.Data;
        }

        private List<Message> EncryptedMessageList(List<Message> message)
        {
            try
            {
                foreach (var item in message)
                {
                    var encryptedBody = item.Body.ToString().AESEncrypt(_cipherKey);
                    item.Body = encryptedBody;
                    var property = new Properties
                    {
                        Key = "apstory_IsEncrypted",
                        Value = "true"
                    };
                    if (item.Properties == null)
                    {
                        item.Properties = new List<Properties>();
                    }
                    item.Properties.Add(property);
                }                
                return message;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to encrypt message body: " + ex.Message, ex.InnerException);
            }
        }

        private Messages DecryptedMessageList(Messages messages)
        {
            try
            {
                foreach (var item in messages.Message)
                {
                    if (item.Properties != null)
                    {
                        var property = item.Properties.Find(s => s.Key == "apstory_IsEncrypted");
                        if (property != null)
                        {
                            if (property.Value == "true")
                            {
                                var decryptedBody = item.Body.ToString().AESDecrypt(_cipherKey);
                                item.Body = decryptedBody;
                            }
                        }
                    }
                }
                return messages;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to decrypt message body: " + ex.Message, ex.InnerException);
            }
        }
    }
}
