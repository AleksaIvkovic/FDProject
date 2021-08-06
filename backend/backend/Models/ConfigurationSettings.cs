using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class ConfigurationSettings
    {
        //Maximum allowed size in bytes
        public int MaxSize { get; set; }
        //Accepted file types, eg. ".jpg, .png"
        public string AcceptedTypes { get; set; }
        public ConfigurationSettings() { }
    }
}
