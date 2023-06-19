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
using System.Security.Cryptography.X509Certificates;

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
            // Create RibbonPanel
            string pipePanel = "管线综合";
            string holePanel = "一键开洞";
            string quantityPanel = "工程量统计";
            string databasePanel = "数据库查询";
            string aboutPanel = "关于";
            RibbonPanel rbpPipe = myRibbonPanel(application, pipePanel);
            RibbonPanel rbpHole = myRibbonPanel(application, holePanel);
            RibbonPanel rbpQuantity = myRibbonPanel(application, quantityPanel);
            RibbonPanel rbpDatabase = myRibbonPanel(application, databasePanel);
            RibbonPanel rbpAbout = myRibbonPanel(application, aboutPanel);

            // Create Pipe PushButton Information List
            List<string> pipeLinkbtInfo = new List<string>
            {
                // button name and text
                "管线连接",
                // button command dll
                "LazyBot.Command",
                // button tooltip
                "一键连接管道",
                // button large icon
                "pipeLinkbtInfo.ico"
            };
            List<string> fittingTypeSelectbtInfo = new List<string>
            {
                // button name and text
                "管件选型",
                // button command dll
                "LazyBot.Command",
                // button tooltip
                "一键生成管件",
                // button large icon
                "fittingTypeSelectbtInfo.ico"
            };

            // Create Hole PushButton Information List
            List<string> holeCreatebtInfo = new List<string>
            {
                // button name and text
                "一键开洞",
                // button command dll
                "LazyBot.Command",
                // button tooltip
                "选中管道一键开洞",
                // button large icon
                "holeCreatebtInfo.ico"
            };
            List<string> holeSleeveSelectbtInfo = new List<string>
            {
                // button name and text
                "套管加装",
                // button command dll
                "LazyBot.Command",
                // button tooltip
                "选中管道生成套管",
                // button large icon
                "holeSleeveSelectbtInfo.ico"
            };

            // Create Quantity PushButton Information List
            List<string> quantityTablebtInfo = new List<string>
            {
                // button name and text
                "工程量表",
                // button command dll
                "Null",
                // button tooltip
                "选中对象生成工程量表，包括设备、管道、管件及管道附件等",
                // button large icon
                "quantityTablebtInfo.ico"
            };
            List<string> EqPpAcesquantityTablebtInfo = new List<string>
            {
                // button name and text
                "设备附件数量表",
                // button command dll
                "LazyBot.Command",
                // button tooltip
                "选中对象生成工程量表，包括设备、管道附件",
                // button large icon
                "EqPpAcesquantityTablebtInfo.ico"
            };
            List<string> PpFtquantityTablebtInfo = new List<string>
            {
                // button name and text
                "管件数量表",
                // button command dll
                "LazyBot.Command",
                // button tooltip
                "选中对象生成工程量表，包括管道、管件",
                // button large icon
                "PpFtquantityTablebtInfo.ico"
            };
            List<string> costCalculatebtInfo = new List<string>
            {
                // button name and text
                "造价表",
                // button command dll
                "LazyBot.Command",
                // button tooltip
                "根据单价一键生成造价表",
                // button large icon
                "costCalculatebtInfo.ico"
            };

            // Create Database Search PushButton Information List
            List<string> PipeDimensionbtInfo = new List<string>
            {
                // button name and text
                "钢管尺寸",
                // button command dll
                "LazyBot.PipeDimSearchCommand",
                // button tooltip
                "查询特定直径钢管的外径、壁厚",
                // button large icon
                "PipeDimensionbtInfo.ico"
            };
            List<string> PipeFittingWeightbtInfo = new List<string>
            {
                // button name and text
                "钢管重量",
                // button command dll
                "LazyBot.PipeFtWghtSearchCommand",
                // button tooltip
                "02S403、02S404图集钢制管件重量查询",
                // button large icon
                "PipeFittingWeightbtInfo.ico"
            };

            // Create About PushButton Information List
            List<string> helpbtInfo = new List<string>
            {
                // button name and text
                "帮助",
                // button command dll
                "LazyBot.Command",
                // button tooltip
                "帮助",
                // button large icon
                "helpbtInfo.ico"
            };
            List<string> aboutbtInfo = new List<string>
            {
                // button name and text
                "关于",
                // button command dll
                "LazyBot.AboutCommand",
                // button tooltip
                "关于",
                // button large icon
                "aboutbtInfo.ico"
            };

            // Create Pipe PushButton
            myPushButton(application, rbpPipe, pipeLinkbtInfo);
            myPushButton(application, rbpPipe, fittingTypeSelectbtInfo);

            // Create Hole PushButton
            myPushButton(application, rbpHole, holeCreatebtInfo);
            myPushButton(application, rbpHole, holeSleeveSelectbtInfo);

            // Create Quantity PushButton
            // myPushButton(application, rbpQuantity, quantityTablebtInfo);
            myPushButton(application, rbpQuantity, costCalculatebtInfo);
            List<List<string>> pulldownpushbuttonList = new List<List<string>> { 
                EqPpAcesquantityTablebtInfo,
                PpFtquantityTablebtInfo
            };
            mySplitButtonPushButton(application, rbpQuantity, quantityTablebtInfo, pulldownpushbuttonList);

            // Create Database Search PushButton
            myPushButton(application, rbpDatabase, PipeDimensionbtInfo);
            myPushButton(application, rbpDatabase, PipeFittingWeightbtInfo);

            // Create About PushButton
            myPushButton(application, rbpAbout, helpbtInfo);
            myPushButton(application, rbpAbout, aboutbtInfo);

            return Result.Succeeded;
        }

        /// <summary>
        /// Create RibbonPanel
        /// </summary>
        /// <param name="a"></param>
        /// <param name="panelName"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Create PushButton 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="panel"></param>
        /// <param name="buttonInfo"></param>
        /// <returns></returns>
        public PushButton myPushButton(UIControlledApplication a, RibbonPanel panel, List<string> buttonInfo)
        {
            PushButton pushButton= null;

            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;

            if (panel.AddItem(new PushButtonData(buttonInfo[0], buttonInfo[0], thisAssemblyPath, buttonInfo[1]))
                is PushButton button)
            {
                button.ToolTip = buttonInfo[2];
                Uri uriLargeImage = new Uri(Path.Combine(Path.GetDirectoryName(thisAssemblyPath), "Icons", buttonInfo[3]));
                button.LargeImage = new BitmapImage(uriLargeImage);
            }

            return pushButton;
        }

        public PulldownButton myPulldownButton(UIControlledApplication a, RibbonPanel panel, List<string> buttonInfo)
        {
            PulldownButton pulldownButton= null;
            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;
            if (panel.AddItem(new PulldownButtonData(buttonInfo[0], buttonInfo[0])) is PulldownButton pulldownbutton)
            {
                pulldownbutton.ToolTip= buttonInfo[2];
                Uri uriLargeImage = new Uri(Path.Combine(Path.GetDirectoryName(thisAssemblyPath), "Icons", buttonInfo[3]));
                pulldownbutton.LargeImage = new BitmapImage(uriLargeImage);
            }

            return pulldownButton;
        }
        public PushButton mySplitButtonPushButton(UIControlledApplication a, RibbonPanel panel, List<string> pulldownbuttonInfo, List<List<string>> pulldownpushbuttonInfoList)
        {
            PushButton splitbuttonPushButton = null;

            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;
            if (panel.AddItem(new SplitButtonData(pulldownbuttonInfo[0], pulldownbuttonInfo[0])) is SplitButton splitbutton)
            {
                splitbutton.ToolTip = pulldownbuttonInfo[2];
                Uri uripldnbtLargeImage = new Uri(Path.Combine(Path.GetDirectoryName(thisAssemblyPath), "Icons", pulldownbuttonInfo[3]));
                splitbutton.LargeImage = new BitmapImage(uripldnbtLargeImage);

                foreach (List<string> pldnphbtInfolist in  pulldownpushbuttonInfoList) 
                {
                    if (splitbutton.AddPushButton(new PushButtonData(pldnphbtInfolist[0], pldnphbtInfolist[0], thisAssemblyPath, pldnphbtInfolist[1])) is PushButton button)
                    {
                        button.ToolTip = pldnphbtInfolist[2];
                        Uri uripldnphbtLargeImage = new Uri(Path.Combine(Path.GetDirectoryName(thisAssemblyPath), "Icons", pldnphbtInfolist[3]));
                        button.LargeImage = new BitmapImage(uripldnphbtLargeImage);
                    }
                }
            }

            return splitbuttonPushButton;
        }
    }
}
