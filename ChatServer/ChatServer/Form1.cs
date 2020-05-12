using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace ChatServer
{
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();
        }


        int porta = 8000;
        TcpListener tcpListener;
        TcpClient tcpClient;
        NetworkStream networkSream;
        Thread thInteraction;

      

        public bool connect()
        {
            bool retorno = false;
            try
            {
                tcpListener = new TcpListener(System.Net.IPAddress.Any, porta);
                tcpListener.Start();
                retorno = true;
            }
            catch { }
            return retorno;

        }

        public void disconect()
        {
            if (this.thInteraction != null)
            {
                if (this.thInteraction.ThreadState == ThreadState.Running)
                {
                    thInteraction.Abort();
                }
            }

            if (this.tcpClient != null)
            {
                this.tcpClient.Client.Disconnect(true);
            }

            tcpListener.Stop();

            setMsg("->Desconectando...", true);

        }

        public void acceptConnection()
        {
            try
            {
                tcpClient = tcpListener.AcceptTcpClient();
            }
            catch { }
        }


        public void enviarMsg(string message)
        {
            if (podeEscrever())
            {
                byte[] sendBytes = Encoding.ASCII.GetBytes(message);
                networkSream.Write(sendBytes, 0, sendBytes.Length);
            }
        }

        private bool podeEscrever()
        {
            if (networkSream == null)
            {
                return false;
            }
            if (networkSream.CanWrite && tcpClient != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        delegate void delSetMsg(string message, bool burlar);
        public void setMsg(string message, bool burlar)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new delSetMsg(setMsg), message, burlar);
            }
            else
            {
                if (burlar == true || podeEscrever() == true)
                {
                    textDialogo.Text += "Eu: " + message + "\n" ;
                }
            }
        }

        delegate void delGetMsg(string message );
        public void getMsg(string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new delGetMsg(getMsg), message);
            }
            else
            {
                if (podeEscrever() == true)
                {
                    textDialogo.Text += "Client: " + message +"\n" ;
                }
            }
        }

        public void start()
        {
            if (connect())
            {
                setMsg("-> Aguardando uma conexão",true);
            }
            thInteraction = new Thread(new ThreadStart(interaction));
            thInteraction.IsBackground = true;
            thInteraction.Priority = ThreadPriority.Highest;
            thInteraction.Name = "ThInteraction";
            thInteraction.Start();
        }

        public void interaction()
        {
            try
            {
                acceptConnection();
                setMsg("-> Conexão aceita", true);
                do
                {
                    networkSream = tcpClient.GetStream();

                    if (networkSream.CanRead)
                    {
                        byte[]  bytes = new byte[tcpClient.ReceiveBufferSize];
                        networkSream.Read(bytes, 0, Convert.ToInt32(tcpClient.ReceiveBufferSize));

                        string clientData = Encoding.ASCII.GetString(bytes);

                        if (clientData.Replace("\0", "").Trim() != "")
                        {
                            getMsg(clientData);
                        }
                        else
                        {
                            tcpClient = null;
                        }

                    }

                } while (tcpClient != null);

                disconect();
                start();
            }
            catch { }
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            start();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            setMsg("-> Encerrando conexão com o servidor", true);
        }

     
        private void textEscreva_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                textEscreva.Clear();
                
            }

        }

        private void textEscreva_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string menssagem = textEscreva.Text;
                enviarMsg(menssagem);
                setMsg(menssagem, false);
            }
        }

       
    }
}
