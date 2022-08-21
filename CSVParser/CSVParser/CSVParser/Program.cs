using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment11
{
    class writer
    {
        public static void w(string [] genere)
        {
            foreach( var i in genere)
            {
                Console.WriteLine(i);
            }
        }
        public static void w2d(string [] a1,string[] a2)
        {
            for(int i = 0;i<a1.Length;i++)
            {
                Console.WriteLine($"{a1[i]} {a2[i]}");
            }
        }
        public static void w10(string[] a1)
        {
            for (int i = 0; i <10; i++)
            {
                Console.WriteLine($"{a1[i]}");
            }
        }
        public static void wint(string[]a)
        {
            foreach (var i in a)
            {
                if(Program.includeint(i.ToCharArray())==true)
                Console.WriteLine(i);
            }
        }

        public static void wcomedy4_10(string []a)
        {
            for (int i = 3; i <=9; i++)
            {
                Console.WriteLine(a[i]);
            }
        }

    }
    class Program
    {
       public static bool includeint(char[]a)
        {
            foreach(var i in a)
            {
                if ((int)i >= 48 && (int)i <= 57) return true;
            }
            return false;
        }
       static double total2011(string[]a)
        {
            double helper = 0;
            foreach(var i in a)
            {
                if(i!=null)
                helper += double.Parse(i);
            }
            return helper;
        }
        static double ave2014(string[] a)
        {
            double helper = 0;
            foreach (var i in a)
            {
                if (i != null)
                    helper += double.Parse(i);
            }
            return helper/a.Length;
        }
        static void Main(string[] args)
        {
            var data = File.ReadAllLines("IMDB-Movie-Data.csv")
                .Skip(1)
                .Select(line => new IMDBData(line));
            

            //1
            Console.WriteLine($"films under 100 min:");
            writer.w(data.under100());
            Console.WriteLine("----------------------------------------");
            //2
            Console.WriteLine($"\ndirector of films with dazel staring:");
            writer.w(data.vin());
            Console.WriteLine("----------------------------------------");
            //3
            Console.WriteLine($"\nmax vote in 2016");
            Console.WriteLine(data.maxvote().Title);
            Console.WriteLine(data.maxvote().Genre);
            Console.WriteLine(data.maxvote().Director);
            Console.Write(data.maxvote().Actor1+" ");
            Console.Write(data.maxvote().Actor2 + " ");
            Console.Write(data.maxvote().Actor3 + " ");
            Console.Write(data.maxvote().Actor4 + " \n");
            Console.WriteLine(data.maxvote().Year);
            Console.WriteLine(data.maxvote().Runtime);
            Console.WriteLine(data.maxvote().Rating);
            Console.WriteLine(data.maxvote().Votes);
            Console.WriteLine(data.maxvote().Revenue);
            Console.WriteLine(data.maxvote().Metascore);
            Console.WriteLine("----------------------------------------");
            //4
            Console.WriteLine($"\nsale amount");
            writer.w2d(data.brayan_name(), data.brayan_sale());
            Console.WriteLine("----------------------------------------");
            //5
            Console.WriteLine($"\ntotal sale in 2011");
            Console.WriteLine(total2011(data.total2011()));
            Console.WriteLine("----------------------------------------");
            //6
            Console.WriteLine("\n average 2014");
            Console.WriteLine(ave2014(data.average2014()));
            Console.WriteLine("----------------------------------------");
            //7
            Console.WriteLine("\n top 10 action more than 120 min");
            writer.w10(data.top10120action());
            Console.WriteLine("----------------------------------------");
            //8
            Console.WriteLine("\n include integer");
            writer.wint(data.includeint());
            Console.WriteLine("----------------------------------------");
            //9
            Console.WriteLine("\n jennifer & anne old to new and max rate to min rate");
            Console.WriteLine("---------------");
            writer.w(data.jenniferyear());
            Console.WriteLine("---------------");
            writer.w(data.jenniferrate());
            Console.WriteLine("---------------");
            writer.w(data.anneyear());
            Console.WriteLine("---------------");
            writer.w(data.annerate());
            Console.WriteLine("----------------------------------------");
            //10
            Console.WriteLine("drama or comedy??? ");
            int a = data.drama8().Length;
            int b = data.comedy8().Length;
            if (a > b) Console.WriteLine("Drama movies are more than Comedy movies");
            else if (b > a) Console.WriteLine("Comedy movies are more than Drama movies");
            else Console.WriteLine("mosavi");
            Console.WriteLine("----------------------------------------");
            //11
            Console.WriteLine("\nbad actor");
            Console.WriteLine(data.bad());
            Console.WriteLine("----------------------------------------");
            //12
            Console.WriteLine("\nmore than 10 digit ");
            writer.w(data.digit10()); 
            Console.WriteLine("----------------------------------------");
            //13
            Console.WriteLine("\n2014 or action ");
            writer.w(data.action2014());

            Console.WriteLine("----------------------------------------");
            //14
            Console.WriteLine("\n4-10 comedy ");
            writer.wcomedy4_10(data.comedy10());



            Console.ReadKey();
        }
    }

    public static class Extensions
    {
        public static Nullable<int> ParseIntOrNull(this string str)
            => !string.IsNullOrEmpty(str) ? int.Parse(str) as Nullable<int> : null;
        public static string ParseStringOrNull(this string str)
            => !string.IsNullOrEmpty(str) ? str : null;

        //For example
        public static IMDBData GetHighestMetascore(this IEnumerable<IMDBData> data)
            => data.OrderByDescending(x => x.Metascore).First();


       

        /// <summary>
        /// you must modify the name of this method and its 
        /// implementation to fit your need and create more methods like this
        //1
        public static string[] under100(this IEnumerable<IMDBData> data)
            => data.Where(x => x.Runtime <= 100).Select(x => x.Genre).Distinct().ToArray();
        //2
        public static string[] vin(this IEnumerable<IMDBData> data)
            => data.Where(x => x.Actor1=="\"Vin Diesel"||x.Actor2 == " Vin Diesel"||
            x.Actor3 == " Vin Diesel"||x.Actor4 == " Vin Diesel").Select(x => x.Director).ToArray();
        //3
        public static IMDBData maxvote(this IEnumerable<IMDBData> data)
           => data.Where(x => x.Year == 2016).OrderByDescending(x => x.Votes).First();
        //4
        public static string[] brayan_sale(this IEnumerable<IMDBData> data)
            => data.Where(x => x.Director == "Bryan Singer").OrderByDescending(x=>x.Revenue).Select(x => x.Revenue).ToArray();

        public static string[] brayan_name(this IEnumerable<IMDBData> data)
            => data.Where(x => x.Director == "Bryan Singer").OrderByDescending(x => x.Revenue).Select(x => x.Title).ToArray();
        //5
        public static string [] total2011(this IEnumerable<IMDBData> data)
            => data.Where(x => x.Year==2011).Select(x =>x.Revenue).ToArray();
        //6
        public static string[] average2014(this IEnumerable<IMDBData> data)
            => data.Where(x => x.Year == 2014).Select(x => x.Revenue).ToArray();
        //7
        public static string[] top10120action(this IEnumerable<IMDBData> data)
            => data.Where(x => x.Genre=="Action").Where(x =>x.Runtime>=120).OrderByDescending(x=>x.Revenue).Select(x => x.Title).ToArray();
        //8
        public static string[] includeint(this IEnumerable<IMDBData> data)
            => data.Select(x => x.Title).ToArray();
        //9
        public static string[] jenniferyear(this IEnumerable<IMDBData> data)
             => data.Where(x => x.Actor1 == "\"Jennifer Lawrence" || x.Actor2 == " Jennifer Lawrence" ||
             x.Actor3 == " Jennifer Lawrence" || x.Actor4 == " Jennifer Lawrence").OrderBy(x =>x.Year).Select(x => x.Title).ToArray();

        public static string[] jenniferrate(this IEnumerable<IMDBData> data)
            => data.Where(x => x.Actor1 == "\"Jennifer Lawrence" || x.Actor2 == " Jennifer Lawrence" ||
            x.Actor3 == " Jennifer Lawrence" || x.Actor4 == " Jennifer Lawrence").OrderByDescending(x => x.Rating).Select(x => x.Title).ToArray();
        
        public static string[] anneyear(this IEnumerable<IMDBData> data)
            => data.Where(x => x.Actor1 == "\"Anne Hathaway" || x.Actor2 == " Anne Hathaway" ||
            x.Actor3 == " Anne Hathaway" || x.Actor4 == " Anne Hathaway").OrderBy(x => x.Year).Select(x => x.Title).ToArray();
        public static string[] annerate(this IEnumerable<IMDBData> data)
          => data.Where(x => x.Actor1 == "\"Anne Hathaway" || x.Actor2 == " Anne Hathaway" ||
          x.Actor3 == " Anne Hathaway" || x.Actor4 == " Anne Hathaway").OrderByDescending(x => x.Rating).Select(x => x.Title).ToArray();
        //10
        public static string[] drama8(this IEnumerable<IMDBData> data)
           => data.Where(x => x.Genre == "Drama").Where(x => x.Rating >8).Select(x => x.Title).ToArray();
        public static string[] comedy8(this IEnumerable<IMDBData> data)
          => data.Where(x => x.Genre == "Comedy").Where(x => x.Rating > 8).Select(x => x.Title).ToArray();
        //11
        public static string bad(this IEnumerable<IMDBData> data)
             => data.Select(x => x.Actor1.Substring(1)).Concat(data.Select(x => x.Actor2)).Concat(data.Select(x => x.Actor3)).Concat(data.Select(x => x.Actor4))
                 .Distinct().OrderByDescending(x => data.Where(y => y.act(x) && y.Rating < 7).Count()).First();
        //12
        public static string[] digit10(this IEnumerable<IMDBData> data)
        => data.Where(x => x.Title.Length>10).Select(x => x.Title).ToArray();
        //13
        public static string[] action2014(this IEnumerable<IMDBData> data)
              => data.Where(x => x.Genre == "Action").Select(x => x.Title).Concat(data.Where(x => x.Year == 2014).Select(x => x.Title)).Distinct().ToArray();
        //14
        public static string[] comedy10(this IEnumerable<IMDBData> data)
        => data.Where(x => x.Genre=="Comedy").OrderByDescending(x=>x.Rating).Select(x => x.Title).ToArray();
    }



    public class IMDBData
    {
        public bool act(string actor)
        {
            if (Actor1.Contains(actor) || Actor2.Contains(actor) || Actor3.Contains(actor) || Actor4.Contains(actor)) return true;
            return false;
        }
        public IMDBData(string line)
        {
            var toks = line.Split(',');
            Rank = int.Parse(toks[0]);
            Title = toks[1];
            Genre = toks[2];
            Director = toks[3];
            Actor1 = toks[4];
            Actor2 = toks[5];
            Actor3 = toks[6];
            Actor4 = toks[7];
            Year = int.Parse(toks[8]);
            Runtime = int.Parse(toks[9]);
            Rating = double.Parse(toks[10]);
            Votes = int.Parse(toks[11]);
            Revenue = toks[12].ParseStringOrNull();
            Metascore = toks[13].ParseIntOrNull();
        }
        public int Rank;
        public string Title;
        public string Genre;
        public string Director;
        public string Actor1;
        public string Actor2;
        public string Actor3;
        public string Actor4;
        public int Year;
        public int Runtime;
        public double Rating;
        public int Votes;
        public string Revenue;
        public Nullable<int> Metascore;
    }
}
