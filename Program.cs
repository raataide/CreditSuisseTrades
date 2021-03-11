using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace CreditSuisseTrades
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Trades> trade = new List<Trades>();
            DateTime referenceDate = Convert.ToDateTime("01/01/0001");
            int nTrades;
            int i = 0;

            Console.WriteLine("Would you like to input datas (D) or read a file (F)?");

            ConsoleKey resp = Console.ReadKey(false).Key;
            while (resp != ConsoleKey.D && resp != ConsoleKey.F)
            {
                Console.WriteLine("\nWould you like to input datas (D) or read a file (F)?");
                resp = Console.ReadKey(false).Key;
            }

            Console.WriteLine();




            if (resp.ToString().ToLower() == "d")
            {
                Console.WriteLine("Enter the reference date MM/dd/YYY:");
                

                while (!DateTime.TryParseExact(Console.ReadLine(), "MM/dd/yyyy", null, DateTimeStyles.None, out referenceDate))
                {
                    Console.WriteLine("Invalid date, please retry");
                }

                Console.WriteLine("Enter the number of trades:");
                nTrades = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter " + nTrades + " trades information:");
                string[] trades = new string[nTrades];
                
                while (nTrades > 0)
                {
                    
                    trades[i] = Console.ReadLine();
                    nTrades--;
                    i++;
                }

                trade = InsertTradeInfo(trades);

                GiveResponse(trade, referenceDate);

            } else
            {
                string workingDirectory = Environment.CurrentDirectory;

                string path = Path.Combine(Directory.GetParent(workingDirectory).Parent.Parent.FullName, "Files\\TradeFile.txt");
                string mensagem;
                List<string> mensagemLinha = new List<string>();

                using (StreamReader texto = new StreamReader(path))
                {
                    while ((mensagem = texto.ReadLine()) != null)
                    {
                        mensagemLinha.Add(mensagem);
                    }
                }

                referenceDate = Convert.ToDateTime(mensagemLinha[0]);
                nTrades = Convert.ToInt32(mensagemLinha[1]);
                string[] trades = new string[nTrades];

                i = 2;
                int n = 0;
                while (n < nTrades)
                {
                    trades[n] = mensagemLinha[i];
                    i++;
                    n++;
                }

                trade = InsertTradeInfo(trades);
               
                GiveResponse(trade, referenceDate);

            }
        }

        public static List<Trades> InsertTradeInfo(string[] trades)
        {
            List<Trades> trade = new List<Trades>();
            foreach (var el in trades)
            {
                var line = el.Split(' ');

                double Value;
                string ClientSector;
                DateTime NextPaymentDate;

                try
                {
                    Value = Convert.ToDouble(line[0]);
                    ClientSector = line[1];
                    NextPaymentDate = DateTime.ParseExact(line[2], "MM/dd/yyyy", CultureInfo.InvariantCulture);
                }
                catch
                {
                    Value = -1;
                    ClientSector = "";
                    NextPaymentDate = Convert.ToDateTime("01/01/0001");
                }

                trade.Add(new Trades()
                {
                    Value = Value,
                    ClientSector = ClientSector,
                    NextPaymentDate = NextPaymentDate
                });

            }
            return trade;
        }

        public static void GiveResponse(List<Trades> trades, DateTime referenceDate)
        {
            foreach (var trade in trades)
            {
                if (trade.Value == -1)
                {
                    Console.WriteLine("ERROR");
                }
                if ((int)trade.NextPaymentDate.Subtract(referenceDate).TotalDays < 30)
                {
                    Console.WriteLine("DEFAULTED");
                }
                else
                {
                    if (trade.Value > 1000000)
                    {
                        if (trade.ClientSector.ToLower() == "private")
                        {
                            Console.Write("HIGHRISK");
                        }
                        else
                        {
                            Console.Write("MEDIUMRISK");
                        }
                    }
                    else
                    {
                        Console.WriteLine("NO RISK: VALUE BELOW 1000000");
                    }
                }
            }
        }

        
    }
}
