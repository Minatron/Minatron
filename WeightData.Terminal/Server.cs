using System;
using System.Net.Sockets;
using System.Threading;

namespace Band.WeightData.Terminal
{
    class Server
    {
        static void Main(string[] args)
        {
            var conf = new ServerConfig(args);
            Logger.InitWithName(conf.Name);
            Thread.CurrentThread.Name = "SERVER";

            var slave = new TcpListener(conf.ListenPort);

            try
            {
                try
                {
                    slave.Start(10);
                }
                catch
                {
                    throw new Exception(@"Невозможно открыть сокет №" + conf.ListenPort.ToString());
                }

                while (true)
                {
                    TcpClient connect = null;
                    if (slave.Pending())
                    {
                        try
                        {
                            connect = slave.AcceptTcpClient();
                        }
                        catch 
                        { 
                        }
                    }

                    if (connect == null) {Thread.Sleep(500);  continue;}
                    if (connect.Connected)
                    {
                        Terminal terminal = new Terminal(connect);
                        Thread thread = new Thread(Terminal.DoWork);
                        thread.Start(terminal);
                    }
                    Thread.Sleep(0);
                }              
            }
            catch (OutOfMemoryException e)
            {
                Logger.WriteMessage(Logger.EventID.ServiceCrash, @"ПЕРЕПОЛНЕНИЕ ВИРТУАЛЬНОЙ ПАМЯТИ: " + e.ToString());
            }
            catch (Exception ex)
            {
                Logger.WriteMessage(Logger.EventID.ServiceCrash, ex.ToString());
            }
            finally
            {
                slave.Stop();
                Logger.Close();
            }
        }


    }
}
