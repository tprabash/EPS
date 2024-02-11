using System.Collections.Generic;
using System.Linq;
using API.Entities.Admin;

namespace API.Helpers
{
    public static class ExtensionMethods
    {
        public static IEnumerable<MstrAgents> WithoutPasswords(this IEnumerable<MstrAgents> users) {
            return users.Select(x => x.WithoutPassword());
        }

        public static MstrAgents WithoutPassword(this MstrAgents user) {
            user.cPassword = null;
            return user;
        }
    }
}