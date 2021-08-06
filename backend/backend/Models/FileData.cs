using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class FileData
    {
        //File name
        public string Name { get; set; }
        //File size in bytes
        public long Size { get; set; }
        //File creation date
        public DateTime CreationDate { get; set; }

        public FileData() { }
    }
}
