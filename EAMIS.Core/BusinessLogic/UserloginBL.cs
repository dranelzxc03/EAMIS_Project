using EAMIS.Common;
using EAMIS.Core.Contract;
using EAMIS.Core.Domain;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EAMIS.Core.BusinessLogic
{
    public class UserloginBL : IUserLoginBL
    {
        private EAMISEntities _eamisEntities = new EAMISEntities();
        public UserloginBL(EAMISEntities eamisEntities)
        {
            _eamisEntities = eamisEntities;
        }
        public async Task<UserloginDTO> GetUserInfo(int UserId)
        {

            var UserLogin = await _eamisEntities.EAMIS_Userlogin.Where(x => x.User_ID == UserId).Select(x => new UserloginDTO
            {
                UserId = x.User_ID,
                PermissionId = x.Permission_ID,
                Username = x.Username,
                Password = x.Password,
            }).FirstOrDefaultAsync();

            return UserLogin;
        }
        public static UserloginDTO MapToDTO(EAMIS_Userlogin userloginE)
        {
            UserloginDTO userlogin = new UserloginDTO();
            if (userloginE != null)
            {
                return new UserloginDTO
                {
                    UserId = userloginE.User_ID,
                    PermissionId = userloginE.Permission_ID,
                    Username = userloginE.Username,
                    Password = userloginE.Password
                };
            }

            return userlogin;
        }

        public static EAMIS_Userlogin MapToEntity(UserloginDTO model)
        {

            if (model == null) return new EAMIS_Userlogin();

            return new EAMIS_Userlogin
            {
                User_ID = model.UserId ?? 0,
                Permission_ID = model.PermissionId ?? 0,
                Username = model.Username,
                Password = model.Password,
            };
        }
        public async Task<UserloginDTO> Insert(UserloginDTO model)
        {
            EAMIS_Userlogin AddUser = new EAMIS_Userlogin();
            AddUser = MapToEntity(model);
            //_eamisEntities.EAMIS_Userlogin.Add(AddUser);
            _eamisEntities.Entry(AddUser).State = EntityState.Added;
            await _eamisEntities.SaveChangesAsync();
            return model;

        }


        public async Task<UserloginDTO> Update(int Id, UserloginDTO model)
        {
            var SelectedUserId = await _eamisEntities.EAMIS_Userlogin.Where(x => x.User_ID == Id).FirstOrDefaultAsync();
            SelectedUserId.User_ID = Id;
            SelectedUserId.Permission_ID = model.PermissionId ?? 0;
            SelectedUserId.Username = model.Username;
            SelectedUserId.Password = model.Password;
            //var UpdateUser = MapToEntity(model);
            //_eamisEntities.EAMIS_Userlogin.Remove(SelectedUserId);
            _eamisEntities.Entry(SelectedUserId).State = EntityState.Modified;
            //_eamisEntities.Entry(UpdateUser).State = EntityState.Deleted;
            _eamisEntities.SaveChanges();
            return model;

        }
        public async Task<UserloginDTO> Delete(int UserId, UserloginDTO model)
        {
            var SelectedUserId = await _eamisEntities.EAMIS_Userlogin.Where(x => x.User_ID == UserId).FirstOrDefaultAsync();
            //var items = MapToEntity(model);
            _eamisEntities.Entry(SelectedUserId).State = EntityState.Deleted;
            _eamisEntities.SaveChanges();
            return model;

        }
        
        public IQueryable<UserloginDTO> QuerytoDTO()
        {
            var lisofusers = _eamisEntities.EAMIS_Userlogin.AsNoTracking().Select(x => new UserloginDTO
            {
                UserId = x.User_ID,
                PermissionId = x.Permission_ID,
                Username = x.Username,
                Password = x.Password,
            });
            return lisofusers;
        }

        //public Expression<Func<EAMIS_Userlogin, bool>> BuildDynamicWhereClause(List<string> searchTerms)
        //{
        //    ExpressionStarter<EAMIS_Userlogin> predicate = PredicateBuilder.New<EAMIS_Userlogin>();
        //    foreach (var item in searchTerms)
        //    {
        //        predicate = predicate.Or(x => x.Username.Contains(item));
        //        predicate = predicate.Or(x => x.Password.Contains(item));
        //    }
        //    return predicate;
        //}
        public IQueryable<EAMIS_Userlogin> SearchEntities(UserloginDTO model)
        {
            UserloginDTO item = model;
            var predicate =  PredicateBuilder.True<EAMIS_Userlogin>();
            if (!string.IsNullOrEmpty(model.Username))
                predicate = predicate.And(x => x.Username == model.Username);
            if (!string.IsNullOrEmpty(model.Password))
                predicate = predicate.And(x => x.Password == model.Password);
            if (model.UserId != null)
                predicate = predicate.And(x => x.User_ID == model.UserId);
            if (model.PermissionId != null)
                predicate = predicate.And(x => x.Permission_ID == model.PermissionId);

            var result = _eamisEntities.EAMIS_Userlogin.Where(predicate);
            return result;
        }
    }
}