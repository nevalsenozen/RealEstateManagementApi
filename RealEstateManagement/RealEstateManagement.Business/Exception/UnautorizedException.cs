using System;

namespace RealEstateManagement.Business.Exceptions;

public class UnauthorizedException : BusinessException
{
    public UnauthorizedException(string message = "Yetkisiz erişim!") : base(message, 401, "UNAUTHORIZED_ERROR")
    {

    }
}
