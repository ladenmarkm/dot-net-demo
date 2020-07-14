using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnicalTask.Utilities;

namespace TechnicalTask.Controllers
{
    public class GridController : Controller
    {
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var results = Reference.GetViewModelList();

            // necessary if dataset becomes too large
            var jsonResult = Json(results.ToDataSourceResult(request));
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
    }
}