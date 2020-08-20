using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLCoreAPI.Controller.Models
{
    public class RefreshDataModel
    {
        public string Token { get; set; }
        public string Refresh { get; set; }
    }
}
