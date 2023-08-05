using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;
using ToDoAppNTierBLL.Interfaces;
using ToDoAppNTierDataAccess.UnitOfWork;
using ToDoAppNTierEntities.Domains;

namespace ToDoAppNTierUI.TagHelpers
{
    [HtmlTargetElement("getOnGoingCount")]
    public class GetOnGoingCount:TagHelper
    {
        public int UserId { get; set; }
        private readonly IToDoService _toDoService;

        public GetOnGoingCount(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var html = "";
            var onGoingTitlesCount = _toDoService.GetToDoCount(x=> x.UserId==UserId && x.IsStarted==true && x.IsCompleted==false);
            html = $"<span class='badge bg-danger'>{onGoingTitlesCount} </span>";
            output.Content.SetHtmlContent(html);

        }
    }
}
