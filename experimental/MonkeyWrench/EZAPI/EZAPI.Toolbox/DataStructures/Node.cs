using System;
using System.Diagnostics;

namespace EZAPI.Toolbox.DataStructures
{
    /// <summary>
    /// Represents a node
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DebuggerDisplay("Value = {Value}")]
    public sealed class Node<T>
    {
        /// <summary>
        /// Gets the Value
        /// </summary>
        public T Value { get; private set; }

        /// <summary>
        /// Gets next node
        /// </summary>
        public Node<T> Next { get; internal set; }

        /// <summary>
        /// Gets previous node
        /// </summary>
        public Node<T> Previous { get; internal set; }

        /// <summary>
        /// Initializes a new <see cref="Node"/> instance
        /// </summary>
        /// <param name="item">Value to be assigned</param>
        internal Node(T item)
        {
            this.Value = item;
        }
    }
}