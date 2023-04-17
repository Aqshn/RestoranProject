using Restoran7.Models;
using Restoran7.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Restoran7.Views
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window, INotifyPropertyChanged
    {
        public List<Garson> garsons;
        public string _name;
        private string _surname;
        private string _password;
        public BTn1 bTn1;
        public BTn2 bTn2;
        public BTn3 bTn3;
        public BTn4 bTn4;
        public BTn5 bTn5;
        public BTn6 bTn6;

        public void B1(BTn1 button)
        {
            bTn1=button;    
        }
        public void B2(BTn2 button)
        {
            bTn2 = button;
        }
        public void B3(BTn3 button)
        {
            bTn3 = button;
        }
        public void B4(BTn4 button)
        {
            bTn4 = button;
        }
        public void B5(BTn5 button)
        {
            bTn5 = button;
        }
        public void B6(BTn6 button)
        {
            bTn6 = button;
        }
        public Window2()
        {
            InitializeComponent();
            this.DataContext = this;
            garsons = new List<Garson>
            {
                new Garson{Name = "1",Surname = "1",Password="1"},
                new Garson{Name = "Ramiz",Surname = "Eliyev",Password="Rt5n_7bv"},
                new Garson{Name = "Vaqif",Surname = "Babayev",Password="mnY2_Tp3"},
                new Garson{Name = "Saddam",Surname = "Huseyn",Password="Wl.c4Kjh"}
            };
        }
        public string _Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Surname
        {
            get { return _surname; }
            set
            {
                if (_surname != value)
                {
                    _surname = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            if (passwordBox != null)
            {
                Window2 viewModel = DataContext as Window2;
                if (viewModel != null)
                {
                    viewModel.Password = passwordBox.Password;
                }
            }
        }

        private ICommand _loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                return _loginCommand = new PasswordCommand(param => this.Login(param), param => this.CanLogin());
            }
        }

        private bool CanLogin()
        {
            return !string.IsNullOrEmpty(_Name) && !string.IsNullOrEmpty(Surname) && !string.IsNullOrEmpty(Password);
        }

        private void Login(object parameter)
        {
            for (int i = 0; i < garsons.Count; i++)
            {
                if (_Name == garsons[i].Name && Surname == garsons[i].Surname && Password == garsons[i].Password)
                {
                    MessageBox.Show("Welcome To Menu!");
                    MenuWindow Window = new MenuWindow();
                    if (bTn1 != null)
                    {
                        BTn1.GetInstance().garson = garsons[i];
                        Window.B1(BTn1.GetInstance());
                    }
                    else if (bTn2 != null)
                    {
                        BTn2.GetInstance().garson = garsons[i];
                        Window.B2(BTn2.GetInstance());
                    }
                    else if (bTn3 != null)
                    {
                        BTn3.GetInstance().garson = garsons[i];
                        Window.B3(BTn3.GetInstance());
                    }
                    else if (bTn4 != null)
                    {
                        BTn4.GetInstance().garson = garsons[i];
                        Window.B4(BTn4.GetInstance());
                    }
                    else if (bTn5 != null)
                    {
                        BTn5.GetInstance().garson = garsons[i];
                        Window.B5(BTn5.GetInstance());
                    }
                    else if (bTn6 != null)
                    {
                        BTn6.GetInstance().garson = garsons[i];
                        Window.B6(BTn6.GetInstance());
                    }
                    Window.Show();
                    Close();
                    return;
                }
            }
            MessageBox.Show("Name or Surname or Password  not correct!");

        }



        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class PasswordCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public PasswordCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
