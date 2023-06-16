using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB.Architecture;
using System.Windows.Controls;
using System;

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
    internal class PipeDimSearchCommand : IExternalCommand
    {
        // ModelessWimdow instance
        internal PipeDimensionSearch PipeDimSearchWindow;
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // If we do not have a dialog yet, create and show it
            if (PipeDimSearchWindow == null || !PipeDimSearchWindow.IsVisible)
            {
                // We Give the objects to the new dialog;
                // The dialog becomes the owner responsible for disposing them, eventually.
                PipeDimensionSearch pipeDimensionSearch = new PipeDimensionSearch();
                pipeDimensionSearch.Show();
            }
            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    internal class AboutCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            AboutWindow aboutWindow = new AboutWindow(commandData.Application);
            aboutWindow.ShowDialog();

            return Result.Succeeded;
        }
    }

    public class WriteTextNoteEvent : IExternalEventHandler
    {
        public string gettext { get; set; }
        public void Execute(UIApplication app)
        {
            UIDocument uiDoc = app.ActiveUIDocument;
            Document doc = uiDoc.Document;
            using (Transaction transaction = new Transaction(doc, "Creating TextNote"))
            {
                // To create TextNote, a transaction must be first started
                if (transaction.Start() == TransactionStatus.Started)
                {
                    // Create TextNote
                    XYZ textLoc = uiDoc.Selection.PickPoint("Pick a point for Writing text.");
                    ElementId defaultTextTypeId = doc.GetDefaultElementTypeId(ElementTypeGroup.TextNoteType);

                    TextNoteOptions opts = new TextNoteOptions(defaultTextTypeId);

                    TextNote textNote = TextNote.Create(doc, doc.ActiveView.Id, textLoc, gettext, opts);

                    // the transaction must be committed before you can
                    // get the value of TextNote.
                    if (transaction.Commit() == TransactionStatus.Committed)
                    {
                        // throw null;
                    }
                }
            }
        }

        public string GetName()
        {
            return "写入 TextNote 文本";
        }
    }
}
