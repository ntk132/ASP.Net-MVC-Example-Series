using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.Context;

namespace WebApp.Areas.Admin.Models
{
    public class AspAdmin
    {
        private string ADName { get; set; }
        private string ADPasswordHash { get; set; }
        private string ADDisplayName { get; set; }
        private string ADEmail { get; set; }
    }
}