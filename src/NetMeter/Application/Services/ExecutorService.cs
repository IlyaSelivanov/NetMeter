using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ExecutorService
    {
        private Plan _plan;
        List<VirtualUser> _users = new List<VirtualUser>();

        public ExecutorService(Plan plan)
        {
            _plan = plan;
        }

        public void CreateUsers()
        {
            for(int i = 0; i < _plan.UsersNumber; i++)
            {
                VirtualUser user = new VirtualUser(_plan);
                user.CreateRequests();
                _users.Add(user);
            }
        }

        public async Task Execute()
        {
            foreach (var user in _users)
                await user.Run();
        }
    }
}
