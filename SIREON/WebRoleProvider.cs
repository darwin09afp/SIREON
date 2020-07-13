
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace SIREON
{
    public class WebRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            //using (var context = new SIREONEntities())
            //{
            //    var result = (from Usuario in context.Usuarios
            //                  join Role in context.Roles on Usuario.ID_Usuario equals Role.ID_Usuario
            //                  where Usuario.Usuario1 == username
            //                  select Role.Rol).ToArray();

            //    return result;
            //}

            using (SIREONEntitiess _Context = new SIREONEntitiess())
            {
                var userRoles = (from Usuario in _Context.Usuarios
                                 join R_Usuarios_Roles in _Context.R_Usuarios_Roles
                                 on Usuario.ID_Usuario equals R_Usuarios_Roles.ID_Usuario
                                 join role in _Context.Roles
                                 on R_Usuarios_Roles.ID_Rol equals role.ID_Rol
                                 where Usuario.Usuario1 == username
                                 select role.Rol).ToArray();
                return userRoles;
            }
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}