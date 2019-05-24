﻿using Rubberduck.Parsing.Grammar;
using Rubberduck.VBEditor;

namespace Rubberduck.Parsing.Annotations
{
    public interface IAnnotation
    {
        AnnotationType AnnotationType { get; }
        QualifiedSelection QualifiedSelection { get; }
        bool AllowMultiple { get; }
        VBAParser.AnnotationContext Context { get; }
        int? AnnotatedLine { get; }
    }
}
