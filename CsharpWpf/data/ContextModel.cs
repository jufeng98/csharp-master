using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;

namespace CsharpWpf.data
{
    internal class ContextModel : DependencyObject, INotifyPropertyChanged
    {
        public static string welcome = "My first Wpf Application!";
        public static string Welcome1 { get; } = "1My first Wpf Application!";

        public int SliderValue { get; set; } = 30;

        public List<Human> Humans { get; set; } = new List<Human>() {
        new Human(1,"jack"),
        new Human(2,"rose"),
        new Human(3,"bill")
        };

        public event PropertyChangedEventHandler PropertyChanged;

        //声明并定义路由事件
        public static readonly RoutedEvent NameChangedEvent = EventManager.RegisterRoutedEvent
        ("NameChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ContextModel));
        public string Name { get; set; }

        //为界面元素添加路由事件侦听
        public static void AddNameChangedHandler(DependencyObject d, RoutedEventHandler h)
        {
            UIElement e = d as UIElement;
            e?.AddHandler(NameChangedEvent, h);
        }

        //移除侦听
        public static void RemoveNameChangedHandler(DependencyObject d, RoutedEventHandler h)
        {
            UIElement e = d as UIElement;
            e?.RemoveHandler(NameChangedEvent, h);
        }

    }
}
