using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Core;
using PcapDotNet.Packets;
namespace Proyecto_Logical_Analysis
{
    class Program
    {
        static void Main(string[] args)
        {

            // Buscar todas las interfaces de red disponibles
            IList<LivePacketDevice> devices = LivePacketDevice.AllLocalMachine;

            if (devices.Count == 0)
            {
                Console.WriteLine("No se encontraron interfaces de red.");
                return;
            }

            // Mostrar las interfaces de red disponibles y permitir al usuario seleccionar una
            Console.WriteLine("Interfaces de red disponibles:");
            for (int i = 0; i < devices.Count; i++)
            {
                LivePacketDevice device = devices[i];
                Console.WriteLine("{0}. {1}", i + 1, device.Description);
            }

            Console.Write("Seleccione la interfaz de red para monitorear: ");
            int selectedDeviceNumber = int.Parse(Console.ReadLine()) - 1;

            if (selectedDeviceNumber < 0 || selectedDeviceNumber >= devices.Count)
            {
                Console.WriteLine("Selección de interfaz de red inválida.");
                return;
            }

            // Obtener la interfaz de red seleccionada
            PacketDevice selectedDevice = devices[selectedDeviceNumber];

            // Abrir el dispositivo seleccionado y configurar el controlador de paquetes
            using (PacketCommunicator communicator = selectedDevice.Open(65536, PacketDeviceOpenAttributes.Promiscuous, 1000))
            {
                // Configurar el controlador de paquetes para capturar y procesar paquetes
                communicator.ReceivePackets(0, packet =>
                {
                    // Lógica para analizar y procesar cada paquete capturado
                    // Puedes imprimir información relevante del paquete o realizar otras acciones

                    // Ejemplo: Imprimir información básica del paquete
                    Console.WriteLine("Paquete capturado: origen = {0}, destino = {1}, protocolo = {2}",
                        packet.Ethernet.IpV4.Source.ToString(),
                        packet.Ethernet.IpV4.Destination.ToString(),
                        packet.Ethernet.IpV4.Protocol.ToString());
                });
            }
        }

    }
}
    

