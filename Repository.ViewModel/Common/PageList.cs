using System;

namespace Repository.ViewModel.Common
{
    public class PageList
    {
        public Int32 CurrentPage { get; set; }
        public Int32 ItemsPerPage { get; set; }//PageSize
        public int TotalItems { get; set; }
        //public int TotalPages
        //{
        //    get
        //    {
        //        return (int)Math.Ceiling((decimal)TotalRecords / PageSize);
        //    }
        //}

        public string Filter { get; set; }
        public string Sort { get; set; }
        public string SortDir { get; set; }

        //public PageList()
        //{
        //    this.Filter = string.Empty;
        //    this.CurrentPage = 1;
        //    this.ItemsPerPage = 5;
        //    this.Sort = "Id";
        //    this.SortDir = "DESC";
        //}
    }
}
