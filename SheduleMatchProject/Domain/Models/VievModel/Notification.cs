using Domain.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.VievModel
{
    
    public class Notification
    {
        public string Message { get; set; }
        public NotificationType Type { get; set; }
    }
}
