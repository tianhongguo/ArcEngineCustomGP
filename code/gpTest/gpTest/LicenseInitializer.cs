using System;
using ESRI.ArcGIS;

namespace gpTest
{
    internal partial class LicenseInitializer
    {
        public LicenseInitializer()
        {
            ResolveBindingEvent += new EventHandler(BindingArcGISRuntime);
        }

        void BindingArcGISRuntime(object sender, EventArgs e)
        {
            //
            // TODO: Modify ArcGIS runtime binding code as needed
            //
            ProductCode[] supportedRuntimes = new ProductCode[] {   
        ProductCode.Engine, ProductCode.Desktop };
            foreach (ProductCode c in supportedRuntimes)
            {
                if (RuntimeManager.Bind(c))
                    return;
            }  
            if (!RuntimeManager.Bind(ProductCode.Engine))
            {
                // Failed to bind, announce and force exit
                System.Windows.Forms.MessageBox.Show("Invalid ArcGIS runtime binding. Application will shut down.");
                System.Environment.Exit(0);
            }
        }
    }
}