using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AutoPublisherv2.Helpers
{
    public class INotifyMe : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
