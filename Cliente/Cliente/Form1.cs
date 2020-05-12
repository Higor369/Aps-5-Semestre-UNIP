using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;

namespace Cliente
{
    public partial class Form1 : Form
    {

        TcpClient tcpClient;
        NetworkStream networkSream;
        Thread thInteraction;

        public Form1()
        {
            InitializeComponent();
        }

        public void connect()
        {
            tcpClient = new TcpClient();
            setMsg("-> Estabelecendo conexão");
            tcpClient.Connect("127.0.0.1", 8000);

        }

        public void disconect()
        {
            if(thInteraction != null)
            {
                if(thInteraction.ThreadState == ThreadState.Running)
                {
                    thInteraction.Abort();
                }
            }

            tcpClient.Close();
        }

        public void enviaMensagem(string menssagem)
        {
            if (networkSream.CanWrite)
            {
                byte[] sendBytes = Encoding.ASCII.GetBytes(menssagem);
                networkSream.Write(sendBytes, 0, sendBytes.Length);


            }
        }

        delegate void selSetMsg(string mensagem);
        public void setMsg(string mensagem)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new selSetMsg(setMsg), mensagem);
            }
            else
            {
                textDialogo.Text += "\nEu: " + mensagem ;

            }
        }

        delegate void delGetMsg(string mensagem);
        public void getMsg(string mensagem)
        {
            if (this.InvokeRequired)
            {
                BeginInvoke(new delGetMsg(getMsg), mensagem);
            }
            else
            {
                textDialogo.Text += "\nServer: " + mensagem ;

            }
        }

        public void interaction()
        {
            try
            {
                do
                {

                    networkSream = tcpClient.GetStream();

                    if (networkSream.CanRead)
                    {
                        byte[] bytes = new byte[tcpClient.ReceiveBufferSize];
                        networkSream.Read(bytes,0, Convert.ToInt32(tcpClient.ReceiveBufferSize));

                        string returnData = Encoding.ASCII.GetString(bytes);
                        getMsg(returnData);
                    }
                    else
                    {
                        setMsg("-> erro no processamento, contate o desenvolvedor");
                        disconect();
                    }

                } while (tcpClient.Connected);
            }
            catch { }
        }

        private void textEscreva_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (networkSream.CanWrite)
                {

                    string menssagem = textEscreva.Text;
                    enviaMensagem(menssagem);
                    setMsg(menssagem);
                }
                else
                {
                    setMsg("-> Erro no processamento, contate o desenvolvedor");
                }
                
                
                
            }
        }

        private void textEscreva_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textEscreva.Clear();

            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            disconect();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connect();
            thInteraction = new Thread(new ThreadStart(interaction));
            thInteraction.IsBackground = true;
            thInteraction.Priority = ThreadPriority.Highest;
            thInteraction.Name = "ThInteractionClient";
            thInteraction.Start();
        }
    }
}
