using System.Threading.Tasks;
using Serendip.IK.Models.TokenAuth;
using Serendip.IK.Web.Controllers;
using Shouldly;
using Xunit;

namespace Serendip.IK.Web.Tests.Controllers
{
    public class HomeController_Tests: IKWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}