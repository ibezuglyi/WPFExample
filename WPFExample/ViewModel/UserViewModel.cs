using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using WPFExample.Model;
using System.ComponentModel;
using System.Linq.Expressions;

namespace WPFExample.ViewModel
{
    public class UserViewModel : INotifyPropertyChanged
    {

        private User user;
        public User User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged(() => User);
            }
        }

        public bool IsNotUserLoggedIn
        {
            get
            {
                return !IsUserLoggedIn;
            }
        }

        private bool isUserLoggedIn;
        public bool IsUserLoggedIn
        {
            get { return isUserLoggedIn; }
            set
            {
                isUserLoggedIn = value;
                OnPropertyChanged(() => IsUserLoggedIn);
            }
        }

        private string login;
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged(() => Login);
                OnPropertyChanged(() => CanExecuteCommand);
            }
        }

        private string pssword;
        public string Password
        {
            get { return pssword; }
            set
            {
                pssword = value;
                OnPropertyChanged(() => Password);
                OnPropertyChanged(() => CanExecuteCommand);
            }
        }
        public ICommand LoginCommand { get; set; }
        public UserViewModel()
        {
            IsUserLoggedIn = false;
            OnPropertyChanged(() => IsNotUserLoggedIn);
            LoginCommand = new Command(new Action(DoLogin));
        }

        public void DoLogin()
        {
            User = UserRepository.Instance.GetUserByLoginAndPassword(Login, Password);
            IsUserLoggedIn = User != null;
            OnPropertyChanged(() => IsNotUserLoggedIn);

        }
        public bool CanExecuteCommand
        {
            get
            {
                return !string.IsNullOrEmpty(Login) && string.IsNullOrEmpty(Password);
            }
        }
        protected void OnPropertyChanged<T>(Expression<Func<T>> action)
        {
            var propertyName = GetPropertyName(action);
            OnPropertyChanged(propertyName);
        }

        private static string GetPropertyName<T>(Expression<Func<T>> action)
        {
            var expression = (MemberExpression)action.Body;
            var propertyName = expression.Member.Name;
            return propertyName;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
