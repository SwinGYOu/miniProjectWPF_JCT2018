﻿using System;



[assembly: CLSCompliant(true)]
namespace DAL
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using BE;

    using DS;

    /// <summary>
    /// Defines the <see cref="Dal" />
    /// </summary>
    public class Dal : IDal
    {
        #region Methods

        public Result<Id> Add(Id item)
        {
            if (item.ID == 0)
                return new Result<Id>(item, new ArgumentNullException($"{nameof(item.ID)} of {item.GetTypeName} can't be 0.", nameof(item.ID)));
            if (DataSource.List.Contains(item))
                return new Result<Id>(item, new InvalidOperationException($"{item.GetTypeName} with ID: {item.ID} is already in the data base, with artibautes: {DataSource.List.Find(i => i.ID == item.ID)}"));
            if (item is Child && DataSource.List.Find(i => i.ID == ((Child)item).MotherId) == new Mother())
                return new Result<Id>(item, new InvalidOperationException($"Mother ID: {item.ID} can't be real"));
            DataSource.List.Add(item);
            return new Result<Id>(item);
        }

        public Result<Id> Edit(Id item)
        {
            Result<Id> deleteResult = Remove(item);
            if (deleteResult.IsErrorOccurred)
            {
                return deleteResult;
            }
            Result<Id> addResult = Add(item);
            if (addResult.IsErrorOccurred)
            {
                return Add(deleteResult.Value);
            }
            return addResult;
        }

        public Result<List<Id>> GetList(Type type) => new Result<List<Id>>((from item in DataSource.List where type == item.GetType select item).ToList());

        public Result<IEnumerable<IGrouping<Func<object, object>, Id>>> Group(string keyField, Type type)
        {
            Func<object, object> codeGetter = type.CreateGetter(keyField);// using the 1st element as an example
            return new Result<IEnumerable<IGrouping<Func<object, object>, Id>>>(from child in GetList(type).Value orderby codeGetter group child by codeGetter);
        }

        public Result<Id> Remove(Id item)
        {
            Id removedChiled = DataSource.List.Find(@Id => @Id == item);
            if (!DataSource.List.Remove(item))
                return new Result<Id>(removedChiled, new InvalidOperationException($"Child: {item} didn't match any Child ID in the data base."));
            return new Result<Id>(removedChiled);
        }

        #endregion
    }
}
