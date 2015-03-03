using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

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
					ServerStart();
					break;
				case "client":
					ClientStart();
					break;
				default:
					Console.WriteLine("Choose either server or client to start. Terminating");
					break;
			}
		}





		private static void ServerStart()
		{
			const int port = 1414;
			TcpListener server = new TcpListener(IPAddress.Any, port);
			try
			{
				server.Start();

				while (true)
				{
					Console.WriteLine("Server: waiting for connections...");
					TcpClient client = server.AcceptTcpClient();
					Console.WriteLine("Server: client");
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


		private static void ClientStart()
		{
			throw new NotImplementedException();
		}


	}
}
