using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetOrchestra.Server.Data
{
    [Table("orch_note")]
    public class Note
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("version")]
        public string? Version { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("app_data")]
        public byte[] AppData { get; set; }

        [Column("app_sdk_type")]
        public int AppSdkType { get; set; }
    }
}
