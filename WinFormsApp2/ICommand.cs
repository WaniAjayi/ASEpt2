using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp2
{
    public interface ICommand
    {
        /// <summary>
        /// Executes the command with the given parameters.
        /// </summary>
        /// <param name="parameters">An array of objects representing the parameters needed for execution.</param>
        void Execute(object[] parameters);
    }
}
