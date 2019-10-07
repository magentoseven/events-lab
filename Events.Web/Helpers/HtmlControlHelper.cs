using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Events.Web.Helpers
{
    public static class HtmlControlHelper
    {
        public static MvcHtmlString DateTimePickerFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            var input = new TagBuilder("input");
            input.MergeAttribute("type", "text");
            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                input.MergeAttributes(attributes);
            }
            var htmlString = input.ToString(TagRenderMode.Normal);
            return MvcHtmlString.Create(htmlString);
        }

        public static MvcHtmlString PageHeader(this HtmlHelper html, string title, object htmlAttributes = null)
        {
            var h3 = new TagBuilder("h2")
            {
                InnerHtml = title
            };
            var div = new TagBuilder("div")
            {
                InnerHtml = h3.ToString(TagRenderMode.Normal)
            };
            div.MergeAttribute("class", "container");
            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                div.MergeAttributes(attributes);
            }
            var htmlString = div.ToString(TagRenderMode.Normal);
            return MvcHtmlString.Create(htmlString);
        }

        public static IDisposable BeginView(this HtmlHelper helper, string viewId)
        {
            helper.ViewContext.Writer.Write(string.Format("<div id='{0}'>", viewId));

            return new MyView(helper);
        }

        class MyView : IDisposable
        {
            private HtmlHelper helper;

            public MyView(HtmlHelper helper)
            {
                this.helper = helper;
            }

            public void Dispose()
            {
                this.helper.ViewContext.Writer.Write("</div>");
            }
        }
    }
}