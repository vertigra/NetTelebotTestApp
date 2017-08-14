﻿using System;
using System.Windows.Forms;
using NetTelebot.Commands.TestApplication.Utils;

namespace NetTelebot.Commands.TestApplication
{
    public partial class frmMain : Form, IWindowsCredential
    {
        private const string mBotName = "NetTelebotTest";

        private readonly CalculatorBot mBot;

        public frmMain()
        {
            var token = GetTelegramCredential(mBotName).Token;
            mBot = new CalculatorBot(token);
            mBot.MessageReceived += Bot_MessageReceived;

            InitializeComponent();
        }

        private void Bot_MessageReceived(object sender, MessageEventArgs e)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action<object, MessageEventArgs > (Bot_MessageReceived), sender, e);
            }
            else
            {
                txtLog.Text += $"{e.Message.From.FirstName} : {e.Message.Text}\r\n";
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            mBot.Start();
            txtLog.Text += $"Bot Start \r\n";
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            mBot.Stop();
            txtLog.Text += $"Bot Stop \r\n";
        }

        public TelegramCredentials GetTelegramCredential(string botAlias)
        {
            return new WindowsCredential().GetTelegramCredential(botAlias);
        }
    }
}
