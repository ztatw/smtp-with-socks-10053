// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Net.Sockets;
using System.Text;

Console.WriteLine("Hello, World!");

var sockListener = new Socket(IPAddress.Loopback.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

try
{
    sockListener.Bind(new IPEndPoint(IPAddress.Loopback, 6666));
    sockListener.Listen(10);
    while (true)
    {
        Console.WriteLine("waiting for client...");
        var clientSock = sockListener.Accept();
        clientSock.Send(Encoding.UTF8.GetBytes("220 This is a fake SMTP server!\r\n"));

        while (true)
        {
            var buffer = new byte[1024];
            var data = string.Empty;
            while (true)
            {
                if(!clientSock.Connected) break;
                
                var receivedLength = clientSock.Receive(buffer);
                if (receivedLength <= 0)
                {
                    Console.WriteLine("received 0, stop receiving");
                    break;
                }

                Console.WriteLine($"received {receivedLength} bytes");
                data += Encoding.UTF8.GetString(buffer, 0, receivedLength);
                if (data.EndsWith("\r\n"))
                {
                    break;
                }
            }
            
            if (string.IsNullOrEmpty(data)) break;

            Console.WriteLine($"Recv: {data}");
            if (data.StartsWith("EHLO"))
            {
                var reqParam = data.Replace("EHLO", "").Trim();
                clientSock.Send(Encoding.UTF8.GetBytes($"250-Hello {reqParam}!\r\n250-PIPELINING\r\n250-8BITMIME\r\n250 SMTPUTF8\r\n"));
                clientSock.Shutdown(SocketShutdown.Both);
            }
        }
    }
}
catch (Exception e)
{
    Console.WriteLine(e);
}