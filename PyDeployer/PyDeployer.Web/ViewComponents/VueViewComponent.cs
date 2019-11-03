using System;
using Microsoft.AspNetCore.Mvc;

namespace PyDeployer.Web.ViewComponents
{
    public class VueViewComponent : ViewComponent
    {


        public IViewComponentResult Invoke(string viewName) 
        {
            var elm = new VueElement()
            {
                ViewName = viewName,
                VmElementId = GetRandomViewElementId()
            };
            return View(elm);
        }
        
        protected string GetRandomViewElementId()
        {
            var guid = Guid.NewGuid();
            return string.Format("pageVm_{0}", guid.ToString());
        }
        
        public class VueElement
        {
            public string ViewName { get; set; }
            public string VmElementId { get; set; }
        }
    }
    
}