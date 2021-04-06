using System;
using System.Collections.Generic;

namespace TNDStudios.Patterns.Repository.Module
{
    public class CosmosRepository<TDomain, TDocument> : IRepository<TDomain, TDocument>
        where TDocument : RepositoryDocument
        where TDomain : RepositoryDomainObject
    {
        public bool Delete(String id)
        {
            throw new NotImplementedException();
        }

        public TDomain Get(String id)
        {
            throw new NotImplementedException();
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
