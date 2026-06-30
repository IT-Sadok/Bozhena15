using System.Linq.Expressions;

namespace SmartHouseManagment.Domain.Spec;

public abstract class SpecificationBase<T> : ISpecification<T>
{
    public List<Expression<Func<T, bool>>> Criterias { get; } = new();

    protected ISpecification<T> ApplyFilter(Expression<Func<T, bool>> filter)
    {
        Criterias.Add(filter);
        
        return this;
    }
}