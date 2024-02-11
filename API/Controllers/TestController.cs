using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    //
    public class TestController : BaseApiController
    {
        private readonly ITestRepository _testRepository;
        // [Obsolete]
        // private readonly IHostingEnvironment _hostingEnv;
        private readonly IApplicationCartonDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IApplicationPTrackDbContext _contextP;
        private readonly IApplicationMTrackDbContext _contextM;

        // [Obsolete]
        public TestController(ITestRepository testRepository, IApplicationCartonDbContext context
            , IWebHostEnvironment webHostEnvironment, IApplicationPTrackDbContext contextP , IApplicationMTrackDbContext contextM)
        {
            _contextM = contextM;
            _contextP = contextP;
            _webHostEnvironment = webHostEnvironment;
            _context = context;
            // _hostingEnv = hostingEnv;
            _testRepository = testRepository;
        }

        public void PrintRecipt()
        {
            var dt = new DataTable();
            var data = _context.MstrArticle.ToList();
            dt = ToDataTable(data);

            // LocalReport report = new LocalReport();
            // report.AddDataSource(new ReportDataSource("receipt", dt)); 
        }

        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        [HttpGet("menus")]
        [Produces("application/xml")]
        public async Task<IActionResult> GetMenuListAsync()
        {
            var menu = await _testRepository.GetMenuListAsync();
            return Ok(menu);
        }

        [HttpGet("RMPO")]
        [Produces("application/xml")]
        public async Task<IActionResult> GetRMPOs()
        {
            var menu = await _contextP.RMPOSheaders
                .Where(x => x.IsSupplierConfirmed == false)
                .Select(x => new { x.Buyer, x.CompanyCode })
                // .Join(_context.rmRMPOs , h => h.PONo , d => d.RMPO ,
                //   (h,d) => new {
                //       h.Buyer,
                //       h.BrandCode,
                //       h.Currency,
                //       h.DeliveryLocation,
                //       h.DeliveryRequireDt ,
                //       d.ColorCode,
                //       d.ColorName ,
                //       d.LineItemNumber
                //   })
                .ToListAsync();

            return Ok(menu);
        }

        [HttpPost("Upload"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadAsync()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                // var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPost("UploadFiles"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFiles(IFormCollection data)
        {
            var result = 0; var soHeaderId = 0;
            // var formCollection = await Request.ReadFormAsync();
            // var file = formCollection.Files.First(); 
            var file = data.Files.First();
            if (data.TryGetValue("soHeaderId", out var someData))
            {
                soHeaderId = Convert.ToInt32(someData);
            }

            var uploadResponse = new FileUploadResponse();
            // List<FileUploadResponseData> uploadedFiles = new List<FileUploadResponseData>();

            var f = file;
            string name = f.FileName.Replace(@"\\\\", @"\\");
            if (f.Length > 0)
            {
                var memoryStream = new MemoryStream();
                try
                {
                    await f.CopyToAsync(memoryStream);
                    // Upload check if less than 2mb!
                    if (memoryStream.Length < 2097152)
                    {
                        var fileObj = new FileDetails()
                        {
                            FileName = Path.GetFileName(name),
                            FileSize = Convert.ToInt64(soHeaderId),//memoryStream.Length,
                            UploadDate = DateTime.Now,
                            UserName = "Admin",
                            DocFile = memoryStream.ToArray()
                        };

                        _context.FileDetails.Add(fileObj);
                        await _context.SaveChangesAsync(default);
                        result = 1;
                    }
                    else
                    {
                        result = -1;
                    }
                }
                finally
                {
                    memoryStream.Close();
                    memoryStream.Dispose();
                }
            }
            return Ok(result);
        }

        // [HttpPost("UploadFiles")]
        // public async Task<IActionResult> UploadFiles(IFormFile files)
        // {   
        //     if (files == null) throw new Exception("File is null");
        //     if (files.Length == 0) throw new Exception("File is empty");
        //     var uploadResponse = await Upload(files);
        //     if (uploadResponse.ErrorMessage != "")
        //         return BadRequest(new { error = uploadResponse.ErrorMessage });
        //     return Ok(uploadResponse);
        // }

        public async Task<FileUploadResponse> Upload(IFormFile files)
        {
            long size = files.Length; //files.Sum(f => f.Length);

            List<FileUploadResponseData> uploadedFiles = new List<FileUploadResponseData>();

            try
            {
                var f = files;
                // foreach (var f in files)
                // {
                string name = f.FileName.Replace(@"\\\\", @"\\");
                if (f.Length > 0)
                {
                    var memoryStream = new MemoryStream();
                    try
                    {
                        await f.CopyToAsync(memoryStream);
                        // Upload check if less than 2mb!
                        if (memoryStream.Length < 2097152)
                        {
                            var file = new FileDetails()
                            {
                                FileName = Path.GetFileName(name),
                                FileSize = memoryStream.Length,
                                UploadDate = DateTime.Now,
                                UserName = "Admin",
                                DocFile = memoryStream.ToArray()
                            };

                            _context.FileDetails.Add(file);
                            await _context.SaveChangesAsync(default);
                            uploadedFiles.Add(new FileUploadResponseData()
                            {
                                Id = file.FileId,
                                Status = "OK",
                                FileName = Path.GetFileName(name),
                                ErrorMessage = "",
                            });
                        }
                        else
                        {
                            uploadedFiles.Add(new FileUploadResponseData()
                            {
                                Id = 0,
                                Status = "ERROR",
                                FileName = Path.GetFileName(name),
                                ErrorMessage = "File " + f + " failed to upload"
                            });
                        }
                    }
                    finally
                    {
                        memoryStream.Close();
                        memoryStream.Dispose();
                    }
                }
                // }
                return new FileUploadResponse() { Data = uploadedFiles, ErrorMessage = "" };
            }
            catch (Exception ex)
            {
                return new FileUploadResponse() { ErrorMessage = ex.Message.ToString() };
            }
        }

        // [Route("DownloadFile")]
        // [HttpGet("DownloadFile/{id}")]
        // public async Task<IActionResult> DownloadFile(int id)
        // {
        //     var stream = Download(id);
        //     if (stream == null)
        //         return NotFound();
        //     return new FileContentResult(stream, "application/octet-stream"); 
        // }

        // public byte[] Download(int id)
        // {
        //     // try
        //     // {
        //         var selectedFile = _context.FileDetails
        //             .Where(f => f.FileId == id)
        //             .FirstOrDefault();

        //         if (selectedFile == null)
        //             return null;
        //         return selectedFile.DocFile;
        //     // }
        //     // catch (Exception ex)
        //     // {
        //     //     return null;
        //     // }
        // }



        // public static string ObjectToXml(IEnumerable<MenuJoinList> menu)
        // {
        //     XmlSerializer xmlSerializer = new XmlSerializer(typeof(MenuJoinList));
        //     using (StringWriter textWriter = new StringWriter())
        //     {
        //         foreach (var item in menu)
        //         {
        //             xmlSerializer.Serialize(textWriter, item);

        //         }
        //         return textWriter.ToString();
        //     }
        // }

    }
}