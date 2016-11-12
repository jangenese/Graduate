using SQLite;

namespace Graduate.Core.Data.Models
{
   public class GraduateEntityBase : IGraduateEntity
    {

        public GraduateEntityBase() {
        }


        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }

    
}
