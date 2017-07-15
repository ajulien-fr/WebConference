using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace WebConferenceClient.Models
{
    public class OutputItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private TextBlock _time;
        public TextBlock Time
        {
            get { return _time; }
            set
            {
                _time = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Time"));
            }
        }

        private TextBlock _username;
        public TextBlock Username
        {
            get { return _username; }
            set
            {
                _username = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Username"));
            }
        }

        private TextBlock _content;
        public TextBlock Content
        {
            get { return _content; }
            set
            {
                _content = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Content"));
            }
        }
    }
}
