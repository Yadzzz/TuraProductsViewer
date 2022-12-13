using System.Runtime.CompilerServices;

namespace TuraProductsViewer.Services.FileCreator
{
    public sealed class FileCreatorUtilities
    {
        private static FileCreatorUtilities _fileCreatorUtilitiesInstance;
        private static readonly object Lock = new object();

        private FileCreatorUtilities()
        {

        }

        public static FileCreatorUtilities GetCreatorUtilities()
        {
            //_fileCreatorUtilitiesInstance??= new FileCreatorUtilities(); //Null checking

            if(_fileCreatorUtilitiesInstance == null)
            {
                lock(Lock)
                {
                    if(_fileCreatorUtilitiesInstance == null)
                    {
                        _fileCreatorUtilitiesInstance = new FileCreatorUtilities();
                    }
                }
            }

            return _fileCreatorUtilitiesInstance;
        }

        public string GetImageClickLink(string language)
        {
            if (language.ToLower() == "swedish")
                return "https://www.turascandinavia.com/sv/produkter/";
            else if (language.ToLower() == "norwegian")
                return "https://www.turascandinavia.com/nb/produkter/";
            else if (language.ToLower() == "finnish")
                return "https://www.turascandinavia.com/fi/tuotteet/";
            else if (language.ToLower() == "danish")
                return "https://www.turascandinavia.com/da/produkter/";
            else if (language.ToLower() == "english")
                return "https://www.turascandinavia.com/en/products/";
            else
                return "https://www.turascandinavia.com/sv/produkter/";
        }

        public Dictionary<string, string> GetLanguageVariables(string language)
        {
            if (language.ToLower() == "swedish")
                return this.GetSwedishVariables();
            else if (language.ToLower() == "norwegian")
                return this.GetNorwegianVariables();
            else if (language.ToLower() == "finnish")
                return this.GetFinnishVariables();
            else if (language.ToLower() == "danish")
                return this.GetDannishVariables();
            else if (language.ToLower() == "english")
                return this.GetEnglishVariables();
            else
                return this.GetSwedishVariables();
        }

        private Dictionary<string, string> GetSwedishVariables()
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
