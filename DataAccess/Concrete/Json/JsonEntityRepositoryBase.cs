using DataAccess.Abstract;
using Entities.Abstract;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Json
{
    public abstract class JsonEntityRepositoryBase<TEntity> : IEntityRepository<TEntity> where TEntity : class, IEntity, new()
    {
        protected string _path;

        public List<TEntity> GetAll()
        {
            using (StreamReader r = new StreamReader(_path))
            {
                var json = r.ReadToEnd();
                var items = JsonSerializer.Deserialize<List<TEntity>>(json);
                return items;
            }
        }

        public void Add(TEntity entity)
        {
            List<TEntity> _data = new List<TEntity>();
            _data = GetAll();
            _data.Add(entity);

            string json = JsonSerializer.Serialize(_data);
            File.WriteAllText(_path, json);
        }
    }
}
