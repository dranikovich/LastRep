using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using Button = System.Windows.Controls.Button;
using MessageBox = System.Windows.MessageBox;
using TextBox = System.Windows.Controls.TextBox;
using System.Data.Entity.Migrations;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;

namespace LanguageAcademyPraktik.Windows
{
    /// <summary>
    /// Логика взаимодействия для Services.xaml
    /// </summary>
    public partial class Services : Page
    {
        List<Service> ServiswList1 = Classes.Base.DBD.Service.ToList();
        List<Service> ServiswList = new List<Service>();
        int doID; int newId; int i = -1; DateTime DT; string CuPat = Environment.CurrentDirectory;
        public Services()
        {
            InitializeComponent();
            FilteredCount.Text = Convert.ToString(ServiswList.Count);
            CurrentCount.Text = Convert.ToString(ServiswList1.Count);
            ServiswList = ServiswList1;
            DGServises.ItemsSource = ServiswList;
        }
        //Инициализация компонентов DataTemplate`
        private void MediaElement_Initialized(object sender, EventArgs e)
        {
            if (i < ServiswList.Count)
            {
                i++;
                MediaElement ME = (MediaElement)sender;
                Service S = ServiswList[i];
                string OPat = CuPat.Substring(0, CuPat.Length - 9) + S.MainImagePath;
                Uri U = new Uri(OPat, UriKind.RelativeOrAbsolute);
                ME.Source = U;
            }
        }
        private void TextBlock_Initialized(object sender, EventArgs e)
        {
            if (i < ServiswList.Count)
            {
                TextBlock TB = (TextBlock)sender;
                Service S = ServiswList[i];
                TB.Text = S.Title;
            }
        }
        private void StackPanel_Initialized(object sender, EventArgs e)
        {
            if (i < ServiswList.Count)
            {
                StackPanel SP = (StackPanel)sender;
                Service S = ServiswList[i];
                if (S.Discount != 0)
                {
                    SP.Background = new SolidColorBrush(Color.FromRgb(231, 250, 191));
                }
            }
        }
        private void TextBlock_Initialized_1(object sender, EventArgs e)
        {
            if (i < ServiswList.Count)
            {
                TextBlock TB = (TextBlock)sender;
                Service S = ServiswList[i];
                if (S.Discount > 0)
                {
                    TB.Text = Convert.ToInt32(S.Cost) + " ";
                }
            }
        }
        private void TextBlock_Initialized_2(object sender, EventArgs e)
        {
            TextBlock TB = (TextBlock)sender;
            Service S = ServiswList[i];
            double x = Convert.ToDouble(S.Discount);
            if (S.Discount > 0)
            {
                TB.Text = "* скидка " + x * 100 + "%";
            }
        }
        private void TextBlock_Initialized_3(object sender, EventArgs e)
        {
            if (i < ServiswList.Count)
            {
                TextBlock TB = (TextBlock)sender;
                Service S = ServiswList[i];
                double x = Convert.ToDouble(S.Discount);
                if (S.Discount == 0)
                {
                    TB.Text = Convert.ToString(Convert.ToInt32(S.Cost) + " рупий за " + S.DurationInSeconds / 60 + " мин.");
                }
                if (S.Discount > 0)
                {
                    TB.Text = Convert.ToString(Convert.ToInt32(S.Cost) - (Convert.ToInt32(S.Cost) * x)) + " рупий за " + S.DurationInSeconds / 60 + " мин.";
                }
            }
        }
        private void StackPanel_Initialized_1(object sender, EventArgs e)
        {
            if (i < ServiswList.Count)
            {
                StackPanel SP = (StackPanel)sender;
                Service S = ServiswList[i];
                if (S.Discount != 0)
                {
                    SP.Background = new SolidColorBrush(Color.FromRgb(231, 250, 191));
                }
            }
        }


        //редактирование услуги
        private void BRed_Initialized(object sender, EventArgs e)
        {
            Button BtnRed = (Button)sender;
            if (BtnRed != null)
            {
                BtnRed.Uid = Convert.ToString(i);
            }
        }
        private void BRed_Click(object sender, RoutedEventArgs e)
        {
            doID = 0;
            Button BtnRed = (Button)sender;
            int ind = Convert.ToInt32(BtnRed.Uid);
            Service S = ServiswList[ind];
            NewGrid.Visibility = Visibility.Collapsed;
            RedGrid.Visibility = Visibility.Visible;
            GButtons.Visibility = Visibility.Collapsed;
            GRHeight.Height = new GridLength(160, GridUnitType.Pixel);
            id.Visibility = Visibility.Visible;
            TBId.Visibility = Visibility.Visible;
            LogoIm.Visibility = Visibility.Collapsed;
            BRedInside.Content = "Изменить";
            int c = S.MainImagePath.IndexOf("У");
            id.Text = S.ID + "";
            title.Text = S.Title + "";
            Cost.Text = Convert.ToInt32(S.Cost) + "";
            Duration.Text = S.DurationInSeconds / 60 + "";
            Description.Text = S.Description + "";
            Discount.Text = S.Discount * 100 + "";
            Path.Text = S.MainImagePath.Substring(c);
        }


        //новый заказ
        private void BNew_Initialized(object sender, EventArgs e)
        {
            Button buttonNew = (Button)sender;
            if (buttonNew != null)
            {
                buttonNew.Uid = Convert.ToString(i);
            }
        }
        private void BNew_Click(object sender, RoutedEventArgs e)
        {
            Button buttonNew = (Button)sender;
            newId = Convert.ToInt32(buttonNew.Uid);
            csCBc.ItemsSource = Classes.Base.DBD.Client.ToList();
            csCBc.SelectedValuePath = "ID";
            csCBc.DisplayMemberPath = "FSP";
            csTBs.Text = ServiswList[newId].Title;
            csTBdur.Text = " " + ServiswList[newId].DurationInSeconds / 60 + " мин.";
            NewGrid.Visibility = Visibility.Visible;
            RedGrid.Visibility = Visibility.Collapsed;
            GButtons.Visibility = Visibility.Collapsed;
            LogoIm.Visibility = Visibility.Collapsed;
            GRHeight.Height = new GridLength(160, GridUnitType.Pixel);
            csBRedInside.IsEnabled = false;
        }
        private void csBBack_Click(object sender, RoutedEventArgs e)
        {
            NewGrid.Visibility = Visibility.Collapsed;
            GButtons.Visibility = Visibility.Visible;
            LogoIm.Visibility = Visibility.Visible;
            GRHeight.Height = new GridLength(60, GridUnitType.Pixel);
            csTBt.Text = "";
            csDP.SelectedDate = null;
            csTBt.Visibility = Visibility.Collapsed;
        }
        TimeSpan asq;
        private void csDP_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            csTBt.Text = "";
            DT = Convert.ToDateTime(csDP.SelectedDate);
            if (DT >= DateTime.Now)
            {
                csTBt.Visibility = Visibility.Visible;
            }
            if (DT < DateTime.Now)
            {
                csTBt.Text = "";
                csTBt.Visibility = Visibility.Collapsed;
            }
        }
        private void csTBt_TextChanged(object sender, TextChangedEventArgs e)
        {
            csBRedInside.IsEnabled = false;
            Regex r1 = new Regex("[0-1][0-9]:[0-5][0-9]");
            Regex r2 = new Regex("[2][0-3]:[0-5][0-9]");
            try
            {
                if ((r1.IsMatch(csTBt.Text)) || (r2.IsMatch(csTBt.Text)) && csTBt.Text.Length == 5)
                {
                    TimeSpan TS = TimeSpan.Parse(csTBt.Text);
                    DT = DT.Add(TS);
                    if (DT > DateTime.Now && csTBt.Text != "")
                    {
                        csBRedInside.IsEnabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Поздно");
                    }
                }
            }catch { }
        }
        private void csBRedInside_Click(object sender, RoutedEventArgs e)
        {
            Service S = ServiswList[newId];
            ClientService obj = new ClientService()
            {
                ClientID = Convert.ToInt32(csCBc.SelectedValue),
                ServiceID = S.ID,
                StartTime = DT,
            };
            DialogResult dialogResult = (DialogResult)MessageBox.Show("Добавить запись?", "Предупреждение!!!", (MessageBoxButton)MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string a = ((S.DurationInSeconds / 60 - S.DurationInSeconds / 60 % 60) / 60) + ":" + S.DurationInSeconds / 60 % 60;
                asq = TimeSpan.Parse(a);
                MessageBox.Show("Занятие начнется в: " + DT.Add(asq));
                Classes.Base.DBD.ClientService.Add(obj);
                Classes.Base.DBD.SaveChanges();
                csTBt.Text = "";
                csDP.SelectedDate = null;
                csTBt.Visibility = Visibility.Collapsed;
            }
            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("Запись не добавлена");
            }
        }


        //удаление заказа
        private void BDel_Initialized(object sender, EventArgs e)
        {
            Button buttonDel = (Button)sender;
            if (buttonDel != null)
            {
                buttonDel.Uid = Convert.ToString(i);
            }
        }
        private void BDel_Click(object sender, RoutedEventArgs e)
        {
            Button buttonDel = (Button)sender;
            int ind = Convert.ToInt32(buttonDel.Uid);
            DialogResult dialogResult = (DialogResult)MessageBox.Show("Предупреждение!!!", "Удалить запись", (MessageBoxButton)MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Service S = ServiswList[ind];
                Classes.Base.DBD.Service.Remove(S);
                MessageBox.Show("Удалена");
                Classes.Base.DBD.SaveChanges();
                Classes.MainFrame.MFrame.Navigate(new Windows.Services());
            }
            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("Запись не удалена");
            }
        }


        //кнопка редактирования и добавления заказа
        private void BRedInside_Click(object sender, RoutedEventArgs e)
        {
            int time = Convert.ToInt32(Duration.Text) * 60;
            string tit = title.Text;
            Service obj = new Service()
            { ID = Convert.ToInt32(id.Text), Title = title.Text, Cost = Convert.ToInt32(Cost.Text), DurationInSeconds = Convert.ToInt32(Duration.Text) * 60, Description = Description.Text, Discount = Convert.ToDouble(Discount.Text) / 100, MainImagePath = Path.Text };
            switch (doID)
            {
                case 0:
                    try
                    {
                        DialogResult dialogResult = (DialogResult)MessageBox.Show("Изменить запись?", "Предупреждение!!!", (MessageBoxButton)MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            if (time <= 4* 60 * 60)
                            {
                                Classes.Base.DBD.Service.AddOrUpdate(obj);
                                Classes.Base.DBD.SaveChanges();
                                MessageBox.Show("Запись изменена");
                                i = -1;
                                ServiswList = Classes.Base.DBD.Service.ToList();
                                DGServises.ItemsSource = ServiswList;
                            }
                            if (time >= 4 * 60 * 60)
                            {
                                MessageBox.Show("Более 4 часов нельзя");
                            }
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            MessageBox.Show("Запись не изменена");
                        }
                    }
                    catch{
                        MessageBox.Show("Неверный формат");
                    }break;
                case 1:
                    BRedInside.Content = "Добавить";
                    try
                    {
                        DialogResult dialogResult = (DialogResult)MessageBox.Show("Вы хотите добавить новую услугу?", "Предупреждение!!!", (MessageBoxButton)MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            if (time <= 4 * 60 * 60 && ServiswList1.Where(x => x.Title == tit).ToList().Count == 0)
                            {
                                Classes.Base.DBD.Service.Add(obj);
                                Classes.Base.DBD.SaveChanges();
                                MessageBox.Show("Запись добавлена");
                                i = -1;
                                ServiswList = Classes.Base.DBD.Service.ToList();
                                DGServises.ItemsSource = ServiswList;
                            }
                            if (time >= 4 * 60 * 60)
                            {
                                MessageBox.Show("Более 4 часов нельзя");
                            }
                            if (ServiswList1.Where(x => x.Title == tit).ToList().Count > 0)
                            {
                                MessageBox.Show("Такая услуга уже существует");
                            }
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            MessageBox.Show("Запись не добавлена");
                        }
                    }catch{
                        MessageBox.Show("Неверный формат");
                    }break;
            }
        }


        //новый сервис
        private void Path_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog OFD = new Microsoft.Win32.OpenFileDialog();
            OFD.ShowDialog();
            string p = OFD.FileName;
            int c = p.IndexOf("У");
            Path.Text = p.Substring(c);
        }
        private void BBack_Click(object sender, RoutedEventArgs e)
        {
            LogoIm.Visibility = Visibility.Visible;
            RedGrid.Visibility = Visibility.Collapsed;
            GButtons.Visibility = Visibility.Visible;
            GRHeight.Height = new GridLength(60, GridUnitType.Pixel);
        }
        private void BNewService_Click(object sender, RoutedEventArgs e)
        {
            BRedInside.Content = "Добавить";
            id.Visibility = Visibility.Collapsed;
            TBId.Visibility = Visibility.Collapsed;
            doID = 1;
            LogoIm.Visibility = Visibility.Collapsed;
            NewGrid.Visibility = Visibility.Collapsed;
            RedGrid.Visibility = Visibility.Visible;
            GButtons.Visibility = Visibility.Collapsed;
            GRHeight.Height = new GridLength(160, GridUnitType.Pixel);
        }


        //фильтры
        private void BFilters_Click(object sender, RoutedEventArgs e)
        {
            GButtons.Visibility = Visibility.Collapsed;
            GFilter.Visibility = Visibility.Visible;
            GRHeight.Height = new GridLength(160, GridUnitType.Pixel);
        }
        private void SortDown_Click(object sender, RoutedEventArgs e)
        {
            i = -1;
            ServiswList.Sort((x, y) => Convert.ToInt32(Convert.ToDouble(x.Cost) - x.Discount * Convert.ToDouble(x.Cost)).CompareTo(Convert.ToInt32(Convert.ToDouble(y.Cost) - y.Discount * Convert.ToDouble(y.Cost))));
            ServiswList.Reverse();
            DGServises.Items.Refresh();
        }
        private void SortUp_Click(object sender, RoutedEventArgs e)
        {
            i = -1;
            ServiswList.Sort((x, y) => Convert.ToInt32(Convert.ToDouble(x.Cost) - x.Discount * Convert.ToDouble(x.Cost)).CompareTo(Convert.ToInt32(Convert.ToDouble(y.Cost) - y.Discount * Convert.ToDouble(y.Cost))));
            DGServises.Items.Refresh();
        }
        List<Service> ServiseListFilter = new List<Service>();
        private void Filter_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            i = -1;
            switch (Filter.SelectedIndex)
            {
                case 0:
                    ServiseListFilter = ServiswList1.Where(x => x.Discount < 0.05 && x.Discount >= 0).ToList();
                    ServiswList = ServiseListFilter;
                    DGServises.ItemsSource = ServiswList;
                    break;
                case 1:
                    ServiseListFilter = ServiswList1.Where(x => x.Discount < 0.15 && x.Discount >= 0.05 ).ToList();
                    ServiswList = ServiseListFilter;
                    DGServises.ItemsSource = ServiswList;
                    break;
                case 2:
                    ServiseListFilter = ServiswList1.Where(x => x.Discount < 0.30 && x.Discount >= 0.15 ).ToList();
                    ServiswList = ServiseListFilter;
                    DGServises.ItemsSource = ServiswList;
                    break;
                case 3:
                    ServiseListFilter = ServiswList1.Where(x => x.Discount < 0.7 && x.Discount >= 0.3 ).ToList();
                    ServiswList = ServiseListFilter;
                    DGServises.ItemsSource = ServiswList;
                    break;
                case 4:
                    ServiseListFilter = ServiswList1.Where(x => x.Discount < 1 && x.Discount >= 0.7 ).ToList();
                    ServiswList = ServiseListFilter;
                    DGServises.ItemsSource = ServiswList;
                    break;
                case 5:
                    ServiseListFilter = ServiswList1.Where(x => x.Discount < 1 && x.Discount >= 0 ).ToList();
                    ServiswList = ServiseListFilter;
                    DGServises.ItemsSource = ServiswList;
                    break;
            }
            FilteredCount.Text = Convert.ToString(ServiswList.Count);
            CurrentCount.Text = Convert.ToString(ServiswList1.Count);
        }
        private void TBPoisk_TextChanged(object sender, TextChangedEventArgs e)
        {
            i = -1;
            if (TBPoisk.Text != "")
            {
                List<Service> ServiseListPoisk = new List<Service>();
                ServiseListPoisk = ServiswList.Where(x => x.Title.Contains(TBPoisk.Text)).ToList();
                ServiswList = ServiseListPoisk;
                DGServises.ItemsSource = ServiswList;
                FilteredCount.Text = Convert.ToString(ServiswList.Count);
                CurrentCount.Text = Convert.ToString(ServiswList1.Count);
            }
            else
            {
                if (ServiseListFilter.Count == 0)
                {
                    ServiswList = ServiswList1;
                    DGServises.ItemsSource = ServiswList;
                    DGServises.Items.Refresh();
                }
                else
                {
                    ServiswList = ServiseListFilter;
                    DGServises.ItemsSource = ServiswList;
                    DGServises.Items.Refresh();
                }
            }
        }
        private void Bdel_Click_1(object sender, RoutedEventArgs e)
        {
            i = -1;
            ServiswList = ServiswList1;
            DGServises.ItemsSource = ServiswList;
            DGServises.Items.Refresh();
        }
        private void BFBack_Click(object sender, RoutedEventArgs e)
        {
            GButtons.Visibility = Visibility.Visible;
            GFilter.Visibility = Visibility.Collapsed;
            GRHeight.Height = new GridLength(60, GridUnitType.Pixel);
        }
    }
}