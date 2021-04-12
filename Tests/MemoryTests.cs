using System;
using Xunit;
using TNDStudios.Patterns.Repository.Module;
using FluentAssertions;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace TNDStudios.Patterns.Repository.Tests
{
    public class MemoryDomainObject : RepositoryDomainObject { }
    public class MemoryDocumentObject : RepositoryDocument { }

    public class MemoryTests
    {
        private MemoryRepository<MemoryDomainObject, MemoryDocumentObject> _repository;

        public MemoryTests()
        {
            _repository = new MemoryRepository<MemoryDomainObject, MemoryDocumentObject>(ToDocumentObject, ToDomainObject);
        }

        private MemoryDomainObject ToDomainObject(MemoryDocumentObject from)
            => new MemoryDomainObject()
            {
                Id = from.Id
            };

        private MemoryDocumentObject ToDocumentObject(MemoryDomainObject from)
            => new MemoryDocumentObject()
            {
                Id = from.Id
            };

        [Fact]
        public virtual void Add()
        {
            // ARRANGE
            MemoryDomainObject domain = new MemoryDomainObject() { };
            Boolean result = false;

            // ACT
            result = _repository.Upsert(domain);

            // ASSERT
            result.Should().BeTrue();
            domain.Id.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void Delete()
        {
            // ARRANGE
            MemoryDomainObject domain = new MemoryDomainObject() { };
            MemoryDomainObject resultObject = null;
            Boolean upsertResult = false;
            String upsertId = String.Empty;
            Boolean deleteResult = false;

            // ACT
            upsertResult = _repository.Upsert(domain);
            if (upsertResult)
            {
                upsertId = domain.Id;
                deleteResult = _repository.Delete(upsertId);
                if (deleteResult)
                {
                    resultObject = _repository.Get(upsertId);
                }
            }

            // ASSERT
            upsertResult.Should().BeTrue();
            upsertId.Should().NotBeEmpty();
            resultObject.Should().BeNull();
        }

        [Fact]
        public void Get()
        {
            // ARRANGE
            MemoryDomainObject domain = new MemoryDomainObject() { };
            MemoryDomainObject resultObject = null;

            // ACT
            Boolean upsertResult = _repository.Upsert(domain);
            if (upsertResult)
            {
                resultObject = _repository.Get(domain.Id);
            }

            // ASSERT
            upsertResult.Should().BeTrue();
            resultObject.Should().NotBeNull();
            domain.Id.Should().NotBeNull();
            resultObject.Id.Should().Be(domain.Id);
        }
        
        Expression<Func<MemoryDocumentObject, Boolean>> QueryById(String id)
            => q => q.Id == id;

        [Fact]
        public void Query()
        {
            // ARRANGE
            Expression<Func<MemoryDocumentObject, Boolean>> query;
            MemoryDomainObject domain = new MemoryDomainObject() { };
            IEnumerable<MemoryDomainObject> results = null;

            // ACT
            Boolean upsertResult = _repository.Upsert(domain);
            if (upsertResult)
            {
                query = QueryById(domain.Id);
                results = _repository.Query(query);
            }

            // ASSERT
            upsertResult.Should().BeTrue();
            results.Count().Should().NotBe(0);
            results.ToList()[0].Id.Should().Be(domain.Id);
        }
    }
}
