using System.Threading.Tasks;
using Serendip.IK.Configuration.Dto;

namespace Serendip.IK.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
