using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_Exam_ImageGallery
{
    /// <summary>
    /// Interaction logic for RegistrationWinsow.xaml
    /// </summary>
    public partial class RegistrationWinsow : Window
    {
        public RegistrationWinsow()
        {
            InitializeComponent();
        }

        private void Clic_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
