using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataManagementTools;
using  ESRI.ArcGIS.ConversionTools;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geoprocessing;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geodatabase;

namespace gpTest
{
    //GPTest1和GPTest2是调用gp工具的demo，后边的两个方法是在项目中的写法
    internal class GpTool
    {
        /// <summary>
        /// 简单调用一个裁剪工具
        /// </summary>
        public void GPtest1()
        {
            Geoprocessor gp = new Geoprocessor();
            //输出文件是否能被覆盖
            gp.OverwriteOutput = true;
            gp.SetEnvironmentValue("scratchworkspace", @"F:\gpTest\data");
            ESRI.ArcGIS.AnalysisTools.Clip clip = new ESRI.ArcGIS.AnalysisTools.Clip();
            clip.in_features = @"F:\gpTest\data\po1.shp";
            clip.clip_features = @"F:\gpTest\data\po2.shp";
            clip.cluster_tolerance = "";
            clip.out_feature_class = @"\gpTest\data\result.shp";
            gp.Execute(clip, null);
        }

        /// <summary>
        /// 调用一个模型
        /// </summary>
        public void GPtest2()
        {
            Geoprocessor gp = new Geoprocessor();
            gp.OverwriteOutput = true;
            gp.SetEnvironmentValue("scratchworkspace", @"F:\gpTest1\data");
            gp.AddToolbox(@"F:\gpTest\Toolbox.tbx");
            IVariantArray parameters = new VarArrayClass();
            parameters.Add(@"F:\gpTest\data\test .xlsx");
            parameters.Add("a");
            parameters.Add("b");
            ISpatialReferenceFactory spatialReferenceFactory = new SpatialReferenceEnvironmentClass();
            ISpatialReference spatialReference = spatialReferenceFactory.CreateProjectedCoordinateSystem(2379);
            parameters.Add(spatialReference);
            parameters.Add(@"F:\gpTest\data\project1.shp");
            try
            {
                gp.Execute("excel", parameters, null);
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
        }
//---------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 用arcgis模型工具根据表格数据把表格转化为shp文件
        /// </summary>
        /// <param name="modelName">arcgis中模型的名称</param>
        /// <param name="excelpath">表格路径</param>
        /// <param name="xField">表格中作为X坐标的字段</param>
        /// <param name="yField">表格中作为Y坐标的字段</param>
        /// <param name="wkid">坐标系参数</param>
        /// <param name="shpPath">输出shp文件路径</param>
        /// <returns></returns>   
        public bool ExcelToShp(string modelName,string excelpath, string xField, string yField, int wkid, string shpPath)
        {
            IVariantArray parameters = new VarArrayClass();
            parameters.Add(excelpath);
            parameters.Add(xField);
            parameters.Add(yField);
            ISpatialReferenceFactory spatialReferenceFactory = new SpatialReferenceEnvironmentClass();
            ISpatialReference spatialReference = spatialReferenceFactory.CreateProjectedCoordinateSystem(wkid);
            parameters.Add(spatialReference);
            parameters.Add(shpPath);
            RunModelTool(System.AppDomain.CurrentDomain.BaseDirectory + "gisModelTool\\Tool\\Toolbox.tbx", System.AppDomain.CurrentDomain.BaseDirectory + "gisModelTool\\Data", parameters, modelName);
            return true;
        }

        /// <summary>
        /// 执行自定义Model GP
        /// </summary>
        /// <param name="parameters">GP 输入参数</param>
        /// <param name="gpname">GP 名字</param>
        /// <param name="modelPath">工具箱路径</param>
        /// <param name="tempFolder">临时工作区路径</param>
        /// <returns></returns>
        public bool RunModelTool(string modelPath, string tempFolder, IVariantArray parameters, string gpname)
        {
            if (parameters == null) { return false; }
            Geoprocessor geoprocessor = new Geoprocessor();
            geoprocessor.OverwriteOutput = true;       
            geoprocessor.SetEnvironmentValue("scratchworkspace", tempFolder);           
            try
            {
                geoprocessor.AddToolbox(modelPath);
                geoprocessor.Execute(gpname, parameters, null);
                Marshal.ReleaseComObject(parameters);          
                return true;
            }
            catch
            {        
                return false;
            }
        }
        
    }
}
