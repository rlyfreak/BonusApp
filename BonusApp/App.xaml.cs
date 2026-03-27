using System;
using System.Collections.Generic;
using System.Text;

namespace BonusApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}
