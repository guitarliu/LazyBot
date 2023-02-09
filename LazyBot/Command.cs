using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.UI.Events;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Macros;

namespace LazyBot
{
    [Transaction(TransactionMode.Manual)]
    internal class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            MessageBox.Show("Plugin Created");


            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    internal class AboutCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            AboutWindow aboutWindow = new AboutWindow(commandData.Application);
            aboutWindow.Show();

            return Result.Succeeded;
        }
    }
}
