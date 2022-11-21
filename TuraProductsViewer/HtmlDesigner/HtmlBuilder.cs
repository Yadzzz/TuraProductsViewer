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
            else if(this.Layout == HtmlLayout.OnePerPage)
            {
                Layouts.OnePerPageLayout onePerPageLayout = new Layouts.OnePerPageLayout(this.creatorService, this.imageService, this.isHTML, this.Title, this.creatorService.Language, 
                                                                                         this.GetLanguageVariables());

                return onePerPageLayout.GetHTML();
            }
            else if(this.Layout == HtmlLayout.TenPerPage)
            {
                Layouts.TenPerPageLayout tenPerPageLayout = new Layouts.TenPerPageLayout(this.creatorService, this.imageService, this.isHTML, this.Title, this.creatorService.Language,
                                                                                         this.GetLanguageVariables());

                return tenPerPageLayout.GetHTML();
            }

            return string.Empty;
        }

        public Dictionary<string,string> GetLanguageVariables()
        {
            if (this.creatorService.Language.ToLower() == "swedish")
                return this.GetSwedishVariables();
            else if (this.creatorService.Language.ToLower() == "norwegian")
                return this.GetNorwegianVariables();
            else if (this.creatorService.Language.ToLower() == "finnish")
                return this.GetFinnishVariables();
            else if (this.creatorService.Language.ToLower() == "danish")
                return this.GetDannishVariables();
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
            variables.Add("rekprisvariabletenperpage", "Rek");
            variables.Add("eanvariable", "EAN");
            variables.Add("frpstlvariable", "Frp Stl");
            variables.Add("ilagervariable", "I Lager");
            variables.Add("measurment", "ST");
            variables.Add("no", "Nej");
            variables.Add("yes", "Ja");
            variables.Add("prisvariable", "Pris");

            return variables;
        }

        private Dictionary<string, string> GetNorwegianVariables()
        {
            Dictionary<string, string> variables = new Dictionary<string, string>();
            variables.Add("artnrvariable", "Art.Nr");
            variables.Add("varumarkevariable", "Varemerke");
            variables.Add("rekprisvariable", "Veil. pris");
            variables.Add("rekprisvariabletenperpage", "Veil");
            variables.Add("eanvariable", "EAN");
            variables.Add("frpstlvariable", "Antall i krt.");
            variables.Add("ilagervariable", "På lager");
            variables.Add("measurment", "pc(s)");
            variables.Add("no", "Nei");
            variables.Add("yes", "Ja");
            variables.Add("prisvariable", "Pris");

            return variables;
        }

        private Dictionary<string, string> GetDannishVariables()
        {
            Dictionary<string, string> variables = new Dictionary<string, string>();
            variables.Add("artnrvariable", "Art.Nr");
            variables.Add("varumarkevariable", "Varemærke");
            variables.Add("rekprisvariable", "Vejl. pris");
            variables.Add("rekprisvariabletenperpage", "Vejl");
            variables.Add("eanvariable", "EAN");
            variables.Add("frpstlvariable", "Forpakning");
            variables.Add("ilagervariable", "På lager");
            variables.Add("measurment", "Stk.");
            variables.Add("no", "Nej");
            variables.Add("yes", "Ja");
            variables.Add("prisvariable", "Pris");

            return variables;
        }

        private Dictionary<string, string> GetFinnishVariables()
        {
            Dictionary<string, string> variables = new Dictionary<string, string>();
            variables.Add("artnrvariable", "Tuotenro");
            variables.Add("varumarkevariable", "Tuotemerkki");
            variables.Add("rekprisvariable", "Suositushinta");
            variables.Add("rekprisvariabletenperpage", "Rec");
            variables.Add("eanvariable", "EAN");
            variables.Add("frpstlvariable", "Pakkauskoko");
            variables.Add("ilagervariable", "Varastossar");
            variables.Add("measurment", "pc(s)");
            variables.Add("no", "ei");
            variables.Add("yes", "Kyllä");
            variables.Add("prisvariable", "Price");

            return variables;
        }

        private Dictionary<string, string> GetEnglishVariables()
        {
            Dictionary<string, string> variables = new Dictionary<string, string>();
            variables.Add("artnrvariable", "Item No.");
            variables.Add("varumarkevariable", "Brand");
            variables.Add("rekprisvariable", "Rec. price");
            variables.Add("rekprisvariabletenperpage", "Rec");
            variables.Add("eanvariable", "EAN");
            variables.Add("frpstlvariable", "Inner Qty");
            variables.Add("ilagervariable", "In Stock");
            variables.Add("measurment", "pc(s)");
            variables.Add("no", "No");
            variables.Add("yes", "Yes");
            variables.Add("prisvariable", "Price");

            return variables;
        }
    }
}

