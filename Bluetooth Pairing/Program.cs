using System;
using InTheHand.Net.Sockets;
using System.Collections.Generic;
using InTheHand.Net;
using InTheHand.Net.Bluetooth;

namespace Bluetooth_Pair
{
    static class Program
    {
        private static Guid guid;                           // Globally Unique IDentifier for device.
        private static List<BluetoothDeviceInfo> deviceList;
        private static BluetoothClient localclient;         // Client class performing connection related operation
        private static BluetoothAddress btAddres;           // Address class to hold bluetooth address wrt system
        private static BluetoothDeviceInfo[] devices;       // Class to hold all the information related a device bluetooth stack
        //private static BluetoothListener blueListener;    // server class holds oprations related server side connection
        private static BluetoothEndPoint localEndpoint;
        private static BluetoothComponent localComponent;

        static Program()
        {
            localEndpoint = new BluetoothEndPoint(btAddres, guid);
            localclient = new BluetoothClient(localEndpoint);
            //devices = client.DiscoverDevices();
            GenerateDeviceInfo();
        }
        private static void GenerateDeviceInfo()
        {
            long address = 0x38184C4B5688;
            Program.btAddres = new BluetoothAddress(address);
            Program.guid = new Guid("0000110b-0000-1000-8000-00805f9b34fb");
        }
        private static void ShowAllDevices()
        {
            foreach (BluetoothDeviceInfo d in devices)
            {
                Console.WriteLine("Device Name              : {0}", d.DeviceName);
                Console.WriteLine("Device Authrntication    : {0}", d.Authenticated);
                Console.WriteLine("Device Class             : {0}", d.ClassOfDevice);
                Console.WriteLine("Device Connection        : {0}", d.Connected);
                Console.WriteLine("Device Address           : {0}", d.DeviceAddress);
                Console.WriteLine("Device Last Used         : {0}\n", d.LastUsed);

                //Console.WriteLine("Device Version           : {0}", d.GetVersions().LmpVersion); // Requires Device in connected state
                //Console.WriteLine("Device Version           : {0}", d.GetVersions().LmpSubversion);
                //Console.WriteLine("Device Version           : {0}", d.GetVersions().LmpSupportedFeatures);
                //Console.WriteLine("Device Version           : {0}\n", d.GetVersions().Manufacturer);
            }

            Guid[] guid = devices[0].InstalledServices;
            foreach (var e in guid)
                Console.WriteLine("Devices GUID             : {0}", e.ToString());
        }

        // Scan For Devices
        private static void ScanForDevices()
        {
            deviceList = new List<BluetoothDeviceInfo>();
            localComponent = new BluetoothComponent(localclient);
            localComponent.DiscoverDevicesAsync(255, true, true, true, true, null);
            localComponent.DiscoverDevicesProgress += new EventHandler<DiscoverDevicesEventArgs>(ComponentDiscoverDevicesProgress);
            localComponent.DiscoverDevicesComplete += new EventHandler<DiscoverDevicesEventArgs>(ComponentDiscoverDevicesComplete);
        }
        private static void ComponentDiscoverDevicesProgress(object sender, DiscoverDevicesEventArgs e)
        {
            // log and save all found devices
            for (int i = 0; i < e.Devices.Length; i++)
            {
                if (e.Devices[i].Remembered)
                {
                    //Print(e.Devices[i].DeviceName + " (" + e.Devices[i].DeviceAddress + "): Device is known");
                }
                else
                {
                    //Print(e.Devices[i].DeviceName + " (" + e.Devices[i].DeviceAddress + "): Device is unknown");
                }
                deviceList.Add(e.Devices[i]);
            }
        }
        private static void ComponentDiscoverDevicesComplete(object sender, DiscoverDevicesEventArgs e)
        {
            // log some stuff
        }

        // Pair Device
        private static void PairDevice()
        {
            bool isPaired = BluetoothSecurity.PairRequest(btAddres, "");

            if (!isPaired)
            {
                ScanForDevices();
                foreach (BluetoothDeviceInfo device in Program.deviceList)
                    if (device.DeviceName == "WH - 1000XM3")
                        isPaired = BluetoothSecurity.PairRequest(device.DeviceAddress,"");
            }

            if (!isPaired)
            { /*Do something*/ }
        }

        // Connect Device
        private static void ConnectToDevice()
        {
            //if (device.Authenticated)
            //{
                //client.Connect(btAddres, Program.guid);
                localclient.BeginConnect(btAddres, Program.guid, new AsyncCallback(BluetoothClientConnectCallback), localclient);
            //}
        }
        private static void BluetoothClientConnectCallback(IAsyncResult ar)
        {
            try
            {
                var bluetoothClient = ar.AsyncState as BluetoothClient;
                bluetoothClient.EndConnect(ar);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        // Remove Connected Device
        private static void RemoveConnecteDevice()
        {
            bool isRemoved = BluetoothSecurity.RemoveDevice(btAddres);

            if(!isRemoved)
                foreach(var device in deviceList)
                    if(device.DeviceName == "WH-1000XM3" && (device.Connected == true || device.Remembered == true))
                        isRemoved = BluetoothSecurity.RemoveDevice(device.DeviceAddress);

            if (!isRemoved)
            { /*Do Something*/}
        }


        static void Main()
        {
            //Program.ShowAllDevices();
            //Program.GenerateDeviceInfo();

            try
            {
                Console.WriteLine("Enter Operation");
                Console.WriteLine("1. Connect BT");
                Console.WriteLine("2. Remove and Pair BT");
                Console.WriteLine("3. Remove BT Device");

                int choice = 0;
                choice = Convert.ToInt32(Console.ReadLine());

                if (choice == 1)
                    ConnectToDevice();
                else if (choice == 2)
                {
                    RemoveConnecteDevice();
                    PairDevice();
                }
                else if (choice == 3)
                    RemoveConnecteDevice();
                else
                    Console.WriteLine("Invalid Input");


                Console.WriteLine("\nEnd");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
               /*while (true)
                    if (localclient.Connected)
                        break;*/

                Console.WriteLine("\nEnd");
                Console.ReadLine();
            }
        }
    }
}


// My BT Device Info.

//Device Name              : WH-1000XM3
//Device Authrntication    : True
//Device Class             : 240404
//Device Connection        : False
//Device Address           : 38184C4B5688
//Device Last Used         : 5/9/2020 7:19:09 PM

//Devices GUID             : 0000110b-0000-1000-8000-00805f9b34fb
//Devices GUID             : 0000110c-0000-1000-8000-00805f9b34fb 
//Devices GUID             : 0000110e-0000-1000-8000-00805f9b34fb 
//Devices GUID             : 0000111e-0000-1000-8000-00805f9b34fb 
//Devices GUID             : 7b265b0e-2232-4d45-bef4-bb8ae62f813d 
//Devices GUID             : 81c2e72a-0591-443e-a1ff-05f988593351
//Devices GUID             : f8d1fbe4-7966-4334-8024-ff96c9330e15

