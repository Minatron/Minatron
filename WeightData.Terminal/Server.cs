using System;
using System.Net.Sockets;
using System.Threading;
using Modbus.Data;
using Modbus.Device;
using Modbus.Message;

namespace Band.WeightData.Terminal
{
    class Server
    {
        static void Main(string[] args)
        {
            ServerConfig conf = new ServerConfig(args);
            Logger log = new Logger(conf.Name);
            Thread.CurrentThread.Name = "TERMINAL";

            ModbusSlave slave = ModbusTcpSlave.CreateTcp(1, new TcpListener(conf.ListenPort));

            log.WriteMessage(Logger.EventID.ServiceStart);
            try
            {
			    
                DataStore data = DataStoreFactory.CreateDefaultDataStore();


                //slave.DataStore = data;
                slave.ModbusSlaveRequestReceived += (sender, arg) => 
                    {
                        IModbusMessage msg = arg.Message;
                    };

			    slave.Listen();

			    Thread.Sleep(Timeout.Infinite);                
            }
            catch (OutOfMemoryException e)
            {
                log.WriteMessage(Logger.EventID.ServiceCrash, @"ПЕРЕПОЛНЕНИЕ ВИРТУАЛЬНОЙ ПАМЯТИ: " + e.ToString());
            }
            catch (Exception ex)
            {
                log.WriteMessage(Logger.EventID.ServiceCrash, ex.ToString());
            }
            finally
            {
                slave.Dispose();
                log.WriteMessage(Logger.EventID.ServiceStop);
                log.Close();
            }
        }


    }
}
