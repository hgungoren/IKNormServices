using Abp.Application.Services;
using Serendip.IK.TextTemplates.Dto;
using System.Threading.Tasks;


namespace Serendip.IK.TextTemplates
{
    public interface ITextTemplateAppService : IAsyncCrudAppService<TextTemplateDto, long, TextTemplateFilter>
    {
        Task<string> GenerateText<T>(long templateId, T model);
        Task<string> GenerateText<T>(string filePath, T model);
        Task<string> GenerateText<T>(string title, string template, T model);
    }
}
