using FullStack.Application.Components;
using FullStack.Application.Interfaces;
using FullStack.Domain;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FullStack.Tests
{
    /// <summary>
    /// Unit Tests For the Persistence Tier
    /// </summary>
    public class ApplicationTests
    {
        /// <summary>
        /// Test changing component status
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TestChangeStatus()
        {
            ListComponentViewModel listComponentViewModel = new ListComponentViewModel()
            {
                PageSize = 100,
                PageNumber = 0
            };

            IComponentService service = new ComponentService();

            var component = (await service.ListComponents(listComponentViewModel)).Components.First(x => x.Id == 1);

            await service.UpdateComponent(component.Id, new UpdateComponentViewModel()
            {
                Status = "CHANGED"
            });

            var upldatedComponent = (await service.ListComponents(listComponentViewModel)).Components.First(x => x.Id == 1);

            Assert.Equal("CHANGED", upldatedComponent.Status);
        }

        /// <summary>
        /// Test that Page Size is Enforced
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TestPageSize()
        {
            ListComponentViewModel listComponentViewModel = new ListComponentViewModel()
            {
                PageSize = 5,
                PageNumber = 0
            };

            IComponentService service = new ComponentService();

            var result = await service.ListComponents(listComponentViewModel);

            Assert.Equal(listComponentViewModel.PageSize, result.Components.Count());
        }

        /// <summary>
        /// Test that sorting by ID works
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TestSorting()
        {
            ListComponentViewModel listComponentViewModel = new ListComponentViewModel()
            {
                SortDir = SortDirConstants.SORT_DIR_DESC,
                SortField = SortFieldConstants.SORT_FIELD_ID
            };

            IComponentService service = new ComponentService();

            var result = (await service.ListComponents(listComponentViewModel)).Components.First();

            Assert.Equal(99, result.Id);
        }

        /// <summary>
        /// Test that sorting by Type works
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TestSortingByType()
        {
            ListComponentViewModel listComponentViewModel = new ListComponentViewModel()
            {
                SortDir = SortDirConstants.SORT_DIR_ASC,
                SortField = SortFieldConstants.SORT_FIELD_TYPE,
            };

            IComponentService service = new ComponentService();

            var result = (await service.ListComponents(listComponentViewModel)).Components.First();

            Assert.Equal("Audio", result.Type);
        }
    }
}
