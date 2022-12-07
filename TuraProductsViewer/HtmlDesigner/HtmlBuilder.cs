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
                                                                                         this.GetLanguageVariables(), this.GetImageClickLink());
                return sixPerPageLayout.GetHTML();
            }
            else if(this.Layout == HtmlLayout.OnePerPage)
            {
                Layouts.OnePerPageLayout onePerPageLayout = new Layouts.OnePerPageLayout(this.creatorService, this.imageService, this.isHTML, this.Title, this.creatorService.Language, 
                                                                                         this.GetLanguageVariables(), this.GetImageClickLink());

                return onePerPageLayout.GetHTML();
            }
            else if(this.Layout == HtmlLayout.TenPerPage)
            {
                Layouts.TenPerPageLayout tenPerPageLayout = new Layouts.TenPerPageLayout(this.creatorService, this.imageService, this.isHTML, this.Title, this.creatorService.Language,
                                                                                         this.GetLanguageVariables(), this.GetImageClickLink());

                return tenPerPageLayout.GetHTML();
            }
            else if(this.Layout == HtmlLayout.PrisEtikett)
            {
                Barcode.BarcodeLayout barcodeLayout = new Barcode.BarcodeLayout(this.creatorService, this.isHTML);

                return barcodeLayout.GetHTML();
            }
            else if (this.Layout == HtmlLayout.Pinnflaga)
            {
                Barcode.PinFlagLayout pinFlagLayout = new Barcode.PinFlagLayout(this.creatorService, this.isHTML);

                return pinFlagLayout.GetHTML();
            }
            else if (this.Layout == HtmlLayout.HylleEtikett)
            {
                Barcode.ShelfLabelLayout shelfLabelLayout = new Barcode.ShelfLabelLayout(this.creatorService, this.isHTML);

                return shelfLabelLayout.GetHTML();
            }

            return string.Empty;
        }

        //Used for Adaptive pages
        public MemoryStream? GenerateMemoryStream()
        {
            if (this.Layout == HtmlLayout.OnePerPage)
            {
                Layouts.OnePerPageAdaptiveLayout onePerPageLayout = new Layouts.OnePerPageAdaptiveLayout(this.creatorService, this.imageService, this.isHTML, this.Title, this.creatorService.Language,
                                                                                         this.GetLanguageVariables(), this.GetImageClickLink());

                return onePerPageLayout.InitializePDF();
            }
            else if(this.Layout == HtmlLayout.SixPerPage)
            {
                Layouts.SixPerPageAdaptiveLayout sixPerPageLayout = new Layouts.SixPerPageAdaptiveLayout(this.creatorService, this.imageService, this.isHTML, this.Title, this.creatorService.Language,
                                                                                         this.GetLanguageVariables(), this.GetImageClickLink());

                return sixPerPageLayout.InitializePDF();
            }
            else if(this.Layout == HtmlLayout.OnePerPage)
            {
                Layouts.TenPerPageAdaptiveLayout tenPerPageLayout = new Layouts.TenPerPageAdaptiveLayout(this.creatorService, this.imageService, this.isHTML, this.Title, this.creatorService.Language,
                                                                                         this.GetLanguageVariables(), this.GetImageClickLink());

                return tenPerPageLayout.InitializePDF();
            }
            else if(this.Layout == HtmlLayout.PrisEtikett)
            {
                Barcode.BarcodeAdaptiveLayout barcodeAdaptiveLayout = new Barcode.BarcodeAdaptiveLayout(this.creatorService, this.isHTML);

                return barcodeAdaptiveLayout.GetMemoryStream();
            }

            return null;
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

        public string GetImageClickLink()
        {
            if (this.creatorService.Language.ToLower() == "swedish")
                return "https://www.turascandinavia.com/sv/produkter/";
            else if (this.creatorService.Language.ToLower() == "norwegian")
                return "https://www.turascandinavia.com/nb/produkter/";
            else if (this.creatorService.Language.ToLower() == "finnish")
                return "https://www.turascandinavia.com/fi/tuotteet/";
            else if (this.creatorService.Language.ToLower() == "danish")
                return "https://www.turascandinavia.com/da/produkter/";
            else if (this.creatorService.Language.ToLower() == "english")
                return "https://www.turascandinavia.com/en/products/";
            else
                return "https://www.turascandinavia.com/sv/produkter/";
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

