// See https://aka.ms/new-console-template for more information

using System.Net.Mail;
using TrySMTP;

Console.WriteLine("Hello, World!");

// var evtListener = new MyEventListener();

var smtp = new SmtpClient("localhost", 6666);
smtp.Send("alice@example.com", "bob@example.com", "sub", "hello!");