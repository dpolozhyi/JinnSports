using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JinnSports.WEB.Areas.Mvc.Models
{
    public class PageInfo
    {
        private const int PREVIOUSPAGES = 4;

        private const int NEXTPAGES = 4;

        public PageInfo(
            string controllerName,
            string actionName,
            int totalItems, 
            int currentPage, 
            int pageSize)
        {
            this.ControllerName = controllerName;
            this.ActionName = actionName;
            
            this.CurrentPage = currentPage;
            this.PageSize = pageSize; 

            this.TotalItems = totalItems;

            this.TotalPages = (int)Math.Ceiling(
                this.TotalItems / (double)this.PageSize);

            this.StartPage = this.CurrentPage - PREVIOUSPAGES;
            if(this.StartPage < 1)
            {
                this.StartPage = 1;
            }

            this.EndPage = this.CurrentPage + NEXTPAGES;
            if(this.EndPage > this.TotalPages)
            {
                this.EndPage = this.TotalPages;
            }
        }

        public string ControllerName { get; private set; }

        public string ActionName { get; private set; }

        public int TotalItems { get; private set; }

        public int TotalPages { get; private set; }

        public int CurrentPage { get; private set; }

        public int StartPage { get; private set; }

        public int EndPage { get; private set; }

        public int PageSize { get; private set; }
    }
}