using ORMToolkit.Core.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Runtime.CompilerServices;
using AutoMapper;
using AutoMapper.Mappers;

namespace OrmBenchmark.OrmToolkit
{
    public class AutoMapperObjectFactory : IObjectFactory1
    {
        public AutoMapperObjectFactory()
        {
            Mapper.Initialize(cfg => { });
            MapperRegistry.Mappers.Add(new DataReaderMapper());
        }
        
        public object CreateObject(IDataReader reader, Type objectType)
        {
            throw new NotImplementedException();
        }

        public T CreateObject<T>(IDataReader reader) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> CreateObjects<T>(IDataReader reader) where T : class, new()
        {
            return Mapper.DynamicMap<IDataReader, List<T>>(reader);
        }
    }
}
