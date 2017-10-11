using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gpTest
{
    //把demo复制到F盘根目录下就可以运行项目
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GpTool gpTool=new GpTool();
          //  gpTool.GPtest1();
            gpTool.GPtest2();

            //string modeName = "excel";
            //string excelPath = @"F:\gpTest\data\test .xlsx";
            //string xField = "a";
            //string yFiled = "b";
            //int wkid = 2379;
            //string resultPath = @"F:\gpTest\data\project1.shp";
            //gpTool.ExcelToShp(modeName, excelPath, xField, yFiled, wkid, resultPath);
          

        }

    
        
    }
}