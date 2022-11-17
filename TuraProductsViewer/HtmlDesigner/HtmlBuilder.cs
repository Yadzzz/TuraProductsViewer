using TuraProductsViewer.Services;

namespace TuraProductsViewer.HtmlDesigner
{
    public class HtmlBuilder
    {
        public string Title { get; set; }
        public HtmlLayout Layout { get; set; }
        private CreatorService creatorService { get; set; }
        private ImageService imageService { get; set; }
        private bool isHTML { get; set; }

        public HtmlBuilder(string title, HtmlLayout layout, CreatorService crtService, ImageService imgService, bool isHtml)
        {
            this.Title = title;
            this.Layout = layout;
            this.creatorService= crtService;
            this.imageService = imgService;
            this.isHTML = isHtml;
        }

        public string GenerateHTML()
        {
            if (this.Layout == HtmlLayout.SixPerPage)
            {
                Layouts.SixPerPageLayout sixPerPageLayout = new Layouts.SixPerPageLayout(this.creatorService, this.imageService, this.isHTML, this.Title, this.creatorService.Language,
                                                                                         this.GetLanguageVariables());
                return sixPerPageLayout.GetHTML();
            }

            return string.Empty;
        }

        public Dictionary<string,string> GetLanguageVariables()
        {
            if (this.creatorService.Language.ToLower() == "swedish")
                return this.GetSwedishVariables();
            else if (this.creatorService.Language.ToLower() == "norwegian")
                return this.GetSwedishVariables();
            else if (this.creatorService.Language.ToLower() == "finnish")
                return this.GetSwedishVariables();
            else if (this.creatorService.Language.ToLower() == "danish")
                return this.GetSwedishVariables();
            else if (this.creatorService.Language.ToLower() == "english")
                return this.GetEnglishVariables();
            else 
                return this.GetSwedishVariables();
        }

        private Dictionary<string,string> GetSwedishVariables()
        {
            Dictionary<string, string> variables = new Dictionary<string, string>();
            variables.Add("artnrvariable", "Art.Nr");
            variables.Add("varumarkevariable", "Varumärke");
            variables.Add("rekprisvariable", "Rek. pris");
            variables.Add("eanvariable", "EAN");
            variables.Add("frpstlvariable", "Frp Stl");
            variables.Add("ilagervariable", "I Lager");
            variables.Add("measurment", "ST");
            variables.Add("no", "Nej");
            variables.Add("yes", "Ja");
            variables.Add("prisvariable", "Pris");

            return variables;
        }

        private Dictionary<string, string> GetEnglishVariables()
        {
            Dictionary<string, string> variables = new Dictionary<string, string>();
            variables.Add("artnrvariable", "Item No.");
            variables.Add("varumarkevariable", "Brand");
            variables.Add("rekprisvariable", "Rec. price");
            variables.Add("eanvariable", "EAN");
            variables.Add("frpstlvariable", "Inner Qty");
            variables.Add("ilagervariable", "In Stock");
            variables.Add("measurment", "pc(s)");
            variables.Add("novariable", "No");
            variables.Add("Yesvariable", "Yes");
            variables.Add("prisvariable", "Price");

            return variables;
        }
    }
}
