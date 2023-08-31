using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data.SqlClient;


namespace WPF_Exam_ImageGallery
{
    interface IModel
    {
        void AddAccountToDataBase(string nameAdd, string surnameAdd, string loginAdd, string passwordAdd);
        void AddImageToDateBase(string fileName, string activeAccount, string fileFullName, string dateNow);
        void UpdateMarkToDataBase(string strDatabaseQuery);
        void LoadAccountFromDataBase(List<AccountViewModel> AccountsList);
        void LoadImagesFromDataDase(ObservableCollection<ImageVeiwModel> ImageList, ref int CountImages, ref int CurrentIndex);
    }
}
