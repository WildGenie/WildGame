using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormClient
{
    using System.Collections;
    using System.Collections.Specialized;

    using Microsoft.AspNetCore.SignalR.Client;

    public partial class Form1 : Form
    {
        private HubConnection connection;

        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            this.connection.SendAsync("SendMessage", this.textBox1.Text, this.textBox2.Text);
        }

        private async void Form1_LoadAsync(object sender, EventArgs e)
        {
            
            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44325/chatHub")
                .Build();

            connection.On<string, string>("ReceiveMessage", (user, message) =>
                {
                    BeginInvoke((Action)(() =>
                                                {
                                                    richTextBox1.AppendText(user, message, Color.DarkGray);
                                                }));
                });

            try
            {
                await connection.StartAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
