using System;

namespace Test.WPF.UI.Services
{
    public class DialogResult<T> : EventArgs
    {
        public T Arg { get; set; }

        public DialogResult(T arg)
        {
            Arg = arg;
        }

        public static implicit operator DialogResult<T>(T arg) => new DialogResult<T>(arg);

        public static implicit operator T(DialogResult<T> arg) => arg.Arg;
    }
}