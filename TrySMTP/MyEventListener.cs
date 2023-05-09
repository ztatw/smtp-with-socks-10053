using System.Diagnostics.Tracing;

namespace TrySMTP;

public class MyEventListener: EventListener
{
    protected override void OnEventSourceCreated(EventSource eventSource)
    {
        // Console.WriteLine(eventSource.Name);
        if (eventSource.Name == "System.Net.Sockets")
        {
            EnableEvents(eventSource, EventLevel.LogAlways);
            Console.WriteLine("enabled for sockets event source");
        }

        if (eventSource.Name == "Private.InternalDiagnostics.System.Net.Mail")
        {
            EnableEvents(eventSource, EventLevel.LogAlways);
            Console.WriteLine("enabled for mail net event source");
        }

        if (eventSource.Name == "Private.InternalDiagnostics.System.Net.Sockets")
        {
            EnableEvents(eventSource, EventLevel.LogAlways);
            Console.WriteLine("enabled for sockets net event source");
        }
    }

    protected override void OnEventWritten(EventWrittenEventArgs eventData)
    {
        Console.WriteLine($"{eventData.EventId}, {eventData.TimeStamp}, {eventData.EventName}, {eventData.Message}, {string.Join(", ", eventData.Payload)}");
    }
}