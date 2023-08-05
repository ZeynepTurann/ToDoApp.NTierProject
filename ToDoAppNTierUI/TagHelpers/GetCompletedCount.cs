using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;
using ToDoAppNTierBLL.Interfaces;
using ToDoAppNTierBLL.Services;
using ToDoAppNTierDataAccess.UnitOfWork;
using ToDoAppNTierEntities.Domains;

namespace ToDoAppNTierUI.TagHelpers
{
    [HtmlTargetElement("getCompletedCount")]
    public class GetCompletedCount:TagHelper
    {
        public int UserId { get; set; }
        private readonly IToDoService _toDoService;

        public GetCompletedCount(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var html = "";
            var compTitlesCount = _toDoService.GetToDoCount(x => x.UserId == UserId && x.IsCompleted == true);
            html = $"<span class='badge bg-danger'>{compTitlesCount} </span>";
            output.Content.SetHtmlContent(html);

        }
    }
}
