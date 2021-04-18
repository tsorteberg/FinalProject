/***************************************************************
* Name        : IRepository.cs
* Author      : Tom Sorteberg
* Created     : 04/18/2021
* Course      : CIS 174
* Version     : 1.0
* OS          : Windows 10 Pro, Visual Studio Community 2019
* Copyright   : This is my own original work based on
*               specifications issued by our instructor
* Description : Final Project.
* I have not used unauthorized source code, either modified or 
* unmodified. I have not given other fellow student(s) access 
* to my program.         
***************************************************************/
using System.Collections.Generic;

namespace FinalProject.Models
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> List(QueryOptions<T> options);

        int Count { get; }

        // overloaded Get() method
        T Get(QueryOptions<T> options);
        T Get(int id);
        T Get(string id);

        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);

        void Save();
    }
}
