using HandyControl.Controls;
using HandyControl.Data;
using System.ComponentModel;
using System.Windows;

namespace CsharpWpf.chapter1
{
    internal class MyRate : Rate
    {
        public static readonly RoutedEvent RateChangedEvent =
           EventManager.RegisterRoutedEvent("RateChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(MyRate));

        [Category("Behavior")]
        public event RoutedEventHandler RateChangedChanged
        {
            // += add 会被调用
            add
            {
                AddHandler(RateChangedEvent, value);
            }
            // -= remove 会被调用
            remove
            {
                RemoveHandler(RateChangedEvent, value);
            }
        }

        public void OnRateChanged()
        {
            RoutedEventArgs e = new RoutedEventArgs(RateChangedEvent, this);
            RaiseEvent(e);
            UpdateItems();
        }
    }
}
