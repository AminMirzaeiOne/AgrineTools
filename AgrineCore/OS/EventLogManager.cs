using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AgrineCore.OS
{
    public static class EventLogManager
    {
        /// <summary>
        /// Get all available event log names on the system
        /// </summary>
        public static List<string> GetEventLogNames()
        {
            var logs = new List<string>();
            foreach (EventLog log in EventLog.GetEventLogs())
            {
                logs.Add(log.Log);
                log.Dispose();
            }
            return logs;
        }

        /// <summary>
        /// Read entries from a specific event log
        /// </summary>
        /// <param name="logName">Name of the event log (e.g., Application, System, Security)</param>
        /// <param name="maxEntries">Maximum number of entries to read (default 100)</param>
        /// <returns>List of EventLogEntry</returns>
        public static List<EventLogEntry> ReadEntries(string logName, int maxEntries = 100)
        {
            var entries = new List<EventLogEntry>();
            try
            {
                if (!EventLog.Exists(logName))
                    return entries;

                using (EventLog log = new EventLog(logName))
                {
                    int count = 0;
                    // Read entries starting from the newest
                    for (int i = log.Entries.Count - 1; i >= 0 && count < maxEntries; i--)
                    {
                        entries.Add(log.Entries[i]);
                        count++;
                    }
                }
            }
            catch
            {
                // Ignore exceptions, return what we got so far
            }
            return entries;
        }

        /// <summary>
        /// Write an entry to a specific event log
        /// </summary>
        /// <param name="logName">Event log name (e.g. Application)</param>
        /// <param name="source">Source name to use (should be registered)</param>
        /// <param name="message">Message text</param>
        /// <param name="type">Entry type (Information, Warning, Error)</param>
        /// <returns>True if success, false otherwise</returns>
        public static bool WriteEntry(string logName, string source, string message, EventLogEntryType type = EventLogEntryType.Information)
        {
            try
            {
                if (!EventLog.SourceExists(source))
                {
                    // Create source if not exists (requires admin)
                    EventLog.CreateEventSource(source, logName);
                }

                EventLog.WriteEntry(source, message, type);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Clear all entries in a specific event log
        /// </summary>
        /// <param name="logName">Event log name</param>
        /// <returns>True if success, false otherwise</returns>
        public static bool ClearLog(string logName)
        {
            try
            {
                if (EventLog.Exists(logName))
                {
                    using (EventLog log = new EventLog(logName))
                    {
                        log.Clear();
                    }
                    return true;
                }
            }
            catch
            {
                // Ignore exceptions
            }
            return false;
        }

        /// <summary>
        /// Delete an event source (useful for cleanup)
        /// </summary>
        /// <param name="source">Source name to delete</param>
        /// <returns>True if success, false otherwise</returns>
        public static bool DeleteSource(string source)
        {
            try
            {
                if (EventLog.SourceExists(source))
                {
                    EventLog.DeleteEventSource(source);
                    return true;
                }
            }
            catch
            {
                // Ignore exceptions
            }
            return false;
        }
    }
}
