namespace Microsoft.Samples.Kinect.KinectExplorer
{
    using System;
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO.Ports;

    public class ArduinoHandler
    {
        SerialPort port;
        const byte Control_Servo = 1;
        const byte Read_Servo = 2;
        const byte Control_Port = 4;
        public Boolean available;
        public ArduinoHandler()
        {
            port = new SerialPort();
            port.PortName = "COM3";
            port.BaudRate = 9600;
            port.Open();
            this.available = this.setup();
        }
        Boolean setup()
        {
            return true;
        }
        Boolean processCommand(string control, string command)
        {
            return true;
        }
        public static Boolean connected()
        {
            return true;
        }
        public void changeServo(int x)
        {
            if ((x > 180) || (x < 0))
            {
                Trace.WriteLine("Invalid Value: " + x.ToString());
            }
            Trace.WriteLine("Changing Servo to: " + x.ToString());
            byte[] buffer_2_send = new byte[1];

            buffer_2_send[0] = Control_Servo;
            port.Write(buffer_2_send, 0, buffer_2_send.Length);//write data on the serial port
            String reply;
            reply = port.ReadLine();
            Trace.WriteLine("Arduino replied: " + reply);
            if (reply == "1\r")
            {
                byte lol = Convert.ToByte(x);
                buffer_2_send[0] = lol;
                port.Write(buffer_2_send, 0, buffer_2_send.Length);
            }
        }
        public int getServoAngle()
        {
            byte[] buffer_2_send = new byte[1];

            buffer_2_send[0] = Read_Servo;
            port.Write(buffer_2_send, 0, buffer_2_send.Length);//write data on the serial port
            String reply;
            reply = port.ReadLine();
            Trace.WriteLine("Arduino replied: " + reply);
            if (reply == "2\r")
            {
                String txt = port.ReadLine();
                Trace.WriteLine("ServoAngle Arduino Output: " + txt);
                String NewString = txt.Remove(txt.Length - 1, 1);
                Trace.WriteLine("ServoAngle: " + NewString);
                return Convert.ToInt32(NewString);
            }
            else
            {
                Trace.WriteLine("Failed");
                return 0;
            }
        }
        public void lights(bool x)
        {
            setPort(6, x ? 1 : 0);
        }
        public void setPort(int portNum, int high)
        {
            byte[] buffer_2_send = new byte[1];
            buffer_2_send[0] = Control_Port;
            String reply;
            port.Write(buffer_2_send, 0, buffer_2_send.Length);//write data on the serial port
            reply = port.ReadLine();
            Trace.WriteLine("Arduino replied: " + reply);
            if (reply == "4\r")
            {
                buffer_2_send[0] = Convert.ToByte(portNum);
                port.Write(buffer_2_send, 0, buffer_2_send.Length);//write data on the serial port
                reply = port.ReadLine();
                Trace.WriteLine("Arduino replied: " + reply);
                if (reply == "1\r")
                {
                    if (high == 1) { buffer_2_send[0] = 1; }
                    else if (high == 0) { buffer_2_send[0] = 0; }
                    port.Write(buffer_2_send, 0, buffer_2_send.Length);//write data on the serial port
                }
            }
        }
 
    }
}
