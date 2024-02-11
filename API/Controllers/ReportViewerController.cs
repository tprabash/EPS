using System.Collections.Generic;
using BoldReports.Web.ReportViewer;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    // [EnableCors("AllowAllOrigins")]
    public class ReportViewerController : ControllerBase, IReportController
    {
        // Report viewer requires a memory cache to store the information of consecutive client request and
        // have the rendered report viewer information in server.
        private IMemoryCache _cache;

        // IHostingEnvironment used with sample to get the application data from wwwroot.
        [System.Obsolete]
        private IHostingEnvironment _hostingEnvironment;
        private readonly IConfiguration _config;

        // Post action to process the report from server based json parameters and send the result back to the client.
        [System.Obsolete]
        public ReportViewerController(IMemoryCache memoryCache, IHostingEnvironment hostingEnvironment, IConfiguration config)
        {
            _config = config;
            _cache = memoryCache;
            _hostingEnvironment = hostingEnvironment;
        }

        // Post action to process the report from server based json parameters and send the result back to the client.
        [HttpPost]
        public object PostReportAction([FromBody] Dictionary<string, object> jsonArray)
        {
            return ReportHelper.ProcessReport(jsonArray, this, this._cache);
        }

        // Method will be called to initialize the report information to load the report with ReportHelper for processing.
        [System.Obsolete]
        public void OnInitReportOptions(ReportViewerOptions reportOption)
        {
            reportOption.ReportModel.ReportServerCredential = new System.Net.NetworkCredential(
                _config["RServerUser"], _config["RServerPassword"]);
            reportOption.ReportModel.DataSourceCredentials.Add(new BoldReports.Web.DataSourceCredentials(
                _config["DataSource"], _config["DBUser"], _config["DBPassword"]));

            // ////Add SSRS Report Server credential
            // reportOption.ReportModel.ReportServerCredential = new System.Net.NetworkCredential("MALIBAN\\Administrator", "Dc~Admin@EMAh0");
            // reportOption.ReportModel.DataSourceCredentials.Add(new BoldReports.Web.DataSourceCredentials("CCSDataSource", "sa", "Sqlserver2017"));

            // reportOption.ReportModel.ReportServerCredential = new System.Net.NetworkCredential("Maliban\\Anjalid","Kaush.1234");
            // reportOption.ReportModel.DataSourceCredentials.Add(new BoldReports.Web.DataSourceCredentials("CCSDataSource", "sa", "Sqlserver2017"));

            // string basePath = _hostingEnvironment.WebRootPath;
            // // Here, we have loaded the sales-order-detail.rdl report from application the folder wwwroot\Resources. sales-order-detail.rdl should be there in wwwroot\Resources application folder.
            // System.IO.FileStream reportStream = new System.IO.FileStream(basePath + @"\Resources\sales-order-detail.rdl", System.IO.FileMode.Open, System.IO.FileAccess.Read);
            // reportOption.ReportModel.Stream = reportStream;
        }

        // Method will be called when reported is loaded with internally to start to layout process with ReportHelper.
        public void OnReportLoaded(ReportViewerOptions reportOption)
        {
        }

        //Get action for getting resources from the report
        [ActionName("GetResource")]
        [AcceptVerbs("GET")]
        // Method will be called from Report Viewer client to get the image src for Image report item.
        public object GetResource(ReportResource resource)
        {
            return ReportHelper.GetResource(resource, this, _cache);
        }

        [HttpPost]
        public object PostFormReportAction()
        {
            return ReportHelper.ProcessReport(null, this, _cache);
        }
    }
}