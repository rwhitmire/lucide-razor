using System.Xml.Linq;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace EstusLabs.Lucide;

[HtmlTargetElement("lucide-icon")]
public class LucideIconTagHelper : TagHelper
{
    [HtmlAttributeName("name")]
    public string Name { get; set; } = string.Empty;

    [HtmlAttributeName("class")]
    public string? Class { get; set; }

    [HtmlAttributeName("size")]
    public string? Size { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                output.SuppressOutput();
                return;
            }

            // Normalize icon name: convert to lowercase and remove .svg extension if present
            var iconName = Name.ToLowerInvariant();
            if (iconName.EndsWith(".svg", StringComparison.OrdinalIgnoreCase))
            {
                iconName = iconName[..^4]; // Remove .svg extension
            }

            // Look up icon in the pre-compiled dictionary (O(1) lookup)
            if (!LucideIconsDictionary.Icons.TryGetValue(iconName, out var svgContent))
            {
                output.SuppressOutput();
                return;
            }

            // Parse the SVG as XML
            var svgDoc = XDocument.Parse(svgContent);
            var svgElement = svgDoc.Root;

            if (svgElement == null)
            {
                output.SuppressOutput();
                return;
            }

            // Add or update attributes on the SVG element
            if (!string.IsNullOrWhiteSpace(Class))
            {
                svgElement.SetAttributeValue("class", Class);
            }

            if (!string.IsNullOrWhiteSpace(Size))
            {
                svgElement.SetAttributeValue("width", Size);
                svgElement.SetAttributeValue("height", Size);
            }

            // Set tag name to empty to output only the SVG content
            output.TagName = null;
            output.TagMode = TagMode.StartTagAndEndTag;

            // Output the modified SVG
            output.Content.SetHtmlContent(svgDoc.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine("error rendering icon: " + Name);
            Console.WriteLine(ex.Message);
        }
    }
}