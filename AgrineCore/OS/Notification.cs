using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace AgrineCore.OS
{
    public static class Notification
    {
        private static NotifyIcon _notifyIcon;
        private static Action _onClickAction;

        static Notification()
        {
            _notifyIcon = new NotifyIcon
            {
                Icon = SystemIcons.Information,
                Visible = true
            };
            _notifyIcon.BalloonTipClicked += NotifyIcon_BalloonTipClicked;
        }

        /// <summary>
        /// Show a basic notification with title and message
        /// </summary>
        public static void ShowNotification(string title, string message, ToolTipIcon icon = ToolTipIcon.Info, int durationMs = 4000)
        {
            Show(title, message, icon, null, durationMs);
        }

        /// <summary>
        /// Show a notification with a custom icon
        /// </summary>
        public static void ShowNotificationWithIcon(string title, string message, Icon customIcon, ToolTipIcon icon = ToolTipIcon.None, int durationMs = 4000)
        {
            _notifyIcon.Icon = customIcon ?? SystemIcons.Information;
            Show(title, message, icon, null, durationMs);
        }

        /// <summary>
        /// Show a notification with click action
        /// </summary>
        public static void ShowNotificationWithAction(string title, string message, Action onClick, ToolTipIcon icon = ToolTipIcon.Info, int durationMs = 4000)
        {
            Show(title, message, icon, onClick, durationMs);
        }

        /// <summary>
        /// Check if notifications are supported in the current environment
        /// </summary>
        public static bool IsSupported()
        {
            return Environment.UserInteractive;
        }

        /// <summary>
        /// Show a sample notification for testing
        /// </summary>
        public static void ShowTest()
        {
            ShowNotificationWithAction("AgrineCore Test", "Click here to test interaction!", () =>
            {
                MessageBox.Show("Notification clicked!", "AgrineCore", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
        }

        /// <summary>
        /// Schedule a notification after a delay
        /// </summary>
        public static void ScheduleNotification(string title, string message, int delayMs, ToolTipIcon icon = ToolTipIcon.Info, int durationMs = 4000)
        {
            new Thread(() =>
            {
                Thread.Sleep(delayMs);
                ShowNotification(title, message, icon, durationMs);
            })
            { IsBackground = true }.Start();
        }

        /// <summary>
        /// Handle click event on the balloon tip
        /// </summary>
        private static void NotifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            _onClickAction?.Invoke();
            _onClickAction = null;
        }

        /// <summary>
        /// Internal method to show notification
        /// </summary>
        private static void Show(string title, string message, ToolTipIcon icon, Action onClick, int durationMs)
        {
            _notifyIcon.BalloonTipTitle = title;
            _notifyIcon.BalloonTipText = message;
            _notifyIcon.BalloonTipIcon = icon;
            _onClickAction = onClick;

            _notifyIcon.ShowBalloonTip(durationMs);
        }

        /// <summary>
        /// Dispose the notification and hide tray icon
        /// </summary>
        public static void Dispose()
        {
            _notifyIcon.Visible = false;
            _notifyIcon.Dispose();
        }
    }
}
