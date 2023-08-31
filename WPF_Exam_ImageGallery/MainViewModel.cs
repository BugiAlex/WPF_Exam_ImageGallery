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
using System.Data;
using System.Windows.Controls;



namespace WPF_Exam_ImageGallery
{
    class MainViewModel : ViewModelBase
    {
        private readonly IModel model;
        public List<AccountViewModel> AccountsList { get; set; }
        public ObservableCollection<ImageVeiwModel> ImageList { get; set; }
        public MainViewModel(IModel model_)
        {
            model = model_;
            AccountsList = new List<AccountViewModel>();                   
            ImageList = new ObservableCollection<ImageVeiwModel>();
            model.LoadAccountFromDataBase(AccountsList);
            LoadImage();
            //CurrentIndex = 0;
        }
        //------- Add account 
       
        string loginAdd = null;
        string passwordAdd = null;
        string passwordAgainAdd = null;
        string surnameAdd = null;
        string nameAdd = null;

        public string LoginAdd
        {

            get { return loginAdd; }
            set
            {
                loginAdd = value;
                OnPropertyChanged(nameof(LoginAdd));
            }
        }
        public string PasswordAdd
        {
            get { return passwordAdd; }
            set
            {
                passwordAdd = value;
                OnPropertyChanged(nameof(PasswordAdd));
            }
        }
        public string PasswordAgainAdd
        {
            get { return passwordAgainAdd; }
            set
            {
                passwordAgainAdd = value;
                OnPropertyChanged(nameof(PasswordAgainAdd));
            }
        }
        public string NameAdd
        {
            get { return nameAdd; }
            set
            {
                nameAdd = value;
                OnPropertyChanged(nameof(NameAdd));
            }
        }
        public string SurnameAdd
        {
            get { return surnameAdd; }
            set
            {
                surnameAdd = value;
                OnPropertyChanged(nameof(SurnameAdd));
            }
        }
        //-------------------------------------
        DelegateCommand passwordAgain;

        public ICommand PasswordAgain
        {
            get
            {
                if (passwordAgain == null)
                {
                    passwordAgain = new DelegateCommand(param => this.PasswordAgainA(), param => this.CanPasswordAgain());
                }
                return passwordAgain;
            }
        }

        void PasswordAgainA()
        {
            
        }

        bool CanPasswordAgain()
        {
            if (passwordAgain.Parameter is PasswordBox)
            {
                PasswordAgainAdd = ((PasswordBox)passwordAgain.Parameter).Password;
            }
            return true;
        }
       //------------------------------------
        private DelegateCommand addAccountCommand;

        public ICommand AddAccountCommand
        {
            get
            {
                if (addAccountCommand == null)
                {
                    addAccountCommand = new DelegateCommand(param => this.AddAccount(), param => this.CanAddAccount());
                }
                return addAccountCommand;
            }
        }

        private void AddAccount()
        {
            bool checkLoginToBase = true;
            for (int i = 0; i < AccountsList.Count; i++)
            {
                if (AccountsList[i].Login == LoginAdd)
                { checkLoginToBase = false; }

            }
            if (checkLoginToBase)
            {
                if (PasswordAdd == PasswordAgainAdd)
                {

                    model.AddAccountToDataBase(NameAdd, SurnameAdd, LoginAdd, PasswordAdd);
                    model.LoadAccountFromDataBase(AccountsList);
                }
                else
                {
                    ExceptionWindow exc = new ExceptionWindow("Passwords do not match!");
                    exc.Show();
                }
            }
            else
            {
                ExceptionWindow exc = new ExceptionWindow("The database already has an account with this login!");
                exc.Show();
            }
            SurnameAdd = null;
            NameAdd = null;
            LoginAdd = null;
            PasswordAdd = null;
            PasswordAgainAdd = null;
        }
        private bool CanAddAccount()
        {
            if (addAccountCommand?.Parameter is PasswordBox)
            {
                PasswordAdd = ((PasswordBox)addAccountCommand.Parameter).Password;
            }
            return (LoginAdd != null && PasswordAdd != null && PasswordAgainAdd != null && NameAdd != null && SurnameAdd != null);
        }
        //------ Check Login
        string loginCheck = null;
        string passworCheck = null;
        string activeAccount = null;
        public string LoginCheck
        {
            get { return loginCheck; }
            set
            {
                loginCheck = value;
                OnPropertyChanged(nameof(LoginCheck));
            }
        }
        public string PasswordCheck
        {
            get { return passworCheck; }
            set
            {
                passworCheck = value;
                OnPropertyChanged(nameof(PasswordCheck));
            }
        }
        public string ActiveAccount
        {
            get { return activeAccount; }
            set
            {
                activeAccount = value;
                OnPropertyChanged(nameof(ActiveAccount));
            }
        }

        private int CheckLogin()
        {
            for (int i = 0; i < AccountsList.Count; i++)
            {
                if (AccountsList[i].Login == loginCheck)
                {
                    ActiveAccount = AccountsList[i].Name + " " + AccountsList[i].Surname;
                    return i;
                }
            }
            return -1;
        }

        private DelegateCommand enterCommand;

        public ICommand EnterCommand
        {
            get
            {
                if (enterCommand == null)
                {
                    enterCommand = new DelegateCommand(param => this.Enter(), param => this.CanEnter());
                }
                return enterCommand;
            }
        }
        private void Enter()
        {
            if (CheckLogin() == -1)
            {
                ExceptionWindow exc = new ExceptionWindow("There is no account with such a login in the database!");
                exc.Show();
                return;
            }
            if (AccountsList[CheckLogin()].Password != passworCheck)
            {
                ExceptionWindow exc = new ExceptionWindow("Wrong password!");
                exc.Show();
                return;
            }
            else
            {

                ImageGalleryWindow igw = new ImageGalleryWindow();
                igw.DataContext = this;
                igw.Show();
            }
        }
        private bool CanEnter()
        {
            if (enterCommand?.Parameter is PasswordBox)
            {
                PasswordCheck = ((PasswordBox)enterCommand.Parameter).Password;
            }           
            return (LoginCheck != null && PasswordCheck != null && LoginCheck != "" && PasswordCheck != "");
        }
        // ---- Добавление изображений
        private DelegateCommand addImageCommand;

        public ICommand AddImageCommand
        {
            get
            {
                if (addImageCommand == null)
                {
                    addImageCommand = new DelegateCommand(param => this.AddImage(), null);
                }
                return addImageCommand;
            }
        }

        private int countImage = -1;
        public int CountImages
        {
            get { return countImage; }
            set
            {   
                countImage = value;
               
                OnPropertyChanged(nameof(CountImages));
            }
        }
        
        private int currentIdex = 0;

        public int CurrentIndex
        {
            get { return currentIdex; }
            set
            {
                currentIdex = value;
                PathFile = ImageList[currentIdex].Path;
                AuthorFile = ImageList[currentIdex].Author;
                NameFile = ImageList[currentIdex].Name;
                DateFile = ImageList[currentIdex].Date;
                SlMark = ImageList[currentIdex].Mark;
                OnPropertyChanged(nameof(currentIdex));
            }
        }
       
        string pathFile;
        public string PathFile
        {

            get { return pathFile; }
            set
            {
                pathFile = value;
                OnPropertyChanged(nameof(PathFile));
            }
        }

        string authorFile;
        public string AuthorFile
        {
            get { return authorFile; }
            set
            {
                authorFile = value;
                OnPropertyChanged(nameof(AuthorFile));
            }
        }

        string nameFile;
        public string NameFile
        {
            get { return nameFile; }
            set
            {
                nameFile = value;
                OnPropertyChanged(nameof(NameFile));
            }
        }

        string dateFile;
        public string DateFile
        {
            get { return dateFile; }
            set
            {
                dateFile = value;
                OnPropertyChanged(nameof(DateFile));
            }
        }
      
        private void AddImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Picture files (*.jpg)|*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    FileInfo fi = new FileInfo(openFileDialog.FileName);
                    string DateN = Convert.ToString(DateTime.Now);

                    model.AddImageToDateBase(fi.Name, ActiveAccount, fi.FullName, DateN);                                                                                                            
                }
                catch (Exception ex)
                {
                    StreamWriter sw = new StreamWriter("../../Exception.txt", false);
                    string line = ex.ToString();
                    sw.WriteLine(line);
                    sw.Close();
                }
            }
            LoadImage();
        }

        private void LoadImage()
        {
            try
            {
                int countImageAfterLoad = 0;
                int currentIndexAfterLoad = 0;
                model.LoadImagesFromDataDase(ImageList, ref countImageAfterLoad, ref currentIndexAfterLoad);
                CountImages = countImageAfterLoad;
                CurrentIndex = currentIndexAfterLoad;
            }
            catch(Exception ex)
            {
                StreamWriter sw = new StreamWriter("../../Exception.txt", false);
                string line = ex.ToString();
                sw.WriteLine(line);
                sw.Close();
            }
            
        }
        //---
        private DelegateCommand firstCommand;

        public ICommand FirstCommand
        {
            get
            {
                if (firstCommand == null)
                {
                    firstCommand = new DelegateCommand(param => this.First(), param => this.CanPrevious());
                }
                return firstCommand;
            }
        }
        private void First()
        {
            CurrentIndex = 0;
        }

        private DelegateCommand lastCommand;

        public ICommand LastCommand
        {
            get
            {
                if (lastCommand == null)
                {
                    lastCommand = new DelegateCommand(param => this.Last(), param => this.CanLast());
                }
                return lastCommand;
            }
        }
        private void Last()
        {
            CurrentIndex = CountImages;
        }
        private bool CanLast()
        {
            if (CurrentIndex == CountImages || CountImages == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private DelegateCommand previousCommand;
        public ICommand PreviousCommand
        {
            get
            {
                if (previousCommand == null)
                {
                    previousCommand = new DelegateCommand(param => this.Previous(), param => this.CanPrevious());
                }
                return previousCommand;
            }
        }
        private void Previous()
        {
            CurrentIndex--;
        }
        private bool CanPrevious()
        {
            if (CurrentIndex == 0) 
            { 
                return false; 
            }
            else
            {
                return true;
            }
        }
        private DelegateCommand nexrCommand;
        public ICommand NextCommand
        {
            get
            {
                if (nexrCommand == null)
                {
                    nexrCommand = new DelegateCommand(param => this.Next(), param => this.CanLast());
                }
                return nexrCommand;
            }
        }
        private void Next()
        {
            CurrentIndex++;
        }
    
        private int slMark;

        public int SlMark
        {
            get { return slMark; }
            set
            {
                slMark = value;
                ImageList[CurrentIndex].Mark = slMark;
                UpdateMark();
                OnPropertyChanged(nameof(SlMark));
            }
        }
        private void UpdateMark()
        {
            string strDatabaseQuery = "update Image set ImageMark = " + ImageList[CurrentIndex].Mark + " where id = " + ImageList[CurrentIndex].Id;

            model.UpdateMarkToDataBase(strDatabaseQuery);           
        }
    }
}
