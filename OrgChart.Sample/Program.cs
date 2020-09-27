using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace OrgChart.Sample
{
    class Program
    {
        static void Main(string[] args)
        {            
            OrgChartOption defaultOption = new OrgChartOption
            {
                BoxFillColor = ColorTranslator.FromHtml("#A7E7FC"),
                BoxBorderColor = ColorTranslator.FromHtml("#A7E7FC"),
                ConnectLineColor = ColorTranslator.FromHtml("#424242")
            };

            //微软雅黑

            OrgChartOption option = new OrgChartOption()
            {
                BoxFillColor = defaultOption.BoxFillColor,
                BoxBorderColor = defaultOption.BoxBorderColor,
                ConnectLineColor = defaultOption.ConnectLineColor,
                FontSize = 12,
                HorizontalSpace = 10,
                VerticalSpace=30,               
                BoxHeight=45,
                BoxWidth = 110,
                FontName="宋体"
                //UseMinBoxWidthWhenHasOnlyOne = true,
                //MinBoxWidth = 80
            };       

            OrgChartGenerator orgChartGenerator = new OrgChartGenerator(GetOrgChartNodes(), option) { DefaultOption = defaultOption };

            string filePath = "皮庄组朱姓祖先.png";
            using (FileStream fs = File.Create(filePath))
            {
                MemoryStream ms = orgChartGenerator.Generate();
                ms.WriteTo(fs);
                fs.Flush();
            }

            try
            {
                Process.Start(filePath);
            }
            catch
            {                
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    filePath = filePath.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {filePath}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", filePath);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", filePath);
                }
                else
                {
                    throw;
                }
            }
        }

        private static List<OrgChartNode> GetOrgChartNodes()
        {
            List<OrgChartNode> nodes = new List<OrgChartNode>();

            nodes.Add(new OrgChartNode("0", "皮庄组朱姓祖先", 
                new OrgChartNode("1.1", "子：朱合祥",
                  new OrgChartNode("1.1.1", "长子 朱长锁",
                     new OrgChartNode("1.1.1.1", "子 朱军",
                      new OrgChartNode("1.1.1.1.1", "女 朱怡菁")
                     ),
                     new OrgChartNode("1.1.1.2", "长女 朱红")
                  ),
                  new OrgChartNode("1.1.2", "次子 朱锁堂",
                   new OrgChartNode("1.1.2.1","子 朱军伟"),
                   new OrgChartNode("1.1.2.2", "女 朱秋红")
                  ),
                  new OrgChartNode("1.1.3", "长女 朱秀绒"),
                  new OrgChartNode("1.1.4", "次女 朱翠花"),
                  new OrgChartNode("1.1.5", "三女 朱小琴")
                ),
                new OrgChartNode("1.2", "女：(不详)")
            ));           

            return nodes;
        }
    }
}
