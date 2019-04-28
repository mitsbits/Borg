//using Borg.System.Backoffice.Security.Domain;

//namespace Borg.System.Backoffice.Security.Contracts
//{
//    public interface ISystemRoleProvider
//    {
//        Role[] Roles();
//    }

//    public class SystemRoleProvider : ISystemRoleProvider
//    {
//        public Role[] Roles()
//        {
//            return new Role[] {
//                SysAdmin(),
//                Reader(),
//                Writer(),
//                Configurer()
//            };
//        }

//        private Role SysAdmin()
//        {
//            var role = new Role() { Title = nameof(SysAdmin) };
//            return role;
//        }

//        private Role Reader()
//        {
//            var role = new Role() { Title = nameof(Reader) };
//            return role;
//        }

//        private Role Writer()
//        {
//            var role = new Role() { Title = nameof(Writer) };
//            return role;
//        }

//        private Role Configurer()
//        {
//            var role = new Role() { Title = nameof(Configurer) };
//            return role;
//        }
//    }
//}