﻿using Rubberduck.Parsing.Rewriter;
using Rubberduck.VBEditor.Utility;
using System;
using Rubberduck.Parsing.Symbols;
using Rubberduck.Refactorings.Exceptions;

namespace Rubberduck.Refactorings
{
    public abstract class InteractiveRefactoringBase<TPresenter, TModel> : RefactoringBase 
        where TPresenter : class, IRefactoringPresenter<TModel>
        where TModel : class, IRefactoringModel
    {
        private readonly Func<TModel, IDisposalActionContainer<TPresenter>> _presenterFactory;

        protected InteractiveRefactoringBase(IRewritingManager rewritingManager, ISelectionService selectionService, IRefactoringPresenterFactory factory) 
        :base(rewritingManager, selectionService)
        {
            _presenterFactory = ((model) => DisposalActionContainer.Create(factory.Create<TPresenter, TModel>(model), factory.Release));
        }

        public override void Refactor(Declaration target)
        {
            Refactor(InitializeModel(target));
        }

        protected void Refactor(TModel initialModel)
        {
            var model = initialModel;
            if (model == null)
            {
                throw new InvalidRefactoringModelException();
            }

            using (var presenterContainer = _presenterFactory(model))
            {
                var presenter = presenterContainer.Value;
                if (presenter == null)
                {
                    throw new InvalidRefactoringPresenterException();
                }

                model = presenter.Show();
                if (model == null)
                {
                    throw new InvalidRefactoringModelException();
                }

                RefactorImpl(model);
            }
        }

        protected abstract TModel InitializeModel(Declaration target);
        protected abstract void RefactorImpl(TModel model);
    }
}