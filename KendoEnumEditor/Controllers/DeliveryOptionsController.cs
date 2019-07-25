using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using KendoEnumEditor.Models;
using DayOfWeek = KendoEnumEditor.Models.DayOfWeek;

namespace KendoEnumEditor.Controllers
{
    public class DeliveryOptionsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ReadAll([DataSourceRequest] DataSourceRequest request)
        {
            var resultList = GetData()
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToArray();

            var result = new DataSourceResult
            {
                Data = resultList.AsQueryable(),
                Total = resultList.Count()
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, DeliveryOptionsGridViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new[] { viewModel }.ToDataSourceResult(request, ModelState));
            }

            var item = GetData().Single(d => d.Id == viewModel.Id);
            item.Type = viewModel.Type;
            item.ChosenDay = viewModel.ChosenDay;

            return Json(new[] { viewModel }.ToDataSourceResult(request, ModelState));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var item = GetData().Single(d => d.Id == id);

            var viewModel = new DeliveryOptionsViewModel(item.Id, item.ChosenDay, item.Type);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(DeliveryOptionsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var item = GetData().Single(d => d.Id == viewModel.Id);
            item.Type = viewModel.Type;
            item.ChosenDay = viewModel.ChosenDay;

            return RedirectToAction("Index");
        }

        private List<DeliveryOptionsGridViewModel> GetData()
        {
            return this.HttpContext.Session["MyData"] as List<DeliveryOptionsGridViewModel>;
        }

        private void SetupData()
        {
            var data = GetData();
            if (data == null)
            {
                this.HttpContext.Session["MyData"] = new List<DeliveryOptionsGridViewModel>()
                {
                    new DeliveryOptionsGridViewModel(1, DayOfWeek.Monday, DeliveryType.Courier),
                    new DeliveryOptionsGridViewModel(2, DayOfWeek.Tuesday, DeliveryType.Courier),
                    new DeliveryOptionsGridViewModel(3, DayOfWeek.Wednesday, DeliveryType.Personal),
                    new DeliveryOptionsGridViewModel(4, DayOfWeek.Saturday, DeliveryType.Electronic),
                };
            }
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            SetupData();
        }
    }
}