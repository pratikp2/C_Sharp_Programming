using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;

namespace Certificate
{
    class Program
    {
        private static string CertificateLocation = @"C:\Utils\hpinc.cer";
        private static StoreName storeName = StoreName.TrustedPublisher;
        private static StoreLocation storeLocation = StoreLocation.LocalMachine;
        private static string ThumbPrint = "A0BD319E4DD789E7BDEAE8E47099376BBC5BBF7E";

        public static void ListExistingCertificates(StoreName storename, StoreLocation storelocation)
        {
            X509Store store = new X509Store(storename, storelocation);
            store.Open(OpenFlags.ReadOnly);
            var list = store.Certificates.GetEnumerator();

            list.Reset();
            while (list.MoveNext())
            {
                Console.WriteLine("Friendly Name    : {0}",list.Current.SignatureAlgorithm.FriendlyName);
                Console.WriteLine("Signature Algo   : {0}", list.Current.SignatureAlgorithm.Value);

                Console.WriteLine("Subject Name     : {0}", list.Current.SubjectName.Name.ToString());
                Console.WriteLine("Serial Name      : {0}",list.Current.SerialNumber.ToString());
                Console.WriteLine("Friendly Name    : {0}", list.Current.FriendlyName.ToString());
                Console.WriteLine("Version          : {0}", list.Current.Version.ToString());
                Console.WriteLine("Thumb Print      : {0}", list.Current.Thumbprint.ToString());
            }
            store.Close();
        }
        public static void InstallCertificate(StoreName storename, StoreLocation storelocation, string file)
        {
            X509Store store = new X509Store(storename, storelocation);
            store.Open(OpenFlags.ReadWrite);
            store.Add(new X509Certificate2(X509Certificate2.CreateFromCertFile(file)));
            store.Close();
        }
        public static bool FindCertificate(StoreName storename, StoreLocation storelocation, string thumbprint)
        {
            X509Store store = new X509Store(StoreName.TrustedPublisher, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadWrite); 
            var certificates = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);

            if (certificates != null && certificates.Count > 0)   
                return true;
            
            store.Close();
            return false;
        }
        public static void RemoveCertificate()
        {
            X509Store store = new X509Store(StoreName.TrustedPublisher, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadWrite);
            var certificates = store.Certificates.Find(X509FindType.FindBySubjectName, "subjectName", false);
            //store.Remove(new X509Certificate2();
            store.Close();
        }

        static void Main()
        {
            InstallCertificate(storeName, storeLocation, CertificateLocation);
            ListExistingCertificates(storeName, storeLocation);
            FindCertificate(storeName, storeLocation, ThumbPrint);

            Console.WriteLine("Program End");
            Console.ReadLine();
        }
    }
}

