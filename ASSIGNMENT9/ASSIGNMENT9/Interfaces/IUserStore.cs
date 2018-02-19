using System.Collections.Generic;

namespace ASSIGNMENT9.Interfaces
{
    public interface IUserStore
    {
        void CreateUser(string name);
        IList<string> ListAll();
    }

    public class InMemoryUserStore : IUserStore
    {
        private IList<string> _users = new List<string>();

        public void CreateUser(string name)
        {
            _users.Add(name);
        }

        public IList<string> ListAll()
        {
            return _users;
        }
    }
}