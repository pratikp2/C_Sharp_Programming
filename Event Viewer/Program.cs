using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.InteropServices;

namespace Event_Viewer
{
    class Program
    {
        // Windows Logs
        private static string ApplicationEventLog = "Application";
        private static string SecurityEventLog = "Security";
        private static string SystemEventLog = "System";


        static void ReadEvenLog(string EventLogType)
        {
            EventLog eventLog = new EventLog();
            eventLog.Log = EventLogType;

            foreach (EventLogEntry log in eventLog.Entries)
                Console.WriteLine("{0}\n", log.Message);       
        }
        static void WriteEventLog(string EventLogType)
        {
            using (EventLog eventLog = new EventLog(EventLogType))
            {
                eventLog.Source = EventLogType;
                eventLog.WriteEntry("Test Log message", EventLogEntryType.Information, 101, 1);
            }
        }
        static void cleanEventLogs(string EventLogType)
        {
            try
            {
                EventLog eventLog = new EventLog();
                eventLog.Log = EventLogType;
                eventLog.Clear();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
        static void CreateCustomSourceEntry()
        {
            if (!EventLog.SourceExists("MySource"))
            {
                //EventLog.DeleteEventSource("MySource");
                EventLog.CreateEventSource("CustomSource", "PratikUtilsEntry");
                Console.WriteLine("CreatedEventSource");
                Console.WriteLine("Exiting, execute the application a second time to use the source.");
                return;
            }

            // Create an EventLog instance and assign its source.
            EventLog myLog = new EventLog();
            myLog.Source = "MySource";

            // Write an informational entry to the event log.
            myLog.WriteEntry("Writing to event log.");
        }
        static void QueryEventViewer()
        {
            //string LogSource = "Microsoft-Windows-GroupPolicy/Operational";
            string LogSource = "HP Analytics";
            //string LogSource = ForwardedEventLog;

            //string query = "*[System/EventID=1400 or System/EventID=84]";
            string query = "*[System/EventID=84]";

            try
            {
                EventLogQuery eventsQuery = new EventLogQuery(LogSource, PathType.LogName, query);
                EventLogReader logReader = new EventLogReader(eventsQuery);
                for (EventRecord eventRecord = logReader.ReadEvent(); eventRecord != null; eventRecord = logReader.ReadEvent())
                {
                    string xml = eventRecord.ToXml();
                    IList<EventProperty> property = eventRecord.Properties;

                    foreach (var item in property)
                        Console.WriteLine(item.Value.ToString());
                    //Console.WriteLine(xml);
                    Console.WriteLine("------------------------------------------------------------------------");
                }
            }
            catch (EventLogNotFoundException e)
            {
                Console.WriteLine("Error while reading the event logs {0}", e.Message);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }

        static int Main()
        {
            //ReadEvenLog(SystemEventLog);
            //WriteEventLog(ApplicationEventLog);
            //cleanEventLogs(SecurityEventLog);
            //CreateCustomSourceEntry();
            QueryEventViewer();

            Console.WriteLine("Program End");
            Console.ReadLine();
            return 0;
        }
    }
}

// EventLogQuery     : It represents the query that will be used to fetch the events and other related information.
// EventLogReader    : Takes an EventLogQuery object as a parameter and allows us to read the events based on the query.
// EventRecord       : EventLogReader returns an EventRecord object that contains the basic details of the event entry.
// EventLogRecord    : This class implements EventRecord and provides additional details of the event like container and so on.