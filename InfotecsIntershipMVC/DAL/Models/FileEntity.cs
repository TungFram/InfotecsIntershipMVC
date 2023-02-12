using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfotecsIntershipMVC.DAL.Models
{
    public class FileEntity
    {
        public FileEntity(string name, IEnumerable<RecordEntity>? records)
        {
            if (string.IsNullOrEmpty(name) || records == null || records.ToList().Count == 0)
                throw new ArgumentNullException("Empty file or file name!");

            Name = name;
            Records = records;
        }
        private FileEntity(
            Guid fileId,
            string name,
            IEnumerable<RecordEntity>? records)
        {
            FileID = fileId;
            Name = name;
            Records = records;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid FileID { get; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public IEnumerable<RecordEntity>? Records { get; }


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
            private Guid _fileID;
            private string _name;
            private IEnumerable<RecordEntity> _records;

            public FileEntityBuilder WithId(Guid id)
            {
                if (id == default(Guid)) 
                    throw new ArgumentNullException("Invalid id!");
                
                _fileID = id;
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
                    throw new ArgumentNullException("Invalid records!");

                _records = recordEntities;
                return this;
            }

            public FileEntity Build()
            {
                var finalFileEntity = new FileEntity(_fileID, _name, _records);
                return finalFileEntity;
            }

        }
    }
}
