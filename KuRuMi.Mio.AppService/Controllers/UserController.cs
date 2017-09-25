using KuRuMi.Mio.BootStarp.IServiceImpl;
using KuRuMi.Mio.DoMain.Infrastructure.IocManager;
using KuRuMi.Mio.DoMain.Infrastructure.ModelDTO;
using System;
using System.Web.Http;
using System.Net.Http;
using KuRuMi.Mio.AppService.Common;
using System.Collections.Generic;
using KuRuMi.Mio.DoMain.Infrastructure.Logger;

namespace KuRuMi.Mio.AppService.Controllers
{
    /// <summary>
    /// 用户Api
    /// </summary>
    public class UserController : ApiController
    {
        protected UserServiceImpl server = null;
        public UserController()
        {
            server = IocManager.Instance.Resolve<UserServiceImpl>();
            server.GetValues();
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="udto"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage UserRegist(Sys_UserDTO udto)
        {
            try
            {
                var obj = server.GetAllUserList(udto);
                if (obj)
                {
                    //注册前先查询注册的账户是否存在
                    server.UserRegist(udto);
                    return HttpResponseExtension.toJson("注册成功!");
                }
                else
                {
                    return HttpResponseExtension.toJson("请检查用户名和邮箱");
                }
            }
            catch (Exception e)
            {
                return HttpResponseExtension.toJson("注册失败");
            }

        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<string> UserLogin(dynamic obj)
        {
            List<string> ls = new List<string>();
            var email = Convert.ToString(obj.email);
            var password = Convert.ToString(obj.passWord);
            var dto = server.CheckLogin(email, password);
            ls.Add(dto.userName);
            ls.Add(Convert.ToString(dto.Id)); 
            if (dto.userName == null || dto.userName == "")
                return null;
            else
                return ls;
        }

        /// <summary>
        /// 修改账户信息
        /// </summary>
        /// <param name="udto"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Modify(Sys_UserDTO udto) {
            try
            {
                server.Modify(udto);
                return HttpResponseExtension.toJson(new { info = "成功", content = "恭喜你修改成功了哦！" });
            }
            catch (Exception ex)
            {
                UnitExtension.Log(ex);
                return HttpResponseExtension.toJson(new { info = "错误", content = "更新信息出错啦！" });
            }
        }

        /// <summary>
        /// 更具用户ID获取信息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetUserInfoById(dynamic obj)
        {
            return HttpResponseExtension.toJson("text");
        }
    }
}
