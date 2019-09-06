using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GDP5
{

    public class Program
    {

        private static string outputPath = @"..\..\..\..\actual-output.json";

        public static void Main(string[] args) {
            Prob();
        }
        private static Dictionary<string, double> Dict()
        {
            return new Dictionary<string, double>()
            {
                { "GDP_2012", 0 },
                { "POPULATION_2012", 0 }
            };


        }
            public static void Prob()
        { 
            String path = @"C:\Users\CGI\GDP5\Data\datafile.csv";

            StreamReader sr = new StreamReader(path);



            List<string[]> gdp = new List<string[]>();

            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                line = line.Replace("\"", "");
                string[] newLine = line.Split(',');
                gdp.Add(newLine);

            }

             String path2 = @"C:\Users\CGI\GDP5\Data\db.json";
            //JArray o1 = JArray.Parse(File.ReadAllText(path2));
            StreamReader file = new StreamReader(path2);
            JsonTextReader reader = new JsonTextReader(file);
            JToken o2 = JToken.ReadFrom(reader);



            var db = new Dictionary<string, string>();

            foreach(var country in o2)
            {
                db.Add((string)country["country"],(string)country["continent"]);
                /*Console.WriteLine(db);*/
            }
            /*double a;
            double b;*/
            var result = new Dictionary<string, Dictionary<string, double>>();

            result.Add("Asia", Dict());
            result.Add("South America", Dict());
            result.Add("Oceania", Dict());
            result.Add("North America", Dict());
            result.Add("Europe", Dict());
            result.Add("Africa", Dict());

            foreach (var item in gdp)
            {
                string country = item[0];
                if(db.ContainsKey(country))
                {

                    result[db[item[0]]]["GDP_2012"] += Convert.ToDouble(item[7]);
                    result[db[item[0]]]["POPULATION_2012"] += Convert.ToDouble(item[4]);

/*                    string continent = db[country];
*/                    /*Console.WriteLine(country);
                    Console.WriteLine(continent);*/

                    /*a = Convert.ToDouble(item[4]);
                    b = Convert.ToDouble(item[7]);*/
                   /* result[continent]["GDP_2012"] += Convert.ToDouble(item[7]);
                    result[continent]["POPULATION_2012"] += Convert.ToDouble(item[4]);
*/
                }


            }



            /*foreach (var go in result)
            {
                Console.WriteLine($"{go.Key}");
                foreach (var item2 in go.Value)
                {
                    Console.WriteLine($"Key: {item2.Key}, Value: {item2.Value}");
                }
                *//*Console.WriteLine(go.Value);*//*
            }*/


            File.WriteAllText(outputPath, JsonConvert.SerializeObject(result));

        }



    }

}



    