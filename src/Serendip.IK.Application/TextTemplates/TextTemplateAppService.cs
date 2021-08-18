using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Serendip.IK.TextTemplates.Dto;
using System.Linq;
using System.Threading.Tasks;


namespace Serendip.IK.TextTemplates
{
    public class TextTemplateAppService : CoreAsyncCrudAppService<TextTemplate, TextTemplateDto, long>, ITextTemplateAppService
    {
        //ITextGenerator _textGenerator;

        public TextTemplateAppService(IRepository<TextTemplate, long> repository) : base(repository)
        {
            //_textGenerator = textGenerator;
        }
         

        public async Task<string> GenerateText<T>(long templateId, T model)
        {
            var template = Repository.Get(templateId);

            if (template == null) { return ""; }

            string result = await GenerateText(template.Title, template.Template, model);

            return result;
        }

        public async Task<string> GenerateText<T>(string filePath, T model)
        {
            if (!System.IO.File.Exists(filePath))
            {
                throw new UserFriendlyException("FileNotFound");
            }

            var filePathParts = filePath.Split('/');

            var fileContent = System.IO.File.ReadAllText(filePath);

            string result = await GenerateText(filePathParts.LastOrDefault(), fileContent, model);

            return result;
        }

        public async Task<string> GenerateText<T>(string title, string template, T model)
        {
            //var result = _textGenerator.Generate(template, model);

            //if (result.HasError)
            //{
            //    throw new UserFriendlyException(result.Error);
            //}

            //return result.Content;

            return "";
        }

        public Task<PagedResultDto<TextTemplateDto>> GetAllAsync(TextTemplateFilter input)
        {
            throw new System.NotImplementedException();
        }
    }
}
