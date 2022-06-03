using P31School.Classes;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace P31School.View.MessageBx
{
    /// <summary>
    /// Логика взаимодействия для WMesBox.xaml
    /// </summary>
    public partial class WMesBox : Window
    {
        public string ReturnString { get; set; }
        public WMesBox(string Text,
                       string Title,
                       MessageBoxs.Buttons buttons,
                       MessageBoxs.Icon icon)
        {
            InitializeComponent();

            txtText.Text = Text;
            TxtTitle.Text = Title;

            switch (buttons)
            {
                case MessageBoxs.Buttons.OK: btnOK.Visibility = Visibility.Visible; break;
                case MessageBoxs.Buttons.Yes_No: btnYes.Visibility = Visibility.Visible; btnNo.Visibility = Visibility.Visible; break;
            }

            switch (icon)
            {
                case MessageBoxs.Icon.Info: Info.Visibility = Visibility.Visible; System.Media.SystemSounds.Exclamation.Play(); break;
                case MessageBoxs.Icon.Error: Error.Visibility = Visibility.Visible; System.Media.SystemSounds.Hand.Play(); break;
                case MessageBoxs.Icon.Default: break;
            }
        }

        private void GBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try { DragMove(); }
            catch { }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            ReturnString = "-1"; Close();
        }

        DoubleAnimation anim;
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Closing -= Window_Closing;
            e.Cancel = true;
            anim = new DoubleAnimation(0, (Duration)TimeSpan.FromSeconds(0.3));
            anim.Completed += (s, _) => this.Close();
            this.BeginAnimation(UIElement.OpacityProperty, anim);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void BtnReturnValue_Click(object sender, RoutedEventArgs e)
        {
            ReturnString = ((Button)sender).Uid.ToString();
            Close();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TxtText_KeyDown(object sender, KeyEventArgs e)
        {
            Clipboard.SetText(txtText.Text);
        }

        private void TxtText_Loaded(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(txtText.Text);
        }
    }
}