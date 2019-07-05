﻿namespace BLL
{
    using System.Collections.Generic;
    using System.Linq;

    public static class ProjectUserService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        ///获取项目用户信息
        /// </summary>
        /// <returns></returns>
        public static Model.Project_ProjectUser GetProjectUserById(string projectUserId)
        {
            return Funs.DB.Project_ProjectUser.FirstOrDefault(e => e.ProjectUserId == projectUserId);
        }

        /// <summary>
        ///获取项目用户信息
        /// </summary>
        /// <returns></returns>
        public static Model.Project_ProjectUser GetProjectUserByUserIdProjectId(string projectId, string userId)
        {
            return Funs.DB.Project_ProjectUser.FirstOrDefault(e => e.ProjectId == projectId && e.UserId == userId);
        }

        /// <summary>
        ///获取项目用户信息 根据用户ID
        /// </summary>
        /// <returns></returns>
        public static List<Model.Project_ProjectUser> GetProjectUserByUserId(string userId)
        {
            return (from x in Funs.DB.Project_ProjectUser where x.UserId == userId select x).ToList();
        }

        /// <summary>
        /// 增加项目用户信息
        /// </summary>
        /// <returns></returns>
        public static void AddProjectUser(Model.Project_ProjectUser projectUser)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Project_ProjectUser newProjectUser = new Model.Project_ProjectUser
            {
                ProjectUserId = SQLHelper.GetNewID(typeof(Model.Project_ProjectUser)),
                ProjectId = projectUser.ProjectId,
                UserId = projectUser.UserId,
                UnitId = projectUser.UnitId,
                RoleId = projectUser.RoleId,
                IsPost = projectUser.IsPost
            };
            //newProjectUser.RoleName = projectUser.RoleName;
            db.Project_ProjectUser.InsertOnSubmit(newProjectUser);
            db.SubmitChanges();
        }

        /// <summary>
        ///修改项目用户信息 
        /// </summary>
        /// <param name="projectUser"></param>
        public static void UpdateProjectUser(Model.Project_ProjectUser projectUser)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Project_ProjectUser newProjectUser = db.Project_ProjectUser.FirstOrDefault(e => e.ProjectUserId == projectUser.ProjectUserId);
            if (newProjectUser != null)
            {
                newProjectUser.ProjectId = projectUser.ProjectId;
                newProjectUser.UserId = projectUser.UserId;
                newProjectUser.UnitId = projectUser.UnitId;
                newProjectUser.RoleId = projectUser.RoleId;
                newProjectUser.IsPost = projectUser.IsPost;
                //newProjectUser.RoleName = projectUser.RoleName;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据项目Id删除一个项目用户信息
        /// </summary>
        /// <param name="projectUserId"></param>
        public static void DeleteProjectUserById(string projectUserId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Project_ProjectUser delProjectUser = db.Project_ProjectUser.FirstOrDefault(e => e.ProjectUserId == projectUserId);
            if (delProjectUser != null)
            {
                db.Project_ProjectUser.DeleteOnSubmit(delProjectUser);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据项目用户Id删除一个项目用户信息
        /// </summary>
        /// <param name="projectUserId"></param>
        public static void DeleteProjectUserByProjectIdUserId(string projectId, string userId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var delProjectUser =from x in db.Project_ProjectUser where x.ProjectId == projectId && x.UserId == userId select x;
            if (delProjectUser.Count() > 0)
            {
                db.Project_ProjectUser.DeleteAllOnSubmit(delProjectUser);
                db.SubmitChanges();
            }
        }  
    }
}
