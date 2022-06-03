using System.Linq;
using System.Windows;

namespace P31School.View.MessageBx
{
    public class CloseMessage
    {
        public static void AllMessageBoxes()
        {
            foreach (WMesBox window in Application.Current.Windows.OfType<WMesBox>()) window.Close();
            while (Application.Current.Windows.OfType<WMesBox>().Count() > 0) { }
        }

        public static void AllMessages()
        {
            AllMessageBoxes();
        }
    }
}
