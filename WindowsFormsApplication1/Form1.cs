using SuperSocket.ClientEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;
using System.Net.WebSockets;

// see http://kimux.net/?p=929

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Send message buffer size.
        /// </summary>
        const int MessageBufferSize = 256;

        public Form1()
        {
            InitializeComponent();
            Connect();
        }

        private async void Connect()
        {
            ClientWebSocket _ws = new ClientWebSocket();

            if (_ws.State != WebSocketState.Open)
            {
                await _ws.ConnectAsync(new Uri("ws://localhost:8080/echo"), CancellationToken.None);

                while (_ws.State == WebSocketState.Open)
                {
                    var buff = new ArraySegment<byte>(new byte[MessageBufferSize]);
                    var ret = await _ws.ReceiveAsync(buff, CancellationToken.None);
                    listBox1.Items.Add((new UTF8Encoding()).GetString(buff.Take(ret.Count).ToArray()));
                }
            }
        }
    }
}
