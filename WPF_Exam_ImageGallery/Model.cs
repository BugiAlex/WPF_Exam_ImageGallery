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
    class Model: IModel
    {
        public void AddAccountToDataBase(string nameAdd, string surnameAdd, string loginAdd, string passwordAdd)
        {
            
                SqlConnection connect = new SqlConnection();
                SqlCommand cmd = null;
                SqlTransaction trans = null;
                try
                {
                    connect.ConnectionString = @"Initial Catalog=ImageGallery;Data Source=DESKTOP-OU59HQH\DATABASEB;Integrated Security=SSPI";
                    connect.Open();
                    trans = connect.BeginTransaction();
                    cmd = connect.CreateCommand();

                    cmd.Connection = connect;
                    cmd.Transaction = trans;

                    cmd.CommandText = "insert into Accounts (Name, Surname, Login, Password) values (\'" + nameAdd + "\',\'" + surnameAdd + "\',\'" + loginAdd + "\',\'" + passwordAdd + "\')";
                    cmd.ExecuteNonQuery();
                    trans.Commit();
                    ExceptionWindow exc = new ExceptionWindow("Your account has been successfully created!");
                    exc.Show();

                }
                catch (Exception ex)
                {
                    StreamWriter sw = new StreamWriter("../../Exception.txt", false);
                    string line = ex.ToString();
                    sw.WriteLine(line);
                    sw.Close();
                }
                finally
                {
                    cmd.Dispose();
                    connect.Close();
                }

            }
        public void AddImageToDateBase(string fileName, string activeAccount, string fileFullName, string dateNow)
        {
            try
            {

                SqlConnection connect = new SqlConnection();
                SqlCommand cmd = null;
                SqlTransaction trans = null;

                connect.ConnectionString = @"Initial Catalog=ImageGallery;Data Source=DESKTOP-OU59HQH\DATABASEB;Integrated Security=SSPI";
                connect.Open();
                trans = connect.BeginTransaction();
                cmd = connect.CreateCommand();

                cmd.Connection = connect;
                cmd.Transaction = trans;

                cmd.CommandText = "insert into Image (FileName, LoadAuthor,  ImageMark, ImagePath, DateLoad) values ('" + fileName + "', '" + activeAccount + "',  " + 1 + ", '" + fileFullName + "','" + dateNow + "' )";
                cmd.ExecuteNonQuery();
                trans.Commit();

            }
            catch (Exception ex)
            {
                StreamWriter sw = new StreamWriter("../../Exception.txt", false);
                string line = ex.ToString();
                sw.WriteLine(line);
                sw.Close();
            }
        }
        public void UpdateMarkToDataBase(string strDatabaseQuery)
        {
            SqlConnection connect = new SqlConnection();
            SqlCommand cmd = null;
            SqlTransaction trans = null;
            try
            {
                connect.ConnectionString = @"Initial Catalog=ImageGallery;Data Source=DESKTOP-OU59HQH\DATABASEB;Integrated Security=SSPI";
                connect.Open();
                trans = connect.BeginTransaction();
                cmd = connect.CreateCommand();

                cmd.Connection = connect;
                cmd.Transaction = trans;

                cmd.CommandText = strDatabaseQuery;
                cmd.ExecuteNonQuery();
                trans.Commit();

            }
            catch (Exception ex)
            {
                StreamWriter sw = new StreamWriter("../../Exception.txt", false);
                string line = ex.ToString();
                sw.WriteLine(line);
                sw.Close();
                trans.Rollback();
            }
            finally
            {
                cmd.Dispose();
                connect.Close();
            }

        }
        public void LoadAccountFromDataBase(List<AccountViewModel> AccountsList) 
        {
            SqlConnection connect = new SqlConnection();
            SqlCommand command = new SqlCommand();

            try
            {
                AccountsList.Clear();
                connect.ConnectionString = @"Initial Catalog=ImageGallery;Data Source=DESKTOP-OU59HQH\DATABASEB;Integrated Security=SSPI";
                connect.Open();
                command.Connection = connect;
                command.CommandText = "select * from Accounts";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Account tmpA = new Account((string)reader[3], (string)reader[4], (string)reader[1], (string)reader[2]);
                    AccountViewModel tmpAVM = new AccountViewModel(tmpA);
                    AccountsList.Add(tmpAVM);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                StreamWriter sw = new StreamWriter("../../Exception.txt", false);
                string line = ex.ToString();
                sw.WriteLine(line);
                sw.Close();
            }
            finally
            {
                command.Dispose();
                connect.Close();
            }
        }
        public void LoadImagesFromDataDase(ObservableCollection<ImageVeiwModel> ImageList, ref int CountImages,ref int CurrentIndex)
        {
            SqlConnection connect = new SqlConnection();
            SqlCommand command = new SqlCommand();

            try
            {

                ImageList.Clear();
                CountImages = -1;

                connect.ConnectionString = @"Initial Catalog=ImageGallery;Data Source=DESKTOP-OU59HQH\DATABASEB;Integrated Security=SSPI";
                connect.Open();
                command.Connection = connect;
                command.CommandText = "select * from Image";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Image tmpA = new Image((int)reader[0], (string)reader[1], (string)reader[4], (string)reader[2], (string)reader[5], (int)reader[3]);
                    ImageVeiwModel tmpIVM = new ImageVeiwModel(tmpA);
                    ImageList.Add(tmpIVM);

                    CountImages++;
                    CurrentIndex = CountImages;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                StreamWriter sw = new StreamWriter("../../Exception.txt", false);
                string line = ex.ToString();
                sw.WriteLine(line);
                sw.Close();
            }

            finally
            {
                command.Dispose();
                connect.Close();
            }
        }
    }
}
