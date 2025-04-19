using System.ComponentModel.DataAnnotations.Schema;

namespace AICenterAPI.Datas
{
    [Table("Role_Permissions")]
    public class RolePermission
    {
        public int RoleId { get; set; }

        public int PermissionId { get; set; }
    }
}
