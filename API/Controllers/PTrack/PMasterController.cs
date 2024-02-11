using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using API.DTOs;
using API.DTOs.ptrack;
using API.Entities.Ptrack;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace API.Controllers.PTrack
{
    [Authorize]
    public class PMasterController : BaseApiController
    {
        private readonly IApplicationPTrackDbContext _context;
        private readonly IPTrackMasterRepository _pTrackMasterRepository;

        public PMasterController(IApplicationPTrackDbContext context, IPTrackMasterRepository pTrackMasterRepository)
        {
            _pTrackMasterRepository = pTrackMasterRepository;
            _context = context;
        }

        [HttpGet("ReportList")]
        public async Task<IActionResult> GetReportMenuList()
        {
            var result = await _pTrackMasterRepository.GetReportMenuListAsync();
            return Ok(result);
        }

        [HttpGet("XML")]
        public async Task<IActionResult> GetXMLAsync()
        {
            var orderList = await _pTrackMasterRepository.GetPurchaseOrdersAsync();
            var result = CreateEmployeeXML(orderList);

            // var basePath = Path.Combine("C:\\", @"XmlFiles\");
            
            // var net = new System.Net.WebClient();
            // var data = net.DownloadData(basePath + "PurchaseOrder.xml" );
            // var content = new System.IO.MemoryStream(data);
            // var contentType = "APPLICATION/octet-stream";
            // var fileName = "PurchaseOrder.xml";
            // return File(content, contentType, fileName);
            return Ok(result);
        }

        public bool CreateEmployeeXML(PurchaseOrdersDto orderList)
        {           

            //XmlDo
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            XmlElement purchaseDataNode = doc.CreateElement("PurchaseOrders");
            // (employeeDataNode).SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            // (employeeDataNode).SetAttribute("schemaLocation", "http://www.w3.org/2001/XMLSchema-instance", "http://www.testwebsite.org/data/schema/rr/2021 xy-abc-1-1.xsd");
            // (employeeDataNode).SetAttribute("xmlns", "http://www.testwebsite.org/data/schema/rr/2021");

            doc.AppendChild(purchaseDataNode);

            // XmlNode headertNode = doc.CreateElement("Header");
            // employeeDataNode.AppendChild(headertNode);

            // XmlNode contentDateNode = doc.CreateElement("ContentDate");
            // contentDateNode.AppendChild(doc.CreateTextNode("2017-02-01T12:00:00Z"));
            // headertNode.AppendChild(contentDateNode);

            foreach (var item in orderList.PurchaseOrder)
            {                
                //PurchaseOrder
                XmlNode purchaseRecordNode = doc.CreateElement("PurchaseOrder");
                doc.DocumentElement.AppendChild(purchaseRecordNode);

                // XmlNode employeeRecordNode = doc.CreateElement("EmployeeRecord");
                // employeeRecordsNode.AppendChild(employeeRecordNode);

                //PurchaseOrderNumber
                XmlNode purchaseOrderNoNode = doc.CreateElement("PurchaseOrderNumber");
                purchaseOrderNoNode.AppendChild(doc.CreateTextNode(item.PurchaseOrderNumber));
                purchaseRecordNode.AppendChild(purchaseOrderNoNode);

                //PurchaseOrderVersion
                XmlNode purchaseOrderVerNode = doc.CreateElement("PurchaseOrderVersion");
                purchaseOrderVerNode.AppendChild(doc.CreateTextNode(item.PurchaseOrderVersion));
                purchaseRecordNode.AppendChild(purchaseOrderVerNode);

                //PurchaseOrderDate
                XmlNode purchaseOrderDateNode = doc.CreateElement("PurchaseOrderDate");
                purchaseOrderDateNode.AppendChild(doc.CreateTextNode(item.PurchaseOrderDate.ToString()));
                purchaseRecordNode.AppendChild(purchaseOrderDateNode);

                //LastRevisionDate
                XmlNode lastRevisionDateNode = doc.CreateElement("LastRevisionDate");
                lastRevisionDateNode.AppendChild(doc.CreateTextNode(item.LastRevisionDate.ToString()));
                purchaseRecordNode.AppendChild(lastRevisionDateNode);

                //Currency
                XmlNode currencyNode = doc.CreateElement("Currency");
                currencyNode.AppendChild(doc.CreateTextNode(item.Currency));
                purchaseRecordNode.AppendChild(currencyNode);

                //ConsigneeName
                XmlNode consigneeNameNode = doc.CreateElement("ConsigneeName");
                consigneeNameNode.AppendChild(doc.CreateTextNode(item.ConsigneeName));
                purchaseRecordNode.AppendChild(consigneeNameNode);

                //ConsigneeAdd1
                XmlNode consigneeAdd1Node = doc.CreateElement("ConsigneeAdd1");
                consigneeAdd1Node.AppendChild(doc.CreateTextNode(item.ConsigneeAdd1));
                purchaseRecordNode.AppendChild(consigneeAdd1Node);

                //ConsigneeAdd2
                XmlNode consigneeAdd2Node = doc.CreateElement("ConsigneeAdd2");
                consigneeAdd2Node.AppendChild(doc.CreateTextNode(item.ConsigneeAdd2));
                purchaseRecordNode.AppendChild(consigneeAdd2Node);

                //ContactPerson
                XmlNode contactPersonNode = doc.CreateElement("ContactPerson");
                contactPersonNode.AppendChild(doc.CreateTextNode(item.ContactPerson));
                purchaseRecordNode.AppendChild(contactPersonNode);

                //ContactTel
                XmlNode contactTelNode = doc.CreateElement("ContactTel");
                contactTelNode.AppendChild(doc.CreateTextNode(item.ContactTel));
                purchaseRecordNode.AppendChild(contactTelNode);

                //ContactTel
                XmlNode contactEMailNode = doc.CreateElement("ContactEMail");
                contactEMailNode.AppendChild(doc.CreateTextNode(item.ContactEMail));
                purchaseRecordNode.AppendChild(contactEMailNode);

                //SalesPer
                XmlNode salesPerNode = doc.CreateElement("SalesPer");
                salesPerNode.AppendChild(doc.CreateTextNode(item.SalesPer));
                purchaseRecordNode.AppendChild(salesPerNode);

                //SupplierName
                XmlNode supplierNameNode = doc.CreateElement("SupplierName");
                supplierNameNode.AppendChild(doc.CreateTextNode(item.SupplierName));
                purchaseRecordNode.AppendChild(supplierNameNode);

                //SupplierAdd1
                XmlNode supplierAdd1Node = doc.CreateElement("SupplierAdd1");
                supplierAdd1Node.AppendChild(doc.CreateTextNode(item.SupplierAdd1));
                purchaseRecordNode.AppendChild(supplierAdd1Node);

                //ContactTel
                XmlNode supplierAdd2Node = doc.CreateElement("SupplierAdd2");
                supplierAdd2Node.AppendChild(doc.CreateTextNode(item.SupplierAdd2));
                purchaseRecordNode.AppendChild(supplierAdd2Node);

                //SupplierTel
                XmlNode supplierTelNode = doc.CreateElement("SupplierTel");
                supplierTelNode.AppendChild(doc.CreateTextNode(item.SupplierTel));
                purchaseRecordNode.AppendChild(supplierTelNode);

                //SupplierFax
                XmlNode supplierFaxNode = doc.CreateElement("SupplierFax");
                supplierFaxNode.AppendChild(doc.CreateTextNode(item.SupplierFax));
                purchaseRecordNode.AppendChild(supplierFaxNode);

                //BillToName
                XmlNode billToNameNode = doc.CreateElement("BillToName");
                billToNameNode.AppendChild(doc.CreateTextNode(item.BillToName));
                purchaseRecordNode.AppendChild(billToNameNode);

                //BillToName2
                XmlNode billToName2Node = doc.CreateElement("BillToName2");
                billToName2Node.AppendChild(doc.CreateTextNode(item.BillToName2));
                purchaseRecordNode.AppendChild(billToName2Node);

                //BillToName3
                XmlNode billToName3Node = doc.CreateElement("BillToName3");
                billToName3Node.AppendChild(doc.CreateTextNode(""));
                purchaseRecordNode.AppendChild(billToName3Node);
                
                //BillToAdd1
                XmlNode billToAdd1Node = doc.CreateElement("BillToAdd1");
                billToAdd1Node.AppendChild(doc.CreateTextNode(item.BillToAdd1));
                purchaseRecordNode.AppendChild(billToAdd1Node);

                 //BillToAdd2
                XmlNode billToAdd2Node = doc.CreateElement("BillToAdd2");
                billToAdd2Node.AppendChild(doc.CreateTextNode(item.BillToAdd2));
                purchaseRecordNode.AppendChild(billToAdd2Node);
                
                //NotifyName
                XmlNode notifyNameNode = doc.CreateElement("NotifyName");
                notifyNameNode.AppendChild(doc.CreateTextNode(item.NotifyName));
                purchaseRecordNode.AppendChild(notifyNameNode);

                //NotifyName2
                XmlNode notifyName2Node = doc.CreateElement("NotifyName2");
                notifyName2Node.AppendChild(doc.CreateTextNode(item.NotifyName2));
                purchaseRecordNode.AppendChild(notifyName2Node);
                
                //NotifyAdd1
                XmlNode notifyAdd1Node = doc.CreateElement("NotifyAdd1");
                notifyAdd1Node.AppendChild(doc.CreateTextNode(item.NotifyAdd1));
                purchaseRecordNode.AppendChild(notifyAdd1Node);

                //NotifyAdd2
                XmlNode notifyAdd2Node = doc.CreateElement("NotifyAdd2");
                notifyAdd2Node.AppendChild(doc.CreateTextNode(item.NotifyAdd2));
                purchaseRecordNode.AppendChild(notifyAdd2Node);
                
                //CusField1
                XmlNode cusField1Node = doc.CreateElement("CusField1");
                cusField1Node.AppendChild(doc.CreateTextNode(item.CusField1));
                purchaseRecordNode.AppendChild(cusField1Node);

                //CusField2
                XmlNode cusField2Node = doc.CreateElement("CusField2");
                cusField2Node.AppendChild(doc.CreateTextNode(""));
                purchaseRecordNode.AppendChild(cusField2Node);
                
                //EndBuyerAccnt
                XmlNode endBuyerAccntNode = doc.CreateElement("EndBuyerAccnt");
                endBuyerAccntNode.AppendChild(doc.CreateTextNode(item.EndBuyerAccnt));
                purchaseRecordNode.AppendChild(endBuyerAccntNode);

                //EndBuyer
                XmlNode endBuyerNode = doc.CreateElement("EndBuyer");
                endBuyerNode.AppendChild(doc.CreateTextNode(item.EndBuyer));
                purchaseRecordNode.AppendChild(endBuyerNode);

                //EndBuyerCode
                XmlNode endBuyerCodeNode = doc.CreateElement("EndBuyerCode");
                endBuyerCodeNode.AppendChild(doc.CreateTextNode(item.EndBuyerCode));
                purchaseRecordNode.AppendChild(endBuyerCodeNode);
                
                //DeliveryAddress
                XmlNode deliveryAddressNode = doc.CreateElement("DeliveryAddress");
                deliveryAddressNode.AppendChild(doc.CreateTextNode(item.DeliveryAddress));
                purchaseRecordNode.AppendChild(deliveryAddressNode);

                //// GET PO RELEVANT LINE ITEMS 
                var LineItems = orderList.LineItem.Where(x => x.PONo == item.PurchaseOrderNumber);

                //LineItems
                XmlNode lineItemsNode = doc.CreateElement("LineItems");
                purchaseRecordNode.AppendChild(lineItemsNode);

                foreach (var line in LineItems)
                {                    
                    //LineItem
                    XmlNode lineItemNode = doc.CreateElement("LineItem");
                    lineItemsNode.AppendChild(lineItemNode);

                    //PurchaseOrderItem
                    XmlNode purchaseOrderItemNode = doc.CreateElement("PurchaseOrderItem");
                    purchaseOrderItemNode.AppendChild(doc.CreateTextNode(line.PurchaseOrderItem));
                    lineItemNode.AppendChild(purchaseOrderItemNode);

                    //MaterialCode
                    XmlNode materialCodeNode = doc.CreateElement("MaterialCode");
                    materialCodeNode.AppendChild(doc.CreateTextNode(line.MaterialCode));
                    lineItemNode.AppendChild(materialCodeNode);

                    //VendorMaterial
                    XmlNode vendorMaterialNode = doc.CreateElement("VendorMaterial");
                    vendorMaterialNode.AppendChild(doc.CreateTextNode(line.VendorMaterial));
                    lineItemNode.AppendChild(vendorMaterialNode);

                    //ColorCode
                    XmlNode colorCodeNode = doc.CreateElement("ColorCode");
                    colorCodeNode.AppendChild(doc.CreateTextNode(line.ColorCode));
                    lineItemNode.AppendChild(colorCodeNode);

                    //RefMaterial
                    XmlNode refMaterialNode = doc.CreateElement("RefMaterial");
                    refMaterialNode.AppendChild(doc.CreateTextNode(line.RefMaterial));
                    lineItemNode.AppendChild(refMaterialNode);

                    //RefMaterial2
                    XmlNode refMaterial2Node = doc.CreateElement("RefMaterial2");
                    refMaterial2Node.AppendChild(doc.CreateTextNode(line.RefMaterial2));
                    lineItemNode.AppendChild(refMaterial2Node);

                    //ItemText
                    XmlNode itemTextNode = doc.CreateElement("ItemText");
                    itemTextNode.AppendChild(doc.CreateTextNode(line.ItemText));
                    lineItemNode.AppendChild(itemTextNode);

                    //MatPoText
                    XmlNode matPoTextNode = doc.CreateElement("MatPoText");
                    matPoTextNode.AppendChild(doc.CreateTextNode(line.MatPoText));
                    lineItemNode.AppendChild(matPoTextNode);

                    //PageFormat
                    XmlNode pageFormatNode = doc.CreateElement("PageFormat");
                    pageFormatNode.AppendChild(doc.CreateTextNode(line.PageFormat));
                    lineItemNode.AppendChild(pageFormatNode);

                    //MaterialDescription
                    XmlNode materialDescriptionNode = doc.CreateElement("MaterialDescription");
                    materialDescriptionNode.AppendChild(doc.CreateTextNode(line.MaterialDescription));
                    lineItemNode.AppendChild(materialDescriptionNode);

                    //SalesOrder
                    XmlNode salesOrderNode = doc.CreateElement("SalesOrder");
                    salesOrderNode.AppendChild(doc.CreateTextNode(line.SalesOrder));
                    lineItemNode.AppendChild(salesOrderNode);

                    //SalesOrderItem
                    XmlNode salesOrderItemNode = doc.CreateElement("SalesOrderItem");
                    salesOrderItemNode.AppendChild(doc.CreateTextNode(line.SalesOrderItem));
                    lineItemNode.AppendChild(salesOrderItemNode);

                    //DeliveryDate
                    XmlNode deliveryDateNode = doc.CreateElement("DeliveryDate");
                    deliveryDateNode.AppendChild(doc.CreateTextNode(line.DeliveryDate.ToString()));
                    lineItemNode.AppendChild(deliveryDateNode);

                    //Quantity
                    XmlNode quantityNode = doc.CreateElement("Quantity");
                    quantityNode.AppendChild(doc.CreateTextNode(line.Quantity));
                    lineItemNode.AppendChild(quantityNode);

                     //UOM
                    XmlNode uomNode = doc.CreateElement("UOM");
                    uomNode.AppendChild(doc.CreateTextNode(line.UOM));
                    lineItemNode.AppendChild(uomNode);

                    //NetPrice
                    XmlNode netPriceNode = doc.CreateElement("NetPrice");
                    netPriceNode.AppendChild(doc.CreateTextNode(line.NetPrice));
                    lineItemNode.AppendChild(netPriceNode);

                    //Per
                    XmlNode perNode = doc.CreateElement("Per");
                    perNode.AppendChild(doc.CreateTextNode(line.Per));
                    lineItemNode.AppendChild(perNode);

                    //NetValue
                    XmlNode netValueNode = doc.CreateElement("NetValue");
                    netValueNode.AppendChild(doc.CreateTextNode(""));
                    lineItemNode.AppendChild(netValueNode);

                    //Text
                    XmlNode textNode = doc.CreateElement("Text");
                    textNode.AppendChild(doc.CreateTextNode(line.Text));
                    lineItemNode.AppendChild(textNode);

                    //ProductType
                    XmlNode productTypeNode = doc.CreateElement("ProductType");
                    productTypeNode.AppendChild(doc.CreateTextNode(line.ProductType));
                    lineItemNode.AppendChild(productTypeNode);

                    //Gender
                    XmlNode genderNode = doc.CreateElement("Gender");
                    genderNode.AppendChild(doc.CreateTextNode(line.Gender));
                    lineItemNode.AppendChild(genderNode);

                    //ShipMode
                    XmlNode shipModeNode = doc.CreateElement("ShipMode");
                    shipModeNode.AppendChild(doc.CreateTextNode(""));
                    lineItemNode.AppendChild(shipModeNode);

                    //ScheduleLineNo
                    XmlNode scheduleLineNoNode = doc.CreateElement("ScheduleLineNo");
                    scheduleLineNoNode.AppendChild(doc.CreateTextNode(""));
                    lineItemNode.AppendChild(scheduleLineNoNode);

                    //GridValue
                    XmlNode gridValueNode = doc.CreateElement("GridValue");
                    gridValueNode.AppendChild(doc.CreateTextNode(""));
                    lineItemNode.AppendChild(gridValueNode);

                    //OverDeliveryTolerance
                    XmlNode overDeliveryToleranceNode = doc.CreateElement("OverDeliveryTolerance");
                    overDeliveryToleranceNode.AppendChild(doc.CreateTextNode(""));
                    lineItemNode.AppendChild(overDeliveryToleranceNode);
                }                
            }

            // //RelationshipStatus
            // XmlNode employeeStatusNode = doc.CreateElement("EmployeeStatus");
            // employeeStatusNode.AppendChild(doc.CreateTextNode("ACTIVE"));
            // employeeRecordNode.AppendChild(employeeStatusNode);

            // var basePath = Path.Combine(Environment.CurrentDirectory, @"XmlFiles\");
            var basePath = Path.Combine("C:\\", @"XmlFiles\");

            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }
            var newFileName = string.Format("{0}{1}", "PurchaseOrder", ".xml"); //Guid.NewGuid().ToString("N")
            doc.Save(basePath + newFileName);
            // return File(memoryStream.ToArray(), "application/xml", "file.xml");
            

            return true;
        }

        #region "WorkStudy"

            [HttpPost("WorkStudySavedata")]
            public async Task<IActionResult> WorkStudySavedata(List<SaveWorkStudyDto> wsDt)
            {
                var result = await _pTrackMasterRepository.WorkStudySavedata(wsDt);
                return Ok(result);
            }

            [HttpPost("GetWorkStudyData")]
            public async Task<IActionResult> GetWorkStudyData(WorkStudyDto wsDt)
            {
                var result = await _pTrackMasterRepository.GetWorkStudyData(wsDt);
                return Ok(result);
            }
        #endregion "WorkStudy"

        #region "CPD"
            [HttpPost("GetCPDData")]
            public async Task<IActionResult> GetCPDData(CPDDto wsDt)
            {
                var result = await _pTrackMasterRepository.GetCPDData(wsDt);
                return Ok(result);
            }

            [HttpPost("SaveCPDData")]
            public async Task<IActionResult> SaveCPDData(List<SaveWorkStudyDto> wsDt)
            {
                
                var result = await _pTrackMasterRepository.SaveCPDData(wsDt);
                return Ok(result);
            }

        #endregion "CPD"
        
        #region "Transport"

            [HttpPost("GetTransportData")]
            public async Task<IActionResult> GetTransportData(TransportDto wsDt)
            {
                var result = await _pTrackMasterRepository.GetTransportData(wsDt);
                return Ok(result);
            }

            [HttpPost("SaveTransportData")]
            public async Task<IActionResult> SaveTransportData(List<SaveTransportDto> wsDt)
            {
                
                var result = await _pTrackMasterRepository.SaveTransportData(wsDt);
                return Ok(result);
            }
        #endregion "Transport"

        #region "PaymentInvoice"
        [HttpPost("GetPaymentInvoiceData")]
        public async Task<IActionResult> GetPaymentInvoiceData(PaymentInvoiceDto pidt)
        {
            var result = await _pTrackMasterRepository.GetPaymentInvoiceData(pidt);
            return Ok(result);
        }

        [HttpPost("SavePaymentInvoiceData")]
        public async Task<IActionResult> SavePaymentInvoiceData(List<SavePaymentInvoiceDto> piDt)
        {
            
            var result = await _pTrackMasterRepository.SavePaymentInvoiceData(piDt);
            return Ok(result);
        }


        #endregion "PaymentInvoice"

        #region FastReact 

        [HttpGet("FROrders/{myDate}")]
        public async Task<IActionResult> getFROrders(DateTime myDate) 
        {
            var result = await _pTrackMasterRepository.getFROrdersAsync(myDate);
            return Ok(result);

        }   
        
        [HttpPost("getFRWFXDetails")]
        public async Task<IActionResult> GetFRWFXDetails(FRwfxDto frDt)
        {
            var result = await _pTrackMasterRepository.GetFRWFXDetailsAsync(frDt);
            return Ok(result);
        }

        [HttpPost("SaveFRPO")]
        public async Task<IActionResult> saveFRAssocationOrder(SaveFROrderDto frPODto)
        {
            var result = await _pTrackMasterRepository.saveFRAssocationOrderAsync(frPODto);
            return Ok(result);
        }

        [HttpGet("FRNewOrders/{myOrder}")]
        public async Task<IActionResult> getFRNewOrders(string myOrder) 
        {
            var result = await _pTrackMasterRepository.getFRNewOrdersAsync(myOrder);
            return Ok(result);

        }   

    #endregion FastReact

    #region Dashboard 

        [HttpPost("GetDashBoardTwoData")]
        public async Task<ActionResult> GetDashBoardTwoDataList(dashBoardTwoSearchDto dashboardDt)
        {
            var result= await _pTrackMasterRepository.GetDashBoardTwoDataListAsync(dashboardDt);
            return Ok(result);
        }

    #endregion Dashboard

    #region Get Employee
        [HttpGet("EmpGet/{location}")]
        public async Task<IActionResult> GetEmployeDt(string location)
        {
            var result= await _pTrackMasterRepository.GetEmployeDtAsync(location);
            return Ok(result);
        }
    #endregion Get Employee

    #region Get Operation
        [HttpGet("OpeGet/{employeeId}")]
        public async Task<ActionResult> GetEmployeeRemaining(int employeeId)
        {
            var result= await _pTrackMasterRepository.GetEmployeeRemainingAsync(employeeId);
            return Ok(result);
        }
    #endregion Get Operation

    #region Operation Assigned To Employee Get
        [HttpGet("EmpOpGet/{employeeId}")]
        public async Task<ActionResult> GetEmployeeOpDt(int employeeId)
        {
            var result= await _pTrackMasterRepository.GetEmployeeOpDtAsync(employeeId);
            return Ok(result);
        }
    #endregion Operation Assigned To Employee Get

    #region Operation Assigned To Employee Save
        [HttpPost("SaveEmp")]
        public async Task<IActionResult> SaveEmployeeOperation(TransOperationsAndSectionsAssignToEmp operation)
        {
            var result = await _pTrackMasterRepository.SaveEmployeeOperationAsync(operation);
            return Ok(result);
        }
    #endregion Operation Assigned To Employee Save
    #region Operation Assigned To Employee Deactive
     [HttpPost("DeactOp")]
        public async Task<IActionResult> DeactiveOperationEmp(TransOperationsAndSectionsAssignToEmp operation)
        {
            var result = await _pTrackMasterRepository.DeactiveOperationEmpAsync(operation);
            return Ok(result);
        }
    #endregion Operation Assigned To Employee Deactive
    #region Get Factory Wise Departments
    [HttpGet("DepartGet/{locationId}")]
        public async Task<IActionResult> GetFactoryWiseDepartments(int locationId)
        {
            var result= await _pTrackMasterRepository.GetFactoryWiseDepartmentsAsync(locationId);
            return Ok(result);
        }
    #endregion Get Factory Wise Departments

    #region Get Factory , Department Wise Section
    [HttpPost("GetSec")]
        public async Task<IActionResult> GetDepartmentWiseSection(SectionDetailsDto section)
        {
            var result = await _pTrackMasterRepository.GetDepartmentWiseSectionAsync(section);
            return Ok(result);
        }
    #endregion Get Factory , Department Wise Section

    #region Get Factory , Department ,Section Wise Sub-Section
    [HttpPost("GetSubSec")]
        public async Task<IActionResult> GetSectionWiseSubSection(SubSectionDto subsection)
        {
            var result = await _pTrackMasterRepository.GetSectionWiseSubSectionAsync(subsection);
            return Ok(result);
        }
    #endregion Get Factory , Department ,Section Wise Sub-Section

    #region Get Factory , Department ,Section ,Sub-Section Wise Lines
     [HttpPost("GetLine")]
        public async Task<IActionResult> GetSubSectionWiseLine(SectionLineDto line)
        {
            var result = await _pTrackMasterRepository.GetSubSectionWiseLineAsync(line);
            return Ok(result);
        }
    #endregion Get Factory , Department ,Section ,Sub-Section Wise Lines

    #region Get Lost Time Reason
    [HttpGet("LostRGet/{moduleId}")]
        public async Task<IActionResult> GetLostTimeReason(int moduleId)
        {
            var result= await _pTrackMasterRepository.GetLostTimeReasonAsync(moduleId);
            return Ok(result);
        }
    #endregion Get Lost Time Reason

    #region Get Lost Time
     [HttpPost("GetLost")]
        public async Task<IActionResult> GetLostTime(LostTimeSearchDto lost)
        {
            var result = await _pTrackMasterRepository.GetLostTimeAsync(lost);
            return Ok(result);
        }
    #endregion Get Lost Time

    #region Save Lost Time
     [HttpPost("SaveLost")]
        public async Task<IActionResult> SaveLostTime(LostTimeSaveDto lostS)
        {
            var result = await _pTrackMasterRepository.SaveLostTimeAsync(lostS);
            return Ok(result);
        }
    #endregion Save Lost Time
    }
}