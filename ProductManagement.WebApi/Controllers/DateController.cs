using Repository.ViewModel.Common;
using System;
using System.Globalization;
using System.Web.Http;
using System.Web.Http.Description;

namespace ProductManagement.WebApi.Controllers
{
    public class DateController : ApiController
    {
        Date date = new Date();

        [ResponseType(typeof(Date))]
        public IHttpActionResult GetDate()
        {
            PersianCalendar p = new PersianCalendar();
            date.Year = p.GetYear(DateTime.Now).ToString();
            date.Month = p.GetMonth(DateTime.Now).ToString();
            date.Day = p.GetDayOfMonth(DateTime.Now).ToString();

            return Ok(date);
        }
    }
}
