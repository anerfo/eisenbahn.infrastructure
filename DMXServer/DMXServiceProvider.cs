using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMXServer
{
    class DMXServiceProvider : TcpLib.TcpServiceProvider
    {
        const byte _cStartByte = 255;
        const byte _cEndByte = 0;
        const byte _cPositionOfTheChannel = 1;
        const byte _cPositionOfTheValue = 2;
        const byte _cLengthOfMessage = 4; //StartByte + Channel + Value + EndByte

        private List<byte> _receiveBuffer;
        private DMXCommunication _dmxCommunication;

        private void ExecuteDMXCommands()
        {
            while (_receiveBuffer.Count > _cLengthOfMessage)
            {
                //First delete everything until a StartByte
                while (_receiveBuffer.Count > _cLengthOfMessage && _receiveBuffer[0] != _cStartByte)
                {
                    _receiveBuffer.RemoveAt(0);
                }
                //If there are to few databytes in the buffer, then wait until next receive
                if (_receiveBuffer.Count < _cLengthOfMessage)
                {
                    break;
                }
                //if the endbyte was not received correctly then delete the startbyte
                //In the next run the rest of the mess is cleaned up.
                if (_receiveBuffer[_cLengthOfMessage - 1] != _cEndByte)
                {
                    _receiveBuffer.RemoveAt(0);
                    continue;
                }
                //Now we have a valid message in the buffer.
                _dmxCommunication.SetChannelData((Int32)_receiveBuffer[_cPositionOfTheChannel],
                    (Int32)_receiveBuffer[_cPositionOfTheValue]);
                _receiveBuffer.RemoveRange(0, _cLengthOfMessage);
            }
        }

        public DMXServiceProvider(DMXCommunication dmxCommunication)
        {
            if (dmxCommunication == null)
            {
                throw new NotSupportedException("dmxCommunication can not be null!");
            }
            _dmxCommunication = dmxCommunication;
            _receiveBuffer = new List<byte>();
        }

        public override object Clone()
        {
            return new DMXServiceProvider(_dmxCommunication);
        }

        public override void OnAcceptConnection(TcpLib.ConnectionState state)
        {
            Console.WriteLine("Accepted new Client");
        }


        public override void OnReceiveData(TcpLib.ConnectionState state)
        {
            byte[] buffer = new byte[1024];
            while (state.AvailableData > 0)
            {
                int readBytes = state.Read(buffer, 0, 1024);
                if (readBytes > 0)
                {
                    _receiveBuffer.AddRange(buffer);
                    ExecuteDMXCommands();
                }
                else state.EndConnection();
                //If read fails then close connection

            }
        }


        public override void OnDropConnection(TcpLib.ConnectionState state)
        {
            Console.WriteLine("Connection closed");
        }
    }
}
