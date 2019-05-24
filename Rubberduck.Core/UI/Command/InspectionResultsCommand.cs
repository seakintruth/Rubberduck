﻿using System.Runtime.InteropServices;
using Rubberduck.UI.Inspections;

namespace Rubberduck.UI.Command
{
    /// <summary>
    /// A command that runs all active code inspections for the active VBAProject.
    /// </summary>
    [ComVisible(false)]
    public class InspectionResultsCommand : CommandBase
    {
        private readonly InspectionResultsDockablePresenter _presenter;

        public InspectionResultsCommand(InspectionResultsDockablePresenter presenter)
        {
            _presenter = presenter;
        }

        /// <summary>
        /// Runs code inspections 
        /// </summary>
        /// <param name="parameter"></param>
        protected override void OnExecute(object parameter)
        {
            _presenter.Show();
        }
    }
}
