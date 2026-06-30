using System.Linq.Expressions;

namespace SmartHouseManagment.Domain.Spec;

public interface ISpecification<T>
{
    List<Expression<Func<T, bool>>> Criterias { get; }
}