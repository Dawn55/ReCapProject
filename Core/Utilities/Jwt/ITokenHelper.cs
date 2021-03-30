using Core.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Jwt
{
   public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);

    }
}
