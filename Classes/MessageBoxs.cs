using P31School.View.MessageBx;
using System.Windows;
using System.Windows.Media.Effects;

namespace P31School.Classes
{
    public class MessageBoxs
    {
        public enum Buttons { Yes_No, OK }
        public enum Icon { Error, Info, Default }

        public static string Show(string Text)
        {
            return Show(Text, "", Buttons.OK, MessageBoxs.Icon.Default);
        }

        public static string Show(string Text, string Title)
        {
            return Show(Text, Title, Buttons.OK, MessageBoxs.Icon.Default);
        }

        public static string Show(string Text, string Title, Buttons buttons, Icon icon)
        {
            WMesBox messageBox = new WMesBox(Text, Title, buttons, MessageBoxs.Icon.Default);
            messageBox.ShowDialog();
            return messageBox.ReturnString;
        }

        public static string ShowDialog(string Text)
        {
            return ShowDialog(Text, "", Buttons.OK, MessageBoxs.Icon.Default);
        }

        public static string ShowDialog(string Text, string Title)
        {
            return ShowDialog(Text, Title, Buttons.OK, MessageBoxs.Icon.Default);
        }

        public static string ShowDialog(string Text, string Title, Buttons buttons)
        {
            return ShowDialog(Text, Title, buttons, MessageBoxs.Icon.Default);
        }

        public static string ShowDialog(string Text, string Title, Buttons buttons, Icon icon)
        {
            ShowBlurEffectAllWindow();
            WMesBox messageBox = new WMesBox(Text, Title, buttons, icon);
            messageBox.ShowDialog();
            StopBlurEffectAllWindow();
            return messageBox.ReturnString;
        }

        static BlurEffect MyBlur = new BlurEffect();

        public static void ShowBlurEffectAllWindow()
        {
            MyBlur.Radius = 15;
            foreach (Window window in Application.Current.Windows)

                window.Effect = MyBlur;
        }

        public static void StopBlurEffectAllWindow() => MyBlur.Radius = 0;
    }
}
