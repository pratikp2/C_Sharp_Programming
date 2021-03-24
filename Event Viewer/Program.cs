using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Xml;

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
        private static void GetRecordsInfo()
        {
            try
            {
                string channel = "Microsoft-Windows-Windows Defender/Operational";

                string filePath = @"C:\Users\patilp\Desktop\DefenderLogs.evtx";

                string query = "Event/System[ TimeCreated[@SystemTime > '2020-07-15T19:08:30.895832100Z' and @SystemTime < '2020-07-15T20:21:30.895832100Z']" +
                    " and ((Level=1 or Level=2 or Level=3 or Level=4) and (EventID = 1006 or EventID = 1007 or EventID = 1008 or EventID = 1009 or " +
                    "EventID = 1010 or EventID = 1011 or EventID = 1012 or EventID = 1015 or EventID = 1116 or EventID = 1117 or EventID = 1118 or EventID = 1119) and Provider[@Name='Microsoft-Windows-Windows Defender'])]";

                // Event Log Query to read from Event Viewer
                //EventLogQuery eventLogQuery = new EventLogQuery(channel, PathType.LogName, query);

                // Event Log Query to read backed up Event viewer Logs in .etvx file
                EventLogQuery eventLogQuery = new EventLogQuery(filePath, PathType.FilePath, query);

                EventLogReader eventLogReader = new EventLogReader(eventLogQuery);

                for (EventRecord record = eventLogReader.ReadEvent(); record != null; record = eventLogReader.ReadEvent())
                {                  
                    if (record == null)
                    {
                        Console.WriteLine("No data found");
                        return;
                    }

                    var _currentEvent = new Dictionary<string, object>();


                    // Read System Data
                    _currentEvent["ProviderName"] = record.ProviderName;
                    _currentEvent["ProviderEventGuid"] = record.ProviderId;
                    _currentEvent["Channel"] = String.IsNullOrEmpty(channel) ? String.Empty : channel;
                    _currentEvent["EventID"] = Convert.ToString(record.Id);
                    _currentEvent["Level"] = Convert.ToString(record.Level);


                    string msg = record.FormatDescription();
                    var lines = msg.Split('\n');

                    if (lines.Length > 1)
                    {
                        for (int i = 0; i < lines.Length; i++)
                        {
                            lines[i] = lines[i].Trim('\r').Trim('\t').Trim();
                            if (lines[i].Contains("Action:"))
                            {
                                _currentEvent["Action"] = lines[i].Replace("Action: ", "");
                            }
                            if (lines[i].Contains("Detection Origin"))
                            {
                                _currentEvent["DetectionOrigin"] = lines[i].Replace("Detection Origin: ", "");
                            }
                            if (lines[i].Contains("Detection Type:"))
                            {
                                _currentEvent["DetectionType"] = lines[i].Replace("Detection Type: ", "");
                            }
                            if (lines[i].Contains("Detection Source:") && lines[i].StartsWith("Detection Source"))
                            {
                                _currentEvent["DetectionSource"] = lines[i].Replace("Detection Source: ", "");
                            }
                            if (lines[i].Contains("Action Status:") && lines[i].StartsWith("Action Status"))
                            {
                                _currentEvent["Status:"] = lines[i].Replace("Action Status: ", "");
                            }
                        }
                    }

                    string rawXml = record.ToXml();
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(rawXml);
                    Dictionary<string, string> _EventData = new Dictionary<string, string>();
                    XmlNodeList xmlnodeList = xmlDoc.GetElementsByTagName("Data");

                    foreach (XmlNode node in xmlnodeList)
                    {
                        string text = node.InnerText;
                        string attr = node.Attributes["Name"]?.InnerText;
                        _EventData.Add(attr, text);
                    }

                    //  Read Event Data specific to Event type.
                    _currentEvent["Time Stamp"] = record.TimeCreated;
                    _currentEvent["ThreatID"] = _EventData.ContainsKey("Threat ID") ? string.IsNullOrWhiteSpace(_EventData["Threat ID"]) ? "NA" : _EventData["Threat ID"] : "NA";
                    _currentEvent["ThreatName"] = _EventData.ContainsKey("Threat Name") ? string.IsNullOrWhiteSpace(_EventData["Threat Name"]) ? "NA" : _EventData["Threat Name"] : "NA";
                    _currentEvent["Severity"] = _EventData.ContainsKey("Severity ID") ? string.IsNullOrWhiteSpace(_EventData["Severity ID"]) ? "NA" : _EventData["Severity ID"] : "NA";
                    _currentEvent["Category"] = _EventData.ContainsKey("Category Name") ? string.IsNullOrWhiteSpace(_EventData["Category Name"]) ? "NA" : _EventData["Category Name"] : "NA";
                    _currentEvent["Path"] = _EventData.ContainsKey("Path") ? string.IsNullOrWhiteSpace(_EventData["Path"]) ? "NA" : _EventData["Path"] : "NA";
                    //_currentEvent["DetectionOrigin"] = _EventData.ContainsKey("Origin Name") ? string.IsNullOrWhiteSpace(_EventData["Origin Name"]) ? "NA" : _EventData["Origin Name"] : "NA";
                    //_currentEvent["DetectionType"] = _EventData.ContainsKey("Type Name") ? string.IsNullOrWhiteSpace(_EventData["Type Name"]) ? "NA" : _EventData["Type Name"] : "NA";
                    //_currentEvent["DetectionSource"] = _EventData.ContainsKey("Source Name") ? string.IsNullOrWhiteSpace(_EventData["Source Name"]) ? "NA" : _EventData["Source Name"] : "NA";
                    //_currentEvent["Status"] = _EventData.ContainsKey("Status Description") ? string.IsNullOrWhiteSpace(_EventData["Status Description"]) ? "NA" : _EventData["Status Description"] : "NA";
                    _currentEvent["ProcessName"] = _EventData.ContainsKey("Process Name") ? string.IsNullOrWhiteSpace(_EventData["Process Name"]) ? "NA" : _EventData["Process Name"] : "NA";
                    //_currentEvent["Action"] = _EventData.ContainsKey("Action Name") ? string.IsNullOrWhiteSpace(_EventData["Action Name"]) ? "NA" : _EventData["Action Name"] : "NA";
                    _currentEvent["ErrorCode"] = _EventData.ContainsKey("Error Code") ? string.IsNullOrWhiteSpace(_EventData["Error Code"]) ? "NA" : _EventData["Error Code"] : "NA";
                    _currentEvent["ErrorDescription"] = _EventData.ContainsKey("Error Description") ? string.IsNullOrWhiteSpace(_EventData["Error Description"]) ? "NA" : _EventData["Error Description"] : "NA";
                    _currentEvent["SignatureVersion"] = _EventData.ContainsKey("Security intelligence Version") ? string.IsNullOrWhiteSpace(_EventData["Security intelligence Version"]) ? "NA" : _EventData["Security intelligence Version"] : "NA";
                    _currentEvent["EngineVersion"] = _EventData.ContainsKey("Engine Version") ? string.IsNullOrWhiteSpace(_EventData["Engine Version"]) ? "NA" : _EventData["Engine Version"] : "NA";
                    _currentEvent["CoRelationID"] = record.ActivityId;

                    foreach (var item in _currentEvent)
                        Console.WriteLine("{0}  :   {1}",item.Key,item.Value);
                    Console.WriteLine();
                }  
            }
            catch (Exception ex)
            { Console.WriteLine("Exception : {0}", ex.Message); }
        }

        static int Main()
        {
            GetRecordsInfo();
            //QueryEventViewer();            

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