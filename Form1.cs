using System.Net;
using System.Net.Sockets;

namespace DelayCtrl
{
    /// <summary>
    /// 四路继电器控制上位机主窗体。
    /// 通过 UDP 协议与继电器设备通信，支持单路和全通道的开关控制。
    ///
    /// 【通信协议说明】
    /// - 连接方式：UDP
    /// - 发送指令格式：3 字节  [0x00] [命令字节] [0xFF]
    /// - 设备响应格式：4 字节  [0x00] [命令字节] [0xFF] [0xEF]
    ///
    /// 【指令对照表】
    ///   第一路开：00 F1 FF      第一路关：00 01 FF
    ///   第二路开：00 F2 FF      第二路关：00 02 FF
    ///   第三路开：00 F3 FF      第三路关：00 03 FF
    ///   第四路开：00 F4 FF      第四路关：00 04 FF
    ///   全通道开：00 F9 FF      全通道关：00 09 FF
    ///
    /// 【命令字节规律】
    ///   开启 = 0xF0 + 通道号 (1~4)，关闭 = 0x00 + 通道号 (1~4)
    ///   全通道开 = 0xF9，全通道关 = 0x09
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>UDP 客户端实例，用于与继电器设备进行数据收发</summary>
        private UdpClient? _udpClient;

        /// <summary>远程设备的 IP 端点（IP 地址 + 端口）</summary>
        private IPEndPoint? _remoteEndPoint;

        /// <summary>当前是否已建立 UDP 连接</summary>
        private bool _isConnected;

        /// <summary>用于取消异步接收循环的令牌源，断开连接时触发取消</summary>
        private CancellationTokenSource? _cts;

        public Form1()
        {
            InitializeComponent();
        }

        #region 连接管理

        /// <summary>
        /// 连接/断开按钮点击事件处理。
        /// 根据当前连接状态切换为连接或断开操作。
        /// </summary>
        private void BtnConnect_Click(object? sender, EventArgs e)
        {
            if (_isConnected)
                Disconnect();
            else
                Connect();
        }

        /// <summary>
        /// 建立 UDP 连接。
        /// 1. 从界面读取 IP 和端口
        /// 2. 创建 UdpClient 并连接到远程设备
        /// 3. 锁定输入框，启用继电器控制按钮
        /// 4. 启动异步接收循环，监听设备返回的状态数据
        /// </summary>
        private void Connect()
        {
            try
            {
                // 从界面获取 IP 地址和端口号
                var ip = txtIP.Text.Trim();
                var port = int.Parse(txtPort.Text.Trim());

                // 创建远程端点并建立 UDP 连接
                _remoteEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
                _udpClient = new UdpClient();
                _udpClient.Connect(_remoteEndPoint);
                _isConnected = true;

                // 更新界面状态：按钮文字、连接状态指示、锁定输入框
                btnConnect.Text = "断开";
                lblConnectionStatus.Text = "● 已连接";
                lblConnectionStatus.ForeColor = Color.Green;
                txtIP.Enabled = false;
                txtPort.Enabled = false;
                SetRelayButtonsEnabled(true);

                // 启动异步接收循环，持续监听设备响应
                _cts = new CancellationTokenSource();
                _ = ReceiveLoopAsync(_cts.Token);

                AppendLog($"已连接到 {ip}:{port}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 断开 UDP 连接。
        /// 1. 取消异步接收循环
        /// 2. 关闭并释放 UdpClient 资源
        /// 3. 恢复界面为未连接状态
        /// </summary>
        private void Disconnect()
        {
            // 取消接收循环
            _cts?.Cancel();

            // 关闭并释放 UDP 客户端
            _udpClient?.Close();
            _udpClient?.Dispose();
            _udpClient = null;
            _isConnected = false;

            // 恢复界面状态
            btnConnect.Text = "连接";
            lblConnectionStatus.Text = "● 未连接";
            lblConnectionStatus.ForeColor = Color.Red;
            txtIP.Enabled = true;
            txtPort.Enabled = true;
            SetRelayButtonsEnabled(false);

            AppendLog("已断开连接");
        }

        /// <summary>
        /// 批量设置所有继电器控制按钮的启用/禁用状态。
        /// 连接成功后启用，断开连接后禁用，防止未连接时误操作。
        /// </summary>
        /// <param name="enabled">true 为启用，false 为禁用</param>
        private void SetRelayButtonsEnabled(bool enabled)
        {
            btnRelay1On.Enabled = enabled;
            btnRelay1Off.Enabled = enabled;
            btnRelay2On.Enabled = enabled;
            btnRelay2Off.Enabled = enabled;
            btnRelay3On.Enabled = enabled;
            btnRelay3Off.Enabled = enabled;
            btnRelay4On.Enabled = enabled;
            btnRelay4Off.Enabled = enabled;
            btnAllOn.Enabled = enabled;
            btnAllOff.Enabled = enabled;
        }

        #endregion

        #region 数据接收与响应解析

        /// <summary>
        /// 异步接收循环，持续监听设备返回的 UDP 数据包。
        /// 在后台运行，收到数据后交由 ProcessResponse 处理。
        /// 当连接断开（CancellationToken 触发）或 UdpClient 被释放时自动退出。
        /// </summary>
        /// <param name="token">取消令牌，断开连接时用于终止循环</param>
        private async Task ReceiveLoopAsync(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested && _udpClient != null)
                {
                    // 异步等待接收 UDP 数据包
                    var result = await _udpClient.ReceiveAsync(token);
                    var data = result.Buffer;
                    ProcessResponse(data);
                }
            }
            // 以下异常为正常断开连接时的预期情况，静默处理
            catch (OperationCanceledException) { }
            catch (ObjectDisposedException) { }
            catch (SocketException) { }
            catch (Exception ex)
            {
                // 非预期异常，记录到日志（需确保在 UI 线程操作控件）
                if (!token.IsCancellationRequested)
                    BeginInvoke(() => AppendLog("接收错误: " + ex.Message));
            }
        }

        /// <summary>
        /// 处理设备返回的原始字节数据。
        /// 1. 将原始数据以十六进制格式写入日志
        /// 2. 校验响应格式：[0x00] [命令字节] [0xFF] [0xEF]，共 4 字节
        /// 3. 提取命令字节，更新对应继电器的界面状态
        /// </summary>
        /// <param name="data">设备返回的原始字节数组</param>
        private void ProcessResponse(byte[] data)
        {
            // 将字节数组转为十六进制字符串，写入日志便于调试
            var hex = BitConverter.ToString(data).Replace("-", " ");
            BeginInvoke(() => AppendLog("收到: " + hex));

            // 校验响应格式：长度 >= 4，首字节 0x00，第三字节 0xFF，第四字节 0xEF
            if (data.Length >= 4 && data[0] == 0x00 && data[2] == 0xFF && data[3] == 0xEF)
            {
                // 第二字节为命令字节，用于判断哪一路继电器以及开关状态
                byte cmd = data[1];
                BeginInvoke(() => UpdateRelayStatus(cmd));
            }
        }

        /// <summary>
        /// 根据设备返回的命令字节，更新对应继电器的界面状态。
        ///
        /// 命令字节含义：
        ///   0xF1~0xF4 = 第 1~4 路开启
        ///   0x01~0x04 = 第 1~4 路关闭
        ///   0xF9      = 全通道开启
        ///   0x09      = 全通道关闭
        /// </summary>
        /// <param name="cmd">设备返回的命令字节（响应数据的第二字节）</param>
        private void UpdateRelayStatus(byte cmd)
        {
            switch (cmd)
            {
                // 单路继电器状态更新
                case 0xF1: SetRelayState(1, true); break;   // 第一路开启
                case 0x01: SetRelayState(1, false); break;   // 第一路关闭
                case 0xF2: SetRelayState(2, true); break;    // 第二路开启
                case 0x02: SetRelayState(2, false); break;   // 第二路关闭
                case 0xF3: SetRelayState(3, true); break;    // 第三路开启
                case 0x03: SetRelayState(3, false); break;   // 第三路关闭
                case 0xF4: SetRelayState(4, true); break;    // 第四路开启
                case 0x04: SetRelayState(4, false); break;   // 第四路关闭

                // 全通道开启：同时更新 4 路状态
                case 0xF9:
                    SetRelayState(1, true);
                    SetRelayState(2, true);
                    SetRelayState(3, true);
                    SetRelayState(4, true);
                    break;

                // 全通道关闭：同时更新 4 路状态
                case 0x09:
                    SetRelayState(1, false);
                    SetRelayState(2, false);
                    SetRelayState(3, false);
                    SetRelayState(4, false);
                    break;
            }
        }

        /// <summary>
        /// 更新指定继电器在界面上的状态显示。
        /// - 状态指示灯（● 符号）：开启时为绿色，关闭时为灰色
        /// - 状态文字：显示"开启"或"关闭"
        /// </summary>
        /// <param name="relay">继电器编号（1~4）</param>
        /// <param name="on">true 表示开启，false 表示关闭</param>
        private void SetRelayState(int relay, bool on)
        {
            // 根据继电器编号映射到对应的界面控件
            var (indicator, status) = relay switch
            {
                1 => (lblRelay1Indicator, lblRelay1Status),
                2 => (lblRelay2Indicator, lblRelay2Status),
                3 => (lblRelay3Indicator, lblRelay3Status),
                4 => (lblRelay4Indicator, lblRelay4Status),
                _ => ((Label?)null, (Label?)null)
            };

            if (indicator == null || status == null) return;

            // 更新指示灯颜色和状态文字
            indicator.ForeColor = on ? Color.LimeGreen : Color.Gray;
            status.Text = on ? "开启" : "关闭";
        }

        #endregion

        #region 指令发送

        /// <summary>
        /// 向继电器设备发送控制指令。
        /// 发送前检查连接状态，发送后将指令内容记录到日志。
        /// </summary>
        /// <param name="command">要发送的字节数组，格式为 [0x00, 命令字节, 0xFF]</param>
        private void SendCommand(byte[] command)
        {
            // 未连接时提示用户
            if (!_isConnected || _udpClient == null)
            {
                MessageBox.Show("请先连接设备", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 通过 UDP 发送指令字节
                _udpClient.Send(command, command.Length);

                // 将发送内容以十六进制写入日志
                var hex = BitConverter.ToString(command).Replace("-", " ");
                AppendLog("发送: " + hex);
            }
            catch (Exception ex)
            {
                AppendLog("发送失败: " + ex.Message);
            }
        }

        /// <summary>
        /// 向日志文本框追加一条带时间戳的消息。
        /// 格式示例：[14:30:05] 已连接到 124.221.145.241:23024
        /// </summary>
        /// <param name="message">要追加的日志消息内容</param>
        private void AppendLog(string message)
        {
            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
        }

        #endregion

        #region 继电器按钮点击事件

        // ---- 单路继电器控制 ----
        // 指令格式：[0x00] [命令字节] [0xFF]
        // 开启命令字节 = 0xF0 + 通道号，关闭命令字节 = 0x00 + 通道号

        /// <summary>第一路继电器 - 开启（指令：00 F1 FF）</summary>
        private void BtnRelay1On_Click(object? sender, EventArgs e) => SendCommand([0x00, 0xF1, 0xFF]);

        /// <summary>第一路继电器 - 关闭（指令：00 01 FF）</summary>
        private void BtnRelay1Off_Click(object? sender, EventArgs e) => SendCommand([0x00, 0x01, 0xFF]);

        /// <summary>第二路继电器 - 开启（指令：00 F2 FF）</summary>
        private void BtnRelay2On_Click(object? sender, EventArgs e) => SendCommand([0x00, 0xF2, 0xFF]);

        /// <summary>第二路继电器 - 关闭（指令：00 02 FF）</summary>
        private void BtnRelay2Off_Click(object? sender, EventArgs e) => SendCommand([0x00, 0x02, 0xFF]);

        /// <summary>第三路继电器 - 开启（指令：00 F3 FF）</summary>
        private void BtnRelay3On_Click(object? sender, EventArgs e) => SendCommand([0x00, 0xF3, 0xFF]);

        /// <summary>第三路继电器 - 关闭（指令：00 03 FF）</summary>
        private void BtnRelay3Off_Click(object? sender, EventArgs e) => SendCommand([0x00, 0x03, 0xFF]);

        /// <summary>第四路继电器 - 开启（指令：00 F4 FF）</summary>
        private void BtnRelay4On_Click(object? sender, EventArgs e) => SendCommand([0x00, 0xF4, 0xFF]);

        /// <summary>第四路继电器 - 关闭（指令：00 04 FF）</summary>
        private void BtnRelay4Off_Click(object? sender, EventArgs e) => SendCommand([0x00, 0x04, 0xFF]);

        // ---- 全通道控制 ----

        /// <summary>全通道 - 开启（指令：00 F9 FF）</summary>
        private void BtnAllOn_Click(object? sender, EventArgs e) => SendCommand([0x00, 0xF9, 0xFF]);

        /// <summary>全通道 - 关闭（指令：00 09 FF）</summary>
        private void BtnAllOff_Click(object? sender, EventArgs e) => SendCommand([0x00, 0x09, 0xFF]);

        #endregion

        /// <summary>
        /// 窗体关闭时，确保断开 UDP 连接并释放资源，防止后台接收线程残留。
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_isConnected) Disconnect();
            base.OnFormClosing(e);
        }
    }
}
