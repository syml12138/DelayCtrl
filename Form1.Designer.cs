namespace DelayCtrl
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            grpConnection = new GroupBox();
            lblIP = new Label();
            txtIP = new TextBox();
            lblPort = new Label();
            txtPort = new TextBox();
            btnConnect = new Button();
            lblConnectionStatus = new Label();

            grpRelay1 = new GroupBox();
            lblRelay1Indicator = new Label();
            lblRelay1Status = new Label();
            btnRelay1On = new Button();
            btnRelay1Off = new Button();

            grpRelay2 = new GroupBox();
            lblRelay2Indicator = new Label();
            lblRelay2Status = new Label();
            btnRelay2On = new Button();
            btnRelay2Off = new Button();

            grpRelay3 = new GroupBox();
            lblRelay3Indicator = new Label();
            lblRelay3Status = new Label();
            btnRelay3On = new Button();
            btnRelay3Off = new Button();

            grpRelay4 = new GroupBox();
            lblRelay4Indicator = new Label();
            lblRelay4Status = new Label();
            btnRelay4On = new Button();
            btnRelay4Off = new Button();

            grpAll = new GroupBox();
            btnAllOn = new Button();
            btnAllOff = new Button();

            txtLog = new TextBox();

            grpConnection.SuspendLayout();
            grpRelay1.SuspendLayout();
            grpRelay2.SuspendLayout();
            grpRelay3.SuspendLayout();
            grpRelay4.SuspendLayout();
            grpAll.SuspendLayout();
            SuspendLayout();

            // ---- grpConnection ----
            grpConnection.Controls.Add(lblIP);
            grpConnection.Controls.Add(txtIP);
            grpConnection.Controls.Add(lblPort);
            grpConnection.Controls.Add(txtPort);
            grpConnection.Controls.Add(btnConnect);
            grpConnection.Controls.Add(lblConnectionStatus);
            grpConnection.Location = new Point(12, 8);
            grpConnection.Name = "grpConnection";
            grpConnection.Size = new Size(620, 58);
            grpConnection.TabIndex = 0;
            grpConnection.TabStop = false;
            grpConnection.Text = "连接设置";

            lblIP.AutoSize = true;
            lblIP.Location = new Point(12, 25);
            lblIP.Name = "lblIP";
            lblIP.Text = "IP地址:";

            txtIP.Location = new Point(65, 22);
            txtIP.Name = "txtIP";
            txtIP.Size = new Size(150, 23);
            txtIP.Text = "192.168.1.100";

            lblPort.AutoSize = true;
            lblPort.Location = new Point(225, 25);
            lblPort.Name = "lblPort";
            lblPort.Text = "端口:";

            txtPort.Location = new Point(262, 22);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(65, 23);
            txtPort.Text = "8080";

            btnConnect.Location = new Point(345, 20);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(80, 28);
            btnConnect.TabIndex = 0;
            btnConnect.Text = "连接";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += BtnConnect_Click;

            lblConnectionStatus.AutoSize = true;
            lblConnectionStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblConnectionStatus.ForeColor = Color.Red;
            lblConnectionStatus.Location = new Point(440, 25);
            lblConnectionStatus.Name = "lblConnectionStatus";
            lblConnectionStatus.Text = "● 未连接";

            // ---- grpRelay1 ----
            grpRelay1.Controls.Add(lblRelay1Indicator);
            grpRelay1.Controls.Add(lblRelay1Status);
            grpRelay1.Controls.Add(btnRelay1On);
            grpRelay1.Controls.Add(btnRelay1Off);
            grpRelay1.Location = new Point(12, 75);
            grpRelay1.Name = "grpRelay1";
            grpRelay1.Size = new Size(302, 80);
            grpRelay1.TabIndex = 1;
            grpRelay1.TabStop = false;
            grpRelay1.Text = "第一路继电器";

            lblRelay1Indicator.Font = new Font("Segoe UI", 18F);
            lblRelay1Indicator.ForeColor = Color.Gray;
            lblRelay1Indicator.Location = new Point(12, 25);
            lblRelay1Indicator.Name = "lblRelay1Indicator";
            lblRelay1Indicator.Size = new Size(35, 40);
            lblRelay1Indicator.Text = "●";

            lblRelay1Status.Location = new Point(47, 35);
            lblRelay1Status.Name = "lblRelay1Status";
            lblRelay1Status.Size = new Size(50, 20);
            lblRelay1Status.Text = "关闭";

            btnRelay1On.Location = new Point(120, 30);
            btnRelay1On.Name = "btnRelay1On";
            btnRelay1On.Size = new Size(75, 32);
            btnRelay1On.Text = "开启";
            btnRelay1On.BackColor = Color.FromArgb(144, 238, 144);
            btnRelay1On.FlatStyle = FlatStyle.Flat;
            btnRelay1On.Click += BtnRelay1On_Click;

            btnRelay1Off.Location = new Point(205, 30);
            btnRelay1Off.Name = "btnRelay1Off";
            btnRelay1Off.Size = new Size(75, 32);
            btnRelay1Off.Text = "关闭";
            btnRelay1Off.BackColor = Color.FromArgb(255, 160, 160);
            btnRelay1Off.FlatStyle = FlatStyle.Flat;
            btnRelay1Off.Click += BtnRelay1Off_Click;

            // ---- grpRelay2 ----
            grpRelay2.Controls.Add(lblRelay2Indicator);
            grpRelay2.Controls.Add(lblRelay2Status);
            grpRelay2.Controls.Add(btnRelay2On);
            grpRelay2.Controls.Add(btnRelay2Off);
            grpRelay2.Location = new Point(330, 75);
            grpRelay2.Name = "grpRelay2";
            grpRelay2.Size = new Size(302, 80);
            grpRelay2.TabIndex = 2;
            grpRelay2.TabStop = false;
            grpRelay2.Text = "第二路继电器";

            lblRelay2Indicator.Font = new Font("Segoe UI", 18F);
            lblRelay2Indicator.ForeColor = Color.Gray;
            lblRelay2Indicator.Location = new Point(12, 25);
            lblRelay2Indicator.Name = "lblRelay2Indicator";
            lblRelay2Indicator.Size = new Size(35, 40);
            lblRelay2Indicator.Text = "●";

            lblRelay2Status.Location = new Point(47, 35);
            lblRelay2Status.Name = "lblRelay2Status";
            lblRelay2Status.Size = new Size(50, 20);
            lblRelay2Status.Text = "关闭";

            btnRelay2On.Location = new Point(120, 30);
            btnRelay2On.Name = "btnRelay2On";
            btnRelay2On.Size = new Size(75, 32);
            btnRelay2On.Text = "开启";
            btnRelay2On.BackColor = Color.FromArgb(144, 238, 144);
            btnRelay2On.FlatStyle = FlatStyle.Flat;
            btnRelay2On.Click += BtnRelay2On_Click;

            btnRelay2Off.Location = new Point(205, 30);
            btnRelay2Off.Name = "btnRelay2Off";
            btnRelay2Off.Size = new Size(75, 32);
            btnRelay2Off.Text = "关闭";
            btnRelay2Off.BackColor = Color.FromArgb(255, 160, 160);
            btnRelay2Off.FlatStyle = FlatStyle.Flat;
            btnRelay2Off.Click += BtnRelay2Off_Click;

            // ---- grpRelay3 ----
            grpRelay3.Controls.Add(lblRelay3Indicator);
            grpRelay3.Controls.Add(lblRelay3Status);
            grpRelay3.Controls.Add(btnRelay3On);
            grpRelay3.Controls.Add(btnRelay3Off);
            grpRelay3.Location = new Point(12, 165);
            grpRelay3.Name = "grpRelay3";
            grpRelay3.Size = new Size(302, 80);
            grpRelay3.TabIndex = 3;
            grpRelay3.TabStop = false;
            grpRelay3.Text = "第三路继电器";

            lblRelay3Indicator.Font = new Font("Segoe UI", 18F);
            lblRelay3Indicator.ForeColor = Color.Gray;
            lblRelay3Indicator.Location = new Point(12, 25);
            lblRelay3Indicator.Name = "lblRelay3Indicator";
            lblRelay3Indicator.Size = new Size(35, 40);
            lblRelay3Indicator.Text = "●";

            lblRelay3Status.Location = new Point(47, 35);
            lblRelay3Status.Name = "lblRelay3Status";
            lblRelay3Status.Size = new Size(50, 20);
            lblRelay3Status.Text = "关闭";

            btnRelay3On.Location = new Point(120, 30);
            btnRelay3On.Name = "btnRelay3On";
            btnRelay3On.Size = new Size(75, 32);
            btnRelay3On.Text = "开启";
            btnRelay3On.BackColor = Color.FromArgb(144, 238, 144);
            btnRelay3On.FlatStyle = FlatStyle.Flat;
            btnRelay3On.Click += BtnRelay3On_Click;

            btnRelay3Off.Location = new Point(205, 30);
            btnRelay3Off.Name = "btnRelay3Off";
            btnRelay3Off.Size = new Size(75, 32);
            btnRelay3Off.Text = "关闭";
            btnRelay3Off.BackColor = Color.FromArgb(255, 160, 160);
            btnRelay3Off.FlatStyle = FlatStyle.Flat;
            btnRelay3Off.Click += BtnRelay3Off_Click;

            // ---- grpRelay4 ----
            grpRelay4.Controls.Add(lblRelay4Indicator);
            grpRelay4.Controls.Add(lblRelay4Status);
            grpRelay4.Controls.Add(btnRelay4On);
            grpRelay4.Controls.Add(btnRelay4Off);
            grpRelay4.Location = new Point(330, 165);
            grpRelay4.Name = "grpRelay4";
            grpRelay4.Size = new Size(302, 80);
            grpRelay4.TabIndex = 4;
            grpRelay4.TabStop = false;
            grpRelay4.Text = "第四路继电器";

            lblRelay4Indicator.Font = new Font("Segoe UI", 18F);
            lblRelay4Indicator.ForeColor = Color.Gray;
            lblRelay4Indicator.Location = new Point(12, 25);
            lblRelay4Indicator.Name = "lblRelay4Indicator";
            lblRelay4Indicator.Size = new Size(35, 40);
            lblRelay4Indicator.Text = "●";

            lblRelay4Status.Location = new Point(47, 35);
            lblRelay4Status.Name = "lblRelay4Status";
            lblRelay4Status.Size = new Size(50, 20);
            lblRelay4Status.Text = "关闭";

            btnRelay4On.Location = new Point(120, 30);
            btnRelay4On.Name = "btnRelay4On";
            btnRelay4On.Size = new Size(75, 32);
            btnRelay4On.Text = "开启";
            btnRelay4On.BackColor = Color.FromArgb(144, 238, 144);
            btnRelay4On.FlatStyle = FlatStyle.Flat;
            btnRelay4On.Click += BtnRelay4On_Click;

            btnRelay4Off.Location = new Point(205, 30);
            btnRelay4Off.Name = "btnRelay4Off";
            btnRelay4Off.Size = new Size(75, 32);
            btnRelay4Off.Text = "关闭";
            btnRelay4Off.BackColor = Color.FromArgb(255, 160, 160);
            btnRelay4Off.FlatStyle = FlatStyle.Flat;
            btnRelay4Off.Click += BtnRelay4Off_Click;

            // ---- grpAll ----
            grpAll.Controls.Add(btnAllOn);
            grpAll.Controls.Add(btnAllOff);
            grpAll.Location = new Point(12, 255);
            grpAll.Name = "grpAll";
            grpAll.Size = new Size(620, 65);
            grpAll.TabIndex = 5;
            grpAll.TabStop = false;
            grpAll.Text = "全通道控制";

            btnAllOn.Location = new Point(100, 24);
            btnAllOn.Name = "btnAllOn";
            btnAllOn.Size = new Size(160, 32);
            btnAllOn.Text = "全部开启";
            btnAllOn.BackColor = Color.FromArgb(100, 200, 100);
            btnAllOn.FlatStyle = FlatStyle.Flat;
            btnAllOn.Click += BtnAllOn_Click;

            btnAllOff.Location = new Point(360, 24);
            btnAllOff.Name = "btnAllOff";
            btnAllOff.Size = new Size(160, 32);
            btnAllOff.Text = "全部关闭";
            btnAllOff.BackColor = Color.FromArgb(240, 128, 128);
            btnAllOff.FlatStyle = FlatStyle.Flat;
            btnAllOff.Click += BtnAllOff_Click;

            // ---- txtLog ----
            txtLog.Location = new Point(12, 330);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new Size(620, 210);
            txtLog.TabIndex = 6;
            txtLog.BackColor = Color.FromArgb(30, 30, 30);
            txtLog.ForeColor = Color.LimeGreen;
            txtLog.Font = new Font("Consolas", 9F);

            // ---- Form1 ----
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(645, 550);
            Controls.Add(grpConnection);
            Controls.Add(grpRelay1);
            Controls.Add(grpRelay2);
            Controls.Add(grpRelay3);
            Controls.Add(grpRelay4);
            Controls.Add(grpAll);
            Controls.Add(txtLog);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "四路继电器控制";

            grpConnection.ResumeLayout(false);
            grpConnection.PerformLayout();
            grpRelay1.ResumeLayout(false);
            grpRelay2.ResumeLayout(false);
            grpRelay3.ResumeLayout(false);
            grpRelay4.ResumeLayout(false);
            grpAll.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox grpConnection;
        private Label lblIP;
        private TextBox txtIP;
        private Label lblPort;
        private TextBox txtPort;
        private Button btnConnect;
        private Label lblConnectionStatus;

        private GroupBox grpRelay1;
        private Label lblRelay1Indicator;
        private Label lblRelay1Status;
        private Button btnRelay1On;
        private Button btnRelay1Off;

        private GroupBox grpRelay2;
        private Label lblRelay2Indicator;
        private Label lblRelay2Status;
        private Button btnRelay2On;
        private Button btnRelay2Off;

        private GroupBox grpRelay3;
        private Label lblRelay3Indicator;
        private Label lblRelay3Status;
        private Button btnRelay3On;
        private Button btnRelay3Off;

        private GroupBox grpRelay4;
        private Label lblRelay4Indicator;
        private Label lblRelay4Status;
        private Button btnRelay4On;
        private Button btnRelay4Off;

        private GroupBox grpAll;
        private Button btnAllOn;
        private Button btnAllOff;

        private TextBox txtLog;
    }
}
