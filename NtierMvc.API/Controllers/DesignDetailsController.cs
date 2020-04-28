using System;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using NtierMvc.BusinessLogic.Interface;
using NtierMvc.BusinessLogic.Worker;
using NtierMvc.Model;
using NtierMvc.Model.DesignEng;

namespace NtierMvc.API.Controllers.Application
{
    public class DesignDetailsController : ApiController
    {
        IDesignWorker _repository = new DesignWorker();

        [Route("api/DesignDetails/GetProductRealisationDetails")]
        public IHttpActionResult GetProductRealisationDetails(int pageIndex, int pageSize, string SearchTypeId = null, string SearchQuoteNo = null, string SearchSONo = null, string SearchVendorId = null, string SearchVendorName = null, string SearchProductGroup = null)
        {
            return Ok(_repository.GetProductRealisationDetails(pageIndex, pageSize, SearchTypeId, SearchQuoteNo, SearchSONo, SearchVendorId, SearchVendorName, SearchProductGroup));
        }

        [Route("api/DesignDetails/GetBOMList")]
        public IHttpActionResult GetBOMList(string ProductName = null, string ProductCode = null, string PL = null, string ProductNo = null, string CasingSize = null, string CasingPPF = null, string Grade = null, string OpenHoleSize = null)
        {
            return Ok(_repository.GetBOMList(ProductName, ProductCode, PL, ProductNo, CasingSize, CasingPPF, Grade, OpenHoleSize));
        }

        [HttpPost]
        [ResponseType(typeof(ProductRealisation))]
        [Route("api/DesignDetails/PRPPopup")]
        public IHttpActionResult PRPPopup(ProductRealisation Model)
        {
            return Ok(_repository.PRPPopup(Model));
        }

        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("api/DesignDetails/SaveBOMDetails")]
        public IHttpActionResult SaveBOMDetails(BOMEntity viewModel)
        {
            return Ok(_repository.SaveBOMDetails(viewModel));
        }

        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("api/DesignDetails/SaveProductRealisationDetails")]
        public IHttpActionResult SaveProductRealisationDetails(ProductRealisation viewModel)
        {
            return Ok(_repository.SaveProductRealisationDetails(viewModel));
        }

        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("api/DesignDetails/BillDetailsPopup")]
        public IHttpActionResult BillDetailsPopup(GateEntryEntity viewModel)
        {
            return Ok(_repository.BillDetailsPopup(viewModel));
        }

        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("api/DesignDetails/SaveBillMonitoringDetails")]
        public IHttpActionResult SaveBillMonitoringDetails(GateEntryEntity viewModel)
        {
            return Ok(_repository.SaveBillMonitoringDetails(viewModel));
        }

        [Route("api/DesignDetails/GetPoSLNoDetails")]
        public IHttpActionResult GetPoSLNoDetails(string POSlNo = null)
        {
            return Ok(_repository.GetPoSLNoDetails(POSlNo));
        }

        [HttpGet]
        [ResponseType(typeof(DataTable))]
        [Route("api/DesignDetails/GetDataTablePRPData")]
        public IHttpActionResult GetDataTablePRPData(string ReportType, string DateFrom, string DateTo, string VendorId = null, string SoNo = null)
        {
            return Ok(_repository.GetDataTablePRPData(ReportType, DateFrom, DateTo, VendorId, SoNo));
        }

        [HttpGet]
        [Route("api/DesignDetails/GetVendorIdFromQuoteType")]
        public IHttpActionResult GetVendorIdFromQuoteType(string ReportType=null)
        {
            return Ok(_repository.GetVendorIdFromQuoteType(ReportType=null));
        }

        [HttpGet]
        [Route("api/DesignDetails/GetQuoteOrderDetailsForPRP")]
        public IHttpActionResult GetQuoteOrderDetailsForPRP(string quoteType, string quoteNoId)
        {
            return Ok(_repository.GetQuoteOrderDetailsForPRP(quoteType, quoteNoId));
        }

    }
}
