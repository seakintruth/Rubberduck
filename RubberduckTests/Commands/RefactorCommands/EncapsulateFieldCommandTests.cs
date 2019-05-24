﻿using Moq;
using NUnit.Framework;
using Rubberduck.Interaction;
using Rubberduck.Parsing.Rewriter;
using Rubberduck.Parsing.VBA;
using Rubberduck.Refactorings;
using Rubberduck.Refactorings.EncapsulateField;
using Rubberduck.UI.Command;
using Rubberduck.UI.Command.Refactorings;
using Rubberduck.UI.Command.Refactorings.Notifiers;
using Rubberduck.VBEditor;
using Rubberduck.VBEditor.SafeComWrappers.Abstract;
using Rubberduck.VBEditor.Utility;

namespace RubberduckTests.Commands.RefactorCommands
{
    public class EncapsulateFieldCommandTests : RefactorCodePaneCommandTestBase
    {
        [Category("Commands")]
        [Test]
        public void EncapsulateField_CanExecute_LocalVariable()
        {
            const string input =
                @"Sub Foo()
    Dim d As Boolean
End Sub";
            var selection = new Selection(2, 9, 2, 9);
            Assert.IsFalse(CanExecute(input, selection));
        }

        [Category("Commands")]
        [Test]
        public void EncapsulateField_CanExecute_Proc()
        {
            const string input =
                @"Dim d As Boolean
Sub Foo()
End Sub";
            var selection = new Selection(2, 7, 2, 7);
            Assert.IsFalse(CanExecute(input, selection));
        }

        [Category("Commands")]
        [Test]
        public void EncapsulateField_CanExecute_Field()
        {
            const string input =
                @"Dim d As Boolean
Sub Foo()
End Sub";
            var selection = new Selection(1, 5, 1, 5);
            Assert.IsTrue(CanExecute(input, selection));
        }

        protected override CommandBase TestCommand(IVBE vbe, RubberduckParserState state, IRewritingManager rewritingManager, ISelectionService selectionService)
        {
            var msgBox = new Mock<IMessageBox>().Object;
            var factory = new Mock<IRefactoringPresenterFactory>().Object;
            var refactoring = new EncapsulateFieldRefactoring(state, null, factory, rewritingManager, selectionService);
            var notifier = new EncapsulateFieldFailedNotifier(msgBox);
            return new RefactorEncapsulateFieldCommand(refactoring, notifier, state, selectionService);
        }

        protected override IVBE SetupAllowingExecution()
        {
            const string input =
                @"Dim d As Boolean
Sub Foo()
End Sub";
            var selection = new Selection(1, 5, 1, 5);
            return TestVbe(input, selection);
        }
    }
}
