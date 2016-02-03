using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace LAN_Messenger
{
    [Serializable]
    public class ChatMessage : Marox.Serialization.SERIALIZABLE
    {
        public ChatMessage(string _Message, string _Sender, string _Receiver, DateTime _DateSent)
        {
            this.message = _Message;
            this.sender = _Sender;
            this.receiver = _Receiver;
            this.dateSent = _DateSent;
        }


        private readonly string sender;
        public string Sender
        {
            get { return sender; }
        }

        private readonly string receiver;
        public string Receiver
        {
            get { return receiver; }
        }

        private readonly DateTime dateSent;
        public DateTime DateSent
        {
            get { return dateSent; }
        }

        private readonly string message;
        public string Message
        {
            get { return message; }
        }


    }

    public static class ChatLogFile
    {
        public static ObservableCollection<ChatMessage> readChatLogFromFile(string fileName)
        {
            ObservableCollection<ChatMessage> obj = new ObservableCollection<ChatMessage>();
            try
            {
                if (!File.Exists(fileName))
                {
                    return obj;
                }

                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                obj = formatter.Deserialize(stream) as ObservableCollection<ChatMessage>;
                stream.Close();

                if (obj == null)
                {
                    obj = new ObservableCollection<ChatMessage>();
                }
            }
            catch (Exception ex)
            {
                global.Log("readChatLogFromFile error: " + ex.Message);
            }

            return obj;
        }

        //todo: writing successful
        //todo: log

        public static void writeChatLogToFile(string fileName, ObservableCollection<ChatMessage> chatLog)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, chatLog);
                stream.Close();
            }
            catch (Exception ex)
            {
                global.Log("writeChatLogToFile error: " + ex.Message);
            }
        }
    }








}





//todo: auto-open web url and file url