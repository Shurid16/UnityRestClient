using System.Collections;
using System.Collections.Generic;
using RestClient.Core.Models;
using RestClient.Core.Singletons;
using UnityEngine;
using UnityEngine.Networking;
using System;
 
namespace RestClient.Core
{
    public class RestWebClient : Singleton<RestWebClient>
    {

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Get(string url, Action<Response> callback)
        {
            StartCoroutine(HttpGet(url, callback));
        }

        public void Post(string url, Dictionary<string,object> body, Dictionary<string, string> headers, Action<Response> callback)
        {
            StartCoroutine(HttpPost(url, body, headers, callback));
        }


        private const string defaultContentType = "application/json";


        public IEnumerator HttpGet(string url, Action<Response> callback)
        {
            using(UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                yield return webRequest.SendWebRequest();
                
                if(webRequest.isNetworkError){

                    Debug.Log("Status Code: " + webRequest.responseCode);
                    Debug.Log("Error: " + webRequest.error);


                    callback(new Response {
                        StatusCode = webRequest.responseCode,
                        Error = webRequest.error,
                        
                    });
                    
                   
                }
                
                else if(webRequest.isDone)
                {
                    string data = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);

                  
                    Debug.Log("Status Code: " + webRequest.responseCode);
                    Debug.Log("Data: " + data);

                    callback(new Response {
                        StatusCode = webRequest.responseCode,
                        Error = webRequest.error,
                        Data = data
                    });

                   
                }
            }
        }

        public IEnumerator HttpDelete(string url, System.Action<Response> callback)
        {
            using(UnityWebRequest webRequest = UnityWebRequest.Delete(url))
            {
                yield return webRequest.SendWebRequest();

                if(webRequest.isNetworkError){
                    callback(new Response {
                        StatusCode = webRequest.responseCode,
                        Error = webRequest.error
                    });
                }
                
                else if(webRequest.isDone)
                {
                    callback(new Response {
                        StatusCode = webRequest.responseCode
                    });
                }
            }
        }

        public IEnumerator HttpPost(string url, Dictionary<string,object> body, Dictionary<string,string> headers, Action<Response> callback)
        {
            Debug.Log(body);

            WWWForm form = new WWWForm();

           foreach(var v in body)
            {
                if(v.Value.GetType().Equals(typeof(int)))
                {
                    form.AddField(v.Key, (int)v.Value);
                }
                else
                {
                    form.AddField(v.Key, (string)v.Value);
                }
                
            }

           /* form.AddField("email", "10ms@gmail.com");
            form.AddField("phone", "+8801.....");
            form.AddField("otp", "1234");
            form.AddField("type", "email");*/

            using (UnityWebRequest webRequest = UnityWebRequest.Post(url, form))
            {
                if(headers != null)
                {
                    foreach (var v in headers)
                    {
                        webRequest.SetRequestHeader(v.Key, v.Value);
                    }
                }

                webRequest.uploadHandler.contentType = defaultContentType;
                //webRequest.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(body));
            

                yield return webRequest.SendWebRequest();

                if(webRequest.isNetworkError)
                {
                    Debug.Log("Status Code: " + webRequest.responseCode);
                    Debug.Log("Error: " + webRequest.error);

                    callback(new Response {
                        StatusCode = webRequest.responseCode,
                        Error = webRequest.error
                    });
                }
                
                else if(webRequest.isDone)
                {
                    string data = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);

                    Debug.Log("Status Code: " + webRequest.responseCode);
                    Debug.Log("Data: " + data);

                    callback(new Response {
                        StatusCode = webRequest.responseCode,
                        Error = webRequest.error,
                        Data = data
                    });
                }
            }
        }

        public IEnumerator HttpPut(string url, string body, System.Action<Response> callback, IEnumerable<RequestHeader> headers = null)
        {
            using(UnityWebRequest webRequest = UnityWebRequest.Put(url, body))
            {
                if(headers != null)
                {
                    foreach (RequestHeader header in headers)
                    {
                        webRequest.SetRequestHeader(header.Key, header.Value);
                    }
                }

                webRequest.uploadHandler.contentType = defaultContentType;
                webRequest.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(body));

                yield return webRequest.SendWebRequest();

                if(webRequest.isNetworkError)
                {
                    callback(new Response {
                        StatusCode = webRequest.responseCode,
                        Error = webRequest.error,
                    });
                }
                
                if(webRequest.isDone)
                {
                    callback(new Response {
                        StatusCode = webRequest.responseCode,
                    });
                }
            }
        }

        public IEnumerator HttpHead(string url, System.Action<Response> callback)
        {
            using(UnityWebRequest webRequest = UnityWebRequest.Head(url))
            {
                yield return webRequest.SendWebRequest();
                
                if(webRequest.isNetworkError){
                    callback(new Response {
                        StatusCode = webRequest.responseCode,
                        Error = webRequest.error,
                    });
                }
                
                if(webRequest.isDone)
                {
                    var responseHeaders = webRequest.GetResponseHeaders();
                    callback(new Response {
                        StatusCode = webRequest.responseCode,
                        Error = webRequest.error,
                        Headers = responseHeaders
                    });
                }
            }
        }
    }
}