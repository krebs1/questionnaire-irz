using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

[Table("file")]
public class File
{
    [Column("id_file")]
    public Guid FileId { get; set; }
    
    [Column("name_file")]
    public string FileName { get; set; }
    
    [Column("extension_file")]
    public string FileExtension { get; set; }
    
    [Column("path_file")]
    public string FilePath { get; set; }
}