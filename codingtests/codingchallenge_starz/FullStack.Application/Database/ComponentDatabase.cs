using FullStack.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullStack.Application.Database
{
    public class ComponentDatabase
    {
        private static List<Component> _data;
        public static Task<IEnumerable<Component>> GetComponents()
        {
            if(_data == null) 
            {
                var list = new List<Component>();

                for (int i = 0; i < 100; i++)
                {
                    list.Add(new Component(i));
                }
                _data = list;
            }

            return Task.FromResult(_data.AsEnumerable());
        }

        public static Component GetById(int id) 
        {
            if(_data==null) return null;
            return _data.FirstOrDefault(x => x.Id == id);
        }
    }
}
