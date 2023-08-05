using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;
using System.Threading.Tasks;
using ToDoAppNTierBLL.Interfaces;
using ToDoAppNTierDataAccess.UnitOfWork;
using ToDoAppNTierEntities.Domains;

namespace ToDoAppNTierUI.TagHelpers
{
    [HtmlTargetElement("getAllTitlesCount")]
    public class GetTitleListsCount:TagHelper
    {
        public int UserId { get; set; }
        private readonly IToDoService _toDoService;

        public GetTitleListsCount(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var html = "";
            var allTitlesCount = _toDoService.GetToDoCount(x=>x.UserId==UserId);
            html= $"<span class='badge bg-danger'>{allTitlesCount} </span>";
            output.Content.SetHtmlContent(html);

        }
    }
    
}
