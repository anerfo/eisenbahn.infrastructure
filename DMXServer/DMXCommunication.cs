using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace DMXServer
{
    class DMXCommunication
    {
        #region DLL Imports
        [DllImport("K8062D.dll")]
        static extern void StartDevice();

        [DllImport("K8062D.dll")]
        static extern void SetData(Int32 Channel, Int32 Data);

        [DllImport("K8062D.dll")]
        static extern void SetChannelCount(Int32 Count);

        [DllImport("K8062D.dll")]
        static extern void StopDevice();
        #endregion
        
        #region "Private Members"
        private bool _communicationAllowed;
        #endregion

        #region Public Methods
        /// <summary>
        /// Initializes the communication with the DMX-bus.
        /// </summary>
        public void StartCommunication()
        {
            if (!_communicationAllowed)
            {
                _communicationAllowed = true;
                StartDevice();
            }
        }

        /// <summary>
        /// Stops the communication with the DMX-bus
        /// </summary>
        public void StopCommunication()
        {
            if (_communicationAllowed)
            {
                _communicationAllowed = false;
                try
                {
                    StopDevice();
                }
                catch(Exception ex)
                {
                    Console.WriteLine("An exception occured during stopping: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Sets the number of used channels. This may improve the performance of the bus.
        /// </summary>
        /// <param name="count">The number of the channels used</param>
        public void SetNumberOfChannels(Int32 count)
        {
            if (_communicationAllowed)
            {
                SetChannelCount(count);
            }
        }

        /// <summary>
        /// Sets the value of a dmx channel.
        /// </summary>
        /// <param name="channel">The channel to set</param>
        /// <param name="data">The value to write</param>
        public void SetChannelData(Int32 channel, Int32 data)
        {
            if (_communicationAllowed)
            {
                SetData(channel, data);
            }
        }
        #endregion
    }
}
