using System.Runtime.InteropServices;
using Rubberduck.UI.CodeExplorer;

namespace Rubberduck.UI.Command
{
    /// <summary>
    /// A command that displays the Code Explorer window.
    /// </summary>
    [ComVisible(false)]
    public class CodeExplorerCommand : CommandBase
    {
        private readonly CodeExplorerDockablePresenter _presenter;

        public CodeExplorerCommand(CodeExplorerDockablePresenter presenter)
        {
            _presenter = presenter;
        }

        protected override void OnExecute(object parameter)
        {
            _presenter.Show();
        }
    }
}
