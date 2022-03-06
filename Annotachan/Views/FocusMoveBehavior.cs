using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Annotachan.Views {
    public class FocusMoveBehavior : Behavior<UIElement> {
        public Key Key {
            get;
            set;
        }
        protected override void OnAttached() {
            this.AssociatedObject.PreviewKeyDown += AssociatedObject_PreviewKeyDown;
        }
        protected override void OnDetaching() {
            this.AssociatedObject.PreviewKeyDown -= AssociatedObject_PreviewKeyDown;
        }
        private void AssociatedObject_PreviewKeyDown(object sender, KeyEventArgs e) {
            if (e.Key != this.Key) {
                return;
            }
            (e.OriginalSource as UIElement)?.MoveFocus(new TraversalRequest(Keyboard.Modifiers == ModifierKeys.Shift ? FocusNavigationDirection.Previous : FocusNavigationDirection.Next));
        }
    }
}
