using EAMIS.Common;
using EAMIS.Core.Contract;
using EAMIS.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace EAMIS.WebApi.Areas.v1.Controllers
{   
    [Route("api/[controller]")]
    public class UserloginController : ApiController
    {
        IUserLoginBL _userLoginBL;
        public UserloginController(IUserLoginBL userLoginBL)
        {
           
            _userLoginBL = userLoginBL;
        }

        //[HttpGet]
        //[Route("SearchList")]
        //public  async Task<IEnumerable<EAMIS_Userlogin>> SearchList(UserloginDTO model)
        //{
        //    var result = await _userLoginBL.SearchEntities(model);
        //    if (result.Any())
        //    {
        //        return result;

        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}


        //[HttpGet]
        //[Route("SearchList")]
        //public async Task<IEnumerable<EAMIS_Userlogin>> SeachList(List<string> searchTerms);

        [HttpGet]
        [Route("UserId")]
        public async Task<UserloginDTO> GetUserId(int? UserId)
        {
            return await _userLoginBL.GetUserInfo(UserId ?? 0);

        }
        [HttpPost]
        [Route("Post")]
        public async Task<UserloginDTO> Post(UserloginDTO item)
        {
          
             var CreatedUser = await _userLoginBL.Insert(item);
            if (CreatedUser != null)
            {
                Ok(CreatedUser);
            }
            else
                NotFound();
                return CreatedUser;
        }
        [HttpPut]
        [Route("Put")]
        public async Task<UserloginDTO> Put(int Id, UserloginDTO item)
        {
            var UpdatedUser = await _userLoginBL.Update(Id, item);
            if (UpdatedUser != null)
            {
                Ok(UpdatedUser);
            }
            else
                NotFound();
                    return UpdatedUser;
        }
        [HttpDelete]
        [Route("Delete")]
        public async Task<UserloginDTO> Delete(int UserId,UserloginDTO item)
        {
            var DeletedUser = await _userLoginBL.Delete(UserId, item);
            if (DeletedUser != null)
            {
                Ok(DeletedUser);
            }
            else
                NotFound();
            return DeletedUser;
        }
        [HttpGet]
        [Route("SearchList")]
        public IQueryable<EAMIS_Userlogin> SearchList([FromUri]UserloginDTO item)
        {
            var SearchModel = _userLoginBL.SearchEntities(item);
            if (SearchModel != null)
            {
                Ok(SearchModel);
            }
            else
                NotFound();
            return SearchModel;
        }
       
    }
}


