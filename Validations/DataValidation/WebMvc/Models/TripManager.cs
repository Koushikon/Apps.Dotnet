using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMvc.Models;

public class TripManager
{
    // [Key]: Denotes one or more properties that uniquely identify an entity.
    [Key]
    public int Id { get; set; }

    // [ForeignKey(<field_name>)]: Specifies the column that is displayed in the referred table as a foreign-key column.
    [ForeignKey("HotelId")]
    public Hotel Hotel { get; set; } = new Hotel();

    public int HotelId { get; set; }

    // [ForeignKey(<field_name>)]: Specifies the column that is displayed in the referred table as a foreign-key column.
    [ForeignKey("CarId")]
    public Car Car { get; set; } = new Car();

    public int CarId { get; set; }

    // [DataType(<type>)]: Specifies the name of an additional type to associate with a data field.
    [DataType(DataType.Date)]
    public DateTime CheckInDate { get; set; }

    [DataType(DataType.Date)]
    public DateTime CheckOutDate { get; set; }

    // [Range(<start>, <end>)]: Specifies the numeric range constraints for the value of a data field.
    [Column("TodaysPrice")]
    [Range(10.30, 46.60)]
    public double Price { get; set; }

    public Person Responsible { get; set; } = new Person();
}