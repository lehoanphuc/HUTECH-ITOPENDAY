using JobFair.DomainModels;
using JobFair.Shared.Constants;
using JobFair.Shared.Utilities;
using JobFair.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JobFair.Services.BUS
{
    public class UserBUS : BaseBUS
    {
        public PagedDataViewModel GetList(int idRole, int page = 0, int itemPerPage = 25)
        {
            var result = new PagedDataViewModel();
            var list = new List<UserViewModel>();

            // Search by vendor and env
            var listDB = db.USERs.AsNoTracking()
                .Where(x => x.IsDeleted != true &&
                    x.IdRole == idRole)
                .OrderBy(x => x.Username);

            if (idRole == (int)RoleKeys.COMPANY)
            {
                listDB = listDB.OrderBy(x => x.IdCompany);
            }

            result.TotalRecord = listDB.Count();
            result.TotalPage = (int)Math.Ceiling((float)result.TotalRecord / itemPerPage);

            IQueryable<USER> listPaged = listDB;
            if (page > 0)
            {
                listPaged = listDB.Skip((page - 1) * itemPerPage).Take(itemPerPage);
            }

            // Convert to object
            foreach (var item in listPaged.ToList())
            {
                var model = new UserViewModel(item);
                list.Add(model);
            }

            result.Data = list;
            return result;
        }

        public void Delete(int id)
        {
            var model = db.USERs.Where(x => x.IdUser == id).FirstOrDefault();
            if (model is null)
            {
                throw new Exception("Không tìm thấy tài khoản này");
            }

            model.IsDeleted = true;
            db.SaveChanges();
        }

        public UserViewModel Get(int id)
        {
            var model = db.USERs.AsNoTracking()
                .Where(x => x.IdUser == id && x.IsDeleted != true)
                .FirstOrDefault();
            if (model is null) throw new Exception("Not found");
            return new UserViewModel(model);
        }

        public string RandomPassword(int id)
        {
            // Make sure user was existed
            var model = db.USERs.Where(x => x.IdUser == id && x.IsDeleted != true).FirstOrDefault();

            // Random password
            var newPass = RandomHelper.RandomString(10);
            model.Password = MD5Helper.Hash(newPass);
            db.SaveChanges();

            return newPass;
        }


        /// <summary>
        /// Remember: password is plain password, not hash
        /// </summary>
        /// <param name="username"></param>
        /// <param name="fullname"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int SaveUser(int id, string username, string fullname, string sclass, string email, string phone, string password, int role, int? idCompany = null)
        {
            username = username.ToLower();

            var user = db.USERs.Where(x => id == x.IdUser).FirstOrDefault();

            if (user is null)
            {
                // Create model and add to db
                user = new USER();
                db.USERs.Add(user);
            }

            user.Username = username;
            user.Fullname = fullname;
            user.Email = email;
            user.Phone = phone;
            user.IdRole = role;
            user.ClassName = sclass;
            user.IdCompany = idCompany;

            if (!string.IsNullOrEmpty(password))
            {
                // Hash password
                password = MD5Helper.Hash(password);
                user.Password = password;
            }

            db.SaveChanges();

            return user.IdUser;
        }

        public int SaveStudent(StudentUserViewModel data)
        {
            if (!string.IsNullOrEmpty(data.Password))
            {
                if (!data.Password.Equals(data.Repassword))
                {
                    throw new Exception("Mật khẩu và xác nhận lại mật khẩu không trùng khớp");
                }
            }

            // Check username is exist or not
            var user = db.USERs.Where(x => x.IsDeleted != true && x.Username.ToLower().Equals(data.Username.ToLower())).FirstOrDefault();
            if (user != null)
            {
                throw new Exception("Tài khoản này đã có người sử dụng, vui lòng chọn tài khoản khác");
            }

            return SaveUser(0, data.Username, data.Fullname, data.Class, data.Email, data.Phone, data.Password, (int)RoleKeys.STUDENT);
        }

        public int SaveCompany(UserViewModel data)
        {
            // Check username is exist or not
            var user = db.USERs.Where(x => x.IsDeleted != true && x.IdUser != data.Id && x.Username.ToLower().Equals(data.Username.ToLower())).FirstOrDefault();
            if (user != null)
            {
                throw new Exception("Tài khoản này đã có người sử dụng, vui lòng chọn tài khoản khác");
            }

            return SaveUser(data.Id, data.Username, data.Fullname, null, data.Email, data.Phone, data.Password, (int)RoleKeys.COMPANY, data.IdCompany);
        }

        public bool CheckToken(string token)
        {
            token = token.ToLower();
            return db.USERs.Any(x => x.Token.ToLower().Equals(token));
        }

        public int GetRoleByUsername(string username)
        {
            var user = db.USERs.Where(x => x.Username.Equals(username)).FirstOrDefault();

            if (user is null)
            {
                return -1;
            }

            return user.IdRole;
        }

        public string ResetPassword(string username, string email)
        {
            username = username.ToLower();
            email = email.ToLower();
            var user = db.USERs.Where(x => x.Username.ToLower().Equals(username)
                                            && x.Email.ToLower().Equals(email))
                .FirstOrDefault();

            if (user is null)
            {
                throw new Exception("Không tìm thấy tài khoản ứng với email này");
            }

            // Create token
            var randomInt = (new Random()).Next(100000, 999999);
            var token = DateTime.Now.ToFileTimeUtc().ToString() + randomInt;
            token = MD5Helper.Hash(token + username);
            user.Token = token;
            db.SaveChanges();

            return token;
        }

        public void ChangePassword(string username, string currentpassword, string newpassword)
        {
            username = username.ToLower();
            currentpassword = MD5Helper.Hash(currentpassword);
            newpassword = MD5Helper.Hash(newpassword);

            currentpassword = currentpassword.ToLower();
            var user = db.USERs.Where(x => x.Username.ToLower().Equals(username) &&
                 x.Password.ToLower().Equals(currentpassword))
                .FirstOrDefault();

            if (user is null)
            {
                throw new Exception("Mật khẩu hiện tại không đúng");
            }

            user.Password = newpassword;
            db.SaveChanges();
        }

        public void SaveResetPassword(string userToken, string password)
        {
            userToken = userToken.ToLower();
            var user = db.USERs.Where(x => x.Token.ToLower().Equals(userToken))
                .FirstOrDefault();

            if (user is null)
            {
                throw new Exception("Không tìm thấy tài khoản ứng với email này");
            }

            user.Password = MD5Helper.Hash(password);
            user.Token = null;
            db.SaveChanges();
        }

        public UserViewModel GetUserByUsername(string username)
        {
            username = username.ToLower();
            var user = db.USERs.Where(x => x.Username.ToLower().Equals(username))
                .FirstOrDefault();

            if (user is null)
            {
                throw new Exception("Không tìm thấy tài khoản này");
            }

            return new UserViewModel(user);
        }
    }
}
