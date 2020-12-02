using FullStack.Domain;
using System.Threading.Tasks;

namespace FullStack.Application.Interfaces
{
    /// <summary>
    /// Component Service Interface
    /// </summary>
    public interface IComponentService
    {
        /// <summary>
        /// Retrieve components from the component database
        /// </summary>
        /// <param name="listComponentViewModel">View Model containng information to shape the list request</param>
        /// <returns></returns>
        Task<ListComponentResponse> ListComponents(ListComponentViewModel listComponentViewModel);

        /// <summary>
        /// Update a component in the component database
        /// </summary>
        /// <param name="id">Component id to update</param>
        /// <param name="updateComponentViewModel">Values to change on the component</param>
        /// <returns></returns>
        Task UpdateComponent(int id, UpdateComponentViewModel updateComponentViewModel);
    }
}
