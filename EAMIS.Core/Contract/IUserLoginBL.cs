using EAMIS.Common;
using EAMIS.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EAMIS.Core.Contract
{
    public interface IUserLoginBL
    {
        Task<UserloginDTO> GetUserInfo(int UserId);
        Task<UserloginDTO> Insert(UserloginDTO model);
        Task<UserloginDTO> Update(int Id, UserloginDTO model);
        Task<UserloginDTO> Delete(int UserId, UserloginDTO mode);
        IQueryable<EAMIS_Userlogin> SearchEntities(UserloginDTO model);
    }
}
