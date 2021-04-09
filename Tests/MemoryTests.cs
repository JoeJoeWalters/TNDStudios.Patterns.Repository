using System;
using Xunit;
using TNDStudios.Patterns.Repository.Module;
using FluentAssertions;

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

            // ACT
            Boolean upsertResult = _repository.Upsert(domain);
            if (upsertResult)
            {
                Boolean deleteResult = _repository.Delete(domain.Id);
                if (deleteResult)
                {
                    resultObject = _repository.Get(domain.Id);
                }
            }

            // ASSERT
            upsertResult.Should().BeTrue();
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
    }
}
