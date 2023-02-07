using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Media.Imaging;
using System.IO;

namespace LazyBot
{
    public class Application : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            string panelName = "管线综合";
            RibbonPanel panel = myRibbonPanel(application, panelName);
            List<string> buttonInfo = new List<string>
            {
                // button name and text
                "管线连接",
                // button command dll
                "LazyBot.Command",
                // button tooltip
                "My First Plugin",
                // button icon
                "jacobian.ico"
            };

            myPushButton(application, panel, buttonInfo);

            return Result.Succeeded;
        }

        public RibbonPanel myRibbonPanel(UIControlledApplication a, string panelName)
        {
            string tabName = "市政通";
            RibbonPanel ribbonPanel = null;

            try
            {
                a.CreateRibbonTab(tabName);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            try
            {
                a.CreateRibbonPanel(tabName, panelName);
            }
            catch (Exception e) 
            {
                Debug.WriteLine(e.Message);
            }

            List<RibbonPanel> panels = a.GetRibbonPanels(tabName);
            foreach (RibbonPanel p in panels.Where(p => p.Name == panelName)) 
            {
                ribbonPanel = p;
            }
            return ribbonPanel;
        }

        public PushButton myPushButton(UIControlledApplication a, RibbonPanel panel, List<string> buttonInfo)
        {
            PushButton pushButton= null;

            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;

            if (panel.AddItem(new PushButtonData(buttonInfo[0], buttonInfo[0], thisAssemblyPath, buttonInfo[1]))
                is PushButton button)
            {
                button.ToolTip = buttonInfo[2];
                Uri uri = new Uri(Path.Combine(Path.GetDirectoryName(thisAssemblyPath), "Resources", buttonInfo[3]));
                button.LargeImage = new BitmapImage(uri);
            }

            return pushButton;
        }
    }
}
