﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace GraphBuilder.Shell.Models
{
    /// <summary>
    /// This class provides the ability to pan the target object when dragging the mouse 
    /// </summary>
    internal class Pan
    {
        private bool _dragging;
        private FrameworkElement _target;
        private MapZoom _zoom;
        private bool _captured;
        private Panel _container;
        private Point _mouseDownPoint;
        private Point _startTranslate;
        private ModifierKeys _mods = ModifierKeys.None;

        /// <summary>
        /// Construct new Pan gesture object.
        /// </summary>
        /// <param name="target">The target to be panned, must live inside a container Panel</param>
        /// <param name="zoom"></param>
        public Pan(FrameworkElement target, MapZoom zoom)
        {
            this._target = target;
            this._container = target.Parent as Panel;
            if (this._container == null)
            {
                // todo: localization
                throw new ArgumentException("Target object must live in a Panel");
            }
            this._zoom = zoom;
            _container.MouseLeftButtonDown += new MouseButtonEventHandler(OnMouseLeftButtonDown);
            _container.MouseLeftButtonUp += new MouseButtonEventHandler(OnMouseLeftButtonUp);
            _container.MouseMove += new MouseEventHandler(OnMouseMove);
        }

        /// <summary>
        /// Handle mouse left button event on container by recording that position and setting
        /// a flag that we've received mouse left down.
        /// </summary>
        /// <param name="sender">Container</param>
        /// <param name="e">Mouse information</param>
        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            ModifierKeys mask = Keyboard.Modifiers & _mods;
            if (!e.Handled && mask == _mods && mask == Keyboard.Modifiers)
            {
                this._container.Cursor = Cursors.Hand;
                _mouseDownPoint = e.GetPosition(this._container);
                Point offset = _zoom.Offset;
                _startTranslate = new Point(offset.X, offset.Y);
                _dragging = true;
            }
        }

        /// <summary>
        /// Handle the mouse move event and this is where we capture the mouse.  We don't want
        /// to actually start panning on mouse down.  We want to be sure the user starts dragging
        /// first.
        /// </summary>
        /// <param name="sender">Mouse</param>
        /// <param name="e">Move information</param>
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (this._dragging)
            {
                if (!_captured)
                {
                    _captured = true;
                    _target.Cursor = Cursors.Hand;
                    Mouse.Capture(this._target, CaptureMode.SubTree);
                }
                this.MoveBy(_mouseDownPoint - e.GetPosition(this._container));
            }
        }

        /// <summary>
        /// Handle the mouse left button up event and stop any panning.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_captured)
            {
                Mouse.Capture(this._target, CaptureMode.None);
                e.Handled = true;
                _target.Cursor = Cursors.Arrow;
                _captured = false;
            }

            _dragging = false;
        }

        /// <summary>
        /// Move the target object by the given delta delative to the start scroll position we recorded in mouse down event.
        /// </summary>
        /// <param name="v">A vector containing the delta from recorded mouse down position and current mouse position</param>
        public void MoveBy(Vector v)
        {
            _zoom.Offset = new Point(_startTranslate.X - v.X, _startTranslate.Y - v.Y);
            _target.InvalidateVisual();
        }
    }
}