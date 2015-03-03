using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace tcpTimeService
{
	static class Program
	{
		static void Main(string[] args)
		{
			if (!args.Any())
			{
				Console.WriteLine("Choose either server or client to start. Terminating");
				return;
			}
			switch (args[1])
			{
				case "server":
					Server.StartListening();
					break;
				case "client":

					break;
				default:
					Console.WriteLine("Choose either server or client to start. Terminating");
					break;
			}
		}

		static class Server
		{
			private static TcpListener server;
			private const int port = 1414;

			public static void StartListening()
			{
				server = new TcpListener(IPAddress.Any, port);
				try
				{
					server.Start();

					while (true)
					{
						Console.WriteLine("Server: waiting for connections...");
						TcpClient client = server.AcceptTcpClient();
						Console.WriteLine("Server: client {0} connected just now", (IPEndPoint)client.Client.RemoteEndPoint);

						Thread thread = new Thread(new ParameterizedThreadStart(ProcessClient));
						thread.Start(client);

						Thread.Sleep(1000);
					}

				}
				catch (Exception e)
				{
					Console.WriteLine("Error {0}", e);
				}
				finally
				{
					server.Stop();
				}
			}

			private static void ProcessClient(object obj)
			{
				try
				{
					while (true)
					{
						TcpClient client = (TcpClient) obj;
						string currentTime = DateTime.Now.ToString();
						byte[] bufferedCurrentTime = Encoding.ASCII.GetBytes(currentTime);
						client.GetStream().Write(bufferedCurrentTime, 0, bufferedCurrentTime.Length);
						Thread.Sleep(1000);
					}
				}
				catch (Exception e)
				{
					Console.WriteLine("Error {0}", e);
				}
			}

		}


		static class Client
		{

			public static void Start()
			{
				
			}


		}







	}
}
