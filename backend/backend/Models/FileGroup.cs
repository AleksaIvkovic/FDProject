using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class FileGroup
    {
        //File type
        public string Type { get; set; }
        //All files of same type
        public List<FileData> Files { get; set; }
    }
}
