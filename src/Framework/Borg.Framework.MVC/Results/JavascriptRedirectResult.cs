using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.Threading.Tasks;

namespace Borg.Framework.MVC.Results
{
    public class JavascriptRedirectResult : IActionResult
    {
        public JavascriptRedirectResult(string url)
        {
            Url = url;
        }

        public string Url { get; private set; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var tag = new TagBuilder("script");
            tag.InnerHtml.AppendHtml(string.Format("$('.main_loader').show(); window.location = '{0}'", Url));
            var data = Encoding.UTF8.GetBytes(tag.ToString());
            await context.HttpContext.Response.Body.WriteAsync(data, 0, data.Length);
        }
    }
}
