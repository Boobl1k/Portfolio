using System.Globalization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portfolio.Entity;

namespace Portfolio.Extensions;

internal static class HtmlHelperExtensions
{
    public static IHtmlContent ShowBlog(this IHtmlHelper helper, Post blog, bool white)
    {
        var div = new TagBuilder("div")
        {
            Attributes =
            {
                {"id", white ? "white" : "grey"}
            }
        };
        var container = new TagBuilder("div")
        {
            Attributes =
            {
                {"class", "container"}
            }
        };
        var row = new TagBuilder("div")
        {
            Attributes =
            {
                {"class", "row"}
            }
        };
        var div2 = new TagBuilder("div")
        {
            Attributes =
            {
                {"class", "col-lg-8 col-lg-offset-2"}
            }
        };
        var p1 = new TagBuilder("p");
        p1.InnerHtml.AppendHtml(new TagBuilder("img")
        {
            Attributes =
            {
                {"src", "img/user.png"},
                {"width", "50px"},
                {"height", "50px"}
            }
        });
        var ba = new TagBuilder("ba");
        ba.InnerHtml.Append("Stanley Stinson");
        p1.InnerHtml.AppendHtml(ba);
        div2.InnerHtml.AppendHtml(p1);
        var p2 = new TagBuilder("p");
        var bd = new TagBuilder("bd");
        bd.InnerHtml.Append(blog.Date.ToString(CultureInfo.CurrentCulture));
        p2.InnerHtml.AppendHtml(bd);
        div2.InnerHtml.AppendHtml(p2);
        var h4 = new TagBuilder("h4");
        h4.InnerHtml.Append(blog.Title);
        div2.InnerHtml.AppendHtml(h4);
        var p3 = new TagBuilder("p");
        p3.InnerHtml.Append(blog.Text);
        div2.InnerHtml.AppendHtml(p3);
        var p4 = new TagBuilder("p");
        p4.InnerHtml.AppendHtml(helper.ActionLink("open", "Blog", "Blog", new {postId = blog.Id}));
        div2.InnerHtml.AppendHtml(p4);
        row.InnerHtml.AppendHtml(div2);
        container.InnerHtml.AppendHtml(row);
        div.InnerHtml.AppendHtml(container);
        return div;
    }
}
