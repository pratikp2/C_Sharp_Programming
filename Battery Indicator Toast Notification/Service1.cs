using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Tulpep.NotificationWindow;

namespace Battery_Indicator_Toast_Notification
{
    //private string path = @"lkmk/kjhkj";
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //while (true)
            //{
                PopupNotifier toast = new PopupNotifier();
                toast.Image = Properties.Resources.index;
                toast.TitleText = "Remove Charger";
                toast.ContentText = "Charging above 90%, Please Remove the Charger";
                toast.Popup();
                //ChargeMonitor();
                //Thread.Sleep(100); // MilliSeconds
            //}
        }

        /*private void ChargeMonitor()
        {
            ToastNotificationCreator("90");
        }*/

        /*private void ToastNotificationCreator(string value)
        {
            PopupNotifier toast = new PopupNotifier();
            toast.Image = Properties.Resources.index;
            toast.TitleText = "Remove Charger";
            toast.ContentText = "Charging above" + value + "%, Please Remove the Charger";
            toast.Popup();
        }*/

        protected override void OnStop()
        {
        }
    }
}
