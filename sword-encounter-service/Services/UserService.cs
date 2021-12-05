using MongoDB.Driver;
using sword_encounter_service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sword_encounter_service.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(ISwordEncounterDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<User>("User");
        }

        public List<User> Get() =>
            _users.Find(user => true).ToList();

        public User Get(string id) =>
            _users.Find<User>(user => user.Id == id).FirstOrDefault();

        public User Get(string email, string password) =>
            _users.Find<User>(user => user.Email == email & user.Password == password).First();

        public Boolean ExistsEmail(string email) =>
            _users.Find<User>(user => user.Email == email).FirstOrDefault() != null;

        public User Create(User user)
        {            
            if (ExistsEmail(user.Email))
            {
                throw new Exception("Email já existe");
            }
            _users.InsertOne(user);
            return user;
        }

        public void Update(string id, User userIn) =>
            _users.ReplaceOne(user => user.Id == id, userIn);

        public void Remove(User userIn) =>
            _users.DeleteOne(user => user.Id == userIn.Id);

        public void Remove(string id) =>
            _users.DeleteOne(user => user.Id == id);
    }
}