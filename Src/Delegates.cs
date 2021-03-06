using System;

namespace ExpertSokoban
{
    /// <summary>
    /// EventArgs for the ConfirmEventHandler delegate.
    /// </summary>
    sealed class ConfirmEventArgs : EventArgs
    {
        /// <summary>
        /// Set this to true/false to confirm or cancel the action.
        /// </summary>
        public bool ConfirmOK;
    }

    /// <summary>
    /// A general-purpose event type which lets the caller confirm an action.
    /// </summary>
    delegate void ConfirmEventHandler(object sender, ConfirmEventArgs e);
}
