using System;
using System.Windows;

namespace RRMDesktopShell.AttachedProperties
{
    /// <summary>
    /// A base attached property to replace the vanilla wpf attached property
    /// </summary>
    /// <typeparam name="TParent">the parent class to be the attached property</typeparam>
    /// <typeparam name="TProperty">the type of this attached property</typeparam>
    public class BaseAttachedProperty<TParent, TProperty> where TParent : BaseAttachedProperty<TParent, TProperty>, new()
    {
        #region Public Events
        /// <summary>
        /// fired when the value changes
        /// </summary>
        public event Action<DependencyObject, DependencyPropertyChangedEventArgs> ValueChanged = (o, args) => { }; 

        #endregion

        #region Public Properties
        /// <summary>
        /// A singleton instance of our parent class
        /// </summary>
        public static TParent Instance { get; private set; } = new TParent();

        #endregion

        #region Attached Property Definitions
        /// <summary>
        /// the attached property for this class 
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached(
            "Value", typeof(TProperty), typeof(BaseAttachedProperty<TParent,TProperty>), new PropertyMetadata(default(TProperty), OnValuePropertyChanged));
        /// <summary>
        /// the callback event when the <see cref="ValueProperty"/> is changed
        /// </summary>
        /// <param name="d">the UI element that had it's property changed</param>
        /// <param name="e">events arguments</param>
        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // call parent function
            Instance.OnValueChanged(d,e);
            // call event listener
            Instance.ValueChanged(d, e);
        }
        /// <summary>
        /// sets the attached property
        /// </summary>
        /// <param name="element">the element to get the property from</param>
        /// <param name="value">the value to set the property to</param>
        public static void SetValue(DependencyObject element, TProperty value) => element.SetValue(ValueProperty, value);
       
        /// <summary>
        /// gets the attached property 
        /// </summary>
        /// <param name="element">the element to get the property from</param>
        /// <returns></returns>
        public static TProperty GetValue(DependencyObject element) => (TProperty) element.GetValue(ValueProperty);


        #endregion

        #region Events Methods
        /// <summary>
        /// method called when any attached property of this type is changed
        /// </summary>
        /// <param name="sender">the ui element that this property was changed for</param>
        /// <param name="e">arguments of the event</param>
        public virtual void OnValueChanged(DependencyObject sender,DependencyPropertyChangedEventArgs e) { }

        #endregion
    }
}
