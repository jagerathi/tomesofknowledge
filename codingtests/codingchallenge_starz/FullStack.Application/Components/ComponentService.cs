using FullStack.Application.Database;
using FullStack.Application.Interfaces;
using FullStack.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FullStack.Application.Components
{
    /// <summary>
    /// Component service to handle manipulating our component entities
    /// </summary>
    public class ComponentService : IComponentService
    {
        /// <summary>
        /// Retrieve components from the component database
        /// </summary>
        /// <param name="listComponentViewModel">View Model containng information to shape the list request</param>
        /// <returns></returns>
        public async Task<ListComponentResponse> ListComponents(ListComponentViewModel listComponentViewModel)
        {
            // TODO Number 1 Sorting and Paging
            var components = await ComponentDatabase.GetComponents();

            var pageSize = listComponentViewModel.PageSize.GetValueOrDefault(9999);
            var pageNo = listComponentViewModel.PageNumber.GetValueOrDefault(0);

            var sortBy = listComponentViewModel.SortField ?? SortFieldConstants.SORT_FIELD_ID;
            var sortDirection = listComponentViewModel.SortDir ?? SortDirConstants.SORT_DIR_ASC;

            if(sortBy == SortFieldConstants.SORT_FIELD_ID && sortDirection == SortDirConstants.SORT_DIR_ASC)
            {
                components = components.OrderBy(x => x.Id).Skip(pageSize*pageNo).Take(pageSize).ToList();

            }
            else if(sortBy == SortFieldConstants.SORT_FIELD_ID && sortDirection == SortDirConstants.SORT_DIR_DESC)
            {
                components = components.OrderByDescending(x => x.Id).Skip(pageSize*pageNo).Take(pageSize).ToList();
            }
            else if(sortBy == SortFieldConstants.SORT_FIELD_NAME && sortDirection == SortDirConstants.SORT_DIR_ASC)
            {
                components = components.OrderBy(x => x.Name).ThenBy(x => x.Id).Skip(pageSize*pageNo).Take(pageSize).ToList();
                
            }
            else if(sortBy == SortFieldConstants.SORT_FIELD_NAME && sortDirection == SortDirConstants.SORT_DIR_DESC)
            {
                components = components.OrderByDescending(x => x.Name).ThenByDescending(x => x.Id).Skip(pageSize*pageNo).Take(pageSize).ToList();
            }
            else if(sortBy == SortFieldConstants.SORT_FIELD_TYPE && sortDirection == SortDirConstants.SORT_DIR_ASC)
            {
                components = components.OrderBy(x => x.Type).ThenBy(x => x.Id).Skip(pageSize*pageNo).Take(pageSize).ToList();
            }
            else if(sortBy == SortFieldConstants.SORT_FIELD_TYPE && sortDirection == SortDirConstants.SORT_DIR_DESC)
            {
                components = components.OrderByDescending(x => x.Type).ThenByDescending(x => x.Id).Skip(pageSize*pageNo).Take(pageSize).ToList();
            }
            else if(sortBy == SortFieldConstants.SORT_FIELD_STATUS && sortDirection == SortDirConstants.SORT_DIR_ASC)
            {
                components = components.OrderBy(x => x.Status).ThenBy(x => x.Id).Skip(pageSize*pageNo).Take(pageSize).ToList();
                
            }
            else if(sortBy == SortFieldConstants.SORT_FIELD_STATUS && sortDirection == SortDirConstants.SORT_DIR_DESC)
            {
                components = components.OrderByDescending(x => x.Status).ThenByDescending(x => x.Id).Skip(pageSize*pageNo).Take(pageSize).ToList();
            }
        

            return new ListComponentResponse()
            {
                Total = components.Count(),
                Components = components
            };
        }

        /// <summary>
        /// Update a component in the component database
        /// </summary>
        /// <param name="id">Component id to update</param>
        /// <param name="updateComponentViewModel">Values to change on the component</param>
        /// <returns></returns>
        public async Task UpdateComponent(int id, UpdateComponentViewModel updateComponentViewModel)
        {
            var component = await ComponentDatabase.GetById(id);
            if(component==null) throw new Exception($"the requested id - {id} - was not found.");

            component.Status = updateComponentViewModel.Status;
        }
    }
}
