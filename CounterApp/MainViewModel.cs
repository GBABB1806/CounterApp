// MainViewModel.cs
using System.ComponentModel;

public class MainViewModel : INotifyPropertyChanged
{
    private string _labelValue;

    public string LabelValue
    {
        get { return _labelValue; }
        set
        {
            if (_labelValue != value)
            {
                _labelValue = value;
                OnPropertyChanged(nameof(LabelValue));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
