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
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            PipeDimensionSearch pipeDimensionSearch = new PipeDimensionSearch();
            pipeDimensionSearch.Show();

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
    internal class WriteTextNoteCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
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
                    TextNote textNote = TextNote.Create(doc, doc.ActiveView.Id, textLoc, "New sample text", opts);

                    // the transaction must be committed before you can
                    // get the value of TextNote.
                    if (transaction.Commit() == TransactionStatus.Committed)
                    {
                        // throw null;
                    }
                }

            }
            return Result.Succeeded;
        }
    }
}
