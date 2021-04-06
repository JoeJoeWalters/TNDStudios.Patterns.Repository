using System;
using System.Collections.Generic;

namespace TNDStudios.Patterns.Repository.Module
{
    public class MemoryRepository<TDomain, TDocument> : IRepository<TDomain, TDocument>
        where TDocument : RepositoryDocument
        where TDomain : RepositoryDomainObject
    {
        private readonly Dictionary<String, TDocument> _values;

        public MemoryRepository()
        {
            _values = new Dictionary<String, TDocument>();
        }

        public bool Delete(String id) =>
                _values.Remove(id);

        public TDomain Get(String id)
        {
            if (_values.ContainsKey(id))
            {
                return ToDomain(_values[id]);
            }

            return null;
        }

        public IEnumerable<TDomain> Query(string query)
        {
            throw new NotImplementedException();
        }

        public TDomain ToDomain(TDocument document)
        {
            throw new NotImplementedException();
        }

        public TDocument ToDocument(TDomain domain)
        {
            throw new NotImplementedException();
        }

        public bool Upsert(TDomain item)
        {
            throw new NotImplementedException();
        }
    }
}
