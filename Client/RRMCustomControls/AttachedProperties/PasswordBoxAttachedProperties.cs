using System.Windows;
using System.Windows.Controls;

namespace RRMCustomControls.AttachedProperties
{
    /// <summary>
    /// The MonitorPassword attached property for <see cref="PasswordBox "/>
    /// </summary>
    public class MonitorPasswordProperty : BaseAttachedProperty<MonitorPasswordProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //get the caller 
            var passwordBox = sender as PasswordBox;
            if (passwordBox == null) return;

            //remove any previous event listener
            passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;

            //if the caller set Monitor to true
            if ((bool)e.NewValue)
            {
                // set default value 
                HasTextProperty.SetValue(passwordBox);

                passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
            }
        }
        /// <summary>
        /// fires when the passwordBox  password values changes
        /// </summary>
        /// <param name="sender"> the ui element that has property changed</param>
        /// <param name="e">event args</param>
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            //set the attached HasText Value 
            HasTextProperty.SetValue((PasswordBox)sender);

            //set the BoundPassword Property
            BoundPasswordProperty.SetValue((PasswordBox)sender);
        }
    }

    /// <summary>
    /// the HasText attached property for <see cref="PasswordBox"/>
    /// </summary>
    public class HasTextProperty : BaseAttachedProperty<HasTextProperty, bool>
    {
        /// <summary>
        /// set the HasText property based on if the caller <see cref="PasswordBox"/> has any text
        /// </summary>
        /// <param name="sender">the ui elemnt that has the value changed</param>
        public static void SetValue(DependencyObject sender)
        {
            SetValue((PasswordBox)sender, ((PasswordBox)sender).SecurePassword.Length > 0);
        }
    }
    /// <summary>
    /// bound password property for <see cref="PasswordBox"/> to bind the password to the viewModel
    /// </summary>
    public class BoundPasswordProperty : BaseAttachedProperty<BoundPasswordProperty, string>
    {
        /// <summary>
        /// set the bound password property from <see cref="PasswordBox"/> element 
        /// </summary>
        /// <param name="sender">the ui element <see cref="PasswordBox"/> that has the password changed </param>
        public static void SetValue(DependencyObject sender)
        {
            if (string.Equals(((PasswordBox)sender).Password, GetValue(sender)))
                return; // and this is how we prevent infinite recursion

            SetValue((PasswordBox)sender, ((PasswordBox)sender).Password);

        }
    }
}
