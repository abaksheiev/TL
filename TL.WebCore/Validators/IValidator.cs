using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TL.WebCore.Validators
{
    public interface IValidator<T>
    {
        bool Validate(T entity, out List<string> errors);
    }
}
