using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.DTOs
{
    public class ImageUploadedDto
    {
        public string FullName { get; set; }
        public string OldName { get; set; }
        public string Extension { get; set; }//hangi eklentide olduğunu bilmek için (png, jpeg...)
        public string Path { get; set; }
        public string FolderName { get; set; }
        public long Size { get; set; }
    }
}
