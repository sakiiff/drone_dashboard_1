using System.Net;
using System.Net.Sockets;

namespace drone_dashboard_1.Services.Mavlink
{
    public class MavlinkUdpService
    {
        private readonly UdpClient _udp;

        public MavlinkUdpService(int port)
        {
            _udp = new UdpClient(port);
        }

        public async Task StartAsync(CancellationToken cancellationToken) 
        {
            Console.WriteLine("MAVlink UDP listener started.");

            while(!cancellationToken.IsCancellationRequested)
            {
                var result = await _udp.ReceiveAsync(cancellationToken);

                Console.WriteLine(
                    $"Received {result.Buffer.Length} bytes" +
                    $"from {result.RemoteEndPoint}");

                Console.WriteLine(
                    Convert.ToHexString(result.Buffer));
            }
        }


    }
}
