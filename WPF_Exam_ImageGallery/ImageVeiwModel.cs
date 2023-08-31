using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Exam_ImageGallery
{
    class ImageVeiwModel : ViewModelBase
    {
        private Image image;

        public ImageVeiwModel(Image image_)
        {
            image = image_;
        }
        public ImageVeiwModel()
        {
            
        }
        public int Id
        {
            get { return image.IdF; }
            set
            {
                image.IdF = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Name
        {
            get { return image.NameF; }
            set
            {
                image.NameF = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Path
        {
            get { return image.PathF; }
            set
            {
                image.PathF = value;
                OnPropertyChanged(nameof(Path));
            }
        }

        public string Author
        {
            get { return image.AuthorF; }
            set
            {
                image.AuthorF = value;
                OnPropertyChanged(nameof(Author));
            }
        }
        public string Date
        {
            get { return image.DateF; }
            set
            {
                image.DateF = value;
                OnPropertyChanged(nameof(Date));
            }
        }
        public int Mark
        {
            get { return image.MarkF; }
            set
            {
                image.MarkF = value;
                OnPropertyChanged(nameof(Mark));
            }
        }
    }
}
