using Magellanic.I2C;
using System;

namespace Magellanic.Sensors.TMP102
{
    public class TMP102 : AbstractI2CDevice
    {
        private byte I2C_ADDRESS;

        public TMP102(A0PinConnection pinConnection)
        {
            I2C_ADDRESS = (byte)pinConnection;
        }

        public override byte GetI2cAddress()
        {
            return I2C_ADDRESS;
        }

        public override byte[] GetDeviceId()
        {
            throw new NotImplementedException("This device does not have a unique device identifier.");
        }

        public float GetTemperature()
        {
            byte[] readBuffer = new byte[2];
            
            this.Slave.WriteRead(new byte[] { I2C_ADDRESS }, readBuffer);

            var mostSignificantByte = readBuffer[0];

            var leastSignificantByte = readBuffer[1];

            // this formula is from the data sheet.
            // 1. Add the most significant and least significant bytes (using logical OR)
            // 2. Right shift the sum by 4 places (i.e. divide by 16)
            // 3. Multiply by 0.0625
            var bytesAddedTogether = mostSignificantByte << 8 | leastSignificantByte;

            var bytesRightShiftedByFourBits = bytesAddedTogether >> 4;

            return bytesRightShiftedByFourBits * 0.0625f;
        }
    }
}
