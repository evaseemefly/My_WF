using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace t9_MongodbTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Person p1=new Person()
            {
                Id = ObjectId.GenerateNewId(),
                Name = "nll",
                Age = 38
            };
            string collectionName = p1.GetType().Name + "s";


            string serverIP = System.Configuration.ConfigurationManager.AppSettings["Server"];

            MongoUrl url=new MongoUrl(serverIP);
            MongoServerSettings setting = MongoServerSettings.FromUrl(url);
            MongoServer server=new MongoServer(setting);

            MongoDatabase db = server.GetDatabase("hm13");
            if (db.CollectionExists(collectionName))
            {
                MongoCollection<Person> collectionPerson = db.GetCollection<Person>(collectionName);

                //增加
                //collectionPerson.Insert(p1);

                //查询
                QueryBuilder<Person> query = new QueryBuilder<Person>();
                IMongoQuery q1 = query.GT(p => p.Age, 10);
                IMongoQuery q2 = query.Matches(p=>p.Name,new BsonRegularExpression("sk"));
                IMongoQuery q = query.And(q1, q2);
                MongoCursor<Person> cursor = collectionPerson.Find(q);
                foreach (var person in cursor)
                {
                    Console.WriteLine(person);
                }
            }
            else
            {
                CommandResult result = db.CreateCollection(collectionName);
            }

            Console.WriteLine("OK");
            Console.ReadKey();
        }
    }
    public partial class Person
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return string.Format("id:{0},name:{1},age:{2}", Id, Name, Age);
        }
    }
}
