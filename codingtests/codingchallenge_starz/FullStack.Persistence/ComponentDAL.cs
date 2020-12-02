using FullStack.Application.Interfaces;
using FullStack.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FullStack.Persistence
{
    public class ComponentDAL : IComponentDAL
    {
        public Task ChangeComponentStatus(int id, string status)
        {
            throw new NotImplementedException();
        }

        public async Task<ListComponentResponse> GetComponents(ListComponentViewModel listComponentViewModel)
        {
            return await ComponentDatabase.GetComponents();
        }
    }
}
