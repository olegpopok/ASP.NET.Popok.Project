using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PL.MVC.Models.Like;

namespace PL.MVC.Infrastructure
{
    public static class CustomHtmlHelperMethods
    {
        public static MvcHtmlString PostLikes(this HtmlHelper helper, List<ViewLikeModel> likes)
        {
            if (likes.Count == 0)
                return MvcHtmlString.Empty;

            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var com = ", ";

            var div = new TagBuilder("div");
            div.Attributes.Add("class", "likes-block");

            for (int i = 0; i < likes.Count - 1; i++)
            {
                var a = new TagBuilder("a");
                a.Attributes.Add("href", urlHelper.Action("Profile", "Home", new {id = likes[i].UserName}));
                a.Attributes.Add("class", "user-link");
                a.SetInnerText(likes[i].UserName);
                div.InnerHtml += a.ToString();
                div.InnerHtml += com;
            }

            div.InnerHtml = div.InnerHtml.TrimEnd(' ');
            div.InnerHtml = div.InnerHtml.TrimEnd(',');

            if (likes.Count != 1)
            {
                div.InnerHtml += " and ";
            }

            var lasta = new TagBuilder("a");
            lasta.SetInnerText(likes[likes.Count - 1].UserName);
            lasta.Attributes.Add("href", urlHelper.Action("Profile", "Home", new {id = likes[likes.Count - 1].UserName}));
            lasta.Attributes.Add("class", "user-link");
            div.InnerHtml += lasta.ToString();
            div.InnerHtml += " like it.";

            return new MvcHtmlString(div.ToString());
        }

        public static MvcHtmlString TimeStamp(this HtmlHelper helper, DateTime createDate)
        {
            var difDate = DateTime.Now.Subtract(createDate);

            string dispaly;

            if (difDate.Days >= 365)
            {
                dispaly = (difDate.Days / 365).ToString() + " y";
            }
            else if (difDate.Days >= 30)
            {
                dispaly = (difDate.Days / 30).ToString() + " d";
            }
            else if (difDate.Days >= 7)
            {
                dispaly = (difDate.Days / 7).ToString() + " w";
            }
            else if (difDate.Days > 0)
            {
                dispaly = difDate.Days.ToString() + " d";
            }
            else if (difDate.Hours > 0)
            {
                dispaly = difDate.Hours.ToString() + " h";
            }
            else if (difDate.Minutes > 2)
            {
                dispaly = difDate.Minutes.ToString() + " min";
            }
            else if(difDate.Seconds > 0)
            {
                dispaly = difDate.Seconds.ToString() + " sec";
            }
            else
            {
                dispaly = "now";
            }

            var div = new TagBuilder("div");
            div.MergeAttribute("class", "timestamp");
            div.InnerHtml += dispaly;

            return new MvcHtmlString(div.ToString());
        }
    }
}