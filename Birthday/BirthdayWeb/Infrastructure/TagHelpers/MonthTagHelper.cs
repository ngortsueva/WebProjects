using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BirthdayWeb.Infrastructure.TagHelpers
{
    [HtmlTargetElement("months", TagStructure = TagStructure.WithoutEndTag)]
    public class MonthsTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "tr";
            output.TagMode = TagMode.StartTagAndEndTag;

            var month = new StringBuilder();
            month.Append("<td></td>");  // Column for days
            month.Append("<td>January</td>");
            month.Append("<td>February</td>");
            month.Append("<td>Mart</td>");
            month.Append("<td>April</td>");
            month.Append("<td>May</td>");
            month.Append("<td>June</td>");
            month.Append("<td>July</td>");
            month.Append("<td>August</td>");
            month.Append("<td>September</td>");
            month.Append("<td>October</td>");
            month.Append("<td>November</td>");
            month.Append("<td>December</td>");

            output.Content.SetHtmlContent(month.ToString());
        }
    }   
}
