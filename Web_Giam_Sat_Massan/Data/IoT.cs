using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using S7.Net;
using Web_Giam_Sat_Massan.Models;

namespace Web_Giam_Sat_Massan.Data
{
    public class IoT
    {
        public static string? statusConnect;
        public static bool isConnected;

        
        public static Plc _Plc;
        public static PlcData _PlcData = new PlcData();
        //public static PlcDataWrite11 _PlcDataWrite11 = new PlcDataWrite11();
        public static int writedb;
        public static void ConnectToPlc()
        {
            _Plc = new Plc(CpuType.S71200, "192.168.0.10", 0, 0);

            if (_Plc.Open() == ErrorCode.NoError)
            {               
                
                isConnected = true;
            }
            else
            {               
                
                isConnected = false;
            }
        }
            
        public static void Readdata()
        {
            if (_Plc.Open() == ErrorCode.NoError)
            {
                _Plc.ReadClass(_PlcData, 5);
            } 
        }

        public static void WirteData(Web_Giam_Sat_Massan.Models.PlcDataWrite11 model)
        {
            if (_Plc.Open() == ErrorCode.NoError)
            {
                _Plc.WriteClass(model, writedb);
            }             
        }

        public static void Btn(int x, int y)
        {
            if (_Plc.Open() == ErrorCode.NoError)
            {
                _Plc.WriteBit(DataType.Input,0, x, y, true);
            }
        }
    }
}
