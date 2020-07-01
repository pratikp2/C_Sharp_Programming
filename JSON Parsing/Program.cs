using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;


namespace JSON_Parsing
{
    class Program
    {
        public class MyDetail
        {
            [DataMember]
            public string name { get; set; }
            [DataMember]
            public string age { get; set; }

            private static void ParseJSON()
            {
                //string example = @"{""name"":""John Doe"",""age"":20}";     //OR
                string example = "{  \"name\":\"John Doe\",\"age\":\"20\" }";

                // 1st Way
                Dictionary<string, string> ObjJSON1 = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(example);
                Console.WriteLine(string.Concat("1st Way  : ", ObjJSON1["name"], " " + ObjJSON1["age"]));
                Console.WriteLine();

                // 2nd Way
                dynamic ObjJSON2 = JObject.Parse(example);
                Console.WriteLine(string.Concat("2nd Way : ", ObjJSON2.name, " " + ObjJSON2.age));
                Console.WriteLine(string.Concat("2nd Way : ", ObjJSON2["name"], " " + ObjJSON2["age"]));
                Console.WriteLine();

                // 3rd Way
                MyDetail ObjJSON3 = JsonConvert.DeserializeObject<MyDetail>(example);
                Console.WriteLine(string.Concat("3rd Way : ", ObjJSON3.name, " " + ObjJSON3.age));
                Console.WriteLine();

                // 4th Way
                DataContractJsonSerializer JSONSerializer = new DataContractJsonSerializer(typeof(MyDetail));
                MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(example));
                stream.Position = 0;
                MyDetail ObjJSON4 = (MyDetail)JSONSerializer.ReadObject(stream);
                Console.WriteLine(string.Concat("4th Way : ", ObjJSON4.name, " " + ObjJSON4.age));
                Console.WriteLine();
            }
        }
        static int Main()
        {


            Console.WriteLine("Program End");
            Console.ReadLine();
            return 0;
        }
    }
}
    