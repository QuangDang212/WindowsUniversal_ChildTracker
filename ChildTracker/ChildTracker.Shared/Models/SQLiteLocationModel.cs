using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChildTracker.Models
{
    [Table("Locations")]
   public class SQLiteLocationModel
    {       
           [PrimaryKey, AutoIncrement]
           public int ID { get; set; }

           
           public double Latitude { get; set; }

           public double Longitude { get; set; }

           public string UserId { get; set; }

           public DateTime LocationDate { get; set; }
       
    }
}
