using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfotecsIntershipMVC.DAL.Models
{
    public class FileEntity
    {
        public FileEntity(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("Empty file or file name!");

            Name = name;
        }
        private FileEntity(Guid fileId, string name, IEnumerable<RecordEntity>? records)
        {
            FileID = fileId;
            Name = name;
            Records = records;
        }


        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid FileID { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "Name of file must contain 255 characters by standards"),
            MinLength(1, ErrorMessage = "Name of file must have at least 1 symbol")]
        public string Name { get; set; }

        public IEnumerable<RecordEntity>? Records { get; set; }


        public FileEntityBuilder ToBuilder()
        {
            var builder = new FileEntityBuilder();
            builder.WithId(FileID);
            builder.WithName(Name);
            builder.WithRecords(Records);
            return builder;
        }


        public class FileEntityBuilder
        {
            private Guid _fileId;
            private string _name;
            private IEnumerable<RecordEntity> _records;

            public FileEntityBuilder WithId(Guid id)
            {                
                _fileId = id;
                return this;
            }
            public FileEntityBuilder WithName(string name)
            {
                if (name == default || string.IsNullOrEmpty(name))
                    throw new ArgumentNullException("Invalid file name!"); 

                _name = name;
                return this;
            }
            public FileEntityBuilder WithRecords(IEnumerable<RecordEntity> recordEntities)
            {
                if (recordEntities == null || recordEntities.ToList().Count == 0)
                    recordEntities = new List<RecordEntity>();

                _records = recordEntities;
                return this;
            }

            public FileEntity Build()
            {
                var finalFileEntity = new FileEntity(_fileId, _name, _records);
                return finalFileEntity;
            }

        }
    }
}
