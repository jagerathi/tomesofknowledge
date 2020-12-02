using FullStack.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FullStack.Persistence
{
    public class ComponentDatabase
    {

        public static Task<List<Component>> GetComponents()
        {            
            var list = new List<Component>();

            for (int i = 0; i < 100; i++)
            {
                list.Add(new Component(i));
            }

            return Task.FromResult(list);
        }
    }
}
