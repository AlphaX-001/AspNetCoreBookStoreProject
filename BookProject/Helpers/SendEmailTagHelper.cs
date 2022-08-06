using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BookProject.Helpers
{
    public class SendEmailTagHelper :TagHelper
    {
        public string Email { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            output.TagName = "a";
            output.Attributes.SetAttribute("href", $"mailto:{Email}");
            //output.Attributes.SetAttribute("href", "mailto:" + Email + "");
            output.Attributes.Add("class", "custom-email");
            //output.Content.SetContent("Click Here to Connect");
        }
    }
}
//Application is in the Contact area of Home Page.