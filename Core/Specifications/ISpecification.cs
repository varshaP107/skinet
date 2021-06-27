using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria {get; } //Generic methods form: Expression takes function and fun takes a type T and returns boolean
        List<Expression<Func<T, object>>> Includes {get; }
 
    }
}