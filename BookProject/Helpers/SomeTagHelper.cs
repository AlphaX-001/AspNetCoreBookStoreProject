using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BookProject.Helpers
{   
    [HtmlTargetElement("some")]
    [HtmlTargetElement(Attributes ="some")]
    public class SomeTagHelper:TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "h4";
            output.Attributes.SetAttribute("class", "h4 lead");
        }
    }
}
//Application is in the "Our Services" of Home Page
// Just like that
// <some>Our services</some>
