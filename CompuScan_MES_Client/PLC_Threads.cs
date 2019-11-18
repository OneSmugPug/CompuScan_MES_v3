using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Sharp7;
using System.Data.SqlClient;

namespace CompuScan_MES_Client
{
    public class PLC_Threads
    {
        public S7Client client = new S7Client();
        public static byte[] readBuffer = new byte[296];
        public static byte[] writeBuffer = new byte[296];
        public static byte[] echoReadBuffer = new byte[24];
        public static byte[] echoWriteBuffer = new byte[2];
        public bool isConnected = false;
        public bool isReading = true;
        public bool hasReadOne = false;

        public string lineID { get; set; }
        public string identifier { get; set; }
        public int identifierCount { get; set; }
        public int readTransactionID { get; set; }
        public int writeTransactionID { get; set; }
        public int channelStatus { get; set; }
        public int stationStatus { get; set; }
        public int errorCode { get; set; }
        public string userName { get; set; }
        public int equipmentID { get; set; }
        public string productionData { get; set; }
        public int heartBeat { get; set; }

        

        public void EstablishConnection()
        {
            if (!isConnected)
            {
                int connectionResult = client.ConnectTo("192.168.1.1", 0, 1);

                if (connectionResult == 0)
                {
                    Console.WriteLine("=========Connection success===========");
                    isConnected = true;
                }
                else
                {
                    Console.WriteLine("=========Connection error============");
                    isConnected = false;
                    Console.WriteLine(connectionResult);
                    return;
                    
                }
            }
        }

        public void Main_PLC_Interaction()
        {
            while (isConnected)
            {
                Echo();
                ReadAllValues();
                //readTransactionID = S7.GetByteAt(readBuffer, 45);
                switch (readTransactionID)
                {
                    case 1:
                        if (!hasReadOne)
                        {
                            //WriteToSQLDataBase();
                            Console.WriteLine("Got Transaction ID: " + readTransactionID + ". Writing to database and sending a transaction ID of " + (readTransactionID + 1) + "back to PLC.");
                            writeTransactionID = readTransactionID + 1;
                            WriteBackToPLC();
                            hasReadOne = true;
                        }
                        break;
                    case 3:
                    case 5:
                    case 7:
                    case 9:
                    case 11:
                    case 13:
                    case 15:
                    case 17:
                    case 19:
                    case 21:
                    case 23:
                    case 25:
                    case 27:
                    case 29:
                    case 31:
                    case 33:
                    case 35:
                        if (hasReadOne)
                        {
                            //WriteToSQLDataBase();
                            Console.WriteLine("Got Transaction ID: " + readTransactionID + ". Writing to database and sending a transaction ID of " + (readTransactionID + 1) + "back to PLC.");
                            writeTransactionID = readTransactionID + 1;
                            WriteBackToPLC();
                        }
                        break;
                    case 99:
                        Console.WriteLine("PLC Requested to stop communication... Sending final transaction to PLC.");
                        writeTransactionID = 100;
                        WriteBackToPLC();
                        hasReadOne = false;
                        break;
                    default:
                        break;
                }
                Thread.Sleep(500);
            }
        }

        public void ReadAllValues()
        {
            client.DBRead(100, 0, readBuffer.Length, readBuffer);//1110

            lineID = S7.GetStringAt(readBuffer, 0);

            identifier = S7.GetStringAt(readBuffer, 22);

            identifierCount = S7.GetByteAt(readBuffer, 44); // Number of entries in the database (Does not really need to read but write it to plc?)

            readTransactionID = S7.GetByteAt(readBuffer, 45);

            channelStatus = S7.GetByteAt(readBuffer, 46);

            stationStatus = S7.GetByteAt(readBuffer, 47);

            errorCode = S7.GetByteAt(readBuffer, 48);

            userName = S7.GetStringAt(readBuffer, 50);

            equipmentID = S7.GetByteAt(readBuffer, 94);

            productionData = S7.GetStringAt(readBuffer, 96);
        }

        public void WriteToSQLDataBase()
        {

            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();
                if (readTransactionID == 1)
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO Production_Line1 VALUES (@Line_ID, @Skid_ID, @Part_Number, @Station_Status, @Error_Code, @Operator, @Timestamp, @Equipment_ID, @Production_Data)", conn))
                        {
                            cmd.Parameters.AddWithValue("@Line_ID", lineID);
                            cmd.Parameters.AddWithValue("@Part_Number", identifier);
                            //cmd.Parameters.AddWithValue("@Transaction_ID", readTransactionID);
                            //cmd.Parameters.AddWithValue("@Channel_Status", channelStatus);
                            cmd.Parameters.AddWithValue("@Station_Status", stationStatus);
                            cmd.Parameters.AddWithValue("@Error_Code", errorCode);
                            //cmd.Parameters.AddWithValue("@Oper", userName);
                            cmd.Parameters.AddWithValue("@Timestamp", DateTime.Now);
                            cmd.Parameters.AddWithValue("@Equipment_ID", equipmentID);
                            cmd.Parameters.AddWithValue("@Production_Data", productionData);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("==> Couldn't write to database " + ex);
                    }
                }
                else if (readTransactionID > 1)
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("UPDATE Production_Line1 SET Production_Data = @Production_Data, Transaction_ID = @Transaction_ID WHERE Identifier = @Identifier", conn))
                        {
                            cmd.Parameters.AddWithValue("@Production_Data", productionData);
                            cmd.Parameters.AddWithValue("@Transaction_ID", readTransactionID);
                            cmd.Parameters.AddWithValue("@Identifier", identifier);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("==> Couldn't write to database" + ex);
                    }
                }
            }
        }

        public void WriteBackToPLC()
        {
            S7.SetStringAt(writeBuffer, 0, 20, lineID);
            S7.SetStringAt(writeBuffer, 22, 20, identifier);
            S7.SetByteAt(writeBuffer, 44, (byte)identifierCount);
            S7.SetByteAt(writeBuffer, 45, (byte)writeTransactionID);
            S7.SetByteAt(writeBuffer, 46, (byte)channelStatus);
            S7.SetByteAt(writeBuffer, 47, (byte)stationStatus);
            S7.SetByteAt(writeBuffer, 48, (byte)errorCode);
            S7.SetStringAt(writeBuffer, 50, 20, userName);
            S7.SetByteAt(writeBuffer, 94, (byte)equipmentID);
            S7.SetStringAt(writeBuffer, 96, 20, productionData);
            int writeResult = client.DBWrite(110, 0, writeBuffer.Length, writeBuffer);//1111
            if (writeResult == 0)
            {
                Console.WriteLine("==> Successfully wrote to PLC");
            }
        }

        public void DisconnectFromPLC()
        {
            client.Disconnect();
            isConnected = false;
        }

        public void Echo()
        {
            client.DBRead(1021, 0, echoReadBuffer.Length, echoReadBuffer);
            heartBeat = S7.GetIntAt(echoReadBuffer, 0);
            S7.SetIntAt(echoWriteBuffer, 0, (short)heartBeat);
            int writeResult = client.DBWrite(1022, 0, echoWriteBuffer.Length, echoWriteBuffer);
        }

        
    }
}
