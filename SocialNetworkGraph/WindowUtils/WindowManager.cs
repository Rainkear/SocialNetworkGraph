﻿using SocialNetworkGraph.Utilities;
using SocialNetworkGraph.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;

namespace SocialNetworkGraph.WindowUtils
{
    public sealed class WindowManager
    {
        private Dictionary<BaseViewModel, Window> _windows;
        private static readonly Lazy<WindowManager> lazy =
                new Lazy<WindowManager>(() => new WindowManager());
        public static WindowManager Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        public WindowManager()
        {
            _windows = new Dictionary<BaseViewModel, Window>();
        }

        public void OpenWindow(BaseViewModel context)
        {
            if (!_windows.ContainsKey(context))
            {
                PersonWindow window = new PersonWindow();
                window.DataContext = context;
                window.Show();
                _windows.Add(context, window);
            }
        }

        public void CloseWindow(BaseViewModel context)
        {
            if (_windows.ContainsKey(context))
            {
                _windows[context].Close();
                _windows.Remove(context);
            }
        }

        public void CloseAllWindows()
        {
            foreach (var window in _windows)
            {
                window.Value.Close();
            }
            _windows.Clear();
        }

        public void ChangeStateAllWindows(object minimize)
        {
            WindowState state;
            if (minimize is WindowState)
            {
                state = (WindowState)minimize;
                //Prevent child windows maximization 
                if (state == WindowState.Maximized) state = WindowState.Normal; 
            }
            else
            {
                ExceptionLogger.Instance.LogFile("Warning: cannot minimize/normalize child windows, skip!");
                return;
            }
            foreach (var window in _windows)
            {
                window.Value.WindowState = state;
            }
        }
    }
}
