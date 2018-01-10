using System;
using System.ComponentModel;

namespace GraphBuilder.Shell.Models
{
    public enum Direction
    {
        From,
        To
    }

    public enum ElementState
    {
        None,
        NodeFrom,
        NodeTo,
        Added
    }

    public enum Arrow
    {
        None,
        Arrow,
        Diamond
    }

    /// <summary>
    /// Indicates which end of the line has an arrow.
    /// </summary>
    [Flags]
    public enum ArrowEnds
    {
        None = 0,
        Start = 1,
        End = 2,
        Both = 3
    }

    public enum EndSymbol
    {
        None,
        SimpleArrow,
        EmptyTriangle,
        FilledTriangle,
        EmptyRhombus,
        FilledRhumbus
    }

    public enum ShapeType
    {
        Ellipse,
        Rectangle,
        Image,
        Text,
        Document,
        Video
    }

    public enum DocumentMode
    {
        Empty,
        Auto,
        Opened,
        Imported
    }

    public enum MouseMode
    {
        /// <summary>
        /// User draws a link
        /// </summary>
        DrawLink,

        /// <summary>
        /// User draws a node
        /// </summary>
        DrawNode,

        /// <summary>
        /// Element can be selected
        /// </summary>
        Select,

        /// <summary>
        /// Multiple elements can be selected
        /// </summary>
        MultiSelect,

        /// <summary>
        /// Element can be dragged
        /// </summary>
        Dragging,

        /// <summary>
        /// Element can be resized
        /// </summary>
        Resize,

        /// <summary>
        /// The user is left-mouse-button-dragging to pan the viewport.
        /// </summary>
        Panning,

        /// <summary>
        /// User turn mouse wheel to zoom in or out.
        /// </summary>
        Zooming
    }

    /// <summary>
    /// Mouse position within shape
    /// </summary>
    public enum HitType
    {
        None,
        Body,
        UpperLeft,
        UpperRight,
        LowerRight,
        LowerLeft,
        Left,
        Right,
        Top,
        Bottom
    };
}