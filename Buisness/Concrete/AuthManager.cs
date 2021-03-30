using Buisness.Abstract;
using Buisness.Constrant;
using Core.Concrete.Entities;
using Core.Hashing;
using Core.Utilities;
using Core.Utilities.Jwt;
using Core.Utilities.Results;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness.Concrete
{
    public class AuthManager : IAuthService
    {
        IUserService _userService;
        ITokenHelper _tokenHelper;
        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<AccessToken> CreateToken(User user)
        {
           var accesstoken = _tokenHelper.CreateToken(user,_userService.GetClaims(user).Data);
            return new SuccessDataResult<AccessToken>(accesstoken,Messages.AccessTokenCreated);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
           var user = _userService.GetByEmail(userForLoginDto.Email);
            if (user == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            if (!HashHelper.VerifyHash(userForLoginDto.Password,user.Data.PasswordHash,user.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordWrong);
            }
            return new SuccessDataResult<User>(Messages.LoginSuccess);
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashHelper.CreateHash(userForRegisterDto.Password,out passwordHash,out passwordSalt);
            User user = new User()
            {
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                Email = userForRegisterDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user,Messages.UserAdded);
        }

        public IResult UserExist(string email)
        {
            var user = _userService.GetByEmail(email);
            if (user != null)
            {
                return new ErrorResult(Messages.UserAlreadyExist);
            }
            return new SuccessResult();
                
        }
    }
}
