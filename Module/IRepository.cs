﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TNDStudios.Patterns.Repository.Module
{
    public class RepositoryDocument
    {
        public virtual String Id { get; set; }
    }

    public class RepositoryDomainObject
    {
        public virtual String Id { get; set; }
    }

    public interface IRepository<TDomain, TDocument> 
        where TDocument: RepositoryDocument 
        where TDomain: RepositoryDomainObject
    {
        TDomain ToDomain(TDocument document);
        TDocument ToDocument(TDomain domain);

        TDomain Get(String id);
        IEnumerable<TDomain> Query(String query);
        Boolean Delete(String id);
        Boolean Upsert(TDomain item);
    }
}
