using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Exam_ImageGallery
{
    class Image
    {
        public int IdF { get; set; }
        public string NameF { get; set; }
        public string PathF { get; set; }
        public string AuthorF { get; set; }      
        public string DateF { get; set; }
        public int MarkF { get; set; }
        public Image( int id_,string name_, string path_, string author_, string date_ , int mark_)
        {
            IdF = id_;
            NameF = name_;
            PathF = path_;
            AuthorF = author_;
            DateF = date_;
            MarkF = mark_;

        }
    }
}
