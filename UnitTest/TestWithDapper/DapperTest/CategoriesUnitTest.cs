using Moq;
using WebApi.Models;
using WebApi.Services;
using WebApi.Services.Interfaces;

namespace DapperTest;

/***
 * Here, we have mocked the IDbService which uses the database operations with dapper implementations.
 * Here, we have mocked the GetAsync<T> method of the IDbService. In that case, we don’t need to
 * connect with the database to test the unit test code.
 * 
 * By Mocking We're getting our specified Category Data set from ReturnAsync, Then
 * Getting that data from Category Service and last comparing it with our original data
 * Now if it matches the Test will pass otherwise inside GetCategory something is changing the data.
 */

public class CategoriesUnitTest
{
    private readonly Mock<IDbServices> _dbService = new();

    [Fact]
    public async Task GetCategory_ReturnNewCategory()
    {
        var category = GetCategory();

        _dbService.Setup(repo => repo.Get<CategoriesModel>(It.IsAny<string>(), It.IsAny<object>())).ReturnsAsync(category);

        var categoriesService = new CategoriesService(_dbService.Object);

        var result = await categoriesService.GetCategory(new Guid("e578ea7f-8f8f-4843-8484-c5b8e1ed4306"));

        Assert.Same(category, result);
    }

    [Fact]
    public async Task GetCategory_ReturnNotEmpty()
    {
        _dbService.Setup(repo => repo.Get<CategoriesModel>(It.IsAny<string>(), It.IsAny<object>())).ReturnsAsync(new CategoriesModel());

        var categoriesService = new CategoriesService(_dbService.Object);

        var result = await categoriesService.GetCategory(new Guid("e578ea7f-8f8f-4843-8484-c5b8e1ed4306"));

        Assert.NotNull(result);
    }

    private CategoriesModel GetCategory()
    {
        var category = new CategoriesModel
        {
            CategoryUId = new Guid("e578ea7f-8f8f-4843-8484-c5b8e1ed4306"),
            Name = "Shamee",
            CreateDate = DateTime.Now
        };
        return category;
    }
}