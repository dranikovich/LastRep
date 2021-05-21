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
        List<Service> ServiswList = Classes.Base.DBD.Service.ToList();
        int doID;
        public Services()
        {
            InitializeComponent();
            DGServises.ItemsSource = ServiswList;

        }
        int i = -1;

        private void MediaElement_Initialized(object sender, EventArgs e)
        {
            if (i < ServiswList.Count)
            {
                i++;
                MediaElement ME = (MediaElement)sender;
                Service S = ServiswList[i];
                string CuPat = Environment.CurrentDirectory;
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
                //  i++;
            }
        }

        private void RedGrid_Loaded(object sender, RoutedEventArgs e)
        {
            Grid gridRed = (Grid)sender;
            if (gridRed != null)
            {
                gridRed.Uid = Convert.ToString(i);
            }
        }
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

            RedGrid.Visibility = Visibility.Visible;
            MainStack.Visibility = Visibility.Collapsed;
            BNewService.Visibility = Visibility.Collapsed;
            id.Visibility = Visibility.Visible;
            TBId.Visibility = Visibility.Visible;

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
                //  i++;
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
                //  i++;
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
        private void BNew_Initialized(object sender, EventArgs e)
        {
            Button buttonNew = (Button)sender;
            if (buttonNew != null)
            {
                buttonNew.Uid = Convert.ToString(i);
            }
        }
        int newId;
        private void BNew_Click(object sender, RoutedEventArgs e)
        {
            Button buttonNew = (Button)sender;
            newId = Convert.ToInt32(buttonNew.Uid);


            csCBc.ItemsSource = Classes.Base.DBD.Client.ToList();
            csCBc.SelectedValuePath = "ID";
            csCBc.DisplayMemberPath = "FSP";


            csTBs.Text = ServiswList[newId].Title;
            NewGrid.Visibility = Visibility.Visible;
            MainStack.Visibility = Visibility.Collapsed;
            BNewService.Visibility = Visibility.Collapsed;
            csBRedInside.IsEnabled = false;
        }

        private void csBBack_Click(object sender, RoutedEventArgs e)
        {
            NewGrid.Visibility = Visibility.Collapsed;
            MainStack.Visibility = Visibility.Visible;
            BNewService.Visibility = Visibility.Visible;
        }
        DateTime DT;

        private void csBRedInside_Click(object sender, RoutedEventArgs e)
        {
            Service S = ServiswList[newId];
            MessageBox.Show(csCBc.SelectedValue + " " + S.ID + " " + DT);
            ClientService obj = new ClientService()
            {
                ClientID = Convert.ToInt32(csCBc.SelectedValue),
                ServiceID = S.ID,
                StartTime = DT,
            };
            DialogResult dialogResult = (DialogResult)MessageBox.Show("Добавить запись?", "Предупреждение!!!", (MessageBoxButton)MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Classes.Base.DBD.ClientService.Add(obj);
                Classes.Base.DBD.SaveChanges();
            }
            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("Запись не добавлена");
            }

        }
        private void csDP_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            csTBt.Text = "";
            DT = Convert.ToDateTime(csDP.SelectedDate);
            if (DT > DateTime.Now)
            {
                csTBt.Visibility = Visibility.Visible;
            }
        }
        private void csTBt_TextChanged(object sender, TextChangedEventArgs e)
        {
            csBRedInside.IsEnabled = false;
            Regex r1 = new Regex("[0-1][0-9]:[0-5][0-9]");
            Regex r2 = new Regex("[2][0-3]:[0-5][0-9]");
            string s = "";
            if ((r1.IsMatch(csTBt.Text)) || (r2.IsMatch(csTBt.Text)) && csTBt.Text.Length == 5)
            {
                MessageBox.Show(csTBt.Text);
                TimeSpan TS = TimeSpan.Parse(csTBt.Text);
                DT = DT.Add(TS);
                if (DT > DateTime.Now)
                {
                    MessageBox.Show(DT + "");
                    csBRedInside.IsEnabled = true;
                }
                else
                {
                    MessageBox.Show("Поздно");
                }
            }
        }

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

        private void BRedInside_Click(object sender, RoutedEventArgs e)
        {
            double dis = Convert.ToDouble(Discount.Text) / 100;
            int time = Convert.ToInt32(Duration.Text) * 60;
            int cost = Convert.ToInt32(Cost.Text);
            string tit = title.Text;
            int idi = Convert.ToInt32(id.Text);
            string desc = Description.Text;
            string pat = Path.Text;
            switch (doID)
            {
                case 0:
                    try
                    {

                        DialogResult dialogResult = (DialogResult)MessageBox.Show("Изменить запись?", "Предупреждение!!!", (MessageBoxButton)MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            if (time <= 240 * 60)
                            {
                                Service obj = new Service()
                                {
                                    ID = idi,
                                    Title = tit,
                                    Cost = cost,
                                    DurationInSeconds = time,
                                    Description = desc,
                                    Discount = dis,
                                    MainImagePath = pat
                                };
                                Classes.Base.DBD.Service.AddOrUpdate(obj);
                                Classes.Base.DBD.SaveChanges();
                                MessageBox.Show("Запись изменена");
                                i = -1;
                                ServiswList = Classes.Base.DBD.Service.ToList();
                                DGServises.ItemsSource = ServiswList;
                                RedGrid.Visibility = Visibility.Collapsed;
                                MainStack.Visibility = Visibility.Visible;
                                BNewService.Visibility = Visibility.Visible;
                            }
                            if (time >= 240 * 60)
                            {
                                MessageBox.Show("Более 4 часов нельзя");
                            }
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            MessageBox.Show("Запись не изменена");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Неверный формат");
                    }
                    break;
                case 1:
                    BRedInside.Content = "Добавить";
                    try
                    {
                        DialogResult dialogResult = (DialogResult)MessageBox.Show("Вы хотите добавить новую услугу?", "Предупреждение!!!", (MessageBoxButton)MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            if (time <= 240 * 60)
                            {
                                Service obj = new Service()
                                {
                                    ID = idi,
                                    Title = tit,
                                    Cost = cost,
                                    DurationInSeconds = time,
                                    Description = desc,
                                    Discount = dis,
                                    MainImagePath = pat
                                };
                                Classes.Base.DBD.Service.Add(obj);
                                Classes.Base.DBD.SaveChanges();
                                MessageBox.Show("Запись добавлена");

                                i = -1;
                                ServiswList = Classes.Base.DBD.Service.ToList();
                                DGServises.ItemsSource = ServiswList;
                                RedGrid.Visibility = Visibility.Collapsed;
                                MainStack.Visibility = Visibility.Visible;
                                BNewService.Visibility = Visibility.Visible;
                            }
                            if (time >= 240 * 60)
                            {
                                MessageBox.Show("Более 4 часов нельзя");
                            }
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            MessageBox.Show("Запись не добавлена");
                        }
                    }
                    catch
                    {

                    }
                    break;
            }
        }

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
            RedGrid.Visibility = Visibility.Collapsed;
            MainStack.Visibility = Visibility.Visible;
            BNewService.Visibility = Visibility.Visible;
        }
        private void BNewService_Click(object sender, RoutedEventArgs e)
        {
            BRedInside.Content = "Добавить";
            id.Visibility = Visibility.Collapsed;
            TBId.Visibility = Visibility.Collapsed;
            doID = 1;
            RedGrid.Visibility = Visibility.Visible;
            MainStack.Visibility = Visibility.Collapsed;
            BNewService.Visibility = Visibility.Collapsed;
        }

        private void csCBc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int x = Convert.ToInt32(csCBc.SelectedValue);
            MessageBox.Show(x + "");
        }
    }
}